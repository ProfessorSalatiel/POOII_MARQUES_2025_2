using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using WinFormsAzureSql.Models;

namespace WinFormsAzureSql.Data
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly string _cs;

        public AlunoRepository()
        {
            _cs = Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING");
            if (string.IsNullOrWhiteSpace(_cs))
            {
                _cs = ConfigurationManager.ConnectionStrings["SqlLab"]?.ConnectionString;
            }
            if (string.IsNullOrWhiteSpace(_cs))
                throw new InvalidOperationException("Connection string não encontrada. Defina SQL_CONNECTION_STRING ou configure em App.config.");
        }

        public IEnumerable<Aluno> Listar()
        {
            var lista = new List<Aluno>();
            const string sql = "SELECT Id, Nome, Email FROM Alunos ORDER BY Id DESC";
            using var con = new SqlConnection(_cs);
            using var cmd = new SqlCommand(sql, con);
            con.Open();
            using var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                lista.Add(new Aluno
                {
                    Id = rd.GetInt32(0),
                    Nome = rd.GetString(1),
                    Email = rd.GetString(2)
                });
            }
            return lista;
        }

        public int Inserir(Aluno a)
        {
            const string sql = @"INSERT INTO Alunos (Nome, Email) VALUES (@nome, @email);
                                 SELECT CAST(SCOPE_IDENTITY() AS INT);";
            using var con = new SqlConnection(_cs);
            using var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@nome", a.Nome);
            cmd.Parameters.AddWithValue("@email", a.Email);
            con.Open();
            var id = (int)cmd.ExecuteScalar();
            return id;
        }

        public void Atualizar(Aluno a)
        {
            const string sql = "UPDATE Alunos SET Nome=@nome, Email=@email WHERE Id=@id";
            using var con = new SqlConnection(_cs);
            using var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@nome", a.Nome);
            cmd.Parameters.AddWithValue("@email", a.Email);
            cmd.Parameters.AddWithValue("@id", a.Id);
            con.Open();
            cmd.ExecuteNonQuery();
        }

        public void Remover(int id)
        {
            const string sql = "DELETE FROM Alunos WHERE Id=@id";
            using var con = new SqlConnection(_cs);
            using var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
