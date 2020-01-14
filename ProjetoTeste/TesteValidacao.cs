using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml;
using System.IO;
using ProjetoPDVModelos;
using ProjetoPDVDao;
using ProjetoPDVServico;
using System.Net.Mail;
using System.Text;
using PetaPoco;
using System.Net;


namespace ProjetoTeste
{
    class TesteValidacao
    {
        static void Main(string[] args)
        {

            Database db = new Database("stringConexao");
            db.BeginTransaction();
            


            StringBuilder cabecalho = new StringBuilder();
            StringBuilder produto = new StringBuilder();
            StringBuilder itens = new StringBuilder();
            StringBuilder total = new StringBuilder();
            


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
            
            List<PedidoItem> lstPItem = new List<PedidoItem>();
            ProdutoDao pdao = new ProdutoDao();

            
            
            
            lstPItem = (new PedidoItemDao()).getlst_Itens_AcumuladodoMes(DateTime.Now);




            for (int i = 0; i <= lstPItem.Count - 1; i++)
            {
                lstPItem[i].produto = pdao.getProduto(lstPItem[i].codpro);

                itens.Append("<tr bgcolor='#eeeee8'><td>" + lstPItem[i].produto.descricao + "</td><td><center>" + lstPItem[i].qtditens + "</center></td><td><center>" + lstPItem[i].numdoc + "</center></td><td><center>" + lstPItem[i].valitens.ToString("0.00") + "</center></td></tr>");
                //Itens = Itens + "<tr bgcolor='#eeeee8'><td>" & rstItens!Descricao & "</td><td><center>" & tipoDireito & "</center></td><td><center>" & rstItens!QtdVendida & "</center></td>" & IIf(rstAutor!dataven = 30, "<td><center>" & StrConv(Format(CDate("01/" & rstItens!Mes & "/2000"), "mmmm"), vbProperCase) & "</center></td>", "") & "<td><center>" & rstItens!PercDireito & "</center></td><td><center>" & Format(rstItens!valorvendido, "##,##0.00") & "</center></td></tr>"
            }
            itens.Append("</table><br />");




            Movimentacao mov = new Movimentacao();
            mov = (new MovimentacaoDao()).getTotal_de_Vendas(DateTime.Now);


            total.Append("<table width='650' class='produtos' align='center' bgcolor='#CCCCCC'>");
            total.Append("<tr class='topo'><td width='350'><b>Loja</b></td>");
            total.Append("<td width='100'><center><b>Vendas do Dia(R$)</b></center></td>");
            total.Append("<td width='100'><center><b>Acumulado do Mês(R$)</b></center></td>");

            total.Append("<tr bgcolor='#eeeee8'><td>Cafeteria</td><td><center>" + mov.venda_dia_cafeteria.ToString("0.00") + "</center></td><td><center>" + mov.venda_mes_cafeteria.ToString("0.00") + "</center></td></tr>");
            total.Append("<tr bgcolor='#eeeee8'><td>Livraria</td><td><center>" + mov.venda_dia_livraria.ToString("0.00") + "</center></td><td><center>" + mov.venda_mes_livraria.ToString("0.00") + "</center></td></tr>");
            total.Append("<tr bgcolor='#eeeee8'><td>Livraria Online</td><td><center>" + mov.venda_dia_online.ToString("0.00") + "</center></td><td><center>" + mov.venda_mes_online.ToString("0.00") + "</center></td></tr>");
            total.Append("</table><br />");


            cabecalho.Append(produto.Append(itens).Append(total));


            var fromAddress = new MailAddress("programacao@impetus.com.br", "Livro Etc.");
            //var toAddress = new MailAddress("rodolphochagas@hotmail.com", "Rodolpho");
            const string fromPassword = "impetus456";
            const string subject = "Relatório de fechamento e conferência de caixa - Livro Etc";
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


            using (var message = new MailMessage()
            {
                Priority = MailPriority.High,
                IsBodyHtml = true,
                Subject = subject,
                Body = body
            })
            {
                message.To.Add(new MailAddress("rodolphochagas@hotmail.com", "Rodolpho"));
                message.To.Add(new MailAddress("rodolphohasselmachado@gmail.com", "Rodolpho"));
                message.From = fromAddress;


                smtp.Send(message);
            }




            //using (var message = new MailMessage(fromAddress, toAddress)
            //{
            //    IsBodyHtml = true,
            //    Subject = subject,
            //    Body = body
            //})
            //{
            //    smtp.Send(message);
            //}


            //System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            //client.Host = "smtp.gmail.com";
            //client.EnableSsl = true;
            //client.Credentials = new System.Net.NetworkCredential("programacao@impetus.com.br", "impetus456");
            //MailMessage mail = new MailMessage();
            //mail.Sender = new System.Net.Mail.MailAddress("programacao@impetus.com.br", "ENVIADOR");
            //mail.From = new MailAddress("programacao@impetus.com.br", "ENVIADOR");
            //mail.To.Add(new MailAddress("rodolphochagas@hotmail.com", "RECEBEDOR"));
            //mail.Subject = "Relatório de fechamento e conferência de caixa - Livro Etc";
            ////mail.Body = cabecalho.ToString();
            //mail.Body = "gdgdgdgd";
            //mail.IsBodyHtml = true;
            //mail.Priority = MailPriority.High;
            //ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;



            //try
            //{
            //    client.Send(mail);
            //}
            //catch (Exception erro)
            //{
            //    //trata erro
            //}
            //finally
            //{
            //    mail = null;
            //}



        }
    }
}
