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
using System.IO;

namespace ProjetoPDVUI
{
    public partial class frmGravaXml : Form
    {

        Pedido p = new Pedido();
        PedidoDao pdao = new PedidoDao();

        public frmGravaXml()
        {
            InitializeComponent();
        }

        private void frmGravaXml_Load(object sender, EventArgs e)
        {
            cboProcurar.SelectedIndex = 0;
        }

        private void cmdLocalizar_Click(object sender, EventArgs e)
        {
            if (txtNumDoc.Text == string.Empty)
            {
                MessageBox.Show("Digite o (" + cboProcurar.Text + ") por favor.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (cboProcurar.SelectedIndex == 0)
                {
                    p = pdao.getPedido(txtNumDoc.Text.Trim());
                }
                else
                {
                    p = pdao.getPedido(Convert.ToInt32(txtNumDoc.Text.Trim()));
                }


                if (p == null)
                {
                    MessageBox.Show("Pedido não encontrado, verifique por favor.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Cursor = Cursors.Default;
                    return;
                }


                p.operacao = (new OperacaoDao()).getOperacaoPedido(p.numdoc);
                p.cliente = (new ClienteDao()).getClientePedido(p.numdoc);
                p.operacao = (new OperacaoDao()).getOperacaoPedido(p.numdoc);
                p.cliente.end = (new EnderecoDao()).getEnderecoCliente(p.numdoc);
                p.xml = new XML();

                lblNFiscal.Text = p.nfiscal.ToString();
                lblDatDoc.Text = p.datadigitacao.ToString().Substring(0, 10);
                lblCodNat.Text = p.operacao.codoperacao.ToString();
                lblValDoc.Text = p.valdoc.ToString("######0.00");
                lblRazaoSocial.Text = p.cliente.firma;
                lblEndereco.Text = p.cliente.end.logradouro + " " + p.cliente.end.complemento;
                lblCidade.Text = p.cliente.end.municipio;
                lblUF.Text = p.cliente.end.uf;

                TxtChaveNFe.Text = p.chave;
                TxtNumProtocolo.Text = p.protocolo;


                Inicializa_Buttons();

                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                Log_Exception.Monta_ArquivoLog(ex);

                MessageBox.Show("Ocorreu um erro inesperado. Informe imediatamente o administrador do sistema!" + Environment.NewLine + ex.Message, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Cursor = Cursors.Default;
                return;
            }

        }

        private void cmdAbrir_Click(object sender, EventArgs e)
        {
            OpenFileDialog AbrirComo = new OpenFileDialog();
            DialogResult caminho;

            AbrirComo.InitialDirectory = @"C:\";
            AbrirComo.Title = "Abrir como";
            AbrirComo.FileName = "*proc*";
            AbrirComo.Filter = "Arquivos Textos (*.xml)|*.xml";

            caminho = AbrirComo.ShowDialog();
            lblArquivo.Text = AbrirComo.FileName;
            txtArquivoName.Text = AbrirComo.SafeFileName;



            if (caminho == DialogResult.Cancel)
            {
                lblArquivo.Text = "";
            }

            Inicializa_Buttons();
        }


        private void Inicializa_Buttons()
        {
            if (p != null)
                cmdAbrir.Enabled = true;

            if (lblArquivo.Text.Trim() != "")
                cmdAplicar.Enabled = true;
        }

        private void cmdAplicar_Click(object sender, EventArgs e)
        {
            if (lblArquivo.Text == "")
            {
                MessageBox.Show("Arquivo de XML não encontrado.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {

                XMLDao xmldao = new XMLDao();

                StreamReader fluxoTexto;
                string linhaTexto = string.Empty;

                if (File.Exists(lblArquivo.Text))
                {
                    fluxoTexto = new StreamReader(lblArquivo.Text);
                    linhaTexto = fluxoTexto.ReadLine();
                    fluxoTexto.Close();
                }


                if (string.IsNullOrEmpty(p.chave) || string.IsNullOrEmpty(p.protocolo))
                {
                    p.chave = txtArquivoName.Text.Substring(0, 44);
                    p.protocolo = linhaTexto.Substring(linhaTexto.IndexOf("<nProt>") + 7, 15);

                    pdao.Update_ChaveProtocolo(p.numdoc,p.chave,p.protocolo);
                }


                p.xml.numdoc = p.numdoc;
                p.xml.arquivoXML = linhaTexto;
                p.xml.data = DateTime.Now;
                p.xml.Modelo = p.modelo;

                if (xmldao.Grava_XML(p.xml))
                {
                    MessageBox.Show("XML vinculado ao Pedido: " + p.numdoc + " com sucesso!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar XML: " + Environment.NewLine + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void cmdSair_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void cboProcurar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboProcurar.SelectedIndex == 1)
            {
                rdNFe.Enabled = false;
                rdNFCe.Enabled = false;
            }
            else
            {
                rdNFe.Enabled = true;
                rdNFCe.Enabled = true;
            }
        }

    }
}
