using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjetoPDVModelos;
using ProjetoPDVDao;
using ProjetoPDVServico;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using PetaPoco;


namespace ProjetoPDVUI
{
    public partial class frmCancela : Form
    {

        XmlDocument xmlCanc = new XmlDocument();
        XmlDocument xmlCanc_Assinado = new XmlDocument();

        Pedido p = new Pedido();
        PedidoDao pdao = new PedidoDao();

        //string caminhoXML;

        public frmCancela()
        {
            InitializeComponent();
        }

        private void frmCancela_Load(object sender, EventArgs e)
        {
            cboProcurar.SelectedIndex = 0;

            pictureBox1.Image = System.Drawing.Bitmap.FromFile(@"Imagens\lixeira.png");
        }

        private void cmdLocalizar_Click(object sender, EventArgs e)
        {
            txtResultado.Text = string.Empty;

            if (txtNumDoc.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Digite o (" + cboProcurar.Text + ") por favor.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            cboMotivo.Text = "PEDIDO DIGITADO INDEVIDAMENTE";
            cboMotivo.Focus();

            this.Cursor = Cursors.WaitCursor;

            try
            {

                if (cboProcurar.SelectedIndex == 0)
                {
                    //Por nota fiscal
                    p = pdao.getPedido(txtNumDoc.Text.Trim());
                }
                else
                {
                    //Por pedido
                    p = pdao.getPedido(Convert.ToInt32(txtNumDoc.Text.Trim()));
                }


                if (p == null)
                {
                    MessageBox.Show("Pedido não encontrado, verifique por favor.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Cursor = Cursors.Default;
                    return;
                }

                if (string.IsNullOrEmpty(p.chave) || string.IsNullOrEmpty(p.protocolo))
                {
                    MessageBox.Show("Não existe Chave e Protocolo nesse pedido, verifique por favor.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Cursor = Cursors.Default;
                    return;
                }

                p.operacao = (new OperacaoDao()).getOperacaoPedido(p.numdoc);
                p.cliente = (new ClienteDao()).getClientePedido(p.numdoc);
                p.operacao = (new OperacaoDao()).getOperacaoPedido(p.numdoc);
                p.lstPedidoItem = (new PedidoItemDao()).getlst_Itens(p.numdoc);
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

                CmdCancela.Enabled = true;

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

        private void CmdCancela_Click(object sender, EventArgs e)
        {

            if ((DateTime.Now - p.datadigitacao).TotalHours > 24)
            {
                MessageBox.Show("Prazo de Cancelamento Superior ao Previsto na Legislação.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (p.conddoc == "C")
            {
                MessageBox.Show("NF-e já cancelada.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cboMotivo.Text.Trim().Length < 15)
            {
                MessageBox.Show("Minimo de 15 caracteres!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            StreamWriter Grava;
            GerarXML geraxml = new GerarXML();
            AssinarXML assinar = new AssinarXML();
            ValidarXML validar = new ValidarXML();
            TransmitirXML transmitir = new TransmitirXML();
            XMLDao xmldao = new XMLDao();

            string retValidar = string.Empty;
            string xMotivo = cboMotivo.Text;
            string retTransmitir = string.Empty;
            txtResultado.Text = string.Empty;

            try
            {
                //Gerando xml
                xmlCanc = geraxml.CancelamentoNFe(p, xMotivo);

                //Assinando xml
                xmlCanc_Assinado = assinar.AssinaXML(xmlCanc.InnerXml, "infEvento", Certificado.getInstance.oCertificado);

            }
            catch (Exception ex)
            {
                Log_Exception.Monta_ArquivoLog(ex);

                MessageBox.Show("Erro ao assinar: " + ex.Message, "Mensagem", MessageBoxButtons.OK);
                this.Cursor = Cursors.Default;
                return;
            }

            try
            {

                //Validando o xml assinado
                retValidar = validar.Valida(xmlCanc_Assinado, "Canc");

            }
            catch (Exception ex)
            {
                Log_Exception.Monta_ArquivoLog(ex);

                MessageBox.Show("Erro ao Validar: " + ex.Message, "Mensagem", MessageBoxButtons.OK);
                this.Cursor = Cursors.Default;
                return;
            }


            if (retValidar == string.Empty)
            {
                try
                {
                    //Recebendo xml de retorno da transmissão
                    retTransmitir = transmitir.XML_CancelamentoNFCe4(xmlCanc_Assinado, p.nfiscal, Certificado.getInstance.oCertificado);

                    if (retTransmitir.Substring(0, 4) != "Erro")
                    {
                        XmlDocument xmlRetorno = new XmlDocument();
                        xmlRetorno.LoadXml(retTransmitir);

                        //Lote processado
                        if (xmlRetorno.GetElementsByTagName("cStat")[0].InnerText == "128")
                        {
                            //Evento registrado e vinculado a NFC-e
                            if (xmlRetorno.GetElementsByTagName("cStat")[1].InnerText == "135")
                            {
                                try
                                {
                                    p.xml.numdoc = p.numdoc;
                                    p.xml.data = DateTime.Now;
                                    p.xml.arquivoXML = xmlRetorno.InnerXml;
                                    p.xml.Modelo = p.modelo;
                                    p.xml.statNFCe = "135";


                                    if (!string.IsNullOrEmpty(Controle.getInstance.caminho_XMLCancelado))
                                    {
                                        //Salva o arquivo XML na pasta
                                        Grava = File.CreateText(Controle.getInstance.caminho_XMLCancelado + @"\110111-" + p.chave + "-1-procEventoNfe.xml");
                                        Grava.Write(p.xml.arquivoXML);
                                        Grava.Close();
                                    }

                                    //Salva arquivo XML no Banco SQL (NFe)
                                    if (xmldao.Grava_XML(p.xml))
                                    {
                                        //Atualizando o status do pedido para cancelada (135)
                                        (new PedidoDao()).Update_StatNFCe_CondDoc(p.numdoc, p.xml.statNFCe);


                                        var db = new Database("stringConexao");

                                        try
                                        {

                                            db.BeginTransaction();

                                            foreach (PedidoItem pItem in p.lstPedidoItem)
                                            {
                                                db.Update("UPDATE produto " +
                                                          "SET Estoque = Estoque + " + pItem.qtditens * 1 +
                                                          ",ValorVnd = ValorVnd + " + (pItem.valitens * -1).ToString().Replace(",", ".") +
                                                          " WHERE CodPro = " + pItem.codpro);
                                            }

                                            //Boleta cancelada
                                            db.Update("UPDATE Boleta set condicao = 2 where numdoc = " + p.numdoc);


                                            db.CompleteTransaction();
                                        }
                                        catch (Exception)
                                        {
                                            db.AbortTransaction();
                                            MessageBox.Show("Houve um erro inesperado ao atualizar o estoque e cancelar as boletas, informe imediatamente ao administrador do sistema!", "Mensagem de erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        }


                                        txtResultado.AppendText("Pedido cancelado com sucesso!");
                                        txtResultado.ForeColor = Color.Green;
                                    }
                                    else
                                    {
                                        txtResultado.AppendText("Pedido cancelado com sucesso, porém houve um erro ao salvar o arquivo XML no banco de dados, informe ao administrador do sistema!");
                                        txtResultado.ForeColor = Color.Green;
                                    }

                                }
                                catch (Exception ex)
                                {
                                    Log_Exception.Monta_ArquivoLog(ex);

                                    txtResultado.AppendText("Pedido cancelado com sucesso, porém houve um erro inesperado, informe ao administrador do sistema!");
                                    txtResultado.ForeColor = Color.Green;
                                }
                            }
                            else
                            {
                                txtResultado.AppendText("Erro ao cancelar (002): " + xmlRetorno.GetElementsByTagName("xMotivo")[1].InnerText + Environment.NewLine);
                                txtResultado.ForeColor = Color.Maroon;
                                this.Cursor = Cursors.Default;
                                return;
                            }
                        }
                        else
                        {
                            txtResultado.AppendText("Erro ao cancelar (001): " + xmlRetorno.GetElementsByTagName("xMotivo")[0].InnerText + Environment.NewLine);
                            txtResultado.ForeColor = Color.Maroon;
                            this.Cursor = Cursors.Default;
                            return;
                        }



                    }
                    else
                    {
                        txtResultado.AppendText("Erro ao transmitir (001): " + retTransmitir + Environment.NewLine);
                        txtResultado.ForeColor = Color.Maroon;
                        this.Cursor = Cursors.Default;
                        return;
                    }

                }
                catch (Exception ex)
                {
                    Log_Exception.Monta_ArquivoLog(ex);

                    MessageBox.Show("Erro ao Transmitir XML: " + ex.Message, "Mensagem", MessageBoxButtons.OK);
                    this.Cursor = Cursors.Default;
                    return;
                }
            }
            else
            {
                txtResultado.AppendText("Erro na validação do XML: " + retValidar + Environment.NewLine);
                txtResultado.ForeColor = Color.Maroon;
                this.Cursor = Cursors.Default;
                return;
            }

            this.Cursor = Cursors.Default;
        }

        private void CmdFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCancela_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyData == Keys.Enter && txtNumDoc.Focused)
            {
                cmdLocalizar_Click(sender, e);
            }
            else if (e.KeyData == Keys.Enter && cboMotivo.Text != "")
            {
                CmdCancela_Click(sender, e);                
            }
        }
     }
}
