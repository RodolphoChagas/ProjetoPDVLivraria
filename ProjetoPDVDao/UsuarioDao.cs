using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetoPDVModelos;


namespace ProjetoPDVDao
{
    public class UsuarioDao
    {


        public bool SelecionaUsuario(string nomeuser, string senha, string descricaoLoja)
        {
            Usuario user = (new PetaPoco.Database("stringConexao")).SingleOrDefault<Usuario>("SELECT CodUser, NomeUser, Setor, Grupo, 2 as Empresa, Senha FROM Usuario WHERE NomeUser=@0 AND Senha=@1", nomeuser, senha);

            if(user != null)
            {
                Usuario.getInstance.codUser = user.codUser;
                Usuario.getInstance.nomeUser = user.nomeUser;
                Usuario.getInstance.setor = user.setor;
                Usuario.getInstance.grupo = user.grupo;
                Usuario.getInstance.senha = user.senha;
                Usuario.getInstance.loja = descricaoLoja == "Livraria"? 0: 1;

                //0 - Livraria   1 - Cafeteria

                user = null;

                return true;
            }

            return false;
        }


        public List<Usuario> getlst_Usuarios()
        {
            try
            {
                //return (new PetaPoco.Database("stringConexao")).Query<Pedido>("SELECT Movdb.numdoc, Convert(varchar(25), nfiscal) as nfiscal, conddoc, valdoc, valfrete, cndfrete, datadigitacao, datanfiscal, dscdoc, peso, volume, modelo, serienfiscal, chave, protocolo FROM Movdb INNER JOIN Operacao ON Movdb.CodOperacao = Operacao.CodOperacao WHERE CondDoc = 'F' And NFiscal <> 0 And NF = 1 And Modelo = '55' And DataDigitacao > '2012-12-30 00:00:00' And (Chave Is NULL or Protocolo Is NULL) AND CodUser = " + Usuario.getInstance.codUser).ToList();
                return (new PetaPoco.Database("stringConexao")).Query<Usuario>("SELECT CodUser, NomeUser FROM Usuario WHERE Condicao = 1 ORDER BY NomeUser").ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
