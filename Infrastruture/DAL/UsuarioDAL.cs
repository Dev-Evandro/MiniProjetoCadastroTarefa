using Core.Interfaces;
using Core.Models;
using Dapper;
using System.Transactions;

namespace Infrastruture.DAL
{
    public class UsuarioDAL : BaseDAL, IUsuarioDAL
    {
        public async Task<bool> LoginExisteAsync(string login, Guid? ignorarUsuarioId = null)
        {
            using var connection = GetConnection();
            const string sql = """
                SELECT CASE WHEN EXISTS
                (
                    SELECT 1
                    FROM USUARIO
                    WHERE Login = @Login
                      AND (@IgnorarUsuarioId IS NULL OR UsuarioId <> @IgnorarUsuarioId)
                )
                THEN CAST(1 AS BIT)
                ELSE CAST(0 AS BIT)
                END;
                """;
            var count = await connection.ExecuteScalarAsync<int>(
                sql,
                new
                {
                    Login = login,
                    IgnorarUsuarioId = ignorarUsuarioId
                });
            return count > 0;
        }
        public async Task InserirAsync(
            USUARIO usuario,
            string permissoesJson)
        {
            using var connection = GetConnection();

            await connection.OpenAsync();

            using var transaction =
                await connection.BeginTransactionAsync();

            try
            {
                const string sqlUsuario = """
                INSERT INTO USUARIO
                (
                    UsuarioId,
                    Nome,
                    Login,
                    SenhaHash,
                    DataCadastro,
                    DataAtualizacao,
                    Inativo
                )
                VALUES
                (
                    @UsuarioId,
                    @Nome,
                    @Login,
                    @SenhaHash,
                    @DataCadastro,
                    @DataAtualizacao,
                    @Inativo
                );
                """;

                await connection.ExecuteAsync(
                    sqlUsuario,
                    usuario,
                    transaction);

                const string sqlAcesso = """
                INSERT INTO USUARIOACESSO
                (
                    UsuarioAcessoId,
                    UsuarioId,
                    PermissoesJson
                )
                VALUES
                (
                    @UsuarioAcessoId,
                    @UsuarioId,
                    @PermissoesJson
                );
                """;

                await connection.ExecuteAsync(
                    sqlAcesso,
                    new
                    {
                        UsuarioAcessoId = Guid.NewGuid(),
                        UsuarioId = usuario.UsuarioId,
                        PermissoesJson = permissoesJson
                    },
                    transaction);

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<USUARIO>> ListarAsync(string? filtro,bool inativo = false)
        {
            using var connection = GetConnection();

            const string sql = """
                SELECT
                    UsuarioId,
                    Nome,
                    Login,
                    DataCadastro,
                    DataAtualizacao,
                    Inativo
                FROM USUARIO
                WHERE (@Filtro IS NULL
                       OR Nome LIKE '%' + @Filtro + '%'
                       OR Login LIKE '%' + @Filtro + '%')
                  AND Inativo = @Inativo
                ORDER BY Nome;
                """;

            var usuarios = await connection.QueryAsync<USUARIO>(
                sql,
                new
                {
                    Filtro = string.IsNullOrWhiteSpace(filtro)
                        ? null
                        : filtro.Trim(),
                    Inativo = inativo
                });

            return usuarios.ToList();
        }

        public async Task<(USUARIO Usuario, string PermissoesJson)?> ConsultarPorIdAsync(Guid usuarioId,bool inativo = false)
        {
            using var connection = GetConnection();

            const string sql = """
                                SELECT
                                    U.UsuarioId,
                                    U.Nome,
                                    U.Login,
                                    U.SenhaHash,
                                    U.DataCadastro,
                                    U.DataAtualizacao,
                                    U.Inativo,
                                    A.PermissoesJson
                                FROM USUARIO U
                                LEFT JOIN USUARIOACESSO A
                                    ON A.UsuarioId = U.UsuarioId
                                WHERE U.UsuarioId = @UsuarioId
                                    AND U.Inativo = @Inativo;
                                """;

            var registro = (await connection.QueryAsync<USUARIO,string?,
                (USUARIO Usuario, string? PermissoesJson)>(
                sql,
                (usuario, permissoesJson) => (usuario, permissoesJson),
                new { 
                        UsuarioId = usuarioId, 
                        Inativo = inativo 
                    },splitOn: "PermissoesJson"))
                .FirstOrDefault();

            if (registro.Usuario is null)
                return null;

            return (registro.Usuario, registro.PermissoesJson ?? "{}");
        }

        public async Task AtualizarAsync(USUARIO usuario, string permissoesJson, bool atualizarSenha)
        {
            using var connection = GetConnection();
            await connection.OpenAsync();

            using var transaction = await connection.BeginTransactionAsync();

            try
            {
                const string sqlUsuario = """
                    UPDATE USUARIO
                    SET Nome = @Nome,
                        Login = @Login,
                        Inativo = @Inativo,
                        DataAtualizacao = @DataAtualizacao
                        /**senha**/
                    WHERE UsuarioId = @UsuarioId;
                    """;

                var sqlComSenha = atualizarSenha
                    ? sqlUsuario.Replace(
                        "/**senha**/",
                        ", SenhaHash = @SenhaHash")
                    : sqlUsuario.Replace("/**senha**/", "");

                var linhasAfetadas = await connection.ExecuteAsync(
                    sqlComSenha,
                    usuario,
                    transaction);

                if (linhasAfetadas == 0)
                    throw new InvalidOperationException(
                        "Usuário não encontrado.");

                const string sqlAcessoExiste = """
                    SELECT CASE WHEN EXISTS
                    (
                        SELECT 1
                        FROM USUARIOACESSO
                        WHERE UsuarioId = @UsuarioId
                    )
                    THEN CAST(1 AS BIT)
                    ELSE CAST(0 AS BIT)
                    END;
                    """;

                var acessoExiste = await connection.ExecuteScalarAsync<bool>(
                    sqlAcessoExiste,
                    new { usuario.UsuarioId },
                    transaction);

                if (acessoExiste)
                {
                    const string sqlAtualizarAcesso = """
                        UPDATE USUARIOACESSO
                        SET PermissoesJson = @PermissoesJson
                        WHERE UsuarioId = @UsuarioId;
                        """;

                    await connection.ExecuteAsync(
                        sqlAtualizarAcesso,
                        new
                        {
                            usuario.UsuarioId,
                            PermissoesJson = permissoesJson
                        },
                        transaction);
                }
                else
                {
                    const string sqlInserirAcesso = """
                        INSERT INTO USUARIOACESSO
                        (
                            UsuarioAcessoId,
                            UsuarioId,
                            PermissoesJson
                        )
                        VALUES
                        (
                            @UsuarioAcessoId,
                            @UsuarioId,
                            @PermissoesJson
                        );
                        """;

                    await connection.ExecuteAsync(sqlInserirAcesso,
                        new
                        {
                            UsuarioAcessoId = Guid.NewGuid(),
                            usuario.UsuarioId,
                            PermissoesJson = permissoesJson
                        }, transaction);
                }

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task AlterarStatusAsync(Guid usuarioId, bool inativo)
        {
            using var connection = GetConnection();

            const string sql = """
                UPDATE USUARIO
                SET Inativo = @Inativo,
                    DataAtualizacao = @DataAtualizacao
                WHERE UsuarioId = @UsuarioId;
                """;

            var linhasAfetadas = await connection.ExecuteAsync(
                sql,
                new
                {
                    UsuarioId = usuarioId,
                    Inativo = inativo,
                    DataAtualizacao = DateTime.Now
                });

            if (linhasAfetadas == 0)
                throw new InvalidOperationException(
                    "Usuário não encontrado.");
        }


    }
}
