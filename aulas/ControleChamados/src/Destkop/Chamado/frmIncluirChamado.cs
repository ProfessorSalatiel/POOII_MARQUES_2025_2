using Controller;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace Desktop.Chamado
{
    public partial class frmIncluirChamado : Form
    {
        public frmIncluirChamado()
        {
            InitializeComponent();
        }

        private async void btnCadastrarChamado_Click(object sender, EventArgs e)
        {
            try
            {
                var connStr = ConfigurationManager.ConnectionStrings["PROFSALATIEL"]?.ToString()
                              ?? throw new InvalidOperationException("ConnectionString 'PROFSALATIEL' não encontrada.");

                var controller = new ChamadoController(connStr);

                var sucesso = await controller.IncluirAsync(txtDescricao.Text?.Trim());

                MessageBox.Show(sucesso ? "Chamado incluído com sucesso!" : "Falha ao incluir chamado.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
