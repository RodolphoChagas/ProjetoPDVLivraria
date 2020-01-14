using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetoPDVModelos;

namespace ProjetoPDVDao
{
    public class BoletaDao
    {
        ~BoletaDao(){}

        public object InsertBoleta(Boleta boleta)
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).Insert(boleta);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateBoleta(Boleta boleta)
        {
            try
            {
                (new PetaPoco.Database("stringConexao")).Update(boleta);
            }
            catch (Exception)
            {
                throw;
            }
        }



        public bool VerificaBoleta(int NumDoc, int NumPagto)
        {
            bool t = false;

            try
            {
                t = (new PetaPoco.Database("stringConexao")).SingleOrDefault<bool>("select numdoc from Boleta where NumDoc=@0 And NumPgto=@1 And NumSeq = 1", NumDoc, NumPagto);
            }
            catch (Exception)
            {
                throw;
            }
            
            return false;
        }





    }
}
