using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;
using System.Net.Mail;
using System.Net;

//using Outlook = Microsoft.Office.Interop.Outlook;

namespace ProjetoPDVModelos
{
    public class Email
    {


        public void Email_ConferenciaCaixa(List<PedidoItem> lstProdutosdoMes, List<PedidoItem> lstPerdasdoDia, Movimentacao movimentacao)
        {
            
            MailMessage mail = new MailMessage();
            StringBuilder cabecalho = new StringBuilder();
            StringBuilder produto = new StringBuilder();
            StringBuilder itens = new StringBuilder();
            StringBuilder perdas = new StringBuilder();
            StringBuilder total = new StringBuilder();

            int iTotalqtd = 0;
            int iTotalqtdAcumulado = 0;
            decimal totalValor = 0;

            try
            {

                cabecalho.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>");
                cabecalho.Append("<html xmlns='http://www.w3.org/1999/xhtml'>");
                cabecalho.Append("<head><meta http-equiv='Content-Type' content='text/html; charset=utf-8' /></head>");
                cabecalho.Append("<style> h4, h3, p, table, td, tr {margin:0px; padding:0px;}");
                cabecalho.Append("p, table, td, tr {color:#333333; font:10px Verdana, Arial, Helvetica, sans-serif ;}");
                cabecalho.Append("table {background:#e9e9e6; border-collapse:collapse;}");
                cabecalho.Append("td {padding:4px;} table.produtos td {border:2px solid #FFFFFF; background-color:#eeeee8;}");
                cabecalho.Append("table.produtos tr.topo td {background-color:#CCCCCC; color:#FFFFFF;}");
                cabecalho.Append("td.branc {background-color:#FFFFFF;} table#estado td {font-size:9px;}</style>");
                cabecalho.Append("<body><table width='650' align='center' bgcolor='#e9e9e6'>");
                cabecalho.Append("<tr><td width='149'><h4>Livraria Concursar</h4></td><td width='200'>&nbsp;</td><td width='119'>Data:" + DateTime.Now + "</td></tr>");
                cabecalho.Append("<tr><td height='60' colspan='3'><center><h2>Relatório de Conferência de Caixa</h2></center></td></tr></table><br />");


                produto.Append("<table width='650' align='center' bgcolor='#e9e9e6'><tr><td width='30'><b>Loja:</b></td><td width='452'>Cafeteria</td><td width='131'>&nbsp;</td>");
                produto.Append("<tr><td height='30' colspan='3'><center><h3>Lista de itens vendidos do dia</h3></center></td></tr></table>");

                produto.Append("<table width='650' class='produtos' align='center' bgcolor='#CCCCCC'>");
                produto.Append("<tr class='topo'><td width='429'><b>Descrição</b></td>");
                produto.Append("<td width='46'><center><b>Qtd.</b></center></td>");
                produto.Append("<td width='100'><center><b>Acumulado do Mês(Qtd)</b></center></td>");
                produto.Append("<td width='100'><center><b>Acumulado do Mês(R$)</b></center></td>");

                for (int i = 0; i <= lstProdutosdoMes.Count - 1; i++)
                {
                    //lstPItem[i].produto = pdao.getProduto(lstPItem[i].codpro);
                    itens.Append("<tr bgcolor='#eeeee8'><td>" + lstProdutosdoMes[i].produto.descricao + "</td><td><center>" + lstProdutosdoMes[i].qtditens + "</center></td><td><center>" + lstProdutosdoMes[i].numdoc + "</center></td><td><center>" + lstProdutosdoMes[i].valitens.ToString("0.00") + "</center></td></tr>");
                }
                itens.Append("</table><br />");

                

                perdas.Append("<table width='650' align='center' bgcolor='#e9e9e6'><tr><td width='30'><b>Loja:</b></td><td width='452'>Cafeteria</td><td width='131'>&nbsp;</td>");
                perdas.Append("<tr><td height='30' colspan='3'><center><h3>Perdas do dia</h3></center></td></tr></table>");

                perdas.Append("<table width='650' class='produtos' align='center' bgcolor='#CCCCCC'>");
                perdas.Append("<tr class='topo'><td width='429'><b>Descrição</b></td>");
                perdas.Append("<td width='46'><center><b>Qtd.</b></center></td>");
                perdas.Append("<td width='100'><center><b>Acumulado do Mês(Qtd)</b></center></td>");
                perdas.Append("<td width='100'><center><b>Acumulado do Mês(R$)</b></center></td>");

                for (int i = 0; i <= lstPerdasdoDia.Count - 1; i++)
                {
                    //lstPItem[i].produto = pdao.getProduto(lstPItem[i].codpro);
                    perdas.Append("<tr bgcolor='#eeeee8'><td>" + lstPerdasdoDia[i].produto.descricao + "</td><td><center>" + lstPerdasdoDia[i].qtditens + "</center></td><td><center>" + lstPerdasdoDia[i].numdoc + "</center></td><td><center>" + lstPerdasdoDia[i].valitens.ToString("0.00") + "</center></td></tr>");
                    
                    iTotalqtd += lstPerdasdoDia[i].qtditens;
                    iTotalqtdAcumulado += lstPerdasdoDia[i].numdoc;
                    totalValor += lstPerdasdoDia[i].valitens;
                }
                perdas.Append("<tr bgcolor='#eeeee8' width='80'><td><b>Total</b></td><td><b><center>" + iTotalqtd + "</center></b></td><td><b><center>" + iTotalqtdAcumulado + "</center></b></td><td><b><center>" + totalValor.ToString("0.00") + "</center></b></td></tr>");
                    
                perdas.Append("</table><br />");



                perdas.Append("<table width='650' align='center' bgcolor='#e9e9e6'><tr><td height='30' colspan='3'><center><h3>Total Geral</h3></center></td></tr></table>");

                total.Append("<table width='650' class='produtos' align='center' bgcolor='#CCCCCC'>");
                total.Append("<tr class='topo'><td width='350'><b>Loja</b></td>");
                total.Append("<td width='100'><center><b>Vendas do Dia(R$)</b></center></td>");
                total.Append("<td width='100'><center><b>Acumulado do Mês(R$)</b></center></td>");

                total.Append("<tr bgcolor='#eeeee8'><td>Cafeteria</td><td><center>" + movimentacao.venda_dia_cafeteria.ToString("0.00") + "</center></td><td><center>" + movimentacao.venda_mes_cafeteria.ToString("0.00") + "</center></td></tr>");
                total.Append("<tr bgcolor='#eeeee8'><td>Livraria</td><td><center>" + movimentacao.venda_dia_livraria.ToString("0.00") + "</center></td><td><center>" + movimentacao.venda_mes_livraria.ToString("0.00") + "</center></td></tr>");
                total.Append("<tr bgcolor='#eeeee8'><td>Livraria Online</td><td><center>" + movimentacao.venda_dia_online.ToString("0.00") + "</center></td><td><center>" + movimentacao.venda_mes_online.ToString("0.00") + "</center></td></tr>");
                total.Append("</table><br />");



                cabecalho.Append(produto.Append(itens).Append(perdas).Append(total));



                var fromAddress = new MailAddress("programacao@impetus.com.br", "Livro Etc.");
                //var toAddress = new MailAddress("rodolphochagas@hotmail.com", "Rodolpho");
                const string fromPassword = "impetus456";
                const string subject = "Relatório de fechamento e conferência de caixa";
                string body = cabecalho.ToString();

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                    Timeout = 20000
                };

                using (var message = new MailMessage(){ Priority = MailPriority.High, IsBodyHtml = true, Subject = subject, Body = body })
                {
                    message.To.Add(new MailAddress("ingrid@impetus.com.br", "Ingrid"));
                    message.To.Add(new MailAddress("gerencia@livrariaconcursar.com.br", "Hugo"));
                    //message.To.Add(new MailAddress("rodolphochagas@hotmail.com", "Rodolpho"));
                    message.From = fromAddress;

                    smtp.Send(message);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cabecalho = null;
                produto = null;
                itens = null;
                total = null;
                mail = null;
            }
        }






        //*************************************************************************************************************************************
        //private string para;
        //private string assunto;
        //private string mensagem;

        /*
        /// <summary>
        /// Envia o email para o Cliente.
        /// </summary>
        public bool EnviaEmailNFe(Pedido p)
        {

            if (string.IsNullOrEmpty(p.cliente.email))
            {
                return false;
            }

            try
            {  
                Outlook._Application oApp = new Outlook.Application();

                // Create a new MailItem.
                Outlook._MailItem oMsg = oApp.CreateItem(Outlook.OlItemType.olMailItem);

                Outlook.Attachments oAttachs = oMsg.Attachments;


                para = p.cliente.email;
                //para = "programacao@impetus.com.br";


                if (Usuario.getInstance.setor != "Divulgacao")
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

                    //oAttachs.Add(@"C:\Documents and Settings\Renan\Desktop\new20Assinada.xml");
                    oAttachs.Add(@"\\Server\Dados\ZNFe\Impetus\Comercial\Retorno\Saidas\" + DateTime.Now.Year + @"\" + DateTime.Now.Month + @"\" + p.chave + "-procNfe.xml");
                }
                else
                {

                    assunto = "Comunicado de envio - Editora Impetus";
                    string endereco = p.cliente.end.logradouro + ", " + p.cliente.end.numero + " - " + p.cliente.end.complemento + " - " + p.cliente.end.bairro + " - " + p.cliente.end.municipio + " - " + p.cliente.end.uf;
                    string produto = "<ul>";

                    for (int i = 0; i <= p.lstPedidoItem.Count - 1; i++)
                    {
                        produto = produto + "<li>" + p.lstPedidoItem[i].produto.descricao + "</li>";
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



                oMsg.Subject = assunto;
                oMsg.HTMLBody = mensagem;
                oMsg.To = para;
                oMsg.BCC = "";
                oMsg.Importance = Outlook.OlImportance.olImportanceHigh;


                // Enviando
                oMsg.Send();

                oApp = null;
                oMsg = null;
                oAttachs = null;

            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        //*************************************************************************************************************************************
        */
    }
}
