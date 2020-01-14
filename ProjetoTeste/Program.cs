using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetoPDVDao;
using ProjetoPDVModelos;
using ProjetoPDVServico;
using System.Xml;
using System.IO;

namespace ProjetoTeste
{
    class Program
    {
        static void Main(string[] args)
        {

            //NumDoc 154988

            (new EmitenteDao()).SelecionaEmitente();
            
            Certificado.getInstance.Seleciona_Certificado();

            GerarXML.str_Ambiente = "2";



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




            Pedido pedido = (new PedidoDao()).getPedido(247259);

            //pedido.cliente = (new ClienteDao()).getClientePedido(pedido.numdoc);
            pedido.cliente = (new ClienteDao()).getClientePedido(247259);
            pedido.cliente.end = ((new EnderecoDao()).getEnderecoCliente(pedido.numdoc));

            pedido.operacao = (new OperacaoDao()).getOperacaoPedido(pedido.numdoc);
            pedido.lstPedidoItem = (new PedidoItemDao()).getlst_Itens(pedido.numdoc);

            //pedido.emitente = (new EmitenteDao()).getEmitente();
            //pedido.emitente.endereco = (new EnderecoDao()).getEnderecoEmitente();

            ProdutoDao pd = new ProdutoDao();
            
            foreach(PedidoItem pedidoitem in pedido.lstPedidoItem)
            {
                pedidoitem.produto = pd.getProduto(pedidoitem.codpro);
                pedidoitem.produto.subGrupo = pd.getSubGrupo(pedidoitem.produto.codgrupo, pedidoitem.produto.codsubGrupo);
                pedidoitem.produto.produto_loja = new Produto_Loja { desconto = 0 };
            }

            pedido.nfiscal = "81";


            GerarXML geraxml = new GerarXML();
            var xml = geraxml.NFe(pedido);


            var xmlAssinado = (new AssinarXML()).AssinaXML(xml.InnerXml, "infNFe", Certificado.getInstance.oCertificado);


            //========================================================================
            var Grava = File.CreateText(@"C:\Users\Admin\Desktop\XML_GERADO.XML");
            Grava.Write(xmlAssinado.InnerXml);
            Grava.Close();
            //========================================================================



            var retValidar = (new ValidarXML()).Valida(xmlAssinado, "NFe");
            //========================================================================
            Grava = File.CreateText(@"C:\Users\Admin\Desktop\XML_VALIDADO.XML");
            Grava.Write(retValidar);
            Grava.Close();
            //========================================================================


            var urlQRCode = geraxml.Gera_Url_QRCode(xmlAssinado, pedido);

            //Inserindo a URL QRCode no xml já assinado
            xmlAssinado.LoadXml(xmlAssinado.InnerXml.Replace("</infNFe>", "</infNFe><infNFeSupl><qrCode><![CDATA[" + urlQRCode + "]]></qrCode><urlChave>http://www4.fazenda.rj.gov.br/consultaNFCe/QRCode</urlChave></infNFeSupl>"));


            //========================================================================
            Grava = File.CreateText(@"C:\Users\Admin\Desktop\XML_ASSINADO_QRCODE.XML");
            Grava.Write(xmlAssinado.InnerXml);
            Grava.Close();
            //========================================================================


            string retTransmitir = "";

            if (retValidar == string.Empty)
                retTransmitir = (new TransmitirXML()).XML_NFCe4(xmlAssinado, pedido.nfiscal, Certificado.getInstance.oCertificado);
            else
                Console.Write(retValidar);


            //========================================================================
            Grava = File.CreateText(@"C:\Users\Admin\Desktop\XML_EMITIDO.XML");
            Grava.Write(retTransmitir);
            Grava.Close();
            //========================================================================



            //MP2032.ConfiguraModeloImpressora(7); // Bematech MP-4200 TH
            //MP2032.IniciaPorta("USB");

            //pedido.chave = "33180911500080000160650010000001011757287148";

            //ProjetoPDVUtil.ImpressoraBema.GeraDANFE_NFCe(pedido, urlQRCode);


            //MP2032.FechaPorta();


            //printa(servico.GeraLote(x));





            Console.Write("Fim");
            //Console.ReadKey();
        }


        private static void printa(XmlDocument doc)
        {
            XmlTextWriter writer = new XmlTextWriter(Console.Out);
            writer.Formatting = Formatting.Indented;
            doc.WriteTo(writer);
            writer.Flush();
            Console.WriteLine();
        }
    }
}
