using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Linq;
using System.Text;
using System.Xml;
using ProjetoPDVModelos;
using System.Net;
using System.Net.Security;

namespace ProjetoPDVServico
{
    public class TransmitirXML
    {

        private XmlDocument xmlLote;
        private XmlNode xmlDados;
        private string TextoXML;


        public TransmitirXML()
        {
        }


        private void ServicePoint()
        {
            ServicePointManager.ServerCertificateValidationCallback
            += new RemoteCertificateValidationCallback(AllwaysGoodCertificate);
        }

        private static bool AllwaysGoodCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
        {
            return true;
        }


        public string XML_NFCe4(XmlDocument xmlAssinado, string nfiscal, X509Certificate2 _X509Cert)
        {
            try
            {
                GerarXML geraxml = new GerarXML();

                //Gerando o xml de Lote
                xmlLote = geraxml.Lote(xmlAssinado, nfiscal);

                xmlDados = xmlLote.DocumentElement;


                ServicePoint();


                if (GerarXML.str_Ambiente == "1")
                {
                    //NFCeRecepcaoP - PRODUCAO  /  NFCeRecepcaoH - HOMOLOGACAO
                    NFCeAutorizacao4P.NFeAutorizacao4 WsHRecepcao = new NFCeAutorizacao4P.NFeAutorizacao4();

                    WsHRecepcao.ClientCertificates.Add(_X509Cert);
                    WsHRecepcao.Timeout = Convert.ToInt32(240000);
                    WsHRecepcao.InitializeLifetimeService();

                    //NFCeAutorizacaoP.nfeCabecMsg cabec = new NFCeAutorizacaoP.nfeCabecMsg();
                    //cabec.cUF = "33";
                    //cabec.versaoDados = "3.10";
                    //WsHRecepcao.nfeCabecMsgValue = cabec;
                    TextoXML = WsHRecepcao.nfeAutorizacaoLote(xmlDados).OuterXml;
                    WsHRecepcao.Dispose();

                }
                else
                {
                    //NFeRecepcaoP - PRODUCAO  /  NFeRecepcaoH - HOMOLOGACAO
                    NFCeAutorizacao4H.NFeAutorizacao4 WsHRecepcao = new NFCeAutorizacao4H.NFeAutorizacao4();

                    WsHRecepcao.ClientCertificates.Add(_X509Cert);
                    WsHRecepcao.Timeout = Convert.ToInt32(240000);
                    WsHRecepcao.InitializeLifetimeService();

                    //NFCeAutorizacaoH.nfeCabecMsg cabec = new NFCeAutorizacaoH.nfeCabecMsg();
                    //cabec.cUF = "33";
                    //cabec.versaoDados = "3.10";
                    //WsHRecepcao.nfeCabecMsgValue = cabec;
                    TextoXML = WsHRecepcao.nfeAutorizacaoLote(xmlDados).OuterXml;
                    WsHRecepcao.Dispose();
                }


                return TextoXML;
            }
            catch (Exception ex)
            {
                Log_Exception.Monta_ArquivoLog(ex);

                return "Erro de conexão na recepção...! " + ex.Message;
            }

        }
        //------------------------------------------------------------------------------





        public string XML_NFCe(XmlDocument xmlAssinado, string nfiscal, X509Certificate2 _X509Cert)
        {
            try
            {
                GerarXML geraxml = new GerarXML();

                //Gerando o xml de Lote
                xmlLote = geraxml.Lote(xmlAssinado, nfiscal);

                xmlDados = xmlLote.DocumentElement;


                ServicePoint();


                if (GerarXML.str_Ambiente == "1")
                {
                    //NFCeRecepcaoP - PRODUCAO  /  NFCeRecepcaoH - HOMOLOGACAO
                    NFCeAutorizacaoP.NfeAutorizacao WsHRecepcao = new NFCeAutorizacaoP.NfeAutorizacao();

                    WsHRecepcao.ClientCertificates.Add(_X509Cert);
                    WsHRecepcao.Timeout = Convert.ToInt32(240000);
                    WsHRecepcao.InitializeLifetimeService();

                    NFCeAutorizacaoP.nfeCabecMsg cabec = new NFCeAutorizacaoP.nfeCabecMsg();
                    cabec.cUF = "33";
                    cabec.versaoDados = "3.10";
                    WsHRecepcao.nfeCabecMsgValue = cabec;
                    TextoXML = WsHRecepcao.nfeAutorizacaoLote(xmlDados).OuterXml;
                    WsHRecepcao.Dispose();

                }
                else
                {
                    //NFeRecepcaoP - PRODUCAO  /  NFeRecepcaoH - HOMOLOGACAO
                    NFCeAutorizacaoH.NfeAutorizacao WsHRecepcao = new NFCeAutorizacaoH.NfeAutorizacao();

                    WsHRecepcao.ClientCertificates.Add(_X509Cert);
                    WsHRecepcao.Timeout = Convert.ToInt32(240000);
                    WsHRecepcao.InitializeLifetimeService();

                    NFCeAutorizacaoH.nfeCabecMsg cabec = new NFCeAutorizacaoH.nfeCabecMsg();
                    cabec.cUF = "33";
                    cabec.versaoDados = "3.10";
                    WsHRecepcao.nfeCabecMsgValue = cabec;
                    TextoXML = WsHRecepcao.nfeAutorizacaoLote(xmlDados).OuterXml;
                    WsHRecepcao.Dispose();
                }


                return TextoXML;
            }
            catch (Exception ex)
            {
                Log_Exception.Monta_ArquivoLog(ex);

                return "Erro de conexão na recepção...! " + ex.Message;
            }

        }
        //------------------------------------------------------------------------------
 
        public string XML_InutilizacaoNFCe4(XmlDocument xmlAssinado, X509Certificate2 _X509Cert)
        {
            try
            {
                //Transmitindo em ambiente de homologação
                xmlDados = xmlAssinado.DocumentElement;

                ServicePoint();

                if (GerarXML.str_Ambiente == "1")
                {
                    //NFCeRecepcaoP - PRODUCAO  /  NFCeRecepcaoH - HOMOLOGACAO
                    NFCeInutilizacao4P.NFeInutilizacao4 WsHInutilizacao = new NFCeInutilizacao4P.NFeInutilizacao4();

                    WsHInutilizacao.ClientCertificates.Add(_X509Cert);
                    WsHInutilizacao.Timeout = Convert.ToInt32(240000);
                    WsHInutilizacao.InitializeLifetimeService();

                    //NFCeInutilizacao2P.nfeCabecMsg cabec = new NFCeInutilizacao2P.nfeCabecMsg();
                    //cabec.cUF = "33";
                    //cabec.versaoDados = "3.10";
                    //WsHInutilizacao.nfeCabecMsgValue = cabec;
                    TextoXML = WsHInutilizacao.nfeInutilizacaoNF(xmlDados).OuterXml;
                    WsHInutilizacao.Dispose();

                }
                else
                {
                    //NFCeRecepcaoP - PRODUCAO  /  NFCeRecepcaoH - HOMOLOGACAO
                    NFCeInutilizacao2H.NfeInutilizacao2 WsHInutilizacao = new NFCeInutilizacao2H.NfeInutilizacao2();

                    WsHInutilizacao.ClientCertificates.Add(_X509Cert);
                    WsHInutilizacao.Timeout = Convert.ToInt32(240000);
                    WsHInutilizacao.InitializeLifetimeService();

                    NFCeInutilizacao2H.nfeCabecMsg cabec = new NFCeInutilizacao2H.nfeCabecMsg();
                    cabec.cUF = "33";
                    cabec.versaoDados = "3.10";
                    WsHInutilizacao.nfeCabecMsgValue = cabec;
                    TextoXML = WsHInutilizacao.nfeInutilizacaoNF2(xmlDados).OuterXml;
                    WsHInutilizacao.Dispose();
                }


                return TextoXML;

            }
            catch (Exception ex)
            {
                Log_Exception.Monta_ArquivoLog(ex);

                return "Erro de conexão na recepção...! " + ex.Message;
            }
        }
        //------------------------------------------------------------------------------

        public string XML_CancelamentoNFCe4(XmlDocument xmlAssinado, string nfiscal, X509Certificate2 _X509Cert)
        {
            try
            {
                GerarXML geraxml = new GerarXML();

                //Gerando o xml de Lote
                xmlLote = geraxml.Lote_Evento(xmlAssinado, nfiscal);

                xmlDados = xmlLote.DocumentElement;

                ServicePoint();

                if (GerarXML.str_Ambiente == "1")
                {
                    //NFCeRecepcaoEventoP - PRODUCAO  /  NFCeRecepcaoEventoH - HOMOLOGACAO
                    NFCeRecepcaoEvento4P.NFeRecepcaoEvento4 WsHRecepcao = new NFCeRecepcaoEvento4P.NFeRecepcaoEvento4();

                    WsHRecepcao.ClientCertificates.Add(_X509Cert);
                    WsHRecepcao.Timeout = Convert.ToInt32(120000);
                    WsHRecepcao.InitializeLifetimeService();

                    //NFCeRecepcaoEventoP.nfeCabecMsg cabec = new NFCeRecepcaoEventoP.nfeCabecMsg();
                    //cabec.cUF = "33";
                    //cabec.versaoDados = "1.00";
                    //WsHRecepcao.nfeCabecMsgValue = cabec;
                    TextoXML = WsHRecepcao.nfeRecepcaoEvento(xmlDados).OuterXml;
                    WsHRecepcao.Dispose();
                }
                else
                {
                    //NFCeRecepcaoEventoP - PRODUCAO  /  NFCeRecepcaoEventoH - HOMOLOGACAO
                    NFCeRecepcaoEventoH.RecepcaoEvento WsHRecepcao = new NFCeRecepcaoEventoH.RecepcaoEvento();

                    WsHRecepcao.ClientCertificates.Add(_X509Cert);
                    WsHRecepcao.Timeout = Convert.ToInt32(120000);
                    WsHRecepcao.InitializeLifetimeService();

                    NFCeRecepcaoEventoH.nfeCabecMsg cabec = new NFCeRecepcaoEventoH.nfeCabecMsg();
                    cabec.cUF = "33";
                    cabec.versaoDados = "1.00";
                    WsHRecepcao.nfeCabecMsgValue = cabec;
                    TextoXML = WsHRecepcao.nfeRecepcaoEvento(xmlDados).OuterXml;
                    WsHRecepcao.Dispose();
                }


                return TextoXML;
            }
            catch (Exception ex)
            {
                Log_Exception.Monta_ArquivoLog(ex);

                return "Erro de conexão na recepção...! " + ex.Message;
            }
        }
    }
}
