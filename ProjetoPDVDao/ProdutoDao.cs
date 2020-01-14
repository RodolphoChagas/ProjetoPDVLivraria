using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetoPDVModelos;

namespace ProjetoPDVDao
{
    public class ProdutoDao
    {

        public ProdutoSubGrupo getSubGrupo(int codGrupo, int codSubGrupo)
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).SingleOrDefault<ProdutoSubGrupo>("SELECT * FROM Produto_SubGrupo WHERE CodGrupo=@0 and CodSub=@1", codGrupo, codSubGrupo);
            }
            catch (Exception)
            {

                throw;
            }

        }



        public void AtualizaEstoque(int codPro, int qtdItens, decimal valItens, Operacao operacao)
        {
            try
            {

                string sqlQuery = "UPDATE produto " +
                                  "SET Estoque = Estoque + " + qtdItens * operacao.STQ +
                                  ",QtdCns = QtdCns + " + qtdItens * operacao.cns +
                                  ",QtdPrm = QtdPrm + " + qtdItens * operacao.prm +
                                  ",QtdAva = QtdAva + " + qtdItens * operacao.ava +
                                  ",QtdEnt = QtdEnt + " + qtdItens * operacao.ent +
                                  ",QtdVnd = QtdVnd + " + qtdItens * operacao.vnd +
                                  ",QtdTro = QtdTro + " + qtdItens * operacao.tro +
                                  ",QtdDif = QtdDif + " + qtdItens * operacao.dif +
                                  ",ValorVnd = ValorVnd + " + (valItens * operacao.vnd).ToString().Replace(",", ".") +
                                  " WHERE CodPro = " + codPro;

                (new PetaPoco.Database("stringConexao")).Update(sqlQuery);
            }
            catch (Exception)
            {
                throw;
            }

        }




        /// <summary>
        /// Retorna uma lista de Produtos com as condições passadas por parâmetros.
        /// </summary>
        /// <param name="descricao">STRING com parte da DESCRICAO do produto a ser pesquisada.</param>
        /// <param name="disponivel">INTEIRO 0 = desativa modo, 1 = Disponivel.</param>
        /// <param name="indisponivel">INTEIRO  0 = desativa modo, 1 = Indisponivel.</param>
        /// <param name="prevenda">INTEIRO 0 = desativa modo, 1 = Pré-Venda.</param>
        /// <param name="bloqueado">INTEIRO 0 = desativa modo, 1 = Bloqueado.</param>
        /// <returns>Lista de Produtos</returns>
        public List<Produto> getLst_Produto_Cafeteria_findDescricao(string descricao, int situacao)
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).Query<Produto>("select produto.CodPro, Descricao, Estoque, PrcVenda, isbn, peso, tipoproduto, codsubGrupo from produto where tipoproduto = 4 and Descricao COLLATE Latin1_General_CI_AI Like '%" + descricao + "%' and situacao = " + situacao + " order by descricao").ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }




        /// <summary>
        /// Retorna uma lista de Produtos com as condições passadas por parâmetros.
        /// </summary>
        /// <param name="descricao">STRING com parte da DESCRICAO do produto a ser pesquisada.</param>
        /// <param name="disponivel">INTEIRO 0 = desativa modo, 1 = Disponivel.</param>
        /// <param name="indisponivel">INTEIRO  0 = desativa modo, 1 = Indisponivel.</param>
        /// <param name="prevenda">INTEIRO 0 = desativa modo, 1 = Pré-Venda.</param>
        /// <param name="bloqueado">INTEIRO 0 = desativa modo, 1 = Bloqueado.</param>
        /// <returns>Lista de Produtos</returns>
        public List<Produto> getLst_Produto_findDescricao(string descricao, int disponivel, int indisponivel, int prevenda, int bloqueado)
        {
            try
            {
                string query = string.Empty;

                if (disponivel == 1)
                    query = "0,";

                if (indisponivel == 1)
                    query += "3,";

                if (prevenda == 1)
                    query += "2,";

                if (bloqueado == 1)
                    query += "1,";


                if (query != string.Empty)
                    query = "Estatus in(" + query.Substring(0, query.Length - 1) + ") AND ";


                return (new PetaPoco.Database("stringConexao")).Query<Produto>("select max(id), produto.CodPro, Descricao, Estoque, PrcVenda, isbn, peso, estatus, tipoproduto, codsubGrupo from produto inner join produto_loja on produto.codpro = produto_loja.codpro where " + query + " Descricao Like '" + descricao + "%' group by produto.CodPro, Descricao, Estoque, PrcVenda, isbn, peso, estatus, tipoproduto, codsubGrupo order by descricao").ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Retorna uma lista de Produtos com as condições passadas por parâmetros.
        /// </summary>
        /// <param name="descricao">STRING com parte da DESCRICAO do produto a ser pesquisada.</param>
        /// <param name="disponivel">INTEIRO 0 = desativa modo, 1 = Disponivel.</param>
        /// <param name="indisponivel">INTEIRO  0 = desativa modo, 1 = Indisponivel.</param>
        /// <param name="prevenda">INTEIRO 0 = desativa modo, 1 = Pré-Venda.</param>
        /// <param name="bloqueado">INTEIRO 0 = desativa modo, 1 = Bloqueado.</param>
        /// <returns>Lista de Produtos</returns>
        public List<Produto> getLst_Produto_findISBN(string isbn, int disponivel, int indisponivel, int prevenda, int bloqueado)
        {
            try
            {
                string query = string.Empty;

                if (disponivel == 1)
                    query = "0,";

                if (indisponivel == 1)
                    query += "3,";

                if (prevenda == 1)
                    query += "2,";

                if (bloqueado == 1)
                    query += "1,";


                if (query != string.Empty)
                    query = "Estatus in(" + query.Substring(0, query.Length - 1) + ") AND ";


                return (new PetaPoco.Database("stringConexao")).Query<Produto>("SELECT max(id), produto.CodPro, Descricao, Estoque, PrcVenda, isbn, peso, estatus from produto inner join produto_loja on produto.codpro = produto_loja.codpro where " + query + " ISBN Like '" + isbn + "%' group by produto.CodPro, Descricao, Estoque, PrcVenda, isbn, peso, estatus order by descricao").ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Produto getProduto(int codPro)
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).SingleOrDefault<Produto>("SELECT * FROM Produto WHERE CodPro=@0", codPro);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Produto getProduto(string ISBN)
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).SingleOrDefault<Produto>("SELECT * FROM Produto WHERE ISBN='" + ISBN + "'");
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
