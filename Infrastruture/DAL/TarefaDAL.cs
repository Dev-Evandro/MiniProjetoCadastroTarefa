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
    public class TarefaDAL : BaseDAL
    {

        private readonly string _connectionString;

        public TarefaDAL()
        {
            _connectionString = SQLServerClass.GetConnectionString();
        }

        public async Task<List<TAREFA>> ListarAsync(string? titulo, bool inativo = false)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @"SELECT 
                        Id,
                        Titulo,
                        Descricao,
                        DataInicio,
                        DataFimPrevisto,
                        DataFimReal,
                        StatusId,
                        DesenvolvedorId,
                        DataCriacao,
                        DataAtualizacao,
                        Inativo
                    FROM TAREFA
                    WHERE (@Titulo IS NULL OR Titulo LIKE '%' + @Titulo + '%')
                    AND Inativo = @Inativo
                    ORDER BY DataInicio DESC";

            var resultado = await connection.QueryAsync<TAREFA>(
                sql,
                new
                {
                    Inativo = inativo,
                    Titulo = string.IsNullOrWhiteSpace(titulo) ? null : titulo
                });

            return resultado.ToList();
        }
        public async Task InserirAsync(TAREFA tarefa)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @"
                        INSERT INTO TAREFA
                        (
                            Id,
                            Titulo,
                            Descricao,
                            DataInicio,
                            DataFimPrevisto,
                            DataFimReal,
                            StatusId,
                            DesenvolvedorId,
                            DataCriacao,
                            DataAtualizacao,
                            Inativo
                        )
                        VALUES
                        (
                            @Id,
                            @Titulo,
                            @Descricao,
                            @DataInicio,
                            @DataFimPrevisto,
                            @DataFimReal,
                            @StatusId,
                            @DesenvolvedorId,
                            @DataCriacao,
                            @DataAtualizacao,
                            @Inativo
                        )";

            await connection.ExecuteAsync(sql, tarefa);
        }
        public async Task<TAREFA?> ConsultarTarefaAsync(Guid id, bool inativo = false)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @"
                            SELECT 
                                Id,
                                Titulo,
                                Descricao,
                                DataInicio,
                                DataFimPrevisto,
                                DataFimReal,
                                StatusId,
                                DesenvolvedorId,
                                DataCriacao,
                                DataAtualizacao,
                                Inativo
                            FROM TAREFA
                            WHERE Id = @Id
                            AND Inativo = @Inativo";

            return await connection.QueryFirstOrDefaultAsync<TAREFA>(
                sql,
                new
                {
                    Id = id,
                    Inativo = inativo
                });
        }
        public async Task AlterarStatusAsync(Guid id, bool inativo)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @"
                            UPDATE TAREFA
                            SET Inativo = @Inativo,
                            DataAtualizacao = @DataAtualizacao
                            WHERE Id = @Id";

            await connection.ExecuteAsync(sql, new
            {
                Id = id,
                Inativo = inativo,
                DataAtualizacao = DateTime.Now
            });
        }
        public async Task AtualizarAsync(TAREFA tarefa)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @"
                            UPDATE TAREFA
                            SET
                                Titulo = @Titulo,
                                Descricao = @Descricao,
                                DataInicio = @DataInicio,
                                DataFimPrevisto = @DataFimPrevisto,
                                DataFimReal = @DataFimReal,
                                StatusId = @StatusId,
                                DesenvolvedorId = @DesenvolvedorId,
                                DataAtualizacao = @DataAtualizacao,
                                Inativo = @Inativo
                            WHERE Id = @Id";

            await connection.ExecuteAsync(sql, tarefa);
        }



    }
}
