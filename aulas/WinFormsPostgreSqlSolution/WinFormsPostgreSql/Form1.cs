using System;
using System.Linq;
using System.Windows.Forms;
using WinFormsPostgreSql.Data;
using WinFormsPostgreSql.Models;
using WinFormsPostgreSql.Services;

namespace WinFormsPostgreSql
{
    public partial class Form1 : Form
    {
        private readonly AlunoService _service;

        public Form1()
        {
            InitializeComponent();
            var repo = new AlunoRepository();
            _service = new AlunoService(repo);
            Shown += (_, __) => CarregarGrid();
        }

        private void CarregarGrid()
        {
            var data = _service.Listar().ToList();
            dgvAlunos.DataSource = data;
            dgvAlunos.ClearSelection();
            LimparFormulario();
        }

        private void LimparFormulario()
        {
            txtNome.Clear();
            txtEmail.Clear();
            tagId.Text = ""; // rótulo oculto para armazenar o Id atual
            txtNome.Focus();
        }

        private void btnNovo_Click(object sender, EventArgs e) => LimparFormulario();

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                var id = 0;
                if (int.TryParse(tagId.Text, out var parsed)) id = parsed;
                var aluno = new Aluno { Id = id, Nome = txtNome.Text.Trim(), Email = txtEmail.Text.Trim() };
                var savedId = _service.Upsert(aluno);
                MessageBox.Show(id == 0 ? $"Inserido! Id: {savedId}" : "Atualizado com sucesso!");
                btnSalvar.Text = "Salvar";
                CarregarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                _service.Delete(aluno.Id);
                CarregarGrid();
            }
        }

        private void btnRecarregar_Click(object sender, EventArgs e) => CarregarGrid();

        private void dgvAlunos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var aluno = (Aluno)dgvAlunos.Rows[e.RowIndex].DataBoundItem;
            tagId.Text = aluno.Id.ToString();
            txtNome.Text = aluno.Nome;
            txtEmail.Text = aluno.Email;
            txtNome.Focus();
            btnSalvar.Text = "Atualizar";
        }
    }
}
