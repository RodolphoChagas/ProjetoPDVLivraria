using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using ProjetoPDVModelos;
using ProjetoPDVDao;
using ProjetoPDVUtil;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography;
using System.Xml.Schema;
using System.Threading;
using System.Runtime.InteropServices;
using System.Web.Services;
using System.Web.Services.Protocols;



namespace ProjetoPDVServico
{
    public class GerarXML
    {

        private string url_site { get; set; }
        private string url_NFCe { get; set; }
        private string CSC_NFCe { get; set; }

        //Peso total da nota
        private double pesoTotal { get; set; }

        //Desconto total da nota
        private decimal TotDscNFe { get; set; }

        private string ChaveNFe { get; set; }
        private int cDV { get; set; }

        //private int cNF { get; set; }
        private string cNF;

        //Dados da nota
        private string strXmlProcNfe { get; set; }
        private string numProc { get; set; }
        private string numChave { get; set; }
        private string xMotivo { get; set; }


        //Dados para calculos de imposto
        private decimal vICMS = 0;
        private decimal vPIS = 0;
        private decimal vCOFINS = 0;

        private decimal vBCICMS = 0;
        private decimal vBCPIS = 0;
        private decimal vBCCOFINS = 0;



        private string strNFe_Assinada { get; set; }
        private string strProc { get; set; }

        private string _strChave;

        public string strChave
        {
            get
            {
                return _strChave;
            }
        }


        private static string _str_Ambiente;

        public static string str_Ambiente
        {
            get
            {
                return _str_Ambiente;
            }
            set
            {
                if (value == "1" || value == "2")
                {
                    _str_Ambiente = value;
                }
            }
        }


        public string Gera_Url_QRCode(XmlDocument xmlAssinado, Pedido pedido)
        {

            url_site = "http://www4.fazenda.rj.gov.br/consultaNFCe/QRCode?";
              

            try
            {

                if (str_Ambiente == "1")
                {
                    //TOKEN para Produção
                    CSC_NFCe = Controle.getInstance.csc_Producao;
                }
                else
                {
                    //TOKEN para Homologação
                    CSC_NFCe = Controle.getInstance.csc_Homologacao;
                }


                //url_NFCe += "chNFe=" + ChaveNFe;
                url_NFCe += "p=";

                var chave = ChaveNFe;

                //versao antiga NFCe 3.10
                //url_NFCe += "&nVersao=" + "100";

                //versao do QRCode 2 da versao NFCe 4.0
                //url_NFCe += "&nVersao=" + "2";
                //url_NFCe += "|" + "2";

                var versaoQrCode = "|" + "2";

                //url_NFCe += "&tpAmb=" + str_Ambiente;
                //url_NFCe += "|" + str_Ambiente;

                var ambiente = "|" + str_Ambiente;


                //if (pedido.cliente.firma.Trim() != "CONSUMIDOR NÃO IDENTIFICADO")
                //{
                //    url_NFCe += "&cDest=";

                //    if ((pedido.cliente.cpf ?? "").Trim().Length == 11)
                //        url_NFCe += pedido.cliente.cpf;
                //    else
                //        url_NFCe += pedido.cliente.cgc;
                //}


                //url_NFCe += "|dhEmi=" + BitConverter.ToString(Encoding.Default.GetBytes(xmlAssinado.GetElementsByTagName("dhEmi")[0].InnerText)).Replace("-", "");
                //url_NFCe += "|vNF=" + xmlAssinado.GetElementsByTagName("vNF")[0].InnerText;
                ////url_NFCe += "&vICMS=" + "0.00";
                //url_NFCe += "|vICMS=" + vICMS.ToString("0.00").Replace(",", ".");
                //url_NFCe += "|digVal=" + BitConverter.ToString(Encoding.Default.GetBytes(xmlAssinado.GetElementsByTagName("DigestValue")[0].InnerText)).Replace("-", "");

                //url_NFCe += "&cIdToken=" + "000001";
                //url_NFCe += "|" + "2";

                var idToken = "|" + "2";

                var qrCode = chave + versaoQrCode + ambiente + idToken;


                var qrCode_CSC = qrCode + CSC_NFCe;

                var SHA1 = getSHA1(qrCode_CSC).ToUpper();

                //url_NFCe += "&cHashQRCode" + getSHA1(url_NFCe + CSC_NFCe).ToUpper();
                url_NFCe += qrCode + "|" + SHA1;

            }
            catch (Exception)
            {
                throw;
            }

            return url_site + url_NFCe;
        }


        public string getSHA1(string texto)
        {
            SHA1CryptoServiceProvider sh = new SHA1CryptoServiceProvider();
            sh.ComputeHash(ASCIIEncoding.ASCII.GetBytes(texto));
            byte[] re = sh.Hash;

            StringBuilder sb = new StringBuilder();

            foreach (byte b in re)
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }


        public XmlDocument Lote(XmlDocument xml, string nfiscal)
        {
            //StringBuilder x = new StringBuilder();

            //x.Append(@"<?xml version=""1.0"" encoding=""utf-8""?>");
            //x.Append(@"<enviNFe xmlns=""http://www.portalfiscal.inf.br/nfe"" versao=""4.00"">");
            ////x.Append("<idLote>" + nfiscal.ToString("000000000000000") + "</idLote>");
            //x.Append("<idLote>" + String.Format(@"{0:000000000000000}", Convert.ToInt32(nfiscal)) + "</idLote>");
            //x.Append("<indSinc>1</indSinc>");
            //x.Append(xml.InnerXml);
            //x.Append("</enviNFe>");

            //XmlDocument xmlEnvLote = new XmlDocument();
            //xmlEnvLote.LoadXml(x.ToString());

            //return xmlEnvLote;


            StringBuilder x = new StringBuilder();

            x.Append(@"<?xml version=""1.0"" encoding=""utf-8""?>");
            //x.Append(@"<enviNFe xmlns=""http://www.portalfiscal.inf.br/nfe"" versao=""3.10"">");
            x.Append(@"<enviNFe xmlns=""http://www.portalfiscal.inf.br/nfe"" versao=""4.00"">");
            //x.Append("<idLote>" + nfiscal.ToString("000000000000000") + "</idLote>");
            x.Append("<idLote>" + String.Format(@"{0:000000000000000}", Convert.ToInt32(nfiscal)) + "</idLote>");
            x.Append("<indSinc>1</indSinc>");
            x.Append(xml.InnerXml);
            x.Append("</enviNFe>");

            XmlDocument xmlEnvLote = new XmlDocument();
            xmlEnvLote.LoadXml(x.ToString());

            return xmlEnvLote;
        }

        public XmlDocument Lote_Evento(XmlDocument xml, string nfiscal)
        {
            StringBuilder x = new StringBuilder();

            x.Append(@"<?xml version=""1.0"" encoding=""utf-8""?>");
            x.Append(@"<envEvento xmlns=""http://www.portalfiscal.inf.br/nfe"" versao=""1.00"">");
            //x.Append("<idLote>" + nfiscal.ToString("000000000000000") + "</idLote>");
            x.Append("<idLote>" + String.Format(@"{0:000000000000000}", Convert.ToInt32(nfiscal)) + "</idLote>");
            x.Append(xml.InnerXml);
            x.Append("</envEvento>");


            XmlDocument xmlEnvLote = new XmlDocument();
            xmlEnvLote.LoadXml(x.ToString());

            return xmlEnvLote;
        }


        //------------------------------------------------------------------------------
        public XmlDocument CancelamentoNFe(Pedido p, string strJustificativa)
        {

            try
            {

                XmlDocument doc = new XmlDocument();

                XmlNode evento = doc.CreateElement("evento");
                doc.AppendChild(evento);

                evento.Attributes.Append(atributo(doc, "xmlns", "http://www.portalfiscal.inf.br/nfe"));
                evento.Attributes.Append(atributo(doc, "versao", "1.00"));

                XmlNode infEvento = doc.CreateElement("infEvento");
                evento.AppendChild(infEvento);


                //A regra de formação do Id é: "ID" + tpEvento + chave da NFe + nSeqEvento
                infEvento.Attributes.Append(atributo(doc, "Id", "ID110111" + p.chave + "01"));
                infEvento.AppendChild(elemento(doc, "cOrgao", "33"));
                infEvento.AppendChild(elemento(doc, "tpAmb", _str_Ambiente));
                infEvento.AppendChild(elemento(doc, "CNPJ", Emitente.getInstance.cnpj));
                infEvento.AppendChild(elemento(doc, "chNFe", p.chave));
                infEvento.AppendChild(elemento(doc, "dhEvento", DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss'-03:00'")));
                infEvento.AppendChild(elemento(doc, "tpEvento", "110111"));
                infEvento.AppendChild(elemento(doc, "nSeqEvento", "1"));
                infEvento.AppendChild(elemento(doc, "verEvento", "1.00"));


                XmlNode detEvento = doc.CreateElement("detEvento");
                infEvento.AppendChild(detEvento);

                detEvento.Attributes.Append(atributo(doc, "versao", "1.00"));

                detEvento.AppendChild(elemento(doc, "descEvento", "Cancelamento"));
                detEvento.AppendChild(elemento(doc, "nProt", p.protocolo));
                detEvento.AppendChild(elemento(doc, "xJust", strJustificativa));

                return doc;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //------------------------------------------------------------------------------


        public XmlDocument InutilizacaoNFe(int NFInicial, int NFFinal, int SerieFiscal, string xJustificativa, string modelo)
        {

            XmlDocument doc = new XmlDocument();

            //doc.AppendChild(doc.CreateXmlDeclaration("1.0", "utf-8", null));

            XmlNode inutNFe = doc.CreateElement("inutNFe");
            doc.AppendChild(inutNFe);

            inutNFe.Attributes.Append(atributo(doc, "xmlns", "http://www.portalfiscal.inf.br/nfe"));
            inutNFe.Attributes.Append(atributo(doc, "versao", "4.00"));

            //Emitente em = (new EmitenteDao()).getEmitente();

            XmlNode infInut = doc.CreateElement("infInut");
            inutNFe.AppendChild(infInut);
            // ID = Literal 33 = Código Estado 15 = Ano 00000000000000 = CNPJ 55 = Modelo 001 = Série 000000411 = Número Inicial 000000411 = Número Final 
            infInut.Attributes.Append(atributo(doc, "Id", "ID" + "33" + DateTime.Now.Year.ToString().Substring(2, 2) + Emitente.getInstance.cnpj + modelo + SerieFiscal.ToString("000") + NFInicial.ToString("000000000") + NFFinal.ToString("000000000")));
            infInut.AppendChild(elemento(doc, "tpAmb", _str_Ambiente));
            infInut.AppendChild(elemento(doc, "xServ", "INUTILIZAR"));
            infInut.AppendChild(elemento(doc, "cUF", "33"));
            infInut.AppendChild(elemento(doc, "ano", DateTime.Now.Year.ToString().Substring(2, 2)));
            infInut.AppendChild(elemento(doc, "CNPJ", Emitente.getInstance.cnpj));
            infInut.AppendChild(elemento(doc, "mod", modelo));
            infInut.AppendChild(elemento(doc, "serie", SerieFiscal.ToString()));
            infInut.AppendChild(elemento(doc, "nNFIni", NFInicial.ToString()));
            infInut.AppendChild(elemento(doc, "nNFFin", NFFinal.ToString()));
            infInut.AppendChild(elemento(doc, "xJust", xJustificativa));

            return doc;
        }


        //------------------------------------------------------------------------------

        public XmlDocument NFe(Pedido p)
        {
            try
            {

                XmlDocument xml = new XmlDocument();

                XmlNode NFe = XML_NFe(xml);
                XmlNode infNFe = XML_infNFe(xml, p);

                XML_Ide(xml, p, infNFe);
                XML_Emitente(xml, p, infNFe);
                XML_Destinatario(xml, p, infNFe);
                XML_Itens(xml, p, infNFe);
                XML_Total(xml, p, infNFe);
                XML_Transp(xml, p, infNFe);

                XML_TPag(xml, p, infNFe); //Tipo de pagamento

                XML_InfAdic(xml, p, infNFe);

                NFe.AppendChild(infNFe);
                xml.AppendChild(NFe);

                return xml;

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void XML_TPag(XmlDocument xml, Pedido p, XmlNode raiz)
        {
            XmlNode pag = xml.CreateElement("pag");


            //Grupo inserido na 4.0
            XmlNode detPag = xml.CreateElement("detPag");
            pag.AppendChild(detPag);


            //Inserido na 4.0
            detPag.AppendChild(elemento(xml, "indPag", p.operacao.TV.ToString().Trim()));
            detPag.AppendChild(elemento(xml, "tPag", "01"));
            detPag.AppendChild(elemento(xml, "vPag", p.valdoc.ToString("######0.00").Replace(",", ".")));

            // versao 3.10
            //pag.AppendChild(elemento(xml, "tPag", p.tipoPgto.codFormaPgtoNFCe.ToString("00")));
            ////pag.AppendChild(elemento(xml, "tPag", "99"));
            //pag.AppendChild(elemento(xml, "vPag", p.valdoc.ToString("######0.00").Replace(",", ".")));

            raiz.AppendChild(pag);
        }

        //--------------------------------------------------------------------

        private XmlNode XML_nfeProc(XmlDocument xml)
        {
            XmlNode nfeProc = xml.CreateElement("nfeProc");
            nfeProc.Attributes.Append(atributo(xml, "xmlns", "http://www.portalfiscal.inf.br/nfe"));
            //nfeProc.Attributes.Append(atributo(xml, "versao", "3.10"));
            nfeProc.Attributes.Append(atributo(xml, "versao", "4.0"));

            return nfeProc;
        }

        private XmlNode XML_NFe(XmlDocument xml)
        {
            XmlNode NFe = xml.CreateElement("NFe");
            NFe.Attributes.Append(atributo(xml, "xmlns", "http://www.portalfiscal.inf.br/nfe"));

            return NFe;
        }

        private void Monta_cNF()
        {
            /*
            string a;
            string b;
            string c;
            a = p.cliente.tipcli == 1 ? p.cliente.cpf.Substring(0, 2) : p.cliente.cgc.Substring(0, 2);
            b = p.cliente.tipcli == 1 ? p.cliente.cpf.Substring(10, 1) : p.cliente.cgc.Substring(13, 1);
            c = p.nfiscal.Substring(0, 1);
            cNF = DateTime.Now.Month.ToString("00") + a + b + c + DateTime.Now.Year.ToString().Substring(2, 2);
            */

            Random randNum = new Random();
            cNF = randNum.Next(99999999).ToString("00000000");

            randNum = null;
        }

        private XmlNode XML_infNFe(XmlDocument xml, Pedido p)
        {
            // Regra para montar a chave
            //33 + ano(2) + mes(2) + cnpj(14) + modelo(2) + serie(3) + NF(9) + codigo
            //codigo = mes(2) + cnpj (2 primeiros numeros iniciais) + cnpj (ultimo numero) + NF (1 numero inicial) + ano(2)
            //EXEMPLO:
            //33 16 08 01578493000295 55 002 000001237 10 59 1 0 11 73

            Monta_cNF();

            ChaveNFe = "33" + p.datanfiscal.ToString().Substring(8, 2) + p.datanfiscal.ToString().Substring(3, 2) + Emitente.getInstance.cnpj + p.modelo + (p.serienfiscal).ToString("000") + (string.IsNullOrEmpty(p.nfiscal) ? "0" : String.Format(@"{0:000000000}", Convert.ToInt32(p.nfiscal))) + "1" + cNF;
            cDV = DigitoVerificador.DigitoModulo11(ChaveNFe);
            ChaveNFe = ChaveNFe + cDV.ToString();

            _strChave = ChaveNFe;

            XmlNode infNFe = xml.CreateElement("infNFe");
            infNFe.Attributes.Append(atributo(xml, "Id", "NFe" + ChaveNFe));
            infNFe.Attributes.Append(atributo(xml, "versao", "4.00"));

            return infNFe;
        }


        private void XML_Ide(XmlDocument xml, Pedido p, XmlNode raiz)
        {
            XmlNode ide = xml.CreateElement("ide");
            ide.AppendChild(elemento(xml, "cUF", "33"));


            ide.AppendChild(elemento(xml, "cNF", cNF));
            ide.AppendChild(elemento(xml, "natOp", p.operacao.descfiscal.Trim()));

            //O campo foi reposicionado na versão 4.0. Agora os dados de pagamento são obrigatórios para NFe e NFCe e se encontram no grupo pag
            //ide.AppendChild(elemento(xml, "indPag", p.operacao.TV.ToString().Trim()));

            ide.AppendChild(elemento(xml, "mod", p.modelo));

            //Contingencia -----------------------------------------
            ide.AppendChild(elemento(xml, "serie", p.serienfiscal.ToString()));
            //------------------------------------------------------

            ide.AppendChild(elemento(xml, "nNF", p.nfiscal.Trim()));


            ide.AppendChild(elemento(xml, "dhEmi", p.datanfiscal.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss-03:00")));
            //ide.AppendChild(elemento(xml, "dhSaiEnt", p.datanfiscal.ToString("yyyy-MM-dd'T'HH:mm:ss'-02:00'")));


            ide.AppendChild(elemento(xml, "tpNF", p.operacao.STQ.ToString() != "1" ? "1" : "0"));
            ide.AppendChild(elemento(xml, "idDest", "1"));
            ide.AppendChild(elemento(xml, "cMunFG", "3304557"));
            ide.AppendChild(elemento(xml, "tpImp", "5"));

            //Forma da emissao da nota fiscal --------------------
            ide.AppendChild(elemento(xml, "tpEmis", "1"));
            //----------------------------------------------------
            //Digito verificador da chave - modulo 11 ------------
            ide.AppendChild(elemento(xml, "cDV", cDV.ToString()));
            //----------------------------------------------------

            // 1:Producao - 2:Homologacao
            ide.AppendChild(elemento(xml, "tpAmb", _str_Ambiente));
            ide.AppendChild(elemento(xml, "finNFe", p.operacao.devolucao.ToString() == "0" ? "1" : "4"));
            ide.AppendChild(elemento(xml, "indFinal", "1"));
            ide.AppendChild(elemento(xml, "indPres", "1"));
            ide.AppendChild(elemento(xml, "procEmi", "0"));
            ide.AppendChild(elemento(xml, "verProc", "1.0.0.160"));

            // Devolucao
            if (p.operacao.devolucao == "1")
            {
                List<string> lstChave = (new PedidoDao()).getlstPedidos_Relacionados(p.numdoc);

                foreach (string strChave in lstChave)
                {
                    XmlNode NFref = xml.CreateElement("NFref");
                    NFref.AppendChild(elemento(xml, "refNFe", strChave));
                    ide.AppendChild(NFref);
                }
            }

            raiz.AppendChild(ide);
        }


        private void XML_Emitente(XmlDocument xml, Pedido p, XmlNode raiz)
        {
            XmlNode emit = xml.CreateElement("emit");
            emit.AppendChild(elemento(xml, "CNPJ", string.IsNullOrEmpty(Emitente.getInstance.cnpj) ? "" : Emitente.getInstance.cnpj.Trim()));

            if (str_Ambiente == "2")
            {
                emit.AppendChild(elemento(xml, "xNome", "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL"));
            }
            else
            {
                emit.AppendChild(elemento(xml, "xNome", string.IsNullOrEmpty(Emitente.getInstance.nome) ? "" : Emitente.getInstance.nome.Trim()));
            }

            emit.AppendChild(elemento(xml, "xFant", string.IsNullOrEmpty(Emitente.getInstance.nomefantasia) ? "" : Emitente.getInstance.nomefantasia.Trim()));


            XmlNode enderEmit = xml.CreateElement("enderEmit");

            enderEmit.AppendChild(elemento(xml, "xLgr", string.IsNullOrEmpty(Emitente.getInstance.endereco.logradouro) ? "" : Emitente.getInstance.endereco.logradouro.Trim()));
            enderEmit.AppendChild(elemento(xml, "nro", string.IsNullOrEmpty(Emitente.getInstance.endereco.numero) ? "" : Emitente.getInstance.endereco.numero.Trim()));
            enderEmit.AppendChild(elemento(xml, "xCpl", string.IsNullOrEmpty(Emitente.getInstance.endereco.complemento) ? "" : Emitente.getInstance.endereco.complemento.Trim()));
            enderEmit.AppendChild(elemento(xml, "xBairro", Emitente.getInstance.endereco.bairro));
            enderEmit.AppendChild(elemento(xml, "cMun", "3304904"));
            enderEmit.AppendChild(elemento(xml, "xMun", string.IsNullOrEmpty(Emitente.getInstance.endereco.municipio) ? "" : Emitente.getInstance.endereco.municipio.Trim()));
            enderEmit.AppendChild(elemento(xml, "UF", string.IsNullOrEmpty(Emitente.getInstance.endereco.uf) ? "" : Emitente.getInstance.endereco.uf.Trim()));
            enderEmit.AppendChild(elemento(xml, "CEP", string.IsNullOrEmpty(Emitente.getInstance.endereco.cep) | Emitente.getInstance.endereco.cep.Trim().Length < 8 ? "" : Emitente.getInstance.endereco.cep.Trim()));
            enderEmit.AppendChild(elemento(xml, "cPais", "1058"));
            enderEmit.AppendChild(elemento(xml, "xPais", "BRASIL"));
            enderEmit.AppendChild(elemento(xml, "fone", "2137114219"));
            emit.AppendChild(enderEmit);

            emit.AppendChild(elemento(xml, "IE", Emitente.getInstance.inscest));
            emit.AppendChild(elemento(xml, "CRT", "3"));

            raiz.AppendChild(emit);
        }


        private void XML_Destinatario(XmlDocument xml, Pedido p, XmlNode raiz)
        {

            if (p.cliente.firma.Trim() == "CONSUMIDOR NÃO IDENTIFICADO")
            {
                return;
            }


            XmlNode dest = xml.CreateElement("dest");

            if (p.cliente.tipcli == 1)
            {
                dest.AppendChild(elemento(xml, "CPF", string.IsNullOrEmpty(p.cliente.cpf.Trim()) || p.cliente.cpf.Trim().Length < 11 ? "" : p.cliente.cpf));
            }
            else
            {
                dest.AppendChild(elemento(xml, "CNPJ", string.IsNullOrEmpty(p.cliente.cgc.Trim()) || p.cliente.cgc.Trim().Length < 11 ? "" : p.cliente.cgc.Trim()));
            }

            // 1 - Producao / 2 - Homologacao
            if (_str_Ambiente == "1")
            {
                dest.AppendChild(elemento(xml, "xNome", p.cliente.firma.Trim()));

            }
            else
            {
                dest.AppendChild(elemento(xml, "xNome", "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL"));
            }


            /*
            if (!string.IsNullOrEmpty(p.cliente.end.logradouro.Trim()))
            {
                XmlNode enderDest = xml.CreateElement("enderDest");
                dest.AppendChild(enderDest);

                enderDest.AppendChild(elemento(xml, "xLgr", string.IsNullOrEmpty(p.cliente.end.logradouro) ? "" : p.cliente.end.logradouro.Trim()));
                enderDest.AppendChild(elemento(xml, "nro", string.IsNullOrEmpty(p.cliente.end.numero) ? "" : p.cliente.end.numero.Trim()));
                enderDest.AppendChild(elemento(xml, "xBairro", string.IsNullOrEmpty(p.cliente.end.bairro) ? "" : p.cliente.end.bairro.Trim()));
                enderDest.AppendChild(elemento(xml, "cMun", string.IsNullOrEmpty(p.cliente.end.cMun) ? "" : p.cliente.end.cMun.Trim()));
                enderDest.AppendChild(elemento(xml, "xMun", string.IsNullOrEmpty(p.cliente.end.municipio) ? "" : p.cliente.end.municipio.Trim()));
                enderDest.AppendChild(elemento(xml, "UF", string.IsNullOrEmpty(p.cliente.end.uf) ? "" : p.cliente.end.uf.Trim()));
                enderDest.AppendChild(elemento(xml, "CEP", string.IsNullOrEmpty(p.cliente.end.cep) || p.cliente.end.cep.Trim().Length < 8 ? "" : p.cliente.end.cep.Trim()));
                enderDest.AppendChild(elemento(xml, "cPais", "1058"));
                enderDest.AppendChild(elemento(xml, "xPais", "Brasil"));
            }
            */

            dest.AppendChild(elemento(xml, "indIEDest", "9"));

            raiz.AppendChild(dest);
        }

        private void XML_Itens(XmlDocument xml, Pedido p, XmlNode raiz)
        {
            int n = 1;
            TotDscNFe = 0;

            string strICMS = string.Empty;
            string strICMS_CST = string.Empty;

            string strPIS = string.Empty;
            string strPIS_CST = string.Empty;

            string strCOFINS = string.Empty;
            string strCOFINS_CST = string.Empty;

            decimal calcula_Imposto = 0;


            foreach (PedidoItem pedidoitem in p.lstPedidoItem)
            {
                //DET
                XmlNode det = xml.CreateElement("det");
                det.Attributes.Append(atributo(xml, "nItem", n.ToString()));

                XmlNode prod = xml.CreateElement("prod");
                prod.AppendChild(elemento(xml, "cProd", pedidoitem.produto.isbn != null ? pedidoitem.produto.isbn.Trim() : pedidoitem.produto.codpro.ToString()));
                prod.AppendChild(elemento(xml, "cEAN", pedidoitem.produto.isbn != null ? pedidoitem.produto.isbn.Trim() : "SEM GTIN"));




                if (str_Ambiente == "2")
                {
                    prod.AppendChild(elemento(xml, "xProd", "NOTA FISCAL EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL"));
                }
                else
                {
                    prod.AppendChild(elemento(xml, "xProd", StringUtil.RemoverAcentos(pedidoitem.produto.descricao.Trim()).ToUpper()));
                }



                //prod.AppendChild(elemento(xml, "NCM", "49019900"));
                prod.AppendChild(elemento(xml, "NCM", pedidoitem.produto.subGrupo.ncm));

                if (pedidoitem.produto.subGrupo.st.Equals(1) && pedidoitem.produto.subGrupo.cest.Trim().Length > 1)
                    prod.AppendChild(elemento(xml, "CEST", pedidoitem.produto.subGrupo.cest));


                if (pedidoitem.produto.subGrupo.st.Equals(1) && pedidoitem.produto.codgrupo != 0)
                    prod.AppendChild(elemento(xml, "CFOP", "5405"));
                else
                    prod.AppendChild(elemento(xml, "CFOP", p.operacao.cfde.ToString()));


                //prod.AppendChild(elemento(xml, "CFOP", p.operacao.cfde.ToString()));
                //prod.AppendChild(elemento(xml, "CFOP", "5405"));
                //prod.AppendChild(elemento(xml, "CFOP", "5102"));

                prod.AppendChild(elemento(xml, "uCom", "Ex"));
                prod.AppendChild(elemento(xml, "qCom", pedidoitem.qtditens.ToString(".0000").Replace(",", ".")));
                prod.AppendChild(elemento(xml, "vUnCom", pedidoitem.prcitens.ToString("####0.0000").Replace(",", ".")));
                prod.AppendChild(elemento(xml, "vProd", (pedidoitem.prcitens * pedidoitem.qtditens).ToString("####0.00").Replace(",", ".")));

                //Comentado da versao 3.10
                //XmlNode cEANTrib = xml.CreateElement("cEANTrib");
                //prod.AppendChild(cEANTrib);

                //Agora informando o codigo GTIN(código de barras) - inserido na versao 4.0
                prod.AppendChild(elemento(xml, "cEANTrib", pedidoitem.produto.isbn != null ? pedidoitem.produto.isbn.Trim() : "SEM GTIN"));
                //prod.AppendChild(elemento(xml, "cEANTrib", "SEM GTIN"));



                prod.AppendChild(elemento(xml, "uTrib", "Ex"));
                prod.AppendChild(elemento(xml, "qTrib", prod.SelectSingleNode("qCom").InnerText));
                prod.AppendChild(elemento(xml, "vUnTrib", prod.SelectSingleNode("vUnCom").InnerText));

                //decimal valDscRateado = 0;
                decimal ValDsc = 0;
                string strDsc = "";


                //====================================================================================================
                //VERIFICAR ESSA PARTE DE DESCONTOS, DA PRA MELHORAR
                //====================================================================================================
                if ((pedidoitem.produto.produto_loja.desconto > 0) || ((pedidoitem.produto.prcvenda * pedidoitem.qtditens) != pedidoitem.valitens)) // || p.valdsc > 0)
                {
                    /*
                    if(p.valdsc > 0)
                        ValDsc = pedidoitem.valDesc;
                    else
                    */

                    ValDsc = decimal.Round(((pedidoitem.produto.prcvenda * pedidoitem.qtditens) - pedidoitem.valitens), 2);


                    //ValDsc = decimal.Round(ValDsc, 2);
                    strDsc = ValDsc.ToString("####0.00").Replace(",", ".");
                    //strDsc = ValDsc.ToString("####0.00").Replace(",", ".");

                    //Calculando o total de desconto
                    TotDscNFe += ValDsc;

                    prod.AppendChild(elemento(xml, "vDesc", strDsc));
                }
                //====================================================================================================



                prod.AppendChild(elemento(xml, "indTot", "1"));
                pesoTotal = (pedidoitem.produto.peso * pedidoitem.qtditens);






                //IMPOSTO ===================================================================================================================
                XmlNode imposto = xml.CreateElement("imposto");



                if (pedidoitem.produto.subGrupo.st.Equals(1)) // Não tributado - Substituição Tributária 
                {
                    // CodGrupo dos Livros
                    if (pedidoitem.produto.codgrupo.Equals(0))
                    {
                        strICMS = "ICMS40";
                        strICMS_CST = "41";
                    }
                    else
                    {
                        strICMS = "ICMS60";
                        strICMS_CST = "60";
                    }


                }
                else if (pedidoitem.produto.subGrupo.st.Equals(0)) // Tributado Integralmente
                {
                    strICMS = "ICMS00";
                    strICMS_CST = "00";
                }

                // PIS E COFINS
                if (pedidoitem.produto.subGrupo.pis > 0 && pedidoitem.produto.subGrupo.cofins > 0)
                {
                    strPIS = "PISAliq";
                    strPIS_CST = "01";
                    strCOFINS = "COFINSAliq";
                    strCOFINS_CST = "01";
                }
                else if (pedidoitem.produto.subGrupo.pis.Equals(0) && pedidoitem.produto.subGrupo.cofins.Equals(0))
                {
                    if (pedidoitem.produto.subGrupo.st.Equals(1))
                    {
                        strPIS = "PISNT";
                        strPIS_CST = "04";
                        strCOFINS = "COFINSNT";
                        strCOFINS_CST = "04";
                    }
                    else
                    {
                        strPIS = "PISNT";
                        strPIS_CST = "06";
                        strCOFINS = "COFINSNT";
                        strCOFINS_CST = "06";
                    }
                }



                // ICMS =====================================================================================================================
                XmlNode ICMS = xml.CreateElement("ICMS");
                XmlNode ICMS40 = xml.CreateElement(strICMS);
                ICMS40.AppendChild(elemento(xml, "orig", "0"));
                ICMS40.AppendChild(elemento(xml, "CST", strICMS_CST));


                if (strICMS_CST.Equals("00"))
                {
                    calcula_Imposto = 0;
                    calcula_Imposto = decimal.Round((pedidoitem.valitens * (pedidoitem.produto.subGrupo.picms / 100)), 2);


                    ICMS40.AppendChild(elemento(xml, "modBC", "3"));
                    ICMS40.AppendChild(elemento(xml, "vBC", pedidoitem.valitens.ToString("0.00").Replace(",", ".")));
                    ICMS40.AppendChild(elemento(xml, "pICMS", pedidoitem.produto.subGrupo.picms.ToString("00")));
                    ICMS40.AppendChild(elemento(xml, "vICMS", calcula_Imposto.ToString("0.00").Replace(",", ".")));


                    vICMS += calcula_Imposto;
                    vBCICMS += pedidoitem.valitens;
                }


                // PIS =====================================================================================================================
                XmlNode PIS = xml.CreateElement("PIS");
                XmlNode PISNT = xml.CreateElement(strPIS);
                PISNT.AppendChild(elemento(xml, "CST", strPIS_CST));

                if (strPIS.Equals("PISAliq"))
                {
                    calcula_Imposto = 0;
                    calcula_Imposto = decimal.Round((pedidoitem.valitens * (pedidoitem.produto.subGrupo.pis / 100)), 2);

                    //PIS
                    PISNT.AppendChild(elemento(xml, "vBC", pedidoitem.valitens.ToString("0.00").Replace(",", ".")));
                    PISNT.AppendChild(elemento(xml, "pPIS", pedidoitem.produto.subGrupo.pis.ToString("0.00").Replace(",", ".")));
                    PISNT.AppendChild(elemento(xml, "vPIS", calcula_Imposto.ToString("0.00").Replace(",", ".")));

                    vPIS += calcula_Imposto;
                    vBCPIS += pedidoitem.valitens;
                }



                // COFINS =====================================================================================================================
                XmlNode COFINS = xml.CreateElement("COFINS");
                XmlNode COFINSNT = xml.CreateElement(strCOFINS);
                COFINSNT.AppendChild(elemento(xml, "CST", strCOFINS_CST));


                if (pedidoitem.produto.subGrupo.cofins > 0)
                {
                    calcula_Imposto = 0;
                    calcula_Imposto = decimal.Round((pedidoitem.valitens * (pedidoitem.produto.subGrupo.cofins / 100)), 2);

                    COFINSNT.AppendChild(elemento(xml, "vBC", pedidoitem.valitens.ToString("0.00").Replace(",", ".")));
                    COFINSNT.AppendChild(elemento(xml, "pCOFINS", pedidoitem.produto.subGrupo.cofins.ToString("0.00").Replace(",", ".")));
                    COFINSNT.AppendChild(elemento(xml, "vCOFINS", calcula_Imposto.ToString("0.00").Replace(",", ".")));

                    vCOFINS += calcula_Imposto;
                    vBCCOFINS += pedidoitem.valitens;
                }



                //  ICMS
                /*
                XmlNode ICMS = xml.CreateElement("ICMS");
                XmlNode ICMS40 = xml.CreateElement("ICMS40");
                ICMS40.AppendChild(elemento(xml, "orig", "0"));
                ICMS40.AppendChild(elemento(xml, "CST", "41"));
                */
                /*
                XmlNode ICMS = xml.CreateElement("ICMS");
                XmlNode ICMS40 = xml.CreateElement("ICMS60");
                ICMS40.AppendChild(elemento(xml, "orig", "0"));
                ICMS40.AppendChild(elemento(xml, "CST", "60"));
                */
                /*
                XmlNode ICMS = xml.CreateElement("ICMS");
                XmlNode ICMS40 = xml.CreateElement("ICMS00");
                ICMS40.AppendChild(elemento(xml, "orig", "0"));
                ICMS40.AppendChild(elemento(xml, "CST", "00"));
                ICMS40.AppendChild(elemento(xml, "modBC", "3"));
                ICMS40.AppendChild(elemento(xml, "vBC", "5.00"));
                ICMS40.AppendChild(elemento(xml, "pICMS", "12"));
                ICMS40.AppendChild(elemento(xml, "vICMS", "0.60"));
                */


                /*
                //IPI
                XmlNode IPI = xml.CreateElement("IPI");
                IPI.AppendChild(elemento(xml, "clEnq", "99999"));
                IPI.AppendChild(elemento(xml, "cEnq", "999"));
                
 
                XmlNode IPINT = xml.CreateElement("IPINT");
                IPINT.AppendChild(elemento(xml, "CST", "53"));

                imposto.AppendChild(IPI);
                IPI.AppendChild(IPINT);
                 * */




                /*
                //  PIS
                //PIS
                XmlNode PIS = xml.CreateElement("PIS");
                XmlNode PISNT = xml.CreateElement("PISAliq");
                PISNT.AppendChild(elemento(xml, "CST", "01"));
                PISNT.AppendChild(elemento(xml, "vBC", p.valdoc.ToString("######0.00").Replace(",", ".")));
                PISNT.AppendChild(elemento(xml, "pPIS", "0.65"));
                PISNT.AppendChild(elemento(xml, "vPIS", "0.04"));

                */

                /*
                //PIS
                XmlNode PIS = xml.CreateElement("PIS");
                XmlNode PISNT = xml.CreateElement("PISNT");
                XmlNode CST = xml.CreateElement("CST");
                PISNT.AppendChild(elemento(xml, "CST", "06"));
                */

                //  COFINS
                /*
                XmlNode COFINS = xml.CreateElement("COFINS");
                XmlNode COFINSNT = xml.CreateElement("COFINSNT");
                COFINSNT.AppendChild(elemento(xml, "CST", "06"));
                */

                /*
                //COFINS
                XmlNode COFINS = xml.CreateElement("COFINS");
                XmlNode COFINSNT = xml.CreateElement("COFINSAliq");
                COFINSNT.AppendChild(elemento(xml, "CST", "01"));
                COFINSNT.AppendChild(elemento(xml, "vBC", p.valdoc.ToString("######0.00").Replace(",", ".")));
                COFINSNT.AppendChild(elemento(xml, "pCOFINS", "3.00"));
                COFINSNT.AppendChild(elemento(xml, "vCOFINS", "0.15"));
                */



                det.AppendChild(prod);
                det.AppendChild(imposto);
                imposto.AppendChild(ICMS);
                ICMS.AppendChild(ICMS40);
                //imposto.AppendChild(IPI);
                //IPI.AppendChild(IPINT);

                /*
                //Removendo o nó IPI da NFC-e
                if(p.modelo == "65")
                {
                    imposto.RemoveChild(IPI);
                }
                */
                imposto.AppendChild(PIS);
                PIS.AppendChild(PISNT);
                imposto.AppendChild(COFINS);
                COFINS.AppendChild(COFINSNT);
                raiz.AppendChild(det);

                n++;
            }
        }


        private void XML_Total(XmlDocument xml, Pedido p, XmlNode raiz)
        {
            XmlNode total = xml.CreateElement("total");

            XmlNode ICMSTot = xml.CreateElement("ICMSTot");
            total.AppendChild(ICMSTot);

            ICMSTot.AppendChild(elemento(xml, "vBC", vBCICMS.ToString("0.00").Replace(",", ".")));
            ICMSTot.AppendChild(elemento(xml, "vICMS", vICMS.ToString("0.00").Replace(",", ".")));
            ICMSTot.AppendChild(elemento(xml, "vICMSDeson", "0.00"));
            ICMSTot.AppendChild(elemento(xml, "vFCPUFDest", "0.00"));
            ICMSTot.AppendChild(elemento(xml, "vICMSUFDest", "0.00"));


            //Valor Total do FCP (Fundo de Combate à Pobreza) retido por substituição tributária - inserido na 4.0
            ICMSTot.AppendChild(elemento(xml, "vFCP", "0.00"));
            //---------------------------------------------------

            ICMSTot.AppendChild(elemento(xml, "vBCST", "0.00"));
            ICMSTot.AppendChild(elemento(xml, "vST", "0.00"));


            //Valor Total do FCP (Fundo de Combate à Pobreza) retido por substituição tributária - inserido na 4.0
            ICMSTot.AppendChild(elemento(xml, "vFCPST", "0.00"));
            //Valor Total do FCP retido anteriormente por Substituição Tributária - inserido na 4.0
            ICMSTot.AppendChild(elemento(xml, "vFCPSTRet", "0.00"));




            ICMSTot.AppendChild(elemento(xml, "vProd", (new PedidoItemDao()).getValorTotal_Itens(p.numdoc).ToString("######0.00").Replace(",", ".")));
            ICMSTot.AppendChild(elemento(xml, "vFrete", "0.00"));
            ICMSTot.AppendChild(elemento(xml, "vSeg", "0.00"));

            //---------------------------------------------------
            //---------------------------------------------------
            ICMSTot.AppendChild(elemento(xml, "vDesc", TotDscNFe.ToString("######0.00").Replace(",", ".")));
            //---------------------------------------------------
            //---------------------------------------------------


            ICMSTot.AppendChild(elemento(xml, "vII", "0.00"));
            ICMSTot.AppendChild(elemento(xml, "vIPI", "0.00"));


            //O campo foi adicionado no grupo de total da versão 4.0. Corresponde ao total da soma dos campos de vIPIDevol dos produtos. 
            //É obrigatório, mesmo que zerado - inserido na 4.0
            ICMSTot.AppendChild(elemento(xml, "vIPIDevol", "0.00"));


            ICMSTot.AppendChild(elemento(xml, "vPIS", vPIS.ToString("0.00").Replace(",", ".")));
            ICMSTot.AppendChild(elemento(xml, "vCOFINS", vCOFINS.ToString("0.00").Replace(",", ".")));
            ICMSTot.AppendChild(elemento(xml, "vOutro", "0.00"));
            ICMSTot.AppendChild(elemento(xml, "vNF", p.valdoc.ToString("######0.00").Replace(",", ".")));
            ICMSTot.AppendChild(elemento(xml, "vTotTrib", "0.00"));

            raiz.AppendChild(total);

        }


        private void XML_Transp(XmlDocument xml, Pedido p, XmlNode raiz)
        {

            //TRANSP
            XmlNode transp = xml.CreateElement("transp");

            if (p.modelo == "65")
            {
                transp.AppendChild(elemento(xml, "modFrete", "9"));
            }
            else
            {
                transp.AppendChild(elemento(xml, "modFrete", p.cndfrete.Trim() == "FOB" ? "1" : "0"));

                XmlNode vol = xml.CreateElement("vol");
                transp.AppendChild(vol);
                //--------------------------------
                vol.AppendChild(elemento(xml, "qVol", p.volume.ToString()));
                //--------------------------------
                vol.AppendChild(elemento(xml, "esp", "Volumes"));
                vol.AppendChild(elemento(xml, "marca", "S/N"));
                vol.AppendChild(elemento(xml, "nVol", "S/N"));

                //--------------------------------
                //(Peso * (QtdItens / 100))
                vol.AppendChild(elemento(xml, "pesoB", (pesoTotal / 1000).ToString("####0.000").Replace(",", ".")));
                //--------------------------------
            }

            raiz.AppendChild(transp);

        }


        private void XML_InfAdic(XmlDocument xml, Pedido p, XmlNode raiz)
        {
            XmlNode infAdic = xml.CreateElement("infAdic");
            infAdic.AppendChild(elemento(xml, "infCpl", p.modelo == "65" ? "Volte sempre" : StringUtil.RemoverAcentos(InfoAdic(p))));

            raiz.AppendChild(infAdic);
        }


        private XmlAttribute atributo(XmlDocument xml, string nome, string valor)
        {
            XmlAttribute attr = xml.CreateAttribute(nome);
            attr.Value = valor;
            return attr;
        }

        private XmlNode elemento(XmlDocument xml, string nome, string valor)
        {
            XmlNode no = xml.CreateElement(nome);
            no.InnerText = valor;

            return no;
        }

        private string InfoAdic(Pedido p)
        {
            PedidoItemDao pedidoitem = new PedidoItemDao();

            try
            {
                string info = "Nao incidencia de ICMS conforme art. 40 inciso I da lei n. 2.657/96.";
                info = info + " Data de Vencimento: " + DateTime.Now.AddDays(p.operacao.intinic);
                info = info + "  N. de itens: " + pedidoitem.getTotal_Itens(p.numdoc) + "   -   Exemp.: " + pedidoitem.getTotal_Exemplares(p.numdoc) + "  -";
                info = info + "  N. do Pedido: " + p.numdoc + "  -";
                info = info + "  Desconto: " + p.lstPedidoItem[0].dscitens + "%";
                info = info + (new DiversosDao()).getDiversos(p.numdoc) + ".";

                return info;
            }
            catch (Exception)
            {
                throw;
            }

        }




    }
}

