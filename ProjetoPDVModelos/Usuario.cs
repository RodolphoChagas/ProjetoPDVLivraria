
namespace ProjetoPDVModelos
{
    public class Usuario
    {
        private static Usuario instance;
        private int _codUser;
        private string _nomeUser;
        private string _setor;
        private string _grupo;
        private string _senha;

        private int _loja;

        public int loja
        {
            get { return _loja; }
            set
            {
                if (_loja == 0)
                    _loja = value;
            }
        }

        public int codUser 
        {
            get { return _codUser; }
            set 
            { 
                if(_codUser == 0)
                _codUser = value; 
            }
        }

        public string nomeUser
        {
            get { return _nomeUser; }
            set 
            {
                if (_nomeUser == null)
                _nomeUser = value; 
            }
        }

        public string setor 
        {
            get { return _setor; }
            set 
            {
                if(_setor == null)
                _setor = value; 
            }
        }

        public string grupo
        {
            get { return _grupo; }
            set
            {
                if (_grupo == null)
                    _grupo = value;
            }
        }

        public string senha 
        {
            get { return _senha; }
            set 
            { 
                if(_senha == null)
                _senha = value; 
            }
        }



        private Usuario()
        {
        }

        public Usuario(int codUser, string nomeUser)
        {
            _codUser = codUser;
            _nomeUser = nomeUser;
        }

        public static Usuario getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Usuario();
                }
                return instance;
            }
        }

    }
}