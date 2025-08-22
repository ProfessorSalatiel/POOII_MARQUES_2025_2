
# Guia Rápido: CRUD em Windows Forms (C#) com **Azure SQL Database**

Este guia ensina passo a passo como criar um **banco no Azure SQL Database** e conectar um aplicativo **Windows Forms em C#** para realizar operações CRUD (Create, Read, Update, Delete).  
Material pensado para **aulas, laboratórios e fins educacionais**.

---

## ✅ Pré-requisitos

- Conta na **Microsoft Azure** (pode ser gratuita, estudante ou trial).
- **Visual Studio** (Community é suficiente) com workload **.NET desktop development**.
- **SSMS** (SQL Server Management Studio) ou **Azure Data Studio** para executar scripts SQL.
- Conexão à internet.
- (Opcional) **Azure CLI** para automação.

---

## 1) Criar o Banco de Dados no Azure

1. Acesse o **Portal Azure** → clique em **Create a resource**.
2. Procure por **Azure SQL** → escolha **SQL Database**.
3. Em **Basics**:
   - **Subscription**: sua assinatura.
   - **Resource group**: crie um novo (ex.: `rg-educacao`) ou use um existente.
   - **Database name**: `db-educacao` (ou outro).
   - **Server**: clique em **Create new** e preencha:
     - **Server name**: `seu-servidor-sql` (único global).
     - **Location**: a mais próxima (ex.: *Brazil South*).
     - **Authentication method**: **Use SQL authentication**.
       - **Server admin login**: `sqladmin` (ou outro).
       - **Password**: defina uma senha forte.
   - **Compute + storage**: escolha um SKU econômico para laboratório (ex.: Basic/Serverless conforme disponibilidade).
4. **Review + Create** → **Create**.

> 💡 **Dica:** Para turmas, prefira um **Resource Group** exclusivo por aluno ou por turma para facilitar o _cleanup_.

---

## 2) Liberar o Acesso de Rede (Firewall)

1. No **SQL Server** criado (não o database, e sim o _server_ lógico), vá em **Networking**.
2. Em **Firewall rules**, clique **Add your client IPv4 address** (adiciona seu IP atual).
3. (Opcional) Ative **Allow Azure services and resources to access this server** = **On**.
4. Salve.

> ⚠️ Se mudar de rede (laboratório/casa), talvez precise atualizar o IP aqui.

---

## 3) Obter a Connection String (ADO.NET)

No recurso **SQL Database** → **Connection strings** → selecione **ADO.NET**.  
Copie algo como:

```
Server=tcp:<seu-servidor>.database.windows.net,1433;
Initial Catalog=<sua-base>;
Persist Security Info=False;
User ID=<admin-login>;
Password=<sua-senha>;
MultipleActiveResultSets=False;
Encrypt=True;
TrustServerCertificate=False;
Connection Timeout=30;
```

> 🔐 Em produção, use **Azure Key Vault** / **Managed Identity**. Para aulas, pode ficar no `App.config`.

---

## 4) Criar a Tabela de Exemplo

Conecte-se ao banco via SSMS/Azure Data Studio e rode:

```sql
CREATE TABLE Alunos (
  Id INT IDENTITY(1,1) PRIMARY KEY,
  Nome NVARCHAR(120) NOT NULL,
  Email NVARCHAR(200) NOT NULL,
  DataCadastro DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);
```

> ✅ Se quiser limpar depois: `DROP TABLE Alunos;`

---

## 5) Projeto Windows Forms (C#)

### 5.1) Criar o projeto
- No **Visual Studio** → **Create a new project** → **Windows Forms App (.NET Framework)** → .NET Framework 4.8 (ou similar).

### 5.2) Pacote NuGet
- **Microsoft.Data.SqlClient**

### 5.3) `App.config`

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <connectionStrings>
    <add name="AzureSql"
         connectionString="Server=tcp:SEU-SERVIDOR.database.windows.net,1433;Initial Catalog=SUA-BASE;Persist Security Info=False;User ID=SEU-LOGIN;Password=SUA-SENHA;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;" />
  </connectionStrings>
  <startup>
    <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.8"/>
  </startup>
</configuration>
```

> 💡 Alternativa: ler a string de `Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTION_STRING")` para evitar deixar senha no arquivo.

### 5.4) Modelo (POCO)

**Models/Aluno.cs**
```csharp
namespace WinFormsAzureSql.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}
```

### 5.5) Repositório (ADO.NET CRUD)

**Data/AlunoRepository.cs**
```csharp
using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Data.SqlClient;
using WinFormsAzureSql.Models;

namespace WinFormsAzureSql.Data
{
    public class AlunoRepository
    {
        private readonly string _cs = ConfigurationManager.ConnectionStrings["AzureSql"].ConnectionString;

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
                                 SELECT SCOPE_IDENTITY();";
            using var con = new SqlConnection(_cs);
            using var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@nome", a.Nome);
            cmd.Parameters.AddWithValue("@email", a.Email);
            con.Open();
            var id = Convert.ToInt32(cmd.ExecuteScalar());
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
```

### 5.6) UI WinForms

- Adicione ao **Form1**:
  - `DataGridView dgvAlunos` (ReadOnly = true, SelectionMode = FullRowSelect, AutoSizeColumnsMode = Fill)
  - `TextBox txtNome`, `TextBox txtEmail`
  - `Button btnNovo`, `btnSalvar`, `btnExcluir`, `btnRecarregar`

**Form1.cs (exemplo de code-behind)**
```csharp
using System;
using System.Linq;
using System.Windows.Forms;
using WinFormsAzureSql.Data;
using WinFormsAzureSql.Models;

namespace WinFormsAzureSql
{
    public partial class Form1 : Form
    {
        private readonly AlunoRepository _repo = new();
        private int? _idEditando = null;

        public Form1()
        {
            InitializeComponent();
            Shown += (_, __) => CarregarGrid();
        }

        private void CarregarGrid()
        {
            var data = _repo.Listar().ToList();
            dgvAlunos.DataSource = data;
            dgvAlunos.ClearSelection();
            LimparFormulario();
        }

        private void LimparFormulario()
        {
            _idEditando = null;
            txtNome.Clear();
            txtEmail.Clear();
            txtNome.Focus();
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Nome é obrigatório.");
                txtNome.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Email é obrigatório.");
                txtEmail.Focus();
                return false;
            }
            return true;
        }

        private void btnNovo_Click(object sender, EventArgs e) => LimparFormulario();

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            var aluno = new Aluno { Id = _idEditando ?? 0, Nome = txtNome.Text.Trim(), Email = txtEmail.Text.Trim() };

            if (_idEditando.HasValue)
            {
                _repo.Atualizar(aluno);
                MessageBox.Show("Registro atualizado com sucesso!");
            }
            else
            {
                var novoId = _repo.Inserir(aluno);
                MessageBox.Show($"Registro inserido! Id: {novoId}");
            }
            CarregarGrid();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dgvAlunos.CurrentRow == null)
            {
                MessageBox.Show("Selecione um registro.");
                return;
            }
            var aluno = (Aluno)dgvAlunos.CurrentRow.DataBoundItem;
            var ok = MessageBox.Show($"Excluir o aluno '{aluno.Nome}'?", "Confirmação", MessageBoxButtons.YesNo);
            if (ok == DialogResult.Yes)
            {
                _repo.Remover(aluno.Id);
                CarregarGrid();
            }
        }

        private void btnRecarregar_Click(object sender, EventArgs e) => CarregarGrid();

        private void dgvAlunos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var aluno = (Aluno)dgvAlunos.Rows[e.RowIndex].DataBoundItem;
            _idEditando = aluno.Id;
            txtNome.Text = aluno.Nome;
            txtEmail.Text = aluno.Email;
            txtNome.Focus();
        }
    }
}
```

**Form1.Designer.cs (eventos)**
```csharp
this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
this.btnRecarregar.Click += new System.EventHandler(this.btnRecarregar_Click);
this.dgvAlunos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAlunos_CellDoubleClick);
```

---

## 6) Testar Conexão

No **SSMS/Azure Data Studio**, faça um **SELECT** para verificar que a tabela existe:
```sql
SELECT TOP (10) * FROM Alunos ORDER BY Id DESC;
```
No app, rode e tente **inserir**, **editar**, **excluir** e **listar**.

---

## 7) Solução de Problemas (Troubleshooting)

- **Erro de firewall**: confirme o IP em **SQL server → Networking**.
- **Timeout**: verifique `Server=tcp:<servidor>.database.windows.net,1433;` e `Encrypt=True`.
- **“Login failed for user”**: confira usuário/senha ou reset a senha do **SQL server**.
- **Permissões**: use o admin do server para criar a tabela; depois considere um usuário com menos privilégios para a aplicação.
- **Conexão em laboratório**: prefira variável de ambiente `AZURE_SQL_CONNECTION_STRING` para não versionar senha.

---

## 8) Custos e Limpeza (Importante em Aulas)

- Monitore **Cost Management** e escolha *SKU* de baixo custo para o banco.
- Ao terminar:
  1. **Pare** recursos que suportem _pause_ (quando aplicável).
  2. **Delete** o **Resource Group** inteiro para apagar tudo de uma vez (server + database).

---

## 9) (Opcional) Azure CLI – Script Básico

> Execute no **Azure Cloud Shell** (bash) ou no seu terminal com Azure CLI instalada.

```bash
# Login
az login

# Variáveis
RG="rg-educacao"
LOC="brazilsouth"
SRV="seuservidorsql$RANDOM"
DB="db-educacao"

# Resource Group
az group create -n $RG -l $LOC

# Servidor SQL (autenticação SQL)
az sql server create -g $RG -l $LOC -n $SRV -u sqladmin -p "S3nhaF0rte!123"

# Regra de firewall para IP atual
MYIP=$(curl -s ifconfig.me)
az sql server firewall-rule create -g $RG -s $SRV -n allowMyIP --start-ip-address $MYIP --end-ip-address $MYIP

# Database (sku básico/exemplo)
az sql db create -g $RG -s $SRV -n $DB --service-objective Basic

echo "Connection string (ajuste conforme necessário):"
echo "Server=tcp:$SRV.database.windows.net,1433;Initial Catalog=$DB;Persist Security Info=False;User ID=sqladmin;Password=S3nhaF0rte!123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
```

> **Não use** senhas do exemplo em produção.

---

## 10) Próximos Passos

- Substituir ADO.NET por **Dapper** ou **Entity Framework** (Code First/Migrations).
- Tratar **validações** (regex de e-mail, `ErrorProvider` no WinForms, etc.).
- Adicionar **camada de serviços** e **tests** (ex.: MSTest/xUnit).

---

### Créditos & Licença
Este material é livre para uso educacional. Ajuste o nome do banco/tabelas conforme seu contexto de aula.

