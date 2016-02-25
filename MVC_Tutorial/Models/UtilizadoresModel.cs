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
    //modelo de dados
    public class UtilizadoresModel
    {
        [Required(ErrorMessage ="Campo nome tem de ser preenchido")]
        public string nome { get; set; }

        [Display( Name ="Palavra passe")]
        [Required(ErrorMessage = "Campo password tem de ser preenchido")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [DataType(DataType.Password)]
        [Compare("password",ErrorMessage ="Palavras passe não são iguais")]
        public string confirmaPassword { get; set; }

        [Required(ErrorMessage = "Indique o perfil do utilizador")]
        public int perfil { get; set; }

        [Required(ErrorMessage ="Indique o estado do utilizador")]
        public bool estado { get; set; }
    }
    //class de acesso aos dados

    public class UtilizadoresBD
    {
        string strLigacao;
        SqlConnection ligacaoBD;

        public UtilizadoresBD()
        {
            strLigacao = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            try
            {
                ligacaoBD = new SqlConnection(strLigacao);
                ligacaoBD.Open();
            }catch(Exception erro)
            {
                Debug.Write(erro.Message);
            }
        }

        ~UtilizadoresBD()
        {
            try
            {
                ligacaoBD.Close();
            }catch(Exception erro)
            {
                Debug.Write(erro.Message);
            }
        }

        public List<UtilizadoresModel> lista()
        {
            string sql = "SELECT * FROM Utilizadores";
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            SqlDataReader dados = comando.ExecuteReader();
            List<UtilizadoresModel> lista = new List<UtilizadoresModel>();

            while (dados.Read())
            {
                UtilizadoresModel novo = new UtilizadoresModel();
                novo.nome = dados[0].ToString();
                novo.password = dados[1].ToString();
                novo.perfil = int.Parse(dados[2].ToString());
                novo.estado = bool.Parse(dados[3].ToString());
                lista.Add(novo);
            }
            comando.Dispose();

            return lista;
        }

        public void adicionarUtilizador(UtilizadoresModel novo)
        {
            string sql = "INSERT INTO utilizadores(nome,ppasse,perfil,estado) VALUES (@nome,HASHBYTES('SHA1',@password),@perfil,@estado)";
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddWithValue("@nome", (string)novo.nome);
            comando.Parameters.AddWithValue("@password",(string)novo.password);
            comando.Parameters.AddWithValue("@perfil", (int)novo.perfil);
            comando.Parameters.AddWithValue("@estado", (bool)novo.estado);
            comando.ExecuteNonQuery();
            comando.Dispose();
            return;
        }

        public List<UtilizadoresModel> lista(string nome)
        {
            string sql = "SELECT * FROM Utilizadores WHERE nome=@nome";
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddWithValue("@nome", (string)nome);
            SqlDataReader dados = comando.ExecuteReader();
            List<UtilizadoresModel> lista = new List<UtilizadoresModel>();

            while (dados.Read())
            {
                UtilizadoresModel novo = new UtilizadoresModel();
                novo.nome = dados[0].ToString();
                novo.password = dados[1].ToString();
                novo.perfil = int.Parse(dados[2].ToString());
                novo.estado = bool.Parse(dados[3].ToString());
                lista.Add(novo);
            }
            comando.Dispose();

            return lista;
        }

        public void editarUtilizador(UtilizadoresModel edita)
        {
            string sql = "UPDATE Utilizadores set ppasse=HASHBYTES('SHA1',@password), perfil=@perfil,estado=@estado WHERE nome=@nome";
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddWithValue("@password", (string)edita.password);
            comando.Parameters.AddWithValue("@perfil", (int)edita.perfil);
            comando.Parameters.AddWithValue("@estado", (bool)edita.estado);
            comando.Parameters.AddWithValue("@nome", (string)edita.nome);
            comando.ExecuteNonQuery();
            comando.Dispose();
        }

        public void removerUtilizador(string nome)
        {
            string sql = "DELETE FROM Utilizadores WHERE nome=@nome";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@nome",SqlDbType=System.Data.SqlDbType.VarChar,Value=nome }
            };
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddRange(parametros.ToArray());
            comando.ExecuteNonQuery();
            comando.Dispose();
        }
    }
}