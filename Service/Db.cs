using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Digests;

namespace Service
{
    public class Db
    {
        private readonly MySqlConnection _con;
        private MySqlCommand? _command;
        private MySqlDataReader? _reader;

        public Db()
        {
            _con = new MySqlConnection("Server=127.0.0.1;Database=cadusuario;Uid=root;Pwd=SslMode=none;");
        }

        public List<UsuarioDTO> GetData()
        {
            _con.Open();
            _command = new MySqlCommand();
            _command.Connection = _con;
            _command.CommandText = "SELECT * FROM usuario";
            _reader = _command.ExecuteReader();

            List<UsuarioDTO> ListaDeUsuarios = new List<UsuarioDTO>();

            while(_reader.Read())
            {
                var usuarioDTO = new UsuarioDTO()
                {
                    id = int.Parse(_reader["Id"].ToString()!),
                    nome = _reader["Nome"].ToString()!,
                    sobrenome = _reader["Sobrenome"].ToString()!,
                    email = _reader["Email"].ToString()!,
                };
                ListaDeUsuarios.Add(usuarioDTO);
            }

            return ListaDeUsuarios;
        }
        public void AddUsuario()
        {
            _con.Open();
            _command = new MySqlCommand();
            _command.Connection = _con;

            _command.CommandText = "INSERT INTO usuario (Nome, Sobrenome, Email) VALUES (?nome, ?sobrenome, ?email)";
        }
    }

}
