using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjetoPDVDao;
using ProjetoPDVModelos;
using System.Threading;

namespace ProjetoPDVUI
{
    public partial class frmLogin : Form
    {
        public bool LogonSuccessful = false;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtLogin.Select();
            cboLoja.SelectedIndex = 1;
        }


        private void txtLogin_Click(object sender, EventArgs e)
        {
            txtLogin.Text = "";
        }

        private void txtSenha_Click(object sender, EventArgs e)
        {
            txtSenha.UseSystemPasswordChar = true;
            txtSenha.Text = "";
        }


        private void Valida_UsuarioeSenha() 
        {

            if (txtLogin.Text.Trim().Length == 0)
            {
                MessageBox.Show("Entre com o nome do usuário.", "Erro - Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtSenha.Text.Trim().Length == 0)
            {
                MessageBox.Show("Entre com a senha do usuário.", "Erro - Senha", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                if (!(new UsuarioDao()).SelecionaUsuario(txtLogin.Text, txtSenha.Text, cboLoja.Text))
                {
                    MessageBox.Show("Nome de usuário ou senha incorretos.", "Erro Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro inesperado ao se comunicar com o banco de dados." + Environment.NewLine + "Erro: " + ex.Message, "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Selecionando o certificado
                Certificado.getInstance.Seleciona_Certificado();

                // Iniciando o certificado
                if (Certificado.getInstance.oCertificado == null)
                {
                    MessageBox.Show("Certificado não encontrado." + Environment.NewLine + "Tente novamente...", "Certificado - Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if ((Certificado.getInstance.dValidadeFinal - DateTime.Now).Days <= 7)
                {
                    if ((Certificado.getInstance.dValidadeFinal - DateTime.Now).Days <= 0)
                    {
                        MessageBox.Show("CERTIFICADO EXPIRADO!" + Environment.NewLine + "Informe imediatamente ao gerente do Setor.", "Certificado - Validade", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //return;
                    }

                    //MessageBox.Show("Atenção, faltam " + (Certificado.getInstance.dValidadeFinal - DateTime.Now).Days + " dias para o certificado expirar!", "Atenção - Validade", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                LogonSuccessful = true;
                this.Close();
            }
            catch (Exception ex)
            {
                Log_Exception.Monta_ArquivoLog(ex);

                MessageBox.Show("Erro ao selecionar CERTIFICADO DIGITAL no computador, verifique por favor!" + Environment.NewLine + "Retorno do erro: " + ex.Message, "Login - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogonSuccessful = false;
                return;
            }
        
        
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Valida_UsuarioeSenha();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            Valida_UsuarioeSenha();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            LogonSuccessful = false;
            this.Close();
        }

        private void txtSenha_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Enter)
            {
                Valida_UsuarioeSenha();
            }
        }

        private void rectangleShape4_Click(object sender, EventArgs e)
        {

        }

        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Enter)
            {
                Valida_UsuarioeSenha();
            }
        }
    }
}