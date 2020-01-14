using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetoPDVModelos;

namespace ProjetoPDVDao
{
    public class TipoPagamentoDAO
    {
        
        public TipoPagamento getTipoPagamento(int codTipoPgto)
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).SingleOrDefault<TipoPagamento>("SELECT * FROM TipoPgto WHERE CodTipoPGTO=@0", codTipoPgto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TipoPagamento getTipoPagamento(string numDoc)
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).SingleOrDefault<TipoPagamento>("SELECT * FROM TipoPgto tp INNER JOIN Movdb m ON tp.CodTipoPGTO = m.TipoPGTO WHERE numdoc =@0", numDoc);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<TipoPagamento> getlstTipoPagamento()
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).Query<TipoPagamento>("SELECT * FROM TipoPgto WHERE is_available = 1 order by DescTipoPgto").ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    
        ~TipoPagamentoDAO()
        {
        }
    
    
    }
}
