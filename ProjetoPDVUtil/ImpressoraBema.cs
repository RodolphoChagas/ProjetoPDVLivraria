using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetoPDVModelos;
using ProjetoPDVServico;
using ProjetoPDVDao;

namespace ProjetoPDVUtil
{
    public class ImpressoraBema
    {
        private string Chr(int asc)
        {
            string ret = "";
            ret += (char)asc;
            return ret;
        }

        public static void Aciona_Gaveta()
        {
            int charCode = 27;
            int charCode2 = 118;
            int charCode3 = 140;
            Char specialChar = Convert.ToChar(charCode);
            Char specialChar2 = Convert.ToChar(charCode2);
            Char specialChar3 = Convert.ToChar(charCode3);

            string s_cmdTX = "" + specialChar + specialChar2 + specialChar3;
            MP2032.ComandoTX(s_cmdTX, s_cmdTX.Length);
        }

        public static void GeraDANFE_NFCe(Pedido p, string urlQRCode, decimal valPago_em_Dinheiro = 0)
        {

            decimal valDesc = 0;


            //CADA LINHA DO CUPOM CONTEM 50 COLUNAS COM LETRA NORMAL

            MP2032.BematechTX("\x1B\x61\x1"); //Centraliza

            //Informações do Cabeçalho
            MP2032.FormataTX(Emitente.getInstance.nome + "\n", 2, 0, 0, 0, 1);
            MP2032.FormataTX("CNPJ " + String.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToInt64(Emitente.getInstance.cnpj)) + "\n", 2, 0, 0, 0, 0);
            MP2032.FormataTX("Av. Amaral Peixoto, 507 - LJ05, Centro, Niterói-RJ" + "\n", 2, 0, 0, 0, 0);
            MP2032.FormataTX("\n", 2, 0, 0, 0, 0);
            MP2032.FormataTX("Documento Auxiliar da Nota Fiscal de Consumidor Eletronica" + "\n", 1, 0, 0, 0, 0);
            MP2032.FormataTX("\n", 2, 0, 0, 0, 0);

            //Informações de detalhes de produtos/serviços
            MP2032.FormataTX("Codigo  Descricao        Qtd Un   Vl.Unit    Total" + "\n", 2, 0, 0, 0, 0);
            MP2032.FormataTX("-------------------------------------------------------------------", 1, 0, 0, 0, 0);

            string strCodpro = string.Empty;
            string strDescricao = string.Empty;
            int totalItens = 0;
            for (int i = 0; i <= p.lstPedidoItem.Count - 1; i++)
            {

                if (p.lstPedidoItem[i].produto.produto_loja.desconto > 0)// || p.lstPedidoItem[i].valitens > 0)
                {
                    //valDesc += (p.lstPedidoItem[i].prcitens * p.lstPedidoItem[i].qtditens) - p.lstPedidoItem[i].valitens;
                    //valDesc += p.lstPedidoItem[i].valDesc;
                    valDesc += decimal.Round(((p.lstPedidoItem[i].produto.prcvenda * p.lstPedidoItem[i].qtditens) - p.lstPedidoItem[i].valitens), 2);
                }


                strCodpro = p.lstPedidoItem[i].codpro.ToString("00000");
                totalItens += 1;

                if (p.lstPedidoItem[i].produto.descricao.Length > 16)
                {
                    strDescricao = StringUtil.RemoverAcentos(p.lstPedidoItem[i].produto.descricao.Substring(0, 17));
                }
                else
                    strDescricao = StringUtil.RemoverAcentos(p.lstPedidoItem[i].produto.descricao.PadLeft(17, ' '));

                MP2032.FormataTX(strCodpro + "   " + strDescricao.PadRight(17, ' ') +
                                 p.lstPedidoItem[i].qtditens.ToString().PadLeft(3) + " UN" +
                                 p.lstPedidoItem[i].prcitens.ToString("######0.00").Replace(".", ",").PadLeft(10, ' ') +
                                 p.lstPedidoItem[i].valitens.ToString("######0.00").Replace(".", ",").PadLeft(9, ' '), 2, 0, 0, 0, 0);
            }
            //valDesc += p.dscdoc;


            MP2032.FormataTX("-------------------------------------------------------------------", 1, 0, 0, 0, 0);

            //Informações de Totais do DANFE NFC-e
            MP2032.FormataTX("QTD. TOTAL DE ITENS" + totalItens.ToString().PadLeft(31, ' '), 2, 0, 0, 0, 1);
            MP2032.FormataTX("VALOR TOTAL R$" + p.valdoc.ToString("######0.00").Replace(".", ",").PadLeft(36, ' '), 2, 0, 0, 0, 1);

            //Desconto
            if (valDesc > 0)
                MP2032.FormataTX("DESCONTO R$" + valDesc.ToString("######0.00").Replace(".", ",").PadLeft(39, ' '), 2, 0, 0, 0, 1);



            MP2032.FormataTX("FORMA DE PAGAMENTO" + "VALOR PAGO R$".PadLeft(32, ' '), 2, 0, 0, 0, 1);


            if (p.tipoPgto.descTipoPgto == "Dinheiro")
            {
                if (valPago_em_Dinheiro > p.valdoc)
                {
                    MP2032.FormataTX("Dinheiro".PadRight(17, ' ') + valPago_em_Dinheiro.ToString("0.00").PadLeft(33, ' '), 2, 0, 0, 0, 1);
                    MP2032.FormataTX("Troco".PadRight(17, ' ') + (valPago_em_Dinheiro - p.valdoc).ToString().PadLeft(33, ' '), 2, 0, 0, 0, 1);
                }
                else
                    MP2032.FormataTX("Dinheiro".PadRight(17, ' ') + p.valdoc.ToString("0.00").PadLeft(33, ' '), 2, 0, 0, 0, 1);
            }
            else if (p.tipoPgto.maximum_number_of_plots == 1)
            {
                MP2032.FormataTX("Cartao de Debito".PadRight(17, ' ') + p.valdoc.ToString("0.00").PadLeft(33, ' '), 2, 0, 0, 0, 1);
            }
            else
                MP2032.FormataTX("Cartao de Credito".PadRight(17, ' ') + p.valdoc.ToString("0.00").PadLeft(33, ' '), 2, 0, 0, 0, 1);

            MP2032.FormataTX("-------------------------------------------------------------------", 1, 0, 0, 0, 0);



            //Informações da consulta via chave de acesso
            MP2032.FormataTX("Consulte pela chave de acesso em" + "\n", 2, 0, 0, 0, 0);
            MP2032.FormataTX("http://nfce.fazenda.rj.gov.br/consulta" + "\n", 2, 0, 0, 0, 0);

            //* chave de acesso impressa em 11 blocos de quatro dígitos, com um espaço entre cada bloco
            int count = 0;
            string chavefinal = string.Empty;

            for (int x = 1; x <= 11; x++)
            {
                chavefinal += p.chave.Substring(count, 4).PadLeft(5, ' ');
                count += 4;
            }
            MP2032.FormataTX(chavefinal.Trim() + "\n", 1, 0, 0, 0, 0);


            var identificaConsumidor = string.Empty;

            if (p.cliente.firma.Trim() == "CONSUMIDOR NÃO IDENTIFICADO")
                identificaConsumidor = "CONSUMIDOR NÃO IDENTIFICADO";
            else if ((p.cliente.cgc ?? "").Trim().Length > 11)
                identificaConsumidor = "CNPJ: " + p.cliente.cgc.Trim();
            else
                identificaConsumidor = "CPF: " + p.cliente.cpf.Trim();



            //Informações sobre o Consumidor
            MP2032.FormataTX("-------------------------------------------------------------------", 1, 0, 0, 0, 0);
            //MP2032.FormataTX((p.cliente.firma.Trim() == "CONSUMIDOR NÃO IDENTIFICADO" ? p.cliente.firma : "CPF: " + p.cliente.cpf.Trim()) + "\n", 2, 0, 0, 0, 1);
            MP2032.FormataTX(identificaConsumidor + "\n", 2, 0, 0, 0, 1);
            MP2032.FormataTX("-------------------------------------------------------------------", 1, 0, 0, 0, 0);

            //Informações de Identificação da NFC-e e do Protocolo de Autorização
            MP2032.FormataTX("NFC-e " + p.nfiscal.PadLeft(7, '0') + " Serie: 002 " + p.datanfiscal.ToString("dd/MM/yyyy HH:mm:ss") + "\n", 2, 0, 0, 0, 0);
            MP2032.FormataTX("Protocolo de Autorizacao: " + p.protocolo + "\n", 2, 0, 0, 0, 0);

            //Informações da consulta via QR Code
            //MP2032.ImprimeCodigoQRCODE(1, 5, 0, 10, 1, "http://www4.fazenda.rj.gov.br/consultaNFCe/QRCode?chNFe=33171111500080000160650010000000301581809781&nVersao=100&tpAmb=2&dhEmi=323031372D31312D31345431313A30363A35342D30323A3030&vNF=151.92&vICMS=0.00&digVal=6B56723030544A4A59634F36534231595A62626D4C4230673561343D&cIdToken=000001&cHashQRCode=8576B480E0BDFFA96A064F65DBC977DCE0FB91B8");
            MP2032.ImprimeCodigoQRCODE(1, 5, 0, 10, 1, urlQRCode);

            //Área de Mensagem Fiscal
            MP2032.FormataTX("\n", 2, 0, 0, 0, 0);
            MP2032.FormataTX("Volte sempre!", 2, 0, 0, 0, 0);

            //Corta o papel parcialmente
            MP2032.AcionaGuilhotina(0);
            MP2032.FechaPorta();
        }


        public static void GeraDANFE_Cupom(Pedido p, decimal valPago_em_Dinheiro = 0)
        {
            decimal valDesc = 0;

            //CADA LINHA DO CUPOM CONTEM 50 COLUNAS COM LETRA NORMAL

            MP2032.BematechTX("\x1B\x61\x1"); //Centraliza

            //Informações do Cabeçalho
            MP2032.FormataTX(Emitente.getInstance.nome + "\n", 2, 0, 0, 0, 1);
            MP2032.FormataTX("CNPJ " + String.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToInt64(Emitente.getInstance.cnpj)) + "\n", 2, 0, 0, 0, 0);
            MP2032.FormataTX("Av. Amaral Peixoto, 507 - LJ05, Centro, Niterói-RJ" + "\n", 2, 0, 0, 0, 0);
            MP2032.FormataTX("\n", 2, 0, 0, 0, 0);
            //MP2032.FormataTX("Documento Auxiliar da Nota Fiscal de Consumidor Eletronica" + "\n", 1, 0, 0, 0, 0);
            MP2032.FormataTX("CUPOM  NAO  FISCAL" + "\n", 2, 0, 0, 0, 0);
            MP2032.FormataTX("\n", 2, 0, 0, 0, 0);

            //Informações de detalhes de produtos/serviços
            MP2032.FormataTX("Codigo  Descricao        Qtd Un   Vl.Unit    Total" + "\n", 2, 0, 0, 0, 0);
            MP2032.FormataTX("-------------------------------------------------------------------", 1, 0, 0, 0, 0);

            string strCodpro = string.Empty;
            string strDescricao = string.Empty;
            int totalItens = 0;
            for (int i = 0; i <= p.lstPedidoItem.Count - 1; i++)
            {

                if (Usuario.getInstance.loja.Equals(0))
                {
                    if (p.lstPedidoItem[i].produto.produto_loja.desconto > 0 || p.lstPedidoItem[i].valitens > 0)
                    {
                        //valDesc += (p.lstPedidoItem[i].prcitens * p.lstPedidoItem[i].qtditens) - p.lstPedidoItem[i].valitens;
                        //valDesc += p.lstPedidoItem[i].valDesc;
                        valDesc += decimal.Round(((p.lstPedidoItem[i].produto.prcvenda * p.lstPedidoItem[i].qtditens) - p.lstPedidoItem[i].valitens), 2);
                    }
                }

                strCodpro = p.lstPedidoItem[i].codpro.ToString("00000");
                totalItens += 1;

                if (p.lstPedidoItem[i].produto.descricao.Length > 16)
                {
                    strDescricao = StringUtil.RemoverAcentos(p.lstPedidoItem[i].produto.descricao.Substring(0, 17));
                }
                else
                    strDescricao = StringUtil.RemoverAcentos(p.lstPedidoItem[i].produto.descricao.PadLeft(17, ' '));

                MP2032.FormataTX(strCodpro + "   " + strDescricao.PadRight(17, ' ') +
                                 p.lstPedidoItem[i].qtditens.ToString().PadLeft(3) + " UN" +
                                 p.lstPedidoItem[i].prcitens.ToString("######0.00").Replace(".", ",").PadLeft(10, ' ') +
                                 p.lstPedidoItem[i].valitens.ToString("######0.00").Replace(".", ",").PadLeft(9, ' '), 2, 0, 0, 0, 0);
            }
            //valDesc += p.dscdoc;


            MP2032.FormataTX("-------------------------------------------------------------------", 1, 0, 0, 0, 0);


            //Informações de Totais do DANFE NFC-e
            MP2032.FormataTX("QTD. TOTAL DE ITENS" + totalItens.ToString().PadLeft(31, ' '), 2, 0, 0, 0, 1);
            MP2032.FormataTX("VALOR TOTAL R$" + p.valdoc.ToString("######0.00").Replace(".", ",").PadLeft(36, ' '), 2, 0, 0, 0, 1);


            //DESCONTO
            if (valDesc > 0)
                MP2032.FormataTX("DESCONTO R$" + valDesc.ToString("######0.00").Replace(".", ",").PadLeft(39, ' '), 2, 0, 0, 0, 1);



            MP2032.FormataTX("FORMA DE PAGAMENTO" + "VALOR PAGO R$".PadLeft(32, ' '), 2, 0, 0, 0, 1);


            if (p.tipoPgto.descTipoPgto == "Dinheiro")
            {
                if (valPago_em_Dinheiro > p.valdoc)
                {
                    MP2032.FormataTX("Dinheiro".PadRight(17, ' ') + valPago_em_Dinheiro.ToString("0.00").PadLeft(33, ' '), 2, 0, 0, 0, 1);
                    MP2032.FormataTX("Troco".PadRight(17, ' ') + (valPago_em_Dinheiro - p.valdoc).ToString().PadLeft(33, ' '), 2, 0, 0, 0, 1);
                }
                else
                    MP2032.FormataTX("Dinheiro".PadRight(17, ' ') + p.valdoc.ToString("0.00").PadLeft(33, ' '), 2, 0, 0, 0, 1);
            }
            else if (p.tipoPgto.maximum_number_of_plots == 1)
            {
                MP2032.FormataTX("Cartao de Debito".PadRight(17, ' ') + p.valdoc.ToString("0.00").PadLeft(33, ' '), 2, 0, 0, 0, 1);
            }
            else
                MP2032.FormataTX("Cartao de Credito".PadRight(17, ' ') + p.valdoc.ToString("0.00").PadLeft(33, ' '), 2, 0, 0, 0, 1);

            MP2032.FormataTX("-------------------------------------------------------------------", 1, 0, 0, 0, 0);

            /*

            //Informações da consulta via chave de acesso
            MP2032.FormataTX("Consulte pela chave de acesso em" + "\n", 2, 0, 0, 0, 0);
            MP2032.FormataTX("http://nfce.fazenda.rj.gov.br/consulta" + "\n", 2, 0, 0, 0, 0);

            //* chave de acesso impressa em 11 blocos de quatro dígitos, com um espaço entre cada bloco
            int count = 0;
            string chavefinal = string.Empty;

            for (int x = 1; x <= 11; x++)
            {
                chavefinal += p.chave.Substring(count, 4).PadLeft(5, ' ');
                count += 4;
            }
            MP2032.FormataTX(chavefinal.Trim() + "\n", 1, 0, 0, 0, 0);


            //Informações sobre o Consumidor
            MP2032.FormataTX("-------------------------------------------------------------------", 1, 0, 0, 0, 0);
            MP2032.FormataTX((p.cliente.firma.Trim() == "CONSUMIDOR NÃO IDENTIFICADO" ? p.cliente.firma : "CPF: " + p.cliente.cpf.Trim()) + "\n", 2, 0, 0, 0, 1);
            MP2032.FormataTX("-------------------------------------------------------------------", 1, 0, 0, 0, 0);

            //Informações de Identificação da NFC-e e do Protocolo de Autorização
            MP2032.FormataTX("NFC-e " + p.nfiscal.PadLeft(7, '0') + " Serie: 002 " + p.datanfiscal.ToString("dd/MM/yyyy HH:mm:ss") + "\n", 2, 0, 0, 0, 0);
            MP2032.FormataTX("Protocolo de Autorizacao: " + p.protocolo + "\n", 2, 0, 0, 0, 0);

            //Informações da consulta via QR Code
            MP2032.ImprimeCodigoQRCODE(1, 5, 0, 10, 1, "http://www4.fazenda.rj.gov.br/consultaNFCe/QRCode?chNFe=33171111500080000160650010000000301581809781&nVersao=100&tpAmb=2&dhEmi=323031372D31312D31345431313A30363A35342D30323A3030&vNF=151.92&vICMS=0.00&digVal=6B56723030544A4A59634F36534231595A62626D4C4230673561343D&cIdToken=000001&cHashQRCode=8576B480E0BDFFA96A064F65DBC977DCE0FB91B8");

             * */

            //Área de Mensagem Fiscal
            MP2032.FormataTX("\n", 2, 0, 0, 0, 0);
            MP2032.FormataTX("Volte sempre!", 2, 0, 0, 0, 0);

            //Corta o papel parcialmente
            MP2032.AcionaGuilhotina(0);
            MP2032.FechaPorta();
        }
    }
}
