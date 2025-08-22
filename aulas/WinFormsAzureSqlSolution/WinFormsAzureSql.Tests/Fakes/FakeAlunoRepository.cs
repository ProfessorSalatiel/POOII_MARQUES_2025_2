using System.Collections.Generic;
using System.Linq;
using WinFormsAzureSql.Data;
using WinFormsAzureSql.Models;

namespace WinFormsAzureSql.Tests.Fakes
{
    public class FakeAlunoRepository : IAlunoRepository
    {
        private readonly List<Aluno> _storage = new();
        private int _nextId = 1;

        public IEnumerable<Aluno> Listar() => _storage.OrderByDescending(x => x.Id);

        public int Inserir(Aluno a)
        {
            a.Id = _nextId++;
            _storage.Add(a);
            return a.Id;
        }

        public void Atualizar(Aluno a)
        {
            var idx = _storage.FindIndex(x => x.Id == a.Id);
            if (idx >= 0) _storage[idx] = a;
        }

        public void Remover(int id)
        {
            _storage.RemoveAll(x => x.Id == id);
        }
    }
}
