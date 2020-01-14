using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetoPDVModelos;

namespace ProjetoPDVDao
{
    public class ConvenioDao
    {

        public Convenio getConvenio(int codConvenio)
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).SingleOrDefault<Convenio>("SELECT * FROM Convenio WHERE CodCon=@0", codConvenio);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int getDesconto(int codConvenio)
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).SingleOrDefault<int>("SELECT desconto FROM Convenio WHERE CodCon=@0", codConvenio);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Convenio> getlstConvenio()
        {
            try
            {
                return (new PetaPoco.Database("stringConexao")).Query<Convenio>("SELECT codcon, convert(varchar(5), desconto) + '% - ' + desccon as desccon, desconto, status  from Convenio where status = 0 order by desccon").ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
