using System;
using System.Windows.Forms;
using ProjetoPDVDao;
using ProjetoPDVModelos;
using ProjetoPDVServico;
using System.Drawing;

namespace ProjetoPDVUI
{
    public partial class frmMenuPrincipal : Form
    {
        public frmMenuPrincipal()
        {
            InitializeComponent();
            
            //pictureBox1.ImageLocation = @"E:\Documents and Settings\Renan\Meus documentos\Visual Studio 2010\Projects\ProjetoPDVUI\Imagens\Azul_1920x1200.jpg";
            
            if(Usuario.getInstance.loja.Equals(0))
                pictureBox1.ImageLocation = Application.StartupPath + @"\Imagens\orange-gradient2560x1600.png";
            else
                pictureBox1.ImageLocation = Application.StartupPath + @"\Imagens\fffff.jpg";


            //ToolStrip imagens
            toolStripButton2.Image = Bitmap.FromFile(Application.StartupPath + @"\Imagens\settings.png");
            toolStripButton3.Image = Bitmap.FromFile(Application.StartupPath + @"\Imagens\cashier.png");
            toolStripButton1.Image = Bitmap.FromFile(Application.StartupPath + @"\Imagens\financeiro.png");
            toolStripButton5.Image = Bitmap.FromFile(Application.StartupPath + @"\Imagens\register.png");
            toolStripButton6.Image = Bitmap.FromFile(Application.StartupPath + @"\Imagens\user.png");


            //pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void Iniciando_Configuracao()
        {
            try
             {
                Controle control = (new ControleNFiscalDao()).getControle();

                if (control != null)
                {
                    Controle.getInstance.ultima_NFCe = control.ultima_NFCe;
                    Controle.getInstance.csc_Homologacao = control.csc_Homologacao;
                    Controle.getInstance.csc_Producao = control.csc_Producao;
                    Controle.getInstance.caminho_XMLAutorizado = control.caminho_XMLAutorizado;
                    Controle.getInstance.caminho_XMLCancelado = control.caminho_XMLCancelado;
                    Controle.getInstance.caminho_XMLInutilizado = control.caminho_XMLInutilizado;
                    
                    control = null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            //===========================================================================
            //===========================================================================
            (new EmitenteDao()).SelecionaEmitente();

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
                    return;
                }

                MessageBox.Show("Atenção, faltam " + (Certificado.getInstance.dValidadeFinal - DateTime.Now).Days + " dias para o certificado expirar!", "Atenção - Validade", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            //===========================================================================
            //===========================================================================
            

            this.Text = "Menu Principal (NFCe 3.10)";

            try
            {
                Iniciando_Configuracao();


                if (!(new EmitenteDao()).SelecionaEmitente())
                {
                    MessageBox.Show("Dados do Emitente não encontrado, tente novamente!", "Erro Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                stbPrincipal.Items[0].Text = Emitente.getInstance.nome;
                stbPrincipal.Items[1].Text = "CNPJ: " + String.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToInt64(Emitente.getInstance.cnpj));
                stbPrincipal.Items[2].Text = "IE: " + Emitente.getInstance.inscest;
                stbPrincipal.Items[3].Text = DateTime.Now.ToString("dd/MM/yyyy");
                stbPrincipal.Items[5].Text = "Usuário: " + Usuario.getInstance.nomeUser;
                stbPrincipal.Items[6].Text = "Loja: " + (Usuario.getInstance.loja.Equals(0)?"Livraria":"Cafeteria");
            }
            catch (Exception ex)
            {
                Log_Exception.Monta_ArquivoLog(ex);

                MessageBox.Show("Ocorreu um erro ao buscar os dados do Emitente no banco de dados, verfique por favor ou contate o administrador do sistema! " + Environment.NewLine + "Retorno do erro: " + ex.Message, "Erro inesperado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            //===========================================
            // Definindo o ambiente
            GerarXML.str_Ambiente = "1";
            //===========================================


            if (GerarXML.str_Ambiente == "1")
            {
                stbPrincipal.Items[4].Text = "Ambiente PRODUÇÃO";
            }
            else
            {
                stbPrincipal.Items[4].Text = "Ambiente HOMOLOGAÇÃO";
            }

        }

        private void cancelarNFeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCancela frm = new frmCancela();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void inutilizaçãoDeFaixaNFeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInutilizar frmIn = new frmInutilizar();
            frmIn.ShowDialog();
            frmIn.Dispose();
        }

        private void movimentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmMenuPrincipal.ActiveForm.ActiveMdiChild != null) frmMenuPrincipal.ActiveForm.ActiveMdiChild.Close();

            pictureBox1.Visible = false;

            frmMovimento frmMov = new frmMovimento();
            frmMov.MdiParent = this;
            frmMov.Show();
        }

        private void GravaXMLtoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmGravaXml frmxml = new frmGravaXml();
            frmxml.ShowDialog();
            frmxml.Dispose();
        }
        private void SINTEGRAtoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.sintegra.gov.br/");
        }

        private void PortaltoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.nfe.fazenda.gov.br/portal/principal.aspx");
        }

        private void DANFEtoolStripMenuItem2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.danfeonline.com.br/");
        }

        private void FechartoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void cmdFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void MDI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Confirma encerrar o programa ?", "Menu Principal",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                // Cancel the Closing event from closing the form.
                e.Cancel = true;
                // Call method to save file...
            }
        }

        private void frmMenuPrincipal_KeyDown(object sender, KeyEventArgs e) 
        {
            if (e.KeyData == Keys.F5) 
            {
                caixaToolStripMenuItem_Click(sender, e);
            }
            else if (e.KeyData == Keys.F9)
            {
                toolStripMenuItem1_Click(sender, e);
            }
            else if (e.KeyData == Keys.F10)
            {
                toolStripButton1_Click(sender, e);
            }
            else if(e.KeyData == Keys.F11)
            {
                toolStripButton7_Click(sender, e);
            }
        }

        private void caixaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCaixa frm = new frmCaixa();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmParametros frm = new frmParametros();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmMovimento frmMov = new frmMovimento();
            frmMov.ShowDialog();
            frmMov.Dispose();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            frmCaixa frm = new frmCaixa();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            frmParametros frm = new frmParametros();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmMovimento frm = new frmMovimento();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            frmFechaCaixa frm = new frmFechaCaixa();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void toolStripButton5_Click_1(object sender, EventArgs e)
        {
            frmListaProduto frm = new frmListaProduto();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {

        }
    }
}
