using System;
using System.Windows.Forms;
using ProjetoPDVServico;
using ProjetoPDVModelos;
using ProjetoPDVDao;


namespace ProjetoPDVUI
{
    public partial class frmParametros : Form
    {
        public frmParametros()
        {
            InitializeComponent();
        }

        private void frmParametros_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = System.Drawing.Bitmap.FromFile(@"Imagens\logotipo-nfce.png");

            btnSalvar.Image = System.Drawing.Bitmap.FromFile(@"Imagens\disk.png");
            btnSair.Image = System.Drawing.Bitmap.FromFile(@"Imagens\cancel.png");

            Inicilializa_tbParametros();
            Inicializa_tbArquivos();

            
            //Apenas os usuarios do grupo ADMIN podem atualizar o formulário
            if(!Usuario.getInstance.grupo.Equals("ADMIN"))
            {
                foreach (Control control in this.tabControl1.Controls)
                {
                    control.Enabled = false;
                }
                
                btnSalvar.Enabled = false;
            }
        }

        private void Inicializa_tbArquivos() 
        {
            lblXMLDestinatario.Text = Controle.getInstance.caminho_XMLAutorizado != null ? Controle.getInstance.caminho_XMLAutorizado: string.Empty;
            lblXMLCancelado.Text = Controle.getInstance.caminho_XMLCancelado != null ? Controle.getInstance.caminho_XMLCancelado : string.Empty;
            lblXMLInutilizado.Text = Controle.getInstance.caminho_XMLInutilizado != null ? Controle.getInstance.caminho_XMLInutilizado : string.Empty;
        }

        private void Inicilializa_tbParametros()
        {
            if (GerarXML.str_Ambiente == "2")
            {
                cboAmbiente.Text = "Homologação";
                txtCSC.Text = Controle.getInstance.csc_Homologacao;
            }
            else
            {
                cboAmbiente.Text = "Produção";
                txtCSC.Text = Controle.getInstance.csc_Producao;
            }

            txtCNPJ.Text = string.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToInt64(Emitente.getInstance.cnpj));
            txtUltNFCe.Text = Controle.getInstance.ultima_NFCe.ToString("000000");
            txtCertificado.Text = Certificado.getInstance.sSubject;

            txtdtEmissao.Text = Certificado.getInstance.dValidadeInicial.ToString();
            txtdtVencimento.Text = Certificado.getInstance.dValidadeFinal.ToString();

            lblMensagem.Text = "Faltam " + (Certificado.getInstance.dValidadeFinal - DateTime.Now).Days + " dias para o vencimento do certificado!";
        }

        private void cboAmbiente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboAmbiente.Text == "Homologação")
            {
                GerarXML.str_Ambiente = "2";
                txtCSC.Text = Controle.getInstance.csc_Homologacao;
            }
            else
            {
                GerarXML.str_Ambiente = "1";
                txtCSC.Text = Controle.getInstance.csc_Producao;
            }
        }


        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void cmdAbrir_Click(object sender, EventArgs e)
        {
            Seleciona_Pasta(lblXMLDestinatario);
        }

        private void Seleciona_Pasta(Label label)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    label.Text = folderDialog.SelectedPath;
                }
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblXMLDestinatario.Text.Trim().Equals("") || lblXMLCancelado.Text.Trim().Equals("") || lblXMLInutilizado.Text.Trim().Equals(""))
                {
                    MessageBox.Show("Você precisa selecionar onde os arquivos serão salvos na aba [Arquivos] por favor.", "Configuração do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                
                string query = "Update Controle Set Caminho_XMLAutorizado = '" + lblXMLDestinatario.Text.Trim() + "', Caminho_XMLCancelado = '" + lblXMLCancelado.Text.Trim() + "', Caminho_XMLInutilizado = '" + lblXMLInutilizado.Text.Trim() + "'  Where ChvControle = 1";

                if ((new ControleNFiscalDao()).Update(query))
                {

                    //Atribuindo o novo caminho 
                    Controle.getInstance.caminho_XMLAutorizado = lblXMLDestinatario.Text.Trim();
                    Controle.getInstance.caminho_XMLCancelado = lblXMLCancelado.Text.Trim();
                    Controle.getInstance.caminho_XMLInutilizado = lblXMLInutilizado.Text.Trim();

                    MessageBox.Show("Configurações salvas com sucesso!", "Configuração do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
                MessageBox.Show("Não foi possível salvar as configurações, informe imediatamente ao administrador do sistema!", "Configuração do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Houve algum erro ao atualizar as informações, feche o formulário e tente novamente.", "Configuração do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //control = null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Seleciona_Pasta(lblXMLCancelado);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Seleciona_Pasta(lblXMLInutilizado);
        }

        private void bntTestarCon_Click(object sender, EventArgs e)
        {

            MP2032.FechaPorta();

            MP2032.ConfiguraModeloImpressora(7); //Bematech MP-4200 TH
            MP2032.IniciaPorta("USB");

            int iRetorno;


            iRetorno = MP2032.Le_Status();
            
            if (iRetorno == 0)
            {
                MessageBox.Show("Erro ao se comunicar com a Impressora Bematech MP-4200 TH, verifique por favor.", "** ATENÇÃO **", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (iRetorno == 5)
            {
                MessageBox.Show("Impressora com pouco papel, verifique por favor.", "** ATENÇÃO **", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (iRetorno == 9)
            {
                MessageBox.Show("Impressora com a tampa aberta, verifique por favor.", "** ATENÇÃO **", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (iRetorno == 32)
            {
                MessageBox.Show("Impressora sem papel, verifique por favor.", "** ATENÇÃO **", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else 
            {
                MessageBox.Show("Papel OK!" + Environment.NewLine + "Tampa fechada OK!"  + Environment.NewLine + "Comunicação OK!", "STATUS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            MP2032.FechaPorta();
        }

        private void cmdCorteParcial_Click(object sender, EventArgs e)
        {
            MP2032.AcionaGuilhotina(0);
        }

        private void btnImp_ImprimirTexto_Click(object sender, EventArgs e)
        {
            MP2032.ConfiguraModeloImpressora(7); //Bematech MP-4200 TH
            MP2032.IniciaPorta("USB");

            MP2032.BematechTX(txtImp_TextoLivre.Text + "\n");
        }

        private void btnImp_ImprimirCabecalhoNFCe_Click(object sender, EventArgs e)
        {
            MP2032.ConfiguraModeloImpressora(7); //Bematech MP-4200 TH
            MP2032.IniciaPorta("USB");

            //CADA LINHA DO CUPOM CONTEM 50 COLUNAS COM LETRA NORMAL
            MP2032.BematechTX("\x1B\x61\x1"); //Centraliza

            //Informações do Cabeçalho
            MP2032.FormataTX(Emitente.getInstance.nome + "\n", 2, 0, 0, 0, 1);
            MP2032.FormataTX("CNPJ " + String.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToInt64(Emitente.getInstance.cnpj)) + "\n", 2, 0, 0, 0, 0);
            MP2032.FormataTX("Av. Amaral Peixoto, 507 - LJ05, Centro, Niterói-RJ" + "\n", 2, 0, 0, 0, 0);
            MP2032.FormataTX("\n", 2, 0, 0, 0, 0);
            MP2032.FormataTX("Documento Auxiliar da Nota Fiscal de Consumidor Eletrônica" + "\n", 1, 0, 0, 0, 0);
            //MP2032.BematechTX("Documento Auxiliar da Nota Fiscal de Consumidor Eletrônica" + "\n");
            MP2032.FormataTX("\n", 2, 0, 0, 0, 0);

            //Informações de detalhes de produtos/serviços
            MP2032.FormataTX("Codigo  Descricao        Qtd Un   Vl.Unit    Total" + "\n", 2, 0, 0, 0, 0);
            MP2032.FormataTX("-------------------------------------------------------------------", 1, 0, 0, 0, 0);

            MP2032.AcionaGuilhotina(0);
            MP2032.FechaPorta();
        }
    }
}