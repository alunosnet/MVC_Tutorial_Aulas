using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;

namespace MVC_Hotel.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Nome de utilizador é obrigatório")]
        public string nome { get; set; }
        
        [Required(ErrorMessage ="Palavra passe é obrigatória")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }

    public class LoginBD
    {
        string strLigacao;
        SqlConnection ligacaoBD;

        public LoginBD()
        {
            strLigacao = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            try
            {
                ligacaoBD = new SqlConnection(strLigacao);
                ligacaoBD.Open();
            }
            catch (Exception erro)
            {
                Debug.Write(erro.Message);
            }
        }

        ~LoginBD()
        {
            try
            {
                ligacaoBD.Close();
            }
            catch (Exception erro)
            {
                Debug.Write(erro.Message);
            }
        }

        public UtilizadoresModel validarLogin(LoginModel login)
        {
            string sql = "SELECT * FROM utilizadores WHERE nome=@nome AND ";
            sql += " ppasse=cast(HASHBYTES('SHA1',@password) as varchar)";
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddWithValue("@nome", login.nome);
            comando.Parameters.AddWithValue("@password", login.password);
            SqlDataReader dados= comando.ExecuteReader();

            UtilizadoresModel utilizador = new UtilizadoresModel();

            if (dados.HasRows)
            {
                dados.Read();
                utilizador.nome = dados[0].ToString();
                utilizador.perfil = int.Parse(dados[2].ToString());
                return utilizador;
            }
            return null;
        }
    }
}