using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Security.Cryptography.Xml;
using System.Threading;
using System.Runtime.InteropServices;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.IO;
using ProjetoPDVModelos;
using ProjetoPDVDao;
using ProjetoPDVServico;
//using Outlook = Microsoft.Office.Interop.Outlook;


namespace ProjetoTeste
{
    class TesteTransmitir
    {

        static void Main(string[] args)
        {

            Pedido p = (new PedidoDao()).getPedido(155149);

            p.cliente = (new ClienteDao()).getClientePedido(p.numdoc);
            p.cliente.end = ((new EnderecoDao()).getEnderecoCliente(p.numdoc));

            p.operacao = (new OperacaoDao()).getOperacaoPedido(p.numdoc);
            p.lstPedidoItem = (new PedidoItemDao()).getlst_Itens(p.numdoc);

            //p.emitente = (new EmitenteDao()).getEmitente();
            //p.emitente.endereco = (new EnderecoDao()).getEnderecoEmitente();

            ProdutoDao pd = new ProdutoDao();

            foreach (PedidoItem pedidoitem in p.lstPedidoItem)
            {
                pedidoitem.produto = pd.getProduto(pedidoitem.codpro);
            }
            
            

            Email em = new Email();


            /*
            if(em.EnviaEmailNFe(p))
            {
                Console.WriteLine("Email enviado com sucesso!");
                Console.ReadKey();
            }
            */




            /*
            string para = "comercial2@impetus.com.br";

            string assunto;
            string mensagem; 


            if (p.cliente.codcli != 1)
            {
                assunto = "NFe nº " + p.nfiscal + " - Editora Impetus";
                mensagem = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8' />" +
                           "<style type='text/css'> p { font: 13px Tahoma, Geneva, sans-serif; letter-spacing: 1px; margin: 0px; padding: 0px; padding-bottom: 2px; }</style></head>" +
                           "<body><p><b>Prezado cliente,</b></p>" +
                           "<p>Segue em anexo arquivo XML da NFe nº" + p.nfiscal + ".</p>" +
                           "<p>Conforme legislação vigente este arquivo deve ser salvo em um local seguro pois somente estará disponível pelo prazo de 180 dias.</p>" +
                           "<p>Chave de acesso: " + p.chave + "</p><p>Protocolo de autorização de uso: " + p.protocolo + "</p>" +
                           "<br /><p>Este e-mail foi enviado automaticamente, favor não responder.</p>" +
                           "<p>Atenciosamente,</p> <p>Departamento Comercial.</p></body></html>";
            }
            else
            {

                assunto = "Comunicado de envio - Editora Impetus";
                string endereco = p.cliente.end.logradouro + ", " + p.cliente.end.numero + " - " + p.cliente.end.complemento + " - " + p.cliente.end.bairro + " - " + p.cliente.end.municipio + " - " + p.cliente.end.uf;
                string produto = "<ul>";

                for (int i = 1; i <= p.lstPedidoItem.Count; i++)
                {
                    produto = produto + "<li>" + p.lstPedidoItem[i-1].produto.descricao + "</li>";
                }

                produto = produto + "</ul>";

                mensagem = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8' /><style type='text/css'> " +
                           "li, p, a {font: 13px Tahoma, Geneva, sans-serif; letter-spacing: 1px; margin: 0px; padding: 0px; padding-bottom: 2px; color:#333333}</style></head><body>" +
                           "<p><b>Prezado Professor (a): " + p.cliente.firma + "</b></p>" +
                           "<p>É com imenso prazer que informamos que postamos hoje a(s) obra(s):</p>" + produto +
                           "<p>Para o endereço abaixo:</p>" + endereco + "<br />" +
                           "<p>Caso a(s) obra(s) não chegue(m) no endereço acima no prazo de 10 dias úteis, por favor entre em contato conosco.</p>" +
                           "<p>Muito carinho foi empregado na produção desta obra. Esperamos que ela o agrade.</p><br /><p>Desde já agradecemos sua atenção.</p> " +
                           "<p>Para comentários, críticas ou sugestões <a href='http://www.impetus.com.br/#display=contact&container=content&content=send'>Clique aqui</a>.</p><br /><p><b>Este remetente não recebe mensagens, favor não responder!</b></p><br /><p>Atenciosamente,</p><p>Fátima Rangel</p><p>Divulgação</p></body></html>";
            }

            Outlook._Application oApp = new Outlook.Application();

            // Create a new MailItem.
            Outlook._MailItem oMsg = oApp.CreateItem(Outlook.OlItemType.olMailItem);

            oMsg.Subject = assunto;
            oMsg.HTMLBody = mensagem;
            oMsg.To = para;
            oMsg.BCC = "";

            Outlook.Attachments oAttachs = oMsg.Attachments;
            oAttachs.Add(@"C:\Documents and Settings\Renan\Desktop\new20Assinada.xml");

            // Enviando
            oMsg.Send();

            oApp = null;
            oMsg = null;
            oAttachs = null;


            */


            //Console.ReadKey();
/*

            Pedido pedido = (new PedidoDao()).getPedido(155311);

            pedido.cliente = (new ClienteDao()).getClientePedido(pedido.numdoc);
            pedido.cliente.end = ((new EnderecoDao()).getEnderecoCliente(pedido.numdoc));

            pedido.operacao = (new OperacaoDao()).getOperacaoPedido(pedido.numdoc);
            pedido.transportadora = (new TransportadoraDao()).getTranspPedido(pedido.numdoc);
            pedido.lstPedidoItem = (new PedidoItemDao()).getlstItens(pedido.numdoc);

            pedido.emitente = (new EmitenteDao()).getEmitente();
            pedido.emitente.endereco = (new EnderecoDao()).getEnderecoEmitente();

            ProdutoDao pd = new ProdutoDao();

            foreach (PedidoItem pedidoitem in pedido.lstPedidoItem)
            {
                pedidoitem.produto = pd.getProduto(pedidoitem.codpro);
            }




            ServicoNFe servico = new ServicoNFe();

            XmlDocument xmlcc = new XmlDocument();
            xmlcc = servico.CartaCorrecaoXML(pedido, "Teste de correcao");

            XmlDocument xmlcc_ass = new XmlDocument();
            xmlcc_ass = servico.AssinaXML(xmlcc.InnerXml, "infEvento", _X509Cert);



            XmlNode xmlDados;
            xmlDados = xmlcc_ass.DocumentElement;

            RecepcaoEventoH.RecepcaoEvento WsHRecepcao = new RecepcaoEventoH.RecepcaoEvento();
            //NFeRecepcao.NfeAutorizacao WsHRecepcao = new NFeRecepcao.NfeAutorizacao();

            WsHRecepcao.ClientCertificates.Add(_X509Cert);
            WsHRecepcao.Timeout = Convert.ToInt32(120000);
            WsHRecepcao.InitializeLifetimeService();

            RecepcaoEventoH.nfeCabecMsg cabec = new RecepcaoEventoH.nfeCabecMsg();
            cabec.cUF = "33";
            cabec.versaoDados = "1.00";
            WsHRecepcao.nfeCabecMsgValue = cabec;
            string TextoXML = WsHRecepcao.nfeRecepcaoEvento(xmlDados).OuterXml;
            WsHRecepcao.Dispose();



            StreamWriter Grava = File.CreateText(@"C:\Documents and Settings\Renan\Desktop\new20Assinada - RETORNO-CARTACORRECAO.xml");
            Grava.Write(TextoXML);
            Grava.Close();

            */


            /*
            // TESTE DE INUTILIZACAO
            //Selecionando o certificado
            X509Certificate2 _X509Cert = new X509Certificate2();
            X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

            X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
            X509Certificate2Collection collection1 = (X509Certificate2Collection)(collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false));
            X509Certificate2Collection collection2 = (X509Certificate2Collection)(collection.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, false));
            //X509Certificate2Collection scollection = X509Certificate2UI.SelectFromCollection(collection2, "Certificado(s) Digital(is) disponível(is)", "Selecione o certificado digital para uso no aplicativo", X509SelectionFlag.SingleSelection);

            _X509Cert = store.Certificates[0];



            GerarXML servico = new GerarXML();
            TransmitirXML trans = new TransmitirXML();
            AssinarXML assinar = new AssinarXML();


            XmlDocument xmlInut = new XmlDocument();
            xmlInut = servico.InutilizacaoNFe(2, 2, "TESTANDO inutilização");


            XmlDocument xmlInut_Ass = new XmlDocument();
            xmlInut_Ass = assinar.AssinaXML(xmlInut.InnerXml, "infInut", _X509Cert);


            XmlNode xmlDados;
            xmlDados = xmlInut_Ass.DocumentElement;


            NFeInutilizacao2H.NfeInutilizacao2 WsHInutilizacao = new NFeInutilizacao2H.NfeInutilizacao2();
            //NFeRecepcao.NfeAutorizacao WsHRecepcao = new NFeRecepcao.NfeAutorizacao();

            WsHInutilizacao.ClientCertificates.Add(_X509Cert);
            WsHInutilizacao.Timeout = Convert.ToInt32(120000);
            WsHInutilizacao.InitializeLifetimeService();

            NFeInutilizacao2H.nfeCabecMsg cabec = new NFeInutilizacao2H.nfeCabecMsg();
            cabec.cUF = "33";
            cabec.versaoDados = "2.00";
            WsHInutilizacao.nfeCabecMsgValue = cabec;
            string TextoXML = WsHInutilizacao.nfeInutilizacaoNF2(xmlDados).OuterXml;
            WsHInutilizacao.Dispose();



            StreamWriter Grava = File.CreateText(@"C:\Documents and Settings\Renan\Desktop\new20Inut - RETORNO.xml");
            Grava.Write(TextoXML);
            Grava.Close();

            */

            //XmlTextReader xmlText = new XmlTextReader(@"C:\Documents and Settings\Renan\Desktop\new20RETORNO_LOTE.xml");



 /*
            while (xmlText.Read())
            {
                if (xmlText.NodeType == XmlNodeType.Text) //XmlNodeType.Element)
                {
                        //xmlText.Read();
                        Console.WriteLine(xmlText.Value);
                }

            }


            
            /*

             // TESTE DE CANCELAMENTO
            //Selecionando o certificado
            X509Certificate2 _X509Cert = new X509Certificate2();
            X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

            X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
            X509Certificate2Collection collection1 = (X509Certificate2Collection)(collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false));
            X509Certificate2Collection collection2 = (X509Certificate2Collection)(collection.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, false));
            //X509Certificate2Collection scollection = X509Certificate2UI.SelectFromCollection(collection2, "Certificado(s) Digital(is) disponível(is)", "Selecione o certificado digital para uso no aplicativo", X509SelectionFlag.SingleSelection);

            _X509Cert = store.Certificates[0];




            Pedido pedido = (new PedidoDao()).getPedido(154904);

            pedido.cliente = (new ClienteDao()).getClientePedido(pedido.numdoc);
            pedido.cliente.end = ((new EnderecoDao()).getEnderecoCliente(pedido.numdoc));

            pedido.operacao = (new OperacaoDao()).getOperacaoPedido(pedido.numdoc);
            pedido.transportadora = (new TransportadoraDao()).getTranspPedido(pedido.numdoc);
            pedido.lstPedidoItem = (new PedidoItemDao()).getlstItens(pedido.numdoc);

            pedido.emitente = (new EmitenteDao()).getEmitente();
            pedido.emitente.endereco = (new EnderecoDao()).getEnderecoEmitente();

            ProdutoDao pd = new ProdutoDao();

            foreach (PedidoItem pedidoitem in pedido.lstPedidoItem)
            {
                pedidoitem.produto = pd.getProduto(pedidoitem.codpro);
            }



            GerarXML geraxml = new GerarXML();
            AssinarXML assinar = new AssinarXML();


            XmlDocument canc = new XmlDocument();
            canc = geraxml.CancelamentoNFe(pedido, "Teste cancelamento homologacao");

            XmlDocument cancEnv = new XmlDocument();
            cancEnv = assinar.AssinaXML(canc.InnerXml, "infEvento", _X509Cert);


            //Gerando o xml de Lote
            XmlDocument xmlLote = new XmlDocument();
            xmlLote = geraxml.Lote_Evento(cancEnv); 

            


            //Transmitindo
            XmlNode xmlDados;
            xmlDados = xmlLote.DocumentElement;

            RecepcaoEventoH.RecepcaoEvento WsHRecepcao = new RecepcaoEventoH.RecepcaoEvento();
            //NFeRecepcao.NfeAutorizacao WsHRecepcao = new NFeRecepcao.NfeAutorizacao();

            WsHRecepcao.ClientCertificates.Add(_X509Cert);
            WsHRecepcao.Timeout = Convert.ToInt32(120000);
            WsHRecepcao.InitializeLifetimeService();

            RecepcaoEventoH.nfeCabecMsg cabec = new RecepcaoEventoH.nfeCabecMsg();
            cabec.cUF = "33";
            cabec.versaoDados = "1.00";
            WsHRecepcao.nfeCabecMsgValue = cabec;
            string TextoXML = WsHRecepcao.nfeRecepcaoEvento(xmlDados).OuterXml;
            WsHRecepcao.Dispose();



            StreamWriter Grava = File.CreateText(@"C:\Documents and Settings\Renan\Desktop\Cancelamento\new20Assinada - RETORNO-CANC.xml");
            Grava.Write(TextoXML);
            Grava.Close();

            */


            /*
                TESTE DE EMISSAO
            //Selecionando o certificado
            X509Certificate2 _X509Cert = new X509Certificate2();
            X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

            X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
            X509Certificate2Collection collection1 = (X509Certificate2Collection)(collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false));
            X509Certificate2Collection collection2 = (X509Certificate2Collection)(collection.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, false));
            //X509Certificate2Collection scollection = X509Certificate2UI.SelectFromCollection(collection2, "Certificado(s) Digital(is) disponível(is)", "Selecione o certificado digital para uso no aplicativo", X509SelectionFlag.SingleSelection);
            
            _X509Cert = store.Certificates[0];

            //---------------------------------------------------------------



            Pedido pedido = (new PedidoDao()).getMovdb(155025);

            pedido.cliente = (new ClienteDao()).getClientePedido(pedido.numdoc);
            pedido.cliente.end = ((new EnderecoDao()).getEnderecoCliente(pedido.numdoc));

            pedido.operacao = (new OperacaoDao()).getOperacaoPedido(pedido.numdoc);
            pedido.transportadora = (new TransportadoraDao()).getTranspPedido(pedido.numdoc);
            pedido.lstPedidoItem = (new PedidoItemDao()).getlstItens(pedido.numdoc);

            pedido.emitente = (new EmitenteDao()).getEmitente();
            pedido.emitente.endereco = (new EnderecoDao()).getEnderecoEmitente();

            ProdutoDao pd = new ProdutoDao();

            foreach (PedidoItem pedidoitem in pedido.lstPedidoItem)
            {
                pedidoitem.produto = pd.getProduto(pedidoitem.codpro);
            }




            ServicoNFe servico = new ServicoNFe();


            //Gerando xml do pedido 155003
            XmlDocument xml = new XmlDocument();
            xml = servico.GeraXml(pedido);



            

            //Assinando xml gerado anteriormente
            XmlDocument xmlAssinado = new XmlDocument();
            xmlAssinado = servico.AssinaXML(xml.InnerXml, "infNFe", _X509Cert);




            //Gerando o xml de Lote
            XmlDocument xmlLote = new XmlDocument();
            xmlLote = servico.GeraLoteXML(xmlAssinado); 

/*

            
            //Transmitindo
            XmlNode xmlDados;
            xmlDados = xmlLote.DocumentElement;

            
            NFeRecepcao.NfeAutorizacao WsHRecepcao = new NFeRecepcao.NfeAutorizacao();
                        
            WsHRecepcao.ClientCertificates.Add(_X509Cert);
            WsHRecepcao.Timeout = Convert.ToInt32(120000);
            WsHRecepcao.InitializeLifetimeService();

            NFeRecepcao.nfeCabecMsg cabec = new NFeRecepcao.nfeCabecMsg();
            cabec.cUF = "33";
            cabec.versaoDados = "3.10";
            WsHRecepcao.nfeCabecMsgValue = cabec;
            string TextoXML = WsHRecepcao.nfeAutorizacaoLote(xmlDados).OuterXml;
            WsHRecepcao.Dispose();
            

            
           StreamWriter Grava = File.CreateText(@"C:\Documents and Settings\Renan\Desktop\new4AssinadaLote.xml");
           Grava.Write(TextoXML);
           Grava.Close();
            */

        }
    }
}