using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ControleChamados
{
    public partial class frmIncluirChamado : Form
    {
        public frmIncluirChamado()
        {
            InitializeComponent();
        }

        private void btnCadastrarChamado_Click(object sender, EventArgs e)
        {
            //1 - Receber as informações da tela
            string descChamado = txtDescricaoChamado.Text;
            //2 - Tratar e realizar persistência no BD
            //Inserir no arquivo app.config na chave PESSOA sua connectionString
            string conexao = ConfigurationManager.ConnectionStrings["PESSOA"].ToString();
            string query = "INSERT INTO [dbo].[TB_Chamado]([desc_chamado],[status_chamado],[dt_criacao_chamado]) VALUES (@desc_chamado, @status_chamado, @dt_criacao_chamado)";
            SqlConnection conn = new SqlConnection(conexao);
            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("@desc_chamado", descChamado));
            cmd.Parameters.Add(new SqlParameter("@status_chamado", true));
            cmd.Parameters.Add(new SqlParameter("@dt_criacao_chamado", DateTime.Now));

            conn.Open();

            //3 - Devolver a PESSOA o resultado da persistência realizada no BD

            if (cmd.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("sucesso");
                conn.Close();
                txtDescricaoChamado.Clear();
                txtDescricaoChamado.Focus();
            }
        }
    }
}
