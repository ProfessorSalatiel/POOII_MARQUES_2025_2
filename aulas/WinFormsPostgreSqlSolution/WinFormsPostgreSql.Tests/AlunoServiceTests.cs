using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using WinFormsPostgreSql.Models;
using WinFormsPostgreSql.Services;
using WinFormsPostgreSql.Tests.Fakes;

namespace WinFormsPostgreSql.Tests
{
    [TestClass]
    public class AlunoServiceTests
    {
        [TestMethod]
        public void Deve_Inserir_Aluno_Valido()
        {
            var repo = new FakeAlunoRepository();
            var svc = new AlunoService(repo);

            var id = svc.Upsert(new Aluno { Nome = "Ana", Email = "ana@exemplo.com" });
            Assert.IsTrue(id > 0);
            Assert.AreEqual(1, svc.Listar().Count());
        }

        [TestMethod]
        public void Deve_Atualizar_Aluno_Existente()
        {
            var repo = new FakeAlunoRepository();
            var svc = new AlunoService(repo);

            var id = svc.Upsert(new Aluno { Nome = "Ana", Email = "ana@exemplo.com" });
            id = svc.Upsert(new Aluno { Id = id, Nome = "Ana Maria", Email = "ana.maria@exemplo.com" });

            var atual = svc.Listar().First();
            Assert.AreEqual("Ana Maria", atual.Nome);
            Assert.AreEqual("ana.maria@exemplo.com", atual.Email);
        }

        [TestMethod]
        public void Deve_Excluir_Aluno()
        {
            var repo = new FakeAlunoRepository();
            var svc = new AlunoService(repo);

            var id = svc.Upsert(new Aluno { Nome = "Ana", Email = "ana@exemplo.com" });
            svc.Delete(id);

            Assert.AreEqual(0, svc.Listar().Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Nao_Deve_Aceitar_Email_Invalido()
        {
            var repo = new FakeAlunoRepository();
            var svc = new AlunoService(repo);
            svc.Upsert(new Aluno { Nome = "Fulano", Email = "invalido" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Nao_Deve_Aceitar_Nome_Vazio()
        {
            var repo = new FakeAlunoRepository();
            var svc = new AlunoService(repo);
            svc.Upsert(new Aluno { Nome = "", Email = "teste@exemplo.com" });
        }
    }
}
