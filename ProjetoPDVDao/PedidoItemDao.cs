using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetoPDVModelos;

namespace ProjetoPDVDao
{
    public class PedidoItemDao
    {

        ~PedidoItemDao(){}

        /// <summary>
        /// Retorna uma lista com todos os itens do pedido passado por parametro.
        /// </summary>
        /// <param name="numDoc"></param>
        /// <returns></returns>
        public List<PedidoItem> getlst_Itens_PerdasdoDia(DateTime data)
        {
            try
            {
                string strAno = data.Year.ToString();
                string strMes = data.Month.ToString();
                string strDia = data.Day.ToString();

                string queryDia = "(YEAR(datadigitacao) = '" + strAno + "' and MONTH(datadigitacao) = '" + strMes + "' and DAY(datadigitacao) = '" + strDia + "') ";
                string queryMes = "(YEAR(datadigitacao) = '" + strAno + "' and MONTH(datadigitacao) = '" + strMes + "') ";





                return (new PetaPoco.Database("stringConexao")).Query<PedidoItem>("select mi.codpro, case when " +
                                                                                  "(select sum(qtditens) from movdb inner join movitens on movdb.numdoc = movitens.numdoc " +
                                                                                  "where codpro = mi.codpro and codoperacao = 905 and " + queryDia + ") is null then 0 " +
                                                                                  "else (select sum(qtditens) from movdb inner join movitens on movdb.numdoc = movitens.numdoc " +
                                                                                  "where codpro = mi.codpro and codoperacao = 905 and " + queryDia + ") " +
                                                                                  "end as qtditens, " +
                                                                                  "sum(qtditens) as numdoc, sum(prccusto*qtditens) as valitens " +
                                                                                  "from movdb inner join movitens mi on movdb.numdoc = mi.numdoc inner join produto on mi.codpro = produto.codpro where codoperacao = 905 and " + queryMes +
                                                                                  "group by mi.codpro").ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// Retorna uma lista com todos os itens do pedido passado por parametro.
        /// </summary>
        /// <param name="numDoc"></param>
        /// <returns></returns>
        public List<PedidoItem> getlst_Itens_AcumuladodoMes(DateTime data)
        {
            try
            {
                string strAno = data.Year.ToString();
                string strMes = data.Month.ToString();
                string strDia = data.Day.ToString();

                string queryDia = "(YEAR(datanfiscal) = '" + strAno + "' and MONTH(datanfiscal) = '" + strMes + "' and DAY(datanfiscal) = '" + strDia + "') ";
                string queryMes = "(YEAR(datanfiscal) = '" + strAno + "' and MONTH(datanfiscal) = '" + strMes + "') ";



                return (new PetaPoco.Database("stringConexao")).Query<PedidoItem>("SELECT mi.codpro, descricao, " +
                                                                                  "CASE WHEN (SELECT SUM(qtditens) " +
                                                                                  "FROM Movdb INNER JOIN Movitens ON Movdb.NumDoc = MovItens.NumDoc " +
                                                                                  "WHERE (Movitens.codpro = mi.codpro and StatNFce = '100' AND Modelo = '65' AND CondDoc = 'F' AND NFiscal <> '0') AND " + queryDia +
                                                                                  "group by mi.codpro) IS NULL THEN 0 ELSE (SELECT SUM(qtditens) " +
                                                                                  "FROM Movdb INNER JOIN Movitens ON Movdb.NumDoc = MovItens.NumDoc " +
                                                                                  "WHERE (Movitens.codpro = mi.codpro and StatNFce = '100' AND Modelo = '65' AND CondDoc = 'F' AND NFiscal <> '0') AND " + queryDia +
                                                                                  "group by mi.codpro) END AS qtditens, SUM(qtditens) as numdoc, SUM(valitens) as valitens " +
                                                                                  "FROM Movdb INNER JOIN Movitens mi ON Movdb.NumDoc = mi.NumDoc " +
                                                                                  "INNER JOIN Produto ON mi.CodPro = Produto.CodPro " +
                                                                                  "WHERE (TipoProduto = 4 AND StatNFce = '100' AND Modelo = '65' AND CondDoc = 'F' AND NFiscal <> '0') AND " + queryMes +
                                                                                  "group by descricao, mi.codpro " +
                                                                                  "order by descricao").ToList();

                //return (new PetaPoco.Database("stringConexao")).Query<PedidoItem>("SELECT mi.codpro, descricao, SUM(qtditens) AS qtditens," +
                //                                                                  "(SELECT SUM(qtditens) AS Acumulado_do_Mes " +
                //                                                                  "FROM Movdb INNER JOIN Movitens ON Movdb.NumDoc = MovItens.NumDoc " +
                //                                                                  "WHERE (Movitens.codpro = mi.codpro and StatNFce = '100' AND Modelo = '65' AND CondDoc = 'F' AND NFiscal <> '0') AND " + queryMes +
                //                                                                  "group by mi.codpro) as numdoc, " +
                //                                                                  "(SELECT SUM(valitens) AS Acumulado_do_Mes FROM Movdb INNER JOIN Movitens ON Movdb.NumDoc = MovItens.NumDoc " +
                //                                                                  "WHERE (Movitens.codpro = mi.codpro and StatNFce = '100' AND Modelo = '65' AND CondDoc = 'F' AND NFiscal <> '0') AND " + queryMes +
                //                                                                  "group by mi.codpro) as valitens " +
                //                                                                  "FROM Movdb INNER JOIN Movitens mi ON Movdb.NumDoc = mi.NumDoc " +
                //                                                                  "INNER JOIN Produto ON mi.CodPro = Produto.CodPro " +
                //                                                                  "WHERE (TipoProduto = 4 AND StatNFce = '100' AND Modelo = '65' AND CondDoc = 'F' AND NFiscal <> '0') AND " + queryDia +
                //                                                                  "group by descricao, mi.codpro " +
                //                                                                  "order by descricao").ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// Insere
        /// </summary>
        /// <param name="numDoc"></param>
        /// /// <param name="List<PedidoItem>"></param>
        /// <returns></returns>
        public bool Insert_lstPedidoItens(int numDoc, List<PedidoItem> lstPedidoItens)
        {
            bool t = false;

            try
            {
                (new PetaPoco.Database("stringConexao")).Insert_lstPedidos(numDoc, lstPedidoItens);
                t = true;
            }
            catch (Exception)
            {
                throw;
            }

            return t;
        }


        /// <summary>
        /// Retorna uma lista com todos os itens do pedido passado por parametro.
        /// </summary>
        /// <param name="numDoc"></param>
        /// <returns></returns>
        public List<PedidoItem> getlst_Itens(int numDoc)
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).Query<PedidoItem>("SELECT MI.* FROM MovItens MI Inner Join Produto P On MI.Codpro = P.Codpro WHERE NumDoc =@0 Order by Descricao", numDoc).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        
        /// <summary>
        /// Total de itens do pedido.
        /// </summary>
        public int getTotal_Itens(int numDoc)
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).SingleOrDefault<int>("SELECT COUNT(*) FROM MovItens WHERE NumDoc=@0", numDoc);
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Total de exemplares do pedido.
        /// </summary>
        public int getTotal_Exemplares(int numDoc)
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).SingleOrDefault<int>("SELECT SUM(QtdItens) FROM MovItens WHERE NumDoc=@0", numDoc);
            }
            catch (Exception)
            {

                throw;
            }

        }

        
        /// <summary>
        /// Valor total de descontos dos itens.
        /// </summary>
        public double getValorTotal_Dsc(int numDoc)
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).SingleOrDefault<double>("SELECT SUM(ROUND(MovItens.PrcItens * MovItens.DscItens / 100 * MovItens.QtdItens, 2)) AS TotDsc FROM MovItens Where MovItens.NumDoc =@0", numDoc);
            }
            catch (Exception)
            {

                throw;
            }

        }

        
        /// <summary>
        /// Valor total dos itens sem desconto.
        /// </summary>
        public double getValorTotal_Itens(int numDoc)
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).SingleOrDefault<double>("SELECT SUM(MovItens.PrcItens * MovItens.QtdItens) AS TotItem FROM MovItens Where MovItens.NumDoc =@0", numDoc);
            }
            catch (Exception)
            {

                throw;
            }

        }


    }
}
