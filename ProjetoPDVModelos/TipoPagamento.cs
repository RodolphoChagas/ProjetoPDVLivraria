using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetoPDVModelos
{
    public class TipoPagamento
    {
        private int _codTipoPgto;
        private string _descTipoPgto;
        private int _tipo;
        private int _maximum_number_of_plots;
        private decimal _minimum_value_for_plots;
        private int _codFormaPgtoNFCe;

        public int tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }


        public int codFormaPgtoNFCe
        {
            get { return _codFormaPgtoNFCe; }
            set { _codFormaPgtoNFCe = value; }
        }

        public int codTipoPgto 
        {
            get { return _codTipoPgto; }
            set { _codTipoPgto = value; }
        }

        public string descTipoPgto
        {
            get { return _descTipoPgto; }
            set { _descTipoPgto = value; }
        }

        public int maximum_number_of_plots
        {
            get { return _maximum_number_of_plots; }
            set { _maximum_number_of_plots = value; }
        }

        public decimal minimum_value_for_plots
        {
            get { return _minimum_value_for_plots; }
            set { _minimum_value_for_plots = value; }
        }


        //Construtores
        public TipoPagamento() { }

        //Construtores
        public TipoPagamento(int codTipoPgto, string descTipoPgto)
        {
            this.codTipoPgto = codTipoPgto;
            this.descTipoPgto = descTipoPgto;
        }

        public TipoPagamento(int codTipoPgto, string descTipoPgto, int maximum_number_of_plots, int minimum_value_for_plots, int tipo) 
        {
            this._codTipoPgto = codTipoPgto;
            this._descTipoPgto = descTipoPgto;
            this._maximum_number_of_plots = maximum_number_of_plots;
            this._minimum_value_for_plots = minimum_value_for_plots;
            this._tipo = tipo;
        }

    }
}
