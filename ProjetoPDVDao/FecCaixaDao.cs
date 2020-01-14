using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetoPDVModelos;

namespace ProjetoPDVDao
{
    public class FecCaixaDao
    {

        public bool Update(FecCaixa fecCaixa)
        {
            try
            {
                new PetaPoco.Database("stringConexao").Update(fecCaixa, fecCaixa.id);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }


        public bool InsertFecCaixa(FecCaixa fecCaixa)
        {
            try
            {
                //return (new PetaPoco.Database("stringConexao")).Insert("Movdb", "NumDoc", true, pedido);
                (new PetaPoco.Database("stringConexao")).Insert(fecCaixa);

                return true;
            }
            catch (Exception)
            {
                throw;
            }

            return false;
        }


        public FecCaixa getFecCaixa_do_Dia_User(int loja)
        {
            try
            {
                DateTime dt = DateTime.Now;
                
                string strAno = dt.Year.ToString();
                string strMes = dt.Month.ToString();
                string strDia = dt.Day.ToString();


                string query = "(YEAR(data) = '" + strAno + "' and MONTH(data) = '" + strMes + "' and DAY(data) = '" + strDia + "')";

                return (new PetaPoco.Database("stringConexao")).SingleOrDefault<FecCaixa>("select * from feccaixapdv where coduser = " + Usuario.getInstance.codUser + " and loja = " + Usuario.getInstance.loja + " and " + query);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public FecCaixa getFecCaixa_do_Dia_F()
        {
            try
            {
                DateTime dt = DateTime.Now;
                int icodVendedor = 0;

                string strAno = dt.Year.ToString();
                string strMes = dt.Month.ToString();
                string strDia = dt.Day.ToString();


                //if (loja == 0)
                //{
                //    icodVendedor = 104; // livraria
                //}
                //else
                //{
                //    icodVendedor = 105; // cafeteria
                //}

                var query = "(YEAR(datadigitacao) = '" + strAno + "' and MONTH(datadigitacao) = '" + strMes + "' and DAY(datadigitacao) = '" + strDia + "')";

                var sql = "select sum(valdoc) as val_dinheirof, (select sum(valdoc) from movdb inner join tipopgto on movdb.tipopgto = tipopgto.codtipopgto where coduser = " + Usuario.getInstance.codUser + " and codformapgtonfce = 4 and modelo = '65' and conddoc = 'F' and " + query + ") as val_debitof, (select sum(valdoc) from movdb inner join tipopgto on movdb.tipopgto = tipopgto.codtipopgto where coduser = " + Usuario.getInstance.codUser + " and codformapgtonfce = 3 and modelo = '65' and conddoc = 'F' and " + query + ") as val_creditof, (select sum(valdoc) from movdb inner join tipopgto on movdb.tipopgto = tipopgto.codtipopgto where coduser = " + Usuario.getInstance.codUser + " and codformapgtonfce = 99 and modelo = '65' and nfiscal <> '0' and conddoc = 'F' and " + query + ") as val_outrosf, (Select sum(ValItens) as Devolucao from movdb inner join movitens on movdb.numdoc = movitens.numdoc where CodOperacao = 800 and CodVendedor in(104,105) and nfiscal <> '0' and CondDoc = 'F' and " + query + ") as Devolucao from movdb inner join tipopgto on movdb.tipopgto = tipopgto.codtipopgto where coduser = " + Usuario.getInstance.codUser + " and codtipopgto = 9 and modelo = '65' and conddoc = 'F' and " + query;

                return (new PetaPoco.Database("stringConexao")).SingleOrDefault<FecCaixa>(sql);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int get_id_FecCaixa_do_Dia_User(int loja)
        {
            try
            {
                DateTime dt = DateTime.Now;

                string strAno = dt.Year.ToString();
                string strMes = dt.Month.ToString();
                string strDia = dt.Day.ToString();

                string query = "(YEAR(data) = '" + strAno + "' and MONTH(data) = '" + strMes + "' and DAY(data) = '" + strDia + "')";

                return (new PetaPoco.Database("stringConexao")).SingleOrDefault<int>("select id from feccaixapdv where coduser = " + Usuario.getInstance.codUser + " and loja = " + Usuario.getInstance.loja + " and " + query);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}