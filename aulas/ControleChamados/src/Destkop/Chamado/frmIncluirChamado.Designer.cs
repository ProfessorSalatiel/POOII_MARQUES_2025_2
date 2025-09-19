namespace Desktop.Chamado
{
    partial class frmIncluirChamado
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblDescricaoChamado = new System.Windows.Forms.Label();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.btnCadastrarChamado = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblDescricaoChamado
            // 
            this.lblDescricaoChamado.AutoSize = true;
            this.lblDescricaoChamado.Location = new System.Drawing.Point(37, 34);
            this.lblDescricaoChamado.Name = "lblDescricaoChamado";
            this.lblDescricaoChamado.Size = new System.Drawing.Size(106, 13);
            this.lblDescricaoChamado.TabIndex = 0;
            this.lblDescricaoChamado.Text = "Descrição Chamado:";
            // 
            // txtDescricao
            // 
            this.txtDescricao.Location = new System.Drawing.Point(149, 31);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Size = new System.Drawing.Size(337, 20);
            this.txtDescricao.TabIndex = 1;
            // 
            // btnCadastrarChamado
            // 
            this.btnCadastrarChamado.Location = new System.Drawing.Point(40, 93);
            this.btnCadastrarChamado.Name = "btnCadastrarChamado";
            this.btnCadastrarChamado.Size = new System.Drawing.Size(120, 20);
            this.btnCadastrarChamado.TabIndex = 2;
            this.btnCadastrarChamado.Text = "Cadastrar Chamado";
            this.btnCadastrarChamado.UseVisualStyleBackColor = true;
            this.btnCadastrarChamado.Click += new System.EventHandler(this.btnCadastrarChamado_Click);
            // 
            // frmIncluirChamado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCadastrarChamado);
            this.Controls.Add(this.txtDescricao);
            this.Controls.Add(this.lblDescricaoChamado);
            this.Name = "frmIncluirChamado";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDescricaoChamado;
        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.Button btnCadastrarChamado;
    }
}

