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
using ProjetoPDVServico;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using System.IO;


namespace ProjetoPDVUI
{
    public partial class frmEmitir : Form
    {
        string retTransmitir;
        //string caminhoXML;

        XmlDocument xmlNFe = new XmlDocument();
        XmlDocument xmlNFe_Assinado = new XmlDocument();

        List<Pedido> lstPedido = new List<Pedido>();

        GerarXML gerarXml = new GerarXML();
        AssinarXML assinarXml = new AssinarXML();
        ValidarXML validarXml = new ValidarXML();
        TransmitirXML transmitirXml = new TransmitirXML();
        Email email = new Email();

        frmMenuPrincipal instancia_MDI;

        public frmEmitir(frmMenuPrincipal menu)
        {
            InitializeComponent();
            instancia_MDI = menu;
        }

        private void frmEmitir_Load(object sender, EventArgs e)
        {
            TxtResultado.Text = string.Empty;
            txtDadosXML.Text = string.Empty;

            /*
            Emitente em = (new EmitenteDao()).getEmitente();
            lblRazaoSocial.Text = em.nome;
            lblCNPJ.Text = em.cnpj;
            lblInscEst.Text = em.inscest;
            */
            txtCertificado.Text = Certificado.getInstance.sSubject + "        ( VALIDO ATÉ " + Certificado.getInstance.dValidadeFinal + " )";
        }


        private void Lista_Pedidos()
        {
            try
            {
                if (GerarXML.str_Ambiente == "1")
                {
                    lstPedido = (new PedidoDao()).getlstPedidos();
                }
                else
                {
                    //lstPedido = (new PedidoDao()).getlstTESTE();
                }


                ProdutoDao pd = new ProdutoDao();
                foreach (Pedido p in lstPedido)
                {
                    p.cliente = (new ClienteDao()).getClientePedido(p.numdoc);
                    p.cliente.end = (new EnderecoDao()).getEnderecoCliente(p.numdoc);

                    //p.emitente = (new EmitenteDao()).getEmitente();
                    //p.emitente.endereco = (new EnderecoDao()).getEnderecoEmitente();

                    p.operacao = (new OperacaoDao()).getOperacaoPedido(p.numdoc);
                    p.lstPedidoItem = (new PedidoItemDao()).getlst_Itens(p.numdoc);
                    p.xml = new XML();

                    foreach (PedidoItem pedidoitem in p.lstPedidoItem)
                    {
                        pedidoitem.produto = pd.getProduto(pedidoitem.codpro);
                    }

                    ListViewItem ls = new ListViewItem(p.numdoc.ToString());
                    ls.SubItems.Add(p.nfiscal);
                    ls.SubItems.Add(p.datadigitacao.ToString());
                    ls.SubItems.Add(p.operacao.nome);
                    ls.SubItems.Add(p.valdoc.ToString("0.00"));
                    ls.SubItems.Add("Em digitação");
                    ls.SubItems.Add(p.cliente.codcli.ToString());
                    ls.SubItems.Add(p.cliente.firma.ToUpper());
                    ls.SubItems.Add(p.cliente.end.municipio.ToUpper());
                    ls.SubItems.Add(p.cliente.end.bairro.ToUpper());
                    ls.SubItems.Add(p.cliente.end.uf);
                    ls.SubItems.Add("STATUS");

                    lstMovdia.Items.Add(ls);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }


        private void cmdViewNotas_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                lstMovdia.Items.Clear();


                Lista_Pedidos();


                if (lstPedido.Count == 0)
                {
                    MessageBox.Show("Não há notas a serem emitidas.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                lblSelecionados.Text = "(Selecionados: " + lstMovdia.Items.Count + ")";
                InicializaButtons();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Log_Exception.Monta_ArquivoLog(ex);

                MessageBox.Show("Erro ao buscar pedidos: " + ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Cursor = Cursors.Default;
                return;
            }
        }


        private void InicializaButtons()
        {
            progressBar1.Value = 0;
            TxtResultado.Text = string.Empty;

            if (lstMovdia.Items.Count != 0)
            {
                cmdTransmitirXML.Enabled = true;
            }
        }

        private void cmdTransmitirXML_Click(object sender, EventArgs e)
        {

            if (lstMovdia.CheckedItems.Count == 0)
            {
                MessageBox.Show("Selecione uma Nota Fiscal na lista abaixo.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                lstMovdia.Focus();
                return;
            }


            PedidoDao pdDao = new PedidoDao();
            XMLDao xmlDao = new XMLDao();
            //StreamWriter Grava;

            string retValidar;
            string strProc;
            string strXmlProcNfe;
            int nPosI;
            int nPosF;

            string cStatus_LoteProcessado;
            string cStatus_Autorizado;

            TxtResultado.Text = string.Empty;
            txtDadosXML.Text = string.Empty;

            progressBar1.Value = 0;
            progressBar1.Maximum = lstMovdia.Items.Count;

            try
            {
                for (int i = 0; i <= lstMovdia.Items.Count - 1; i++)
                {

                    progressBar1.Value = progressBar1.Value + 1;

                    if (lstMovdia.Items[i].Checked == true)
                    {

                        retTransmitir = string.Empty;
                        retValidar = string.Empty;

                        cStatus_LoteProcessado = string.Empty;
                        cStatus_Autorizado = string.Empty;

                        try
                        {
                            // Gerando o XML
                            xmlNFe = (gerarXml.NFe(lstPedido[i]));

                            // Assinando o XML
                            xmlNFe_Assinado = assinarXml.AssinaXML(xmlNFe.InnerXml, "infNFe", Certificado.getInstance.oCertificado);
                        }
                        catch (Exception ex)
                        {
                            Log_Exception.Monta_ArquivoLog(ex);

                            lst_Color(i, "Erro ao Assinar", Color.Red);
                            lista_Erros(i, "Erro: " + ex.Message);
                            continue;
                        }


                        try
                        {
                            // Validando o XML
                            retValidar = validarXml.Valida(xmlNFe_Assinado, "NFe");
                        }
                        catch (Exception ex)
                        {
                            Log_Exception.Monta_ArquivoLog(ex);

                            lst_Color(i, "Erro inesperado ao Validar", Color.Red);
                            lista_Erros(i, "Erro ao Validar: " + ex.Message);
                            continue;
                        }


                        if (lstPedido[i].modelo == "65")
                        {
                            //Inserindo a URL QRCode no xml já assinado
                            xmlNFe_Assinado.LoadXml(xmlNFe_Assinado.InnerXml.Replace("</infNFe>", "</infNFe><infNFeSupl><qrCode><![CDATA[" +
                            gerarXml.Gera_Url_QRCode(xmlNFe_Assinado, lstPedido[i]) + "]]></qrCode></infNFeSupl>"));
                        }

                        /*
                        // Gravando o arquivo xml na pasta de Saidas
                        Grava = File.CreateText(@"C:\Documents and Settings\Renan\Desktop\xxxx1.XML");
                        Grava.Write(xmlNFe_Assinado.InnerXml);
                        Grava.Close();
                        */

                        
                        if (retValidar == string.Empty)
                        {
                            try
                            {
                                // Recebendo o XML de retorno da transmissão
                                //retTransmitir = lstPedido[i].modelo == "65" ? transmitirXml.XML_NFCe(xmlNFe_Assinado, lstPedido[i].nfiscal, Certificado.getInstance.oCertificado) : transmitirXml.XML_NFe(xmlNFe_Assinado, lstPedido[i].nfiscal, Certificado.getInstance.oCertificado);

                                if (retTransmitir.Substring(0, 4) != "Erro")
                                {
                                    XmlDocument xmlRetorno = new XmlDocument();
                                    xmlRetorno.LoadXml(retTransmitir);

                                    // Lote processado
                                    if (xmlRetorno.GetElementsByTagName("cStat")[0].InnerText == "104")
                                    {
                                        // Autorizado
                                        if (xmlRetorno.GetElementsByTagName("cStat")[1].InnerText == "100")
                                        {

                                            try
                                            {

                                                lstPedido[i].chave = xmlRetorno.GetElementsByTagName("chNFe")[0].InnerText;
                                                lstPedido[i].protocolo = xmlRetorno.GetElementsByTagName("nProt")[0].InnerText;

                                                // Separar somente o conteúdo a partir da tag <protNFe> até </protNFe>
                                                nPosI = retTransmitir.IndexOf("<protNFe");
                                                nPosF = retTransmitir.Length - (nPosI + 13);
                                                strProc = retTransmitir.Substring(nPosI, nPosF);

                                                // XML pronto para salvar
                                                strXmlProcNfe = @"<?xml version=""1.0"" encoding=""utf-8"" ?><nfeProc xmlns=""http://www.portalfiscal.inf.br/nfe"" versao=""3.10"">" + xmlNFe_Assinado.InnerXml + strProc + "</nfeProc>";

                                                lstPedido[i].xml.numdoc = lstPedido[i].numdoc;
                                                lstPedido[i].xml.arquivoXML = strXmlProcNfe;
                                                lstPedido[i].xml.data = DateTime.Now;
                                                lstPedido[i].xml.Modelo = lstPedido[i].modelo;


                                                if (GerarXML.str_Ambiente == "1")
                                                {
                                                    SalvarArquivoXML_Pasta(i);
                                                }



                                                // Salvando o xml no Banco de Dados
                                                if (xmlDao.Grava_XML(lstPedido[i].xml))
                                                {
                                                    // Atualizando o pedido com Chave e Protocolo
                                                    if (pdDao.Update_ChaveProtocolo(lstPedido[i].numdoc,lstPedido[i].chave,lstPedido[i].protocolo))
                                                    {
                                                        lst_Color(i, xmlRetorno.GetElementsByTagName("xMotivo")[1].InnerText, Color.Green);

                                                        txtDadosXML.AppendText("Nota Fiscal: " + lstPedido[i].nfiscal + " emitida com sucesso..." + Environment.NewLine);
                                                        txtDadosXML.AppendText("Arquivo XML salvo com sucesso..." + Environment.NewLine);
                                                        txtDadosXML.AppendText("Chave: " + lstPedido[i].chave + Environment.NewLine);
                                                        txtDadosXML.AppendText("Protocolo: " + lstPedido[i].protocolo + Environment.NewLine + Environment.NewLine);
                                                        /*
                                                        if (Usuario.getInstance.empresa == 1)
                                                        {
                                                            if (GerarXML.str_Ambiente == "1")
                                                            {
                                                                // Enviando email para o cliente
                                                                if (email.EnviaEmailNFe(lstPedido[i])) { pdDao.Atualiza_Pedido_Email(lstPedido[i].numdoc); }
                                                            }
                                                        }*/
                                                    }
                                                    else
                                                    {
                                                        lst_Color(i, xmlRetorno.GetElementsByTagName("xMotivo")[1].InnerText, Color.Green);
                                                        lista_Erros(i, "NOTA EMITIDA, mas houve um erro ao atualizar o pedido com a CHAVE e o PROTOCOLO, informe imediatamente ao administrador do sistema!");
                                                    }
                                                }
                                                else
                                                {
                                                    lst_Color(i, xmlRetorno.GetElementsByTagName("xMotivo")[1].InnerText, Color.Green);
                                                    lista_Erros(i, "NOTA EMITIDA, mas houve um erro ao salvar o arquivo de XML no banco de dados, informe imediatamente ao administrador do sistema!");
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                Log_Exception.Monta_ArquivoLog(ex);

                                                lista_Erros(i, ex.Message);
                                                lst_Color(i, "NOTA EMITIDA, mas houve um erro inesperado (005)", Color.Red);
                                                continue;
                                            }

                                        }
                                        else
                                        {
                                            lista_Erros(i, xmlRetorno.GetElementsByTagName("xMotivo")[1].InnerText);
                                            lst_Color(i, "Erro ao Transmitir (004)", Color.Red);
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        lista_Erros(i, xmlRetorno.GetElementsByTagName("xMotivo")[0].InnerText);
                                        lst_Color(i, "Erro ao Transmitir (003)", Color.Red);
                                        continue;
                                    }
                                }
                                else
                                {
                                    lista_Erros(i, retTransmitir);
                                    lst_Color(i, "Erro ao Transmitir (002)", Color.Red);
                                    continue;
                                }
                            }
                            catch (Exception ex)
                            {
                                Log_Exception.Monta_ArquivoLog(ex);

                                lista_Erros(i, ex.Message);
                                lst_Color(i, "Erro ao Transmitir (001)", Color.Red);
                                continue;
                            }
                        }
                        else
                        {
                            lst_Color(i, "Erro no XML", Color.Red);
                            lista_Erros(i, "XML Shema: " + retValidar);
                            continue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log_Exception.Monta_ArquivoLog(ex);

                TxtResultado.AppendText("Ocorreu um erro inesperado, informe ao administrador do sistema!" + Environment.NewLine + ex.Message + Environment.NewLine);
                return;
            }
        }


        private void SalvarArquivoXML_Pasta(int item)
        {
            //StreamWriter Grava;

            try
            {
                /*
                // Gravando o arquivo xml na pasta de Saidas
                switch (Usuario.getInstance.empresa)
                {
                    case 1:
                        caminhoXML = @"Impetus\Comercial\Retorno\Saidas\" + DateTime.Now.Year + @"\" + DateTime.Now.Month + @"\" + lstPedido[item].chave + "-procNfe.xml";
                        break;
                    case 2:
                        caminhoXML = @"Concursar\Retorno\Gravados\" + DateTime.Now.Year + @"\" + DateTime.Now.Month + @"\" + lstPedido[item].chave + "-procNfe.xml";
                        break;
                    case 3:
                        caminhoXML = @"Nahgash\Retorno\Gravados\" + lstPedido[item].chave + "-procNfe.xml";
                        break;
                }

                // Gravando o arquivo xml na pasta de Saidas
                Grava = File.CreateText(@"\\Server\Dados\ZNFe\" + caminhoXML);
                Grava.Write(lstPedido[item].xml.arquivoXML);
                Grava.Close();
                */
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void lista_Erros(int item, string mensagem)
        {
            TxtResultado.AppendText("** NOTA FISCAL: " + lstPedido[item].nfiscal + " **" + Environment.NewLine);
            TxtResultado.AppendText(mensagem + Environment.NewLine + Environment.NewLine);
            TxtResultado.ForeColor = Color.Maroon;
        }

        private void lst_Color(int item, string mensagem, Color cor)
        {
            lstMovdia.Items[item].SubItems[5].Text = mensagem;
            lstMovdia.Items[item].ForeColor = cor;
            lstMovdia.Items[item].Checked = false;
        }

        private void ckTodos_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i <= lstMovdia.Items.Count - 1; i++)
            {
                lstMovdia.Items[i].Checked = ckTodos.Checked;
            }
        }

        private void cmdFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void frmEmitir_FormClosed(object sender, FormClosedEventArgs e)
        {
            instancia_MDI.pictureBox1.Visible = true;
        }

        private void GroupBox3_Enter(object sender, EventArgs e)
        {

        }

    }
}
