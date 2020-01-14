using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetoPDVModelos;

namespace ProjetoPDVDao
{
    public class OperacaoDao
    {
        ~OperacaoDao() { }

        public Operacao getOperacaoPedido(int numDoc) 
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).SingleOrDefault<Operacao>("SELECT Operacao.* FROM Operacao,Movdb WHERE Operacao.CodOperacao = Movdb.CodOperacao AND NumDoc=@0", numDoc);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Operacao getOperacao(int codOperacao) 
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).SingleOrDefault<Operacao>("SELECT * FROM Operacao WHERE CodOperacao=@0", codOperacao);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
