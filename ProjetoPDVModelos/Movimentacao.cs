using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetoPDVModelos
{
    public class Movimentacao
    {
        private decimal _venda_dia_cafeteria;
        private decimal _venda_dia_livraria;
        private decimal _venda_dia_online;
        private decimal _venda_mes_cafeteria;
        private decimal _venda_mes_livraria;
        private decimal _venda_mes_online;



        public decimal venda_dia_cafeteria
        {
            get { return _venda_dia_cafeteria; }
            set
            {
                _venda_dia_cafeteria = value;
            }
        }

        public decimal venda_dia_livraria
        {
            get { return _venda_dia_livraria; }
            set
            {
                _venda_dia_livraria = value;
            }
        }

        public decimal venda_dia_online
        {
            get { return _venda_dia_online; }
            set
            {
                _venda_dia_online = value;
            }
        }

        public decimal venda_mes_cafeteria
        {
            get { return _venda_mes_cafeteria; }
            set
            {
                _venda_mes_cafeteria = value;
            }
        }

        public decimal venda_mes_livraria
        {
            get { return _venda_mes_livraria; }
            set
            {
                _venda_mes_livraria = value;
            }
        }

        public decimal venda_mes_online
        {
            get { return _venda_mes_online; }
            set
            {
                _venda_mes_online = value;
            }
        }


    }
}
