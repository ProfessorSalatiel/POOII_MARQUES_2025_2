using System;
using System.Collections.Generic;
using WinFormsPostgreSql.Models;

namespace WinFormsPostgreSql.Data
{
    public interface IAlunoRepository
    {
        IEnumerable<Aluno> Listar();
        Int64 Inserir(Aluno a);
        void Atualizar(Aluno a);
        void Remover(Int64 id);
    }
}
