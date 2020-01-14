using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetoPDVModelos;

namespace ProjetoPDVDao
{
    public class ClienteDao
    {
        private string query;


        
        public bool Verifica_Cliente_Existente(string cpf)
        {
            try
            {
                object ret = (new PetaPoco.Database("stringConexao")).ExecuteScalar<object>("select codcli from Cliente where cpf = @0", cpf);

                if (ret == null)
                    return false;

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public object InsertClienteJuridica(string cnpj, string nome, string email, string telefone)
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).Insert("Cliente", "CodCli", new { cgc = cnpj, Firma = nome, email = email, telefone = telefone, tipcli = 0 });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public object InsertClienteFisica(string cpf, string nome, string email, string telefone)
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).Insert("Cliente", "CodCli", new { cpf = cpf, Firma = nome,  email = email, telefone = telefone, tipcli = 1});
            }
            catch (Exception)
            {
                throw;
            }
        }


        public object InsertCliente(Pedido pedido)
        {
            try
            {
                //return (new PetaPoco.Database("stringConexao")).Insert("Movdb", "NumDoc", true, pedido);
                return (new PetaPoco.Database("stringConexao")).Insert(pedido);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Cliente getClientePedido(int numDoc)
        {
            try
            {
                query = "SELECT C.CodCli AS CodCli, C.Firma AS Firma, C.EMail AS email, C.TipCli AS TipCli, C.CGC AS CGC, C.CPF AS CPF, C.inscest AS inscest, C.Telefone FROM Cliente C,Movdb WHERE C.CodCli = Movdb.CodCli AND NumDoc=@0";

                return (new PetaPoco.Database("stringConexao")).SingleOrDefault<Cliente>(query, numDoc);

            }
            catch (Exception)
            {
                throw;
            }

        }

        public Cliente getCliente(string codcli)
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).SingleOrDefault<Cliente>("SELECT C.CodCli AS CodCli, C.Firma AS Firma, C.EMail AS email, C.TipCli AS TipCli, C.CGC AS CGC, C.CPF AS CPF, C.inscest AS inscest, C.Telefone FROM Cliente C WHERE C.CodCli = @0", codcli);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Cliente getCliente_CONSUMIDOR_NAO_IDENTIFICADO()
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).SingleOrDefault<Cliente>("SELECT C.CodCli AS CodCli, C.Firma AS Firma, C.EMail AS email, C.TipCli AS TipCli, C.CGC AS CGC, C.CPF AS CPF, C.inscest AS inscest FROM Cliente C WHERE LTRIM(RTRIM(C.Firma)) = 'CONSUMIDOR NÃO IDENTIFICADO'");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Cliente getCliente_CPF(string cpf)
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).SingleOrDefault<Cliente>("SELECT C.CodCli AS CodCli, C.Firma AS Firma, C.EMail AS email, C.TipCli AS TipCli, C.CGC AS CGC, C.CPF AS CPF, C.inscest AS inscest, C.Telefone FROM Cliente C WHERE C.cpf = @0", cpf);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public Cliente getCliente_CNPJ(string cnpj)
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).SingleOrDefault<Cliente>("SELECT C.CodCli AS CodCli, C.Firma AS Firma, C.EMail AS email, C.TipCli AS TipCli, C.CGC AS CGC, C.CPF AS CPF, C.inscest AS inscest, C.Telefone FROM Cliente C WHERE C.CGC = @0", cnpj);
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}