using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Controller
{
    public class ChamadoController
    {
        private readonly string _connStr;

        public ChamadoController(string connectionString)
        {
            _connStr = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<ChamadoModel> BuscarPorIdAsync(long id)
        {
            using (var conn = new SqlConnection(_connStr))
            {
                await conn.OpenAsync();

                const string sql = @"
                    SELECT id_chamado, desc_chamado, status_chamado, dt_criacao_chamado
                    FROM [dbo].[TB_Chamado]
                    WHERE id_chamado = @id;";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = id;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new ChamadoModel
                            {
                                Id = reader.GetInt64(0),
                                Descricao = reader.GetString(1),
                                Status = reader.GetBoolean(2),
                                DataCriacao = reader.GetDateTime(3)
                            };
                        }
                    }
                }
            }

            return null;
        }
        public bool atualizar(ChamadoModel chamado)
        {
            string descricao = chamado.Descricao;
            return true;
        }
        public async Task<bool> IncluirAsync(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new ArgumentException("Descrição obrigatória");

            using (var conn = new SqlConnection(_connStr))
            {
                await conn.OpenAsync();

                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        const string sql = @"
                    INSERT INTO [dbo].[TB_Chamado]
                        ([desc_chamado],[status_chamado],[dt_criacao_chamado])
                    VALUES
                        (@desc_chamado, @status_chamado, @dt_criacao_chamado);";

                        using (var cmd = new SqlCommand(sql, conn, tx))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandTimeout = 30;

                            cmd.Parameters.Add("@desc_chamado", SqlDbType.VarChar, 50).Value = descricao.Trim();
                            cmd.Parameters.Add("@status_chamado", SqlDbType.Bit).Value = true;
                            cmd.Parameters.Add("@dt_criacao_chamado", SqlDbType.DateTime).Value = DateTime.UtcNow;

                            var linhas = await cmd.ExecuteNonQueryAsync();

                            if (linhas > 0)
                            {
                                tx.Commit();
                                return true;
                            }

                            tx.Rollback();
                            return false;
                        }
                    }
                    catch
                    {
                        try { tx.Rollback(); } catch { /* log opcional */ }
                        throw;
                    }
                }
            }
        }
        public async Task<List<ChamadoModel>> BuscarTodosAsync()
        {
            var lista = new List<ChamadoModel>();

            using (var conn = new SqlConnection(_connStr))
            {
                await conn.OpenAsync();

                const string sql = @"
                    SELECT id_chamado, desc_chamado, status_chamado, dt_criacao_chamado
                    FROM [dbo].[TB_Chamado];";

                using (var cmd = new SqlCommand(sql, conn))
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new ChamadoModel
                        {
                            Id = reader.GetInt64(0),
                            Descricao = reader.GetString(1),
                            Status = reader.GetBoolean(2),
                            DataCriacao = reader.GetDateTime(3)
                        });
                    }
                }
            }

            return lista;
        }

    }
}
