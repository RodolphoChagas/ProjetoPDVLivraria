using System;
using System.Drawing;
using System.Windows.Forms;
using ProjetoPDVServico;
using ProjetoPDVDao;
using ProjetoPDVModelos;
using System.Xml;
using System.IO;
using PetaPoco;

namespace ProjetoPDVUI
{
    public partial class frmInutilizar : Form
    {

        XmlDocument xmlInut = new XmlDocument();
        XmlDocument xmlInut_Assinado = new XmlDocument();

        Pedido p = new Pedido();


        public frmInutilizar()
        {
            InitializeComponent();
        }

        private void frmInutilizar_Load(object sender, EventArgs e)
        {
            cboProcurar.SelectedIndex = 0;

            pictureBox1.Image = System.Drawing.Bitmap.FromFile(@"Imagens\10885_128x128.png");
        }
                                                                         


        private void cmdInutilizar_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            string retValidar = string.Empty;
            string retTransmitir = string.Empty;
            txtResultado.Text = string.Empty;

            GerarXML geraxml = new GerarXML();
            TransmitirXML transmitir = new TransmitirXML();
            ValidarXML validarXml = new ValidarXML();
            AssinarXML assinar = new AssinarXML();
            StreamWriter Grava;

            try
            {
                //Gerando xml
                xmlInut = geraxml.InutilizacaoNFe(Convert.ToInt32(txtNFInicial.Text), Convert.ToInt32(txtNFInicial.Text), p.serienfiscal, "Erro interno do sistema", p.modelo);

                //Assinando xml
                xmlInut_Assinado = assinar.AssinaXML(xmlInut.InnerXml, "infInut", Certificado.getInstance.oCertificado);
            }
            catch (Exception ex)
            {
                Log_Exception.Monta_ArquivoLog(ex);

                txtResultado.AppendText("Erro ao assinar XML: " + ex.Message + Environment.NewLine);
                txtResultado.ForeColor = Color.Maroon;
                this.Cursor = Cursors.Default;
                return;
            }





            try
            {
                // Validando o XML
                retValidar = validarXml.Valida(xmlInut_Assinado, "Inut");
            }
            catch (Exception ex)
            {
                Log_Exception.Monta_ArquivoLog(ex);

                txtResultado.AppendText("Erro ao validar XML: " + ex.Message + Environment.NewLine);
                txtResultado.ForeColor = Color.Maroon;
                this.Cursor = Cursors.Default;
                return;
            }


            if (retValidar == string.Empty)
            {

                try
                {
                    //Recebendo xml de retorno da transmissão
                    retTransmitir = transmitir.XML_InutilizacaoNFCe4(xmlInut_Assinado, Certificado.getInstance.oCertificado);


                    if (retTransmitir.Substring(0, 4) != "Erro")
                    {
                        XmlDocument retxml = new XmlDocument();
                        retxml.LoadXml(retTransmitir);

                        //Lote Processado
                        if (retxml.GetElementsByTagName("cStat")[0].InnerText == "102")
                        {
                            try
                            {
                                XMLDao xmlDao = new XMLDao();

                                p.xml.numdoc = p.numdoc;
                                p.xml.data = DateTime.Now;
                                p.xml.arquivoXML = retxml.InnerXml;
                                p.xml.Modelo = p.modelo;
                                p.xml.statNFCe = "102";

                                if (!string.IsNullOrEmpty(Controle.getInstance.caminho_XMLInutilizado))
                                {
                                    //Salvando o arquivo XML na pasta selecionada no FORM Parametros > aba Arquivos
                                    Grava = File.CreateText(Controle.getInstance.caminho_XMLInutilizado + @"\NOTA " + p.nfiscal + " - INUTILIZADA.xml");
                                    Grava.Write(p.xml.arquivoXML);
                                    Grava.Close();
                                }


                                //Atualizando o status do pedido para inutilizada
                                (new PedidoDao()).Update_StatNFCe_CondDoc(p.numdoc, "102");



                                using (var db = new Database("stringConexao"))
                                {
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
                                }


                                txtResultado.AppendText("Nota Fiscal inutilizada com sucesso!");
                                txtResultado.ForeColor = Color.Green;
                            }
                            catch (Exception ex)
                            {
                                Log_Exception.Monta_ArquivoLog(ex);

                                txtResultado.AppendText("Nota Fiscal inutilizada com sucesso, porém houve um erro inesperado, informe ao administrador do sistema!");
                                txtResultado.ForeColor = Color.Maroon;
                            }

                        }
                        else
                        {
                            txtResultado.AppendText("Erro ao transmitir XML (002): " + retxml.GetElementsByTagName("xMotivo")[0].InnerText + Environment.NewLine);
                            txtResultado.ForeColor = Color.Maroon;
                        }


                    }
                    else
                    {
                        txtResultado.AppendText("Erro ao transmitir XML (001): " + retTransmitir + Environment.NewLine);
                        txtResultado.ForeColor = Color.Maroon;
                    }


                }
                catch (Exception ex)
                {
                    Log_Exception.Monta_ArquivoLog(ex);

                    txtResultado.AppendText("Erro ao finalizar XML: " + ex.Message + Environment.NewLine);
                    txtResultado.ForeColor = Color.Maroon;
                }

            }

            this.Cursor = Cursors.Default;
        }

        private void cmdLocalizar_Click(object sender, EventArgs e)
        {

            try
            {
                txtResultado.Text = string.Empty;
                this.Cursor = Cursors.WaitCursor;

                p = (new PedidoDao()).getPedido(txtNFInicial.Text);

                if (p == null)
                {
                    MessageBox.Show("Esse pedido não foi encontrado dentro do sistema, verifique por favor ou informe ao administrador do sistema.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    LimpaCampos();
                    this.Cursor = Cursors.Default;
                    return;
                }

                if (!string.IsNullOrEmpty(p.chave) || !string.IsNullOrEmpty(p.protocolo))
                {
                    MessageBox.Show("Essa Nota Fiscal já foi emitida, verifique por favor.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpaCampos();
                    this.Cursor = Cursors.Default;
                    return;
                }

                p.lstPedidoItem = (new PedidoItemDao()).getlst_Itens(p.numdoc);
                p.operacao = (new OperacaoDao()).getOperacaoPedido(p.numdoc);
                p.cliente = (new ClienteDao()).getClientePedido(p.numdoc);
                p.cliente.end = (new EnderecoDao()).getEnderecoCliente(p.numdoc);
                p.xml = new XML();

                lblNFiscal.Text = p.nfiscal.ToString();
                lblDatDoc.Text = p.datadigitacao.ToString().Substring(0, 10);
                lblCodNat.Text = p.operacao.codoperacao.ToString();
                lblValDoc.Text = p.valdoc.ToString("######0.00");
                lblRazaoSocial.Text = string.IsNullOrEmpty(p.cliente.firma) ? "" : p.cliente.firma;
                lblEndereco.Text = string.IsNullOrEmpty(p.cliente.end.logradouro) ? "" : p.cliente.end.logradouro + " " + p.cliente.end.complemento;
                lblCidade.Text = string.IsNullOrEmpty(p.cliente.end.municipio) ? "" : p.cliente.end.municipio;
                lblUF.Text = string.IsNullOrEmpty(p.cliente.end.uf) ? "" : p.cliente.end.uf;

                
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Log_Exception.Monta_ArquivoLog(ex);

                txtResultado.AppendText("Ocorreu um erro inesperado. Informe ao administrador do sistema!" + Environment.NewLine + ex.Message);
                txtResultado.ForeColor = Color.Maroon;
                this.Cursor = Cursors.Default;
                return;
            }
        }

        private void LimpaCampos() 
        {
            lblNFiscal.Text = string.Empty;
            lblDatDoc.Text = string.Empty;
            lblCodNat.Text = string.Empty;
            lblValDoc.Text = string.Empty;
            lblRazaoSocial.Text = string.Empty;
            lblEndereco.Text = string.Empty;
            lblCidade.Text = string.Empty;
            lblUF.Text = string.Empty;

            txtResultado.Text = string.Empty;
        }



        private void CmdFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void rectangleShape2_Click(object sender, EventArgs e)
        {

        }

        private void frmInutilizar_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyData == Keys.Enter && txtNFInicial.Focused)
            {
                cmdLocalizar_Click(sender, e);
            }
            else if (e.KeyData == Keys.Enter && lblNFiscal.Text != "")
            {
                cmdInutilizar_Click(sender, e);                
            }
        }

    }
}
