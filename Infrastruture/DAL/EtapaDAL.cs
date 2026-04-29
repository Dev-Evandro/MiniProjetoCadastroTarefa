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
    public class EtapaDAL : BaseDAL
    {
        private readonly string _connectionString;

        public EtapaDAL()
        {
            _connectionString = SQLServerClass.GetConnectionString();
        }

        public async Task<List<ETAPA>> ListarAsync(
            Guid tarefaId,
            string? titulo,
            bool inativo = false)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @"
            SELECT 
                Id,
                TarefaId,
                Titulo,
                Descricao,
                DataInicio,
                DataFimPrevisto,
                DataFimReal,
                Ordem,
                Inativo
            FROM ETAPA
            WHERE TarefaId = @TarefaId
            AND (@Titulo IS NULL OR Titulo LIKE '%' + @Titulo + '%')
            AND Inativo = @Inativo
            ORDER BY Ordem";

            var resultado = await connection.QueryAsync<ETAPA>(
                sql,
                new
                {
                    TarefaId = tarefaId,
                    Inativo = inativo,
                    Titulo = string.IsNullOrWhiteSpace(titulo) ? null : titulo
                });

            return resultado.ToList();
        }

        public async Task InserirAsync(ETAPA etapa)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @"
            INSERT INTO ETAPA
            (
                Id,
                TarefaId,
                Titulo,
                Descricao,
                DataInicio,
                DataFimPrevisto,
                DataFimReal,
                Ordem,
                Inativo
            )
            VALUES
            (
                @Id,
                @TarefaId,
                @Titulo,
                @Descricao,
                @DataInicio,
                @DataFimPrevisto,
                @DataFimReal,
                @Ordem,
                @Inativo
            )";

            await connection.ExecuteAsync(sql, etapa);
        }

        public async Task<ETAPA?> ConsultarAsync(Guid id, bool inativo = false)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @"
            SELECT 
                Id,
                TarefaId,
                Titulo,
                Descricao,
                DataInicio,
                DataFimPrevisto,
                DataFimReal,
                Ordem,
                Inativo
            FROM ETAPA
            WHERE Id = @Id
            AND Inativo = @Inativo";

            return await connection.QueryFirstOrDefaultAsync<ETAPA>(
                sql,
                new
                {
                    Id = id,
                    Inativo = inativo
                });
        }

        public async Task AtualizarAsync(ETAPA etapa)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @"
            UPDATE ETAPA
            SET
                Titulo = @Titulo,
                Descricao = @Descricao,
                DataInicio = @DataInicio,
                DataFimPrevisto = @DataFimPrevisto,
                DataFimReal = @DataFimReal,
                Ordem = @Ordem,
                Inativo = @Inativo
            WHERE Id = @Id";

            await connection.ExecuteAsync(sql, etapa);
        }

        public async Task AlterarStatusAsync(Guid id, bool inativo)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @"
            UPDATE ETAPA
            SET Inativo = @Inativo
            WHERE Id = @Id";

            await connection.ExecuteAsync(sql, new
            {
                Id = id,
                Inativo = inativo
            });
        }
    }
}
