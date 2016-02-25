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
    public class ClientesModel
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage ="Tem de indicar o nome do cliente")]
        [StringLength(50)]
        [MinLength(5, ErrorMessage = "O nome é muito pequeno")]
        public string nome { get; set; }

        [Required(ErrorMessage = "Tem de indicar a morada do cliente")]
        [StringLength(50)]
        [MinLength(5, ErrorMessage = "Morada muito pequena")]
        public string morada { get; set; }

        [Required(ErrorMessage = "Tem de indicar o código postal do cliente")]
        [StringLength(8)]
        [MinLength(7, ErrorMessage = "O código postal é muito pequeno")]
        [Display(Name ="Código Postal")]
        public string cp { get; set; }

        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        public string telefone { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        [Required(ErrorMessage = "Tem de indicar a data de nascimento do cliente")]
        public DateTime dada_nascimento { get; set; }
    }

    public class ClientesBD
    {
        string strLigacao;
        SqlConnection ligacaoBD;

        public ClientesBD()
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

        ~ClientesBD()
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

        public List<ClientesModel> lista()
        {
            string sql = "SELECT * FROM Clientes";
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            SqlDataReader dados = comando.ExecuteReader();
            List<ClientesModel> lista = new List<ClientesModel>();

            while (dados.Read())
            {
                ClientesModel novo = new ClientesModel();
                novo.id = int.Parse(dados[0].ToString());
                novo.nome = dados[1].ToString();
                novo.morada = dados[2].ToString();
                novo.cp = dados[3].ToString();
                novo.email = dados[4].ToString();
                novo.telefone = dados[5].ToString();
                novo.dada_nascimento = DateTime.Parse(dados[6].ToString());
                lista.Add(novo);
            }
            comando.Dispose();

            return lista;
        }
        public List<ClientesModel> lista(string nome)
        {
            string sql = "SELECT * FROM Clientes WHERE nome=@nome";
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddWithValue("@nome", (string)nome);
            SqlDataReader dados = comando.ExecuteReader();
            List<ClientesModel> lista = new List<ClientesModel>();

            while (dados.Read())
            {
                ClientesModel novo = new ClientesModel();
                novo.id = int.Parse(dados[0].ToString());
                novo.nome = dados[1].ToString();
                novo.morada = dados[2].ToString();
                novo.cp = dados[3].ToString();
                novo.email = dados[4].ToString();
                novo.telefone = dados[5].ToString();
                novo.dada_nascimento = DateTime.Parse(dados[6].ToString());
                lista.Add(novo);
            }
            comando.Dispose();

            return lista;
        }
        public List<ClientesModel> lista(int id)
        {
            string sql = "SELECT * FROM Clientes WHERE id=@id";
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddWithValue("@id", (int)id);
            SqlDataReader dados = comando.ExecuteReader();
            List<ClientesModel> lista = new List<ClientesModel>();

            while (dados.Read())
            {
                ClientesModel novo = new ClientesModel();
                novo.id = int.Parse(dados[0].ToString());
                novo.nome = dados[1].ToString();
                novo.morada = dados[2].ToString();
                novo.cp = dados[3].ToString();
                novo.email = dados[4].ToString();
                novo.telefone = dados[5].ToString();
                novo.dada_nascimento = DateTime.Parse(dados[6].ToString());
                lista.Add(novo);
            }
            comando.Dispose();

            return lista;
        }
        public void adicionarCliente(ClientesModel novo)
        {
            string sql = "INSERT INTO Clientes(nome,morada,cp,email,telefone,data_nascimento) VALUES ";
            sql+= " (@nome,@morada,@cp,@email,@telefone,@data)";
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddWithValue("@nome", (string)novo.nome);
            comando.Parameters.AddWithValue("@morada", (string)novo.morada);
            comando.Parameters.AddWithValue("@cp", (string)novo.cp);
            comando.Parameters.AddWithValue("@email", (string)novo.email);
            if(novo.telefone!=null) comando.Parameters.AddWithValue("@telefone", (string)novo.telefone);
            else comando.Parameters.AddWithValue("@telefone", "");
            comando.Parameters.AddWithValue("@data", (DateTime)novo.dada_nascimento);
            try {
                comando.ExecuteNonQuery();
            }catch(Exception erro)
            {
                Debug.Write(erro.Message);
            }
            finally
            {
                Debug.Write("Sem erros");
            }
            comando.Dispose();
            return;
        }
        public void atualizarCliente(ClientesModel cliente)
        {
            string sql = "UPDATE Clientes SET nome=@nome,morada=@morada,cp=@cp,";
            sql += "email=@email,telefone=@telefone,data_nascimento=@data ";
            sql += "WHERE id=@id";
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddWithValue("@nome", (string)cliente.nome);
            comando.Parameters.AddWithValue("@morada", (string)cliente.morada);
            comando.Parameters.AddWithValue("@cp", (string)cliente.cp);
            comando.Parameters.AddWithValue("@email", (string)cliente.email);
            if (cliente.telefone != null) comando.Parameters.AddWithValue("@telefone", (string)cliente.telefone);
            else comando.Parameters.AddWithValue("@telefone", "");
            comando.Parameters.AddWithValue("@data", (DateTime)cliente.dada_nascimento);
            comando.Parameters.AddWithValue("@id", (int)cliente.id);
            try
            {
                comando.ExecuteNonQuery();
            }
            catch (Exception erro)
            {
                Debug.Write(erro.Message);
            }
            finally
            {
                Debug.Write("Sem erros");
            }
            comando.Dispose();
            return;
        }
        public void removerCliente(int id)
        {
            string sql = "DELETE FROM Clientes WHERE id=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=System.Data.SqlDbType.Int,Value=id }
            };
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddRange(parametros.ToArray());
            comando.ExecuteNonQuery();
            comando.Dispose();
        }
    }
}