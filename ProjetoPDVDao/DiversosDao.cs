using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetoPDVModelos;

namespace ProjetoPDVDao
{
    public class DiversosDao
    {

        /// <summary>
        /// Retorna todas as observações feitas no pedido.
        /// </summary>
        public string getDiversos(int numDoc)
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).SingleOrDefault<string>("SELECT MSG1 + ' - ' + MSG2 + ' - ' MSG3 FROM Diversos WHERE NumDoc=@0", numDoc);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
