using Core.Interfaces;
using Core.Models;
using Dapper;

namespace Infrastruture.DAL
{
    public class UsuarioDAL : BaseDAL, IUsuarioDAL
    {
        public async Task<bool> LoginExisteAsync(string login)
        {
            using var connection = GetConnection();
            const string sql = """
                SELECT CASE WHEN EXISTS 
                ( SELECT 1 FROM USUARIO WHERE Login = @Login ) 
                THEN CAST(1 AS BIT) 
                ELSE CAST(0 AS BIT) 
                END
                """;
            var count = await connection.ExecuteScalarAsync<int>(
                sql,
                new { Login = login });
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
    }
}
