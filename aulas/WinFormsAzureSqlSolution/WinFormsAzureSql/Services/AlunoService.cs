using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WinFormsAzureSql.Data;
using WinFormsAzureSql.Models;

namespace WinFormsAzureSql.Services
{
    public class AlunoService
    {
        private readonly IAlunoRepository _repo;
        private static readonly Regex EmailRegex =
            new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public AlunoService(IAlunoRepository repo) => _repo = repo;

        public IEnumerable<Aluno> Listar() => _repo.Listar();

        public int Upsert(Aluno a)
        {
            Validar(a);
            if (a.Id == 0) return _repo.Inserir(a);
            _repo.Atualizar(a);
            return a.Id;
        }

        public void Delete(int id)
        {
            if (id <= 0) throw new ArgumentException("Id inválido para exclusão.");
            _repo.Remover(id);
        }

        public void Validar(Aluno a)
        {
            if (a is null) throw new ArgumentNullException(nameof(a));
            if (string.IsNullOrWhiteSpace(a.Nome)) throw new ArgumentException("Nome é obrigatório.");
            if (string.IsNullOrWhiteSpace(a.Email)) throw new ArgumentException("Email é obrigatório.");
            if (!EmailRegex.IsMatch(a.Email)) throw new ArgumentException("Email inválido.");
        }
    }
}
