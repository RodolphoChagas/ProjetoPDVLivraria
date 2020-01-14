using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetoPDVModelos;


namespace ProjetoPDVDao
{
    public class Produto_LojaDao
    {


        public Produto_Loja getProduto_Loja(int codPro)
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).SingleOrDefault<Produto_Loja>("select top 1 * from produto_loja where codloja = 1 and codpro =@0 order by id desc", codPro);
            }
            catch (Exception)
            {

                throw;
            }

        }


    }
}
