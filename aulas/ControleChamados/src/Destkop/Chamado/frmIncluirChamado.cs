using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Desktop.Chamado
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
            try
            {
                //2 - Tratar e realizar persistência no BD
                //Inserir no arquivo app.config na chave PESSOA sua connectionString
                string conexao = ConfigurationManager.ConnectionStrings["PESSOA"].ToString();
                using (var conn = new SqlConnection(conexao))
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            string query = "INSERT INTO [dbo].[TB_Chamado]([desc_chamado],[status_chamado],[dt_criacao_chamado]) VALUES (@desc_chamado, @status_chamado, @dt_criacao_chamado)";
                            using (var cmd = new SqlCommand(query, conn, transaction))
                            {
                                cmd.CommandType = CommandType.Text;
                                cmd.Parameters.Add(new SqlParameter("@desc_chamado", descChamado));
                                cmd.Parameters.Add(new SqlParameter("@status_chamado", true));
                                cmd.Parameters.Add(new SqlParameter("@dt_criacao_chamado", DateTime.Now));

                                //3 - Devolver a PESSOA o resultado da persistência realizada no BD

                                if (cmd.ExecuteNonQuery() > 0)
                                {
                                    transaction.Commit();
                                    MessageBox.Show("sucesso");
                                    txtDescricaoChamado.Clear();
                                    txtDescricaoChamado.Focus();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Erro: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro geral: " + ex.Message);
            }
        }
    }
}