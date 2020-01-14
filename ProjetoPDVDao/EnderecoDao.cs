using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetoPDVModelos;

namespace ProjetoPDVDao
{
    public class EnderecoDao
    {
        private string query;

        public Endereco getEnderecoEmitente()
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).SingleOrDefault<Endereco>("SELECT * FROM Controle");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Endereco getEnderecoCliente(int numDoc)
        {
            try
            {
                if (Usuario.getInstance.setor != "Divulgacao")
                {
                    query = "SELECT C.Endereco AS Logradouro, C.Numero AS Numero, C.Complemento AS Complemento, C.Bairro AS Bairro,C.Cidade AS Municipio, C.CEP AS Cep, C.UF AS UF, (SELECT NumIBGE FROM TabIBGE WHERE (UF = C.uf) AND InicialCEP <= (C.cep) AND FinalCEP >= (C.cep)) as cMun FROM Cliente C,Movdb M WHERE C.CodCli = M.CodCli AND NumDoc=@0";
                }
                else
                {
                    query = "SELECT DV.Endereço AS Logradouro, DV.Numero AS Numero, DV.Complemento AS Complemento, DV.Bairro AS Bairro, DV.Cidade AS Municipio, DV.CEP AS Cep, DV.UF AS UF, (SELECT NumIBGE FROM TabIBGE WHERE (UF = DV.UF) AND InicialCEP <= (DV.cep) AND FinalCEP >= (DV.cep)) as cMun FROM Tab_Divulgacao DV,Movdb M WHERE DV.DivulgacaoID = M.CodCliDVG AND NumDoc=@0";
                }
        
                return (new PetaPoco.Database("stringConexao")).SingleOrDefault<Endereco>(query, numDoc);
            }
            catch (Exception)
            {
                throw;
            }
        }
   }
}