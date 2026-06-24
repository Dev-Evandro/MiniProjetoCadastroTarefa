using Core.Models;
using Dapper;
using Infrastruture.Database;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruture.DAL
{
    public class UsuarioDAL : BaseDAL
    {
        

        public async Task InserirAsync(USUARIO usuario, string permissoesJson)
        {
            using var connection = new SqlConnection();

            await connection.OpenAsync();

            using var transaction = connection.BeginTransaction();

            try
            {
                const string sqlUsuario = @"
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
                    )";

                await connection.ExecuteAsync(
                    sqlUsuario,
                    usuario,
                    transaction);

                const string sqlAcesso = @"
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
                    )";

                await connection.ExecuteAsync(
                    sqlAcesso,
                    new
                    {
                        UsuarioAcessoId = Guid.NewGuid(),
                        UsuarioId = usuario.UsuarioId,
                        PermissoesJson = permissoesJson
                    },
                    transaction);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
