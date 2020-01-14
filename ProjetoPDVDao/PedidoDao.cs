using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetoPDVModelos;
//using PetaPoco;

namespace ProjetoPDVDao
{
    
    public class PedidoDao
    {
        ~PedidoDao(){}


        public Pedido getPedido_do_Dia()
        {
            try
            {
                DateTime dt = DateTime.Now;

                string strAno = dt.Year.ToString();
                string strMes = dt.Month.ToString();
                string strDia = dt.Day.ToString();

                string query = "(YEAR(datadigitacao) = '" + strAno + "' and MONTH(datadigitacao) = '" + strMes + "' and DAY(datadigitacao) = '" + strDia + "')";

                string sql = "SELECT numdoc,  codcon, Convert(varchar(20), nfiscal) as nfiscal, conddoc, valdoc, valdsc, codtransp, valfrete, cndfrete, datadigitacao, datanfiscal, dscdoc, peso, volume, modelo, serienfiscal, chave, protocolo, cartao_codautorizacao, StatNFCe FROM Movdb WHERE CondDoc = 'F' AND CodOperacao = 905 AND " + query;



                return (new PetaPoco.Database("stringConexao")).SingleOrDefault<Pedido>("SELECT numdoc,  codcon, Convert(varchar(20), nfiscal) as nfiscal, conddoc, valdoc, valdsc, codtransp, valfrete, cndfrete, datadigitacao, datanfiscal, dscdoc, peso, volume, modelo, serienfiscal, chave, protocolo, cartao_codautorizacao, StatNFCe FROM Movdb WHERE CondDoc = 'F' AND CodOperacao = 905 AND " + query);
            }
            catch (Exception)
            {
                throw;
            }
        }



        public object InsertPedido(Pedido pedido)
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
        
         //Pedidos a serem emitidos
        /// <summary>Retorna uma lista com todos os pedidos a serem emitidos.
        /// <seealso cref="PedidoDao.cs"/>
        /// </summary>
        public List<Pedido> getlstPedidos()
        {
            try
            {
                //return (new PetaPoco.Database("stringConexao")).Query<Pedido>("SELECT Movdb.numdoc, Convert(varchar(25), nfiscal) as nfiscal, conddoc, valdoc, valfrete, cndfrete, datadigitacao, datanfiscal, dscdoc, peso, volume, modelo, serienfiscal, chave, protocolo FROM Movdb INNER JOIN Operacao ON Movdb.CodOperacao = Operacao.CodOperacao WHERE CondDoc = 'F' And NFiscal <> 0 And NF = 1 And Modelo = '55' And DataDigitacao > '2012-12-30 00:00:00' And (Chave Is NULL or Protocolo Is NULL) AND CodUser = " + Usuario.getInstance.codUser).ToList();
                return (new PetaPoco.Database("stringConexao")).Query<Pedido>("SELECT Movdb.numdoc, codcon, Convert(varchar(25), nfiscal) as nfiscal, conddoc, valdoc, valdsc, codtransp, valfrete, cndfrete, datadigitacao, datanfiscal, dscdoc, peso, volume, modelo, serienfiscal, chave, protocolo, cartao_codautorizacao, StatNFCe FROM Movdb INNER JOIN Operacao ON Movdb.CodOperacao = Operacao.CodOperacao WHERE CondDoc = 'F' And NFiscal <> 0 And NF = 1 And Modelo = '65' And DataDigitacao > '2012-12-30 00:00:00' And (Chave Is NULL or Protocolo Is NULL) AND CodUser = " + Usuario.getInstance.codUser).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        //Consultas
        public List<Pedido> getlstPedidos(string nfiscal)
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).Query<Pedido>("SELECT top 1 numdoc,  codcon, Convert(varchar(20), nfiscal) as nfiscal, conddoc, valdoc, valdsc, codtransp, valfrete, cndfrete, datadigitacao, datanfiscal, dscdoc, peso, volume, modelo, serienfiscal, chave, protocolo, cartao_codautorizacao, StatNFCe FROM Movdb WHERE Nfiscal = '" + nfiscal + "' order by nfiscal").ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public List<Pedido> getlstPedidos(int pedido)
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).Query<Pedido>("SELECT numdoc,  codcon, Convert(varchar(20), nfiscal) as nfiscal, conddoc, valdoc, valdsc, codtransp, valfrete, cndfrete, datadigitacao, datanfiscal, dscdoc, peso, volume, modelo, serienfiscal, chave, protocolo, cartao_codautorizacao, StatNFCe FROM Movdb Where modelo = '65' and NumDoc = " + pedido + " order by nfiscal").ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Retorna uma lista com todos os pedidos já EMITIDOS dentro do período.
        /// </summary>
        public List<Pedido> getlstPedidos(string dtInicial, string dtFinal)
        {
            try
            {
                //dtInicial = dtInicial + " 00:00:00";
                //dtFinal = dtFinal + " 23:59:59";

                return (new PetaPoco.Database("stringConexao")).Query<Pedido>("SELECT numdoc,  codcon, Convert(varchar(20), nfiscal) as nfiscal, conddoc, valdoc, valdsc, codtransp, valfrete, cndfrete, datadigitacao, datanfiscal, dscdoc, peso, volume, modelo, serienfiscal, chave, protocolo, cartao_codautorizacao, StatNFCe FROM Movdb INNER JOIN Operacao ON Movdb.CodOperacao = Operacao.CodOperacao INNER JOIN Usuario ON Movdb.CodUser = Usuario.CodUser WHERE modelo = '65' and CondDoc in('F','C') And NFiscal <> '0' And (DataNFiscal Between '" + dtInicial + "' And '" + dtFinal + "') order by nfiscal").ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>Retorna uma lista com todos os pedidos já EMITIDOS dentro do período e condição.
        /// </summary>
        public List<Pedido> getlstPedidos(string loja, int codUsuario, string dtInicial, string dtFinal, string condicao)
        {
            try
            {
                string query = string.Empty;

                if (!string.IsNullOrEmpty(condicao.Trim()))
                {
                    query = " AND " + condicao.Trim();
                }

                string usuario;
                string vendedor;

                if (loja.Equals("Livraria"))
                    vendedor = "CodVendedor = 104 AND ";
                else if (loja.Equals("Cafeteria"))
                    vendedor = "CodVendedor = 105 AND ";
                else
                    vendedor = string.Empty;

                if (codUsuario.Equals(0))
                    usuario = string.Empty;
                else
                    usuario = "Movdb.CodUser = " + codUsuario + " AND ";


                return (new PetaPoco.Database("stringConexao")).Query<Pedido>("SELECT numdoc,  codcon, Convert(varchar(20), nfiscal) as nfiscal, conddoc, valdoc, valdsc, codtransp, valfrete, cndfrete, datadigitacao, datanfiscal, dscdoc, peso, volume, modelo, serienfiscal, chave, protocolo, cartao_codautorizacao, StatNFCe, codvendedor FROM Movdb INNER JOIN Operacao ON Movdb.CodOperacao = Operacao.CodOperacao INNER JOIN Usuario ON Movdb.CodUser = Usuario.CodUser WHERE Movdb.CodOperacao <> 905 And " + vendedor + usuario + " modelo = '65' And (DataDigitacao Between '" + dtInicial + "' And '" + dtFinal + "') " + query + " order by nfiscal desc").ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Pedido getPedido(int numDoc)
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).SingleOrDefault<Pedido>("SELECT numdoc,  codcon, Convert(varchar(20), nfiscal) as nfiscal, conddoc, valdoc, valdsc, codtransp, valfrete, cndfrete, datadigitacao, datanfiscal, dscdoc, peso, volume, modelo, serienfiscal, chave, protocolo, cartao_codautorizacao, StatNFCe, codVendedor FROM Movdb WHERE NumDoc=@0", numDoc);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Pedido getPedido(string nfiscal)
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).SingleOrDefault<Pedido>("SELECT top 1 numdoc,  codcon, Convert(varchar(20), nfiscal) as nfiscal, conddoc, valdoc, valdsc, codtransp, valfrete, cndfrete, datadigitacao, datanfiscal, dscdoc, peso, volume, modelo, serienfiscal, chave, protocolo, cartao_codautorizacao, StatNFCe, codVendedor FROM Movdb WHERE nfiscal=@0 And modelo = '65' order by NumDoc desc", nfiscal);
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Retorna uma lista com todas a Chaves relacionadas ao pedido de devolução
        /// <para>passado por parametro.</para>
        /// </summary>
        public List<string> getlstPedidos_Relacionados(int numDoc)
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).Query<string>("Select Chave From Movdb m left Join MovdbRelacionamento mr On m.numdoc = mr.numdocfilho where mr.numdocpai = " + numDoc).ToList();
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Atualiza a tabela 'Movdb' com a Chave, Protocolo, e o status do pedido.
        /// </summary>
        public bool Update_ChaveProtocolo_DataNFiscal_condDoc_StatNFCe(int numdoc, DateTime dataNFiscal, string chave, string protocolo, string statNFCe)
        {
            try
            {
                if ((new PetaPoco.Database("stringConexao")).Update("Update Movdb Set Chave='" + chave + "' ,Protocolo='" + protocolo + "', StatNFCe = '" + statNFCe + "', DataNFiscal = '" + dataNFiscal.ToString("yyyy-MM-dd HH:mm:ss") + "' Where NumDoc=" + numdoc) != 1)
                    return false;
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }
        

        /// <summary>
        /// Atualiza a tabela 'Movdb' com a Chave, Protocolo, e o status do pedido.
        /// </summary>
        public bool Update_ChaveProtocolo_condDoc_StatNFCe(int numdoc, string chave, string protocolo, string statNFCe)
        {
            try
            {
                if ((new PetaPoco.Database("stringConexao")).Update("Update Movdb Set Chave='" + chave + "' ,Protocolo='" + protocolo + "', StatNFCe = '" + statNFCe + "' Where NumDoc=" + numdoc) != 1)
                    return false;
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }
        

        /// <summary>
        /// Atualiza a tabela 'Movdb' com o numero da Nota Fiscal, Chave, Protocolo, e o status do pedido.
        /// </summary>
        public bool Update_NumeroNFiscal_ChaveProtocolo_condDoc_StatNFCe(int numdoc, string nfiscal,string chave, string protocolo, string condDoc, string statNFCe)
        {
            try
            {
                if ((new PetaPoco.Database("stringConexao")).Update("Update Movdb Set nfiscal = '" + nfiscal + "',Chave='" + chave + "' ,Protocolo='" + protocolo + "', CondDoc = '" + condDoc + "', StatNFCe = '" + statNFCe + "' Where NumDoc=" + numdoc) != 1)
                    return false;
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }

        /// <summary>
        /// Atualiza o STATUS NFCe do pedido e Condição para cancelada "C".
        /// <para>Valores para o parametro 'status'</para>
        /// <para>"102" - INUTILIZADA.</para>
        /// <para>"100" - AUTORIZADA.</para>
        /// <para>"135" - CANCELADA.</para>
        /// </summary>
        public void Update_StatNFCe_CondDoc(int numDoc, string status)
        {
            try
            {
                (new PetaPoco.Database("stringConexao")).Update("Update Movdb Set StatNFCe = '" + status + "',CondDoc = 'C' Where NumDoc=" + numDoc);
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Atualiza a tabela 'Movdb' com a Chave e o Protocolo.
        /// </summary>
        public bool Update_ChaveProtocolo(int numdoc, string chave, string protocolo) 
        {
            try
            {
                if ((new PetaPoco.Database("stringConexao")).Update("Update Movdb Set Chave='" + chave + "' ,Protocolo='" + protocolo + "' Where NumDoc=" + numdoc) != 1)
                    return false;
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }

        

        /// <summary>
        /// Atualiza as colunas 'DataDigitacao' e 'DataNFiscal' da tabela 'Movdb'.
        /// </summary>
        public void Update_DataNFiscal(int numdoc, DateTime data)
        {
            try
            {
                new PetaPoco.Database("stringConexao").Update("Update Movdb Set DataNFiscal = '" + data.ToString("yyyy-MM-dd HH:mm:ss") + "' Where NumDoc=" + numdoc);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        /// <summary>
        /// Atualiza o numero da nota fiscal
        /// </summary>
        public void Update_NFiscal(int numdoc, string nfiscal)
        {
            try
            {
                new PetaPoco.Database("stringConexao").Update("Update Movdb Set nfiscal = '" + nfiscal + "' Where NumDoc=" + numdoc);
            }
            catch (Exception)
            {
                throw;
            }
        }





        /// <summary>
        /// Atualiza a coluna 'EnvioMail' da tabela 'Movdb'.
        /// </summary>
        public void Update_Pedido_Email(int numdoc)
        {
            try
            {
                new PetaPoco.Database("stringConexao").Update("Update Movdb Set EnvioMail = 1 Where NumDoc=" + numdoc);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
