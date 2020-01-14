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
using System.Xml;
using ProjetoPDVServico;

namespace ProjetoPDVUI
{
    public partial class frmFinalizaNFCe : Form
    {

        string retTransmitir;
        //string caminhoXML;
        
        XmlDocument xmlNFe = new XmlDocument();
        XmlDocument xmlNFe_Assinado = new XmlDocument();

        Pedido pedido;

        GerarXML gerarXml = new GerarXML();
        AssinarXML assinarXml = new AssinarXML();
        ValidarXML validarXml = new ValidarXML();
        TransmitirXML transmitirXml = new TransmitirXML();
        Email email = new Email();



        public frmFinalizaNFCe(Pedido p)
        {
            InitializeComponent();

            pedido = p;
        }
        /*
        private int Gera_NumeroNFiscal()
        {
            int nfiscal = 0;

            nfiscal = ((new ControleNFiscalDao()).getNumNFiscal()+1);
            
            if (!(new ControleNFiscalDao()).UpdateNFiscal(nfiscal))
                return 0;


            return nfiscal;
        }
        */
        private void frmFinalizaNFCe_Load(object sender, EventArgs e)
        {
            
            //Gerando o número de nota fiscal e atualizando a tabela Controle
            //com o novo número de nota fiscal
            //pedido.nfiscal = Gera_NumeroNFiscal().ToString();
            
            //pedido.nfiscal = "6 7 8 9 10 11";
            pedido.nfiscal = "11";

            lblUltNFCe.Text = (Convert.ToInt32(pedido.nfiscal) - 1).ToString("000000000");
            lblNFCe.Text = pedido.nfiscal;

            Gerando_NFCe();
        }

        private void Gerando_NFCe()
        {

            PedidoDao pdDao = new PedidoDao();
            XMLDao xmlDao = new XMLDao();
            StreamWriter Grava;

            string retValidar;
            string strProc;
            string strXmlProcNfe;
            int nPosI;
            int nPosF;

            string cStatus_LoteProcessado;
            string cStatus_Autorizado;

            //TxtResultado.Text = string.Empty;
            //txtDadosXML.Text = string.Empty;


            try
            {

                retTransmitir = string.Empty;
                retValidar = string.Empty;

                cStatus_LoteProcessado = string.Empty;
                cStatus_Autorizado = string.Empty;

                try
                {
                    // Gerando o XML
                    xmlNFe = (gerarXml.NFe(pedido));

                    MensagemSistema("Arquivo Gerado", Color.Green);

                   
                    txtChave.Text = gerarXml.strChave;

                    // Assinando o XML
                    xmlNFe_Assinado = assinarXml.AssinaXML(xmlNFe.InnerXml, "infNFe", Certificado.getInstance.oCertificado);

                }
                catch (Exception ex)
                {
                    Log_Exception.Monta_ArquivoLog(ex);
                    MensagemSistema("** Erro ao ASSINAR **" + Environment.NewLine + "Erro: " + ex.Message, Color.Maroon);
                    return;
                }
                
                MensagemSistema("Arquivo Assinado", Color.Green);
                                
                try
                {
                    // Validando o XML
                    retValidar = validarXml.Valida(xmlNFe_Assinado, "NFe");
                }
                catch (Exception ex)
                {
                    Log_Exception.Monta_ArquivoLog(ex);

                    MensagemSistema("** Erro ao VALIDAR **" + Environment.NewLine + "Erro: " + ex.Message, Color.Maroon);
                    return;
                }

                
                //Inserindo a URL QRCode no xml já assinado
                xmlNFe_Assinado.LoadXml(xmlNFe_Assinado.InnerXml.Replace("</infNFe>", "</infNFe><infNFeSupl><qrCode><![CDATA[" +
                gerarXml.Gera_Url_QRCode(xmlNFe_Assinado, pedido) + "]]></qrCode></infNFeSupl>"));
                


                //==========================================================================
                string caminho = @"C:\Documents and Settings\Renan\Desktop\XmlAssinado.xml";
                Grava = File.CreateText(caminho);
                Grava.Write(xmlNFe_Assinado.InnerXml);
                Grava.Close();
                //==========================================================================



                if (retValidar == string.Empty)
                {
                    try
                    {
                        MensagemSistema("Enviando a NFC-e", Color.Black);

                        // Recebendo o XML de retorno da transmissão
                        retTransmitir = transmitirXml.XML_NFCe(xmlNFe_Assinado, pedido.nfiscal, Certificado.getInstance.oCertificado);

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

                                        MensagemSistema("Autorizado o uso da NFC-e", Color.Green);

                                        pedido.chave = xmlRetorno.GetElementsByTagName("chNFe")[0].InnerText;
                                        pedido.protocolo = xmlRetorno.GetElementsByTagName("nProt")[0].InnerText;

                                        txtProtocolo.Text = pedido.protocolo;
                                        txtData.Text = DateTime.Now.ToString();

                                        // Separar somente o conteúdo a partir da tag <protNFe> até </protNFe>
                                        nPosI = retTransmitir.IndexOf("<protNFe");
                                        nPosF = retTransmitir.Length - (nPosI + 13);
                                        strProc = retTransmitir.Substring(nPosI, nPosF);

                                        // XML pronto para salvar
                                        strXmlProcNfe = @"<?xml version=""1.0"" encoding=""utf-8"" ?><nfeProc xmlns=""http://www.portalfiscal.inf.br/nfe"" versao=""3.10"">" + xmlNFe_Assinado.InnerXml + strProc + "</nfeProc>";

                                        pedido.xml = new XML();
                                        pedido.xml.numdoc = pedido.numdoc;
                                        pedido.xml.arquivoXML = strXmlProcNfe;
                                        pedido.xml.data = DateTime.Now;
                                        pedido.xml.Modelo = pedido.modelo;


                                        if (GerarXML.str_Ambiente == "1")
                                        {
                                            //SalvarArquivoXML_Pasta(i);
                                        }

                                        //SalvarArquivoXML_Pasta(@"C:\Documents and Settings\Renan\Desktop\NFCE EMITIDA.xml", pedido.xml.arquivoXML);



                                        //==========================================================================
                                        caminho = @"C:\Documents and Settings\Renan\Desktop\NFCE EMITIDA.xml";
                                        Grava = File.CreateText(caminho);
                                        Grava.Write(strXmlProcNfe);
                                        Grava.Close();
                                        //==========================================================================



                                        // Salvando o xml no Banco de Dados
                                        if (xmlDao.Grava_XML(pedido.xml))
                                        {
                                            // Atualizando o pedido com Chave e Protocolo
                                            if (pdDao.Update_ChaveProtocolo(pedido.numdoc, pedido.chave, pedido.protocolo))
                                            {
                                                //Atualizando a data do pedido
                                                pdDao.Update_DataNFiscal(pedido.numdoc, DateTime.Now);
                                            }
                                        }
                                        else
                                        {
                                            
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Log_Exception.Monta_ArquivoLog(ex);
                                        MensagemSistema("** NOTA EMITIDA **, mas houve um erro inesperado ao salvar (005)" + Environment.NewLine + "Erro: " + ex.Message, Color.Maroon);
                                        return;
                                    }
                                }
                                else
                                {
                                    MensagemSistema("Erro ao Transmitir (004)" + Environment.NewLine + "Erro: " + xmlRetorno.GetElementsByTagName("xMotivo")[1].InnerText, Color.Maroon);
                                    return;
                                }
                            }
                            else
                            {
                                MensagemSistema("Erro ao Transmitir (003)" + Environment.NewLine + "Erro: " + xmlRetorno.GetElementsByTagName("xMotivo")[0].InnerText, Color.Maroon);
                                return;
                            }
                        }
                        else
                        {
                            MensagemSistema("Erro ao Transmitir (002)" + Environment.NewLine + "Erro: " + retTransmitir, Color.Maroon);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        Log_Exception.Monta_ArquivoLog(ex);

                        MensagemSistema("Erro ao Transmitir (001)" + Environment.NewLine + "Erro: " + ex.Message, Color.Maroon);
                        return;
                    }
                }
                else
                {
                    MensagemSistema("Erro no XML" + Environment.NewLine + "XML Shema: " + retValidar, Color.Maroon);
                    return;
                }
            }
            catch (Exception ex)
            {
                Log_Exception.Monta_ArquivoLog(ex);
                MensagemSistema("Ocorreu um erro inesperado, informe ao administrador do sistema!" + Environment.NewLine + ex.Message, Color.Maroon);
                return;
            }
        }
    

        private void SalvarArquivoXML_Pasta(string caminho, string arquivoXML)
        {
            StreamWriter Grava;

            try
            {
                /*
                case 1:
                    caminhoXML = @"Impetus\Comercial\Retorno\Saidas\" + DateTime.Now.Year + @"\" + DateTime.Now.Month + @"\" + lstPedido[item].chave + "-procNfe.xml";
                    break;
                case 2:
                    caminhoXML = @"Concursar\Retorno\Gravados\" + DateTime.Now.Year + @"\" + DateTime.Now.Month + @"\" + lstPedido[item].chave + "-procNfe.xml";
                    break;
                case 3:
                    caminhoXML = @"Nahgash\Retorno\Gravados\" + lstPedido[item].chave + "-procNfe.xml";
                    break;
                */
 
                
                // Gravando o arquivo xml na pasta de Saidas
                Grava = File.CreateText(caminho);
                Grava.Write(arquivoXML);
                Grava.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }


        private void MensagemSistema(string mensagem, Color backgound)
        {
            lblRespostaServidor.ForeColor = backgound;
            lblRespostaServidor.Text = mensagem;
        }
    }
}
