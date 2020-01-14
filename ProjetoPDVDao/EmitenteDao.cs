using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetoPDVModelos;

namespace ProjetoPDVDao
{
    public class EmitenteDao
    {
        public bool SelecionaEmitente()
        {
            Emitente em = (new PetaPoco.Database("stringConexao")).SingleOrDefault<Emitente>("SELECT CNPJ, [Razão Social] as nome, InscEst, [NomeFant] as nomefantasia FROM Controle");
            em.endereco = (new EnderecoDao()).getEnderecoEmitente();

            if (em != null)
            {
                Emitente.getInstance.cnpj = em.cnpj;
                Emitente.getInstance.inscest = em.inscest;
                Emitente.getInstance.endereco = em.endereco;
                Emitente.getInstance.nome = em.nome;
                Emitente.getInstance.nomefantasia = em.nomefantasia;

                em = null;

                return true;
            }
            return false;
        }
    }
}
