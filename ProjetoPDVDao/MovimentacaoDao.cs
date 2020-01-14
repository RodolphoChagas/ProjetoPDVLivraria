using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetoPDVModelos;

namespace ProjetoPDVDao
{
    public class MovimentacaoDao
    {


        public Movimentacao getTotal_de_Vendas(DateTime data)
        {
            try
            {

                string strAno = data.Year.ToString();
                string strMes = data.Month.ToString();
                string strDia = data.Day.ToString();

                string queryDia = "(YEAR(datanfiscal) = '" + strAno + "' and MONTH(datanfiscal) = '" + strMes + "' and DAY(datanfiscal) = '" + strDia + "') ";
                string queryMes = "(YEAR(datanfiscal) = '" + strAno + "' and MONTH(datanfiscal) = '" + strMes + "') ";


                return (new PetaPoco.Database("stringConexao")).SingleOrDefault<Movimentacao>("select sum(valitens) as Venda_dia_Cafeteria, " +
                                                                                              "(select sum(valitens) as Venda_dia_Cafeteria FROM Movdb INNER JOIN Movitens mi ON Movdb.NumDoc = mi.NumDoc INNER JOIN Produto ON mi.CodPro = Produto.CodPro " +
                                                                                              "WHERE (TipoProduto = 4 AND StatNFce = '100' AND Modelo = '65' AND CondDoc = 'F' AND NFiscal <> '0') AND " + queryMes +
                                                                                              ") as Venda_Mes_Cafeteria, " +
                                                                                              "(select sum(valitens) as Venda_dia_Livraria FROM Movdb INNER JOIN Movitens mi ON Movdb.NumDoc = mi.NumDoc INNER JOIN Produto ON mi.CodPro = Produto.CodPro " +
                                                                                              "WHERE (TipoProduto = 0 AND StatNFce = '100' AND Modelo = '65' AND CondDoc = 'F' AND NFiscal <> '0') AND " + queryDia +
                                                                                              ") as Venda_dia_Livraria, " +
                                                                                              "(select sum(valitens) as Venda_dia_Livraria FROM Movdb INNER JOIN Movitens mi ON Movdb.NumDoc = mi.NumDoc INNER JOIN Produto ON mi.CodPro = Produto.CodPro " +
                                                                                              "WHERE (TipoProduto = 0 AND StatNFce = '100' AND Modelo = '65' AND CondDoc = 'F' AND NFiscal <> '0') AND " + queryMes +
                                                                                              ") as Venda_Mes_Livraria, " +
                                                                                              "(select sum(valitens*vnd) as Venda_dia_Online  " +
                                                                                              "from movdb inner join operacao on movdb.codoperacao = operacao.codoperacao " +
                                                                                              "inner join movitens on movdb.numdoc = movitens.numdoc " +
                                                                                              "where vnd <> 0 and modelo = '55' and conddoc = 'F' and nfiscal <> '0' and " + queryDia + ") as Venda_dia_Online, " +
                                                                                              "(select sum(valitens*vnd) as Venda_dia_Online  " +
                                                                                              "from movdb inner join operacao on movdb.codoperacao = operacao.codoperacao " +
                                                                                              "inner join movitens on movdb.numdoc = movitens.numdoc " +
                                                                                              "where vnd <> 0 and modelo = '55' and conddoc = 'F' and nfiscal <> '0' and " + queryMes + ") as Venda_Mes_Online " +
                                                                                              "FROM Movdb INNER JOIN Movitens mi ON Movdb.NumDoc = mi.NumDoc INNER JOIN Produto ON mi.CodPro = Produto.CodPro " +
                                                                                              "WHERE (TipoProduto = 4 AND StatNFce = '100' AND Modelo = '65' AND CondDoc = 'F' AND NFiscal <> '0') AND " + queryDia);
            }
            catch (Exception)
            {

                throw;
            }
        }




    }
}
