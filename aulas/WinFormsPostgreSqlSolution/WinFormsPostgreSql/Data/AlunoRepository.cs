using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Configuration;
using WinFormsPostgreSql.Models;

namespace WinFormsPostgreSql.Data
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly string _cs;

        public AlunoRepository()
        {
            if (string.IsNullOrWhiteSpace(_cs))
            {
                _cs = ConfigurationManager.ConnectionStrings["Postgres"]?.ConnectionString;
            }
            if (string.IsNullOrWhiteSpace(_cs))
                throw new InvalidOperationException("Connection string não encontrada. Defina SQL_CONNECTION_STRING ou configure em App.config.");
        }

        public IEnumerable<Aluno> Listar
            ()
        {
            var lista = new List<Aluno>();
            const string sql = "select id, nome, email from public.alunos order by id desc";
            using var con = new NpgsqlConnection(_cs);
            using var cmd = new NpgsqlCommand(sql, con);
            con.Open();
            using var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                lista.Add(new Aluno
                {
                    Id = rd.GetInt64(0),
                    Nome = rd.GetString(1),
                    Email = rd.GetString(2)
                });
            }
            return lista;
        }

        public Int64 Inserir(Aluno a)
        {
            const string sql = @"insert into public.alunos (nome, email) values (@nome, @email) returning id;";
            using var con = new NpgsqlConnection(_cs);
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.Add("nome", NpgsqlTypes.NpgsqlDbType.Varchar, 120).Value = a.Nome;
            cmd.Parameters.Add("email", NpgsqlTypes.NpgsqlDbType.Varchar, 200).Value = a.Email;
            con.Open();
            var id = (long)cmd.ExecuteScalar();
            return id;
        }

        public void Atualizar(Aluno a)
        {
            const string sql = "update public.alunos set nome=@nome, email=@email where id=@id";
            using var con = new NpgsqlConnection(_cs);
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.Add("nome", NpgsqlDbType.Varchar, 120).Value = a.Nome;
            cmd.Parameters.Add("email", NpgsqlDbType.Varchar, 200).Value = a.Email;
            cmd.Parameters.Add("id", NpgsqlDbType.Bigint).Value = a.Id;
            con.Open();
            cmd.ExecuteNonQuery();
        }

        public void Remover(Int64 id)
        {
            const string sql = "delete from public.alunos where id=@id";
            using var con = new NpgsqlConnection(_cs);
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.Add("id", NpgsqlDbType.Bigint).Value = id;
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
}