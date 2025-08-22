using System.Collections.Generic;
using WinFormsAzureSql.Models;

namespace WinFormsAzureSql.Data
{
    public interface IAlunoRepository
    {
        IEnumerable<Aluno> Listar();
        int Inserir(Aluno a);
        void Atualizar(Aluno a);
        void Remover(int id);
    }
}
