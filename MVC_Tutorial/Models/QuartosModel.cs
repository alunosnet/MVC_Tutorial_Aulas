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
    public class QuartosModel
    {
        [Key]
        public int nr { get; set; }

        [Required(ErrorMessage ="Deve indicar o piso do quarto")]
        public int piso { get; set; }

        [Required(ErrorMessage = "Deve indicar a lotação")]
        public int lotacao { get; set; }

        [Required(ErrorMessage ="Deve indicar o estado do quarto")]
        public bool estado { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage ="Deve indicar o preço por dia do quarto")]
        public decimal custo_dia { get; set; }

        public int idCliente { get; set; }
    }
    public class QuartosBD {
        string strLigacao;
        SqlConnection ligacaoBD;

        public QuartosBD()
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

        ~QuartosBD()
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
       
        public List<QuartosModel> lista()
        {
            string sql = "SELECT * FROM Quartos";
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            SqlDataReader dados = comando.ExecuteReader();
            List<QuartosModel> lista = new List<QuartosModel>();

            while (dados.Read())
            {
                QuartosModel novo = new QuartosModel();
                novo.nr = int.Parse(dados[0].ToString());
                novo.piso = int.Parse(dados[1].ToString());
                novo.lotacao = int.Parse(dados[2].ToString());
                novo.estado = bool.Parse(dados[3].ToString());
                int id_cliente=-1;
                int.TryParse(dados[4].ToString(), out id_cliente);
                novo.idCliente = id_cliente;
                novo.custo_dia = decimal.Parse(dados[5].ToString());
                lista.Add(novo);
            }
            comando.Dispose();

            return lista;
        }
        public List<QuartosModel> lista(int nr)
        {
            string sql = "SELECT * FROM Quartos WHERE nr=@nr";
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddWithValue("@nr", nr);
            SqlDataReader dados = comando.ExecuteReader();
            List<QuartosModel> lista = new List<QuartosModel>();

            while (dados.Read())
            {
                QuartosModel novo = new QuartosModel();
                novo.nr = int.Parse(dados[0].ToString());
                novo.piso = int.Parse(dados[1].ToString());
                novo.lotacao = int.Parse(dados[2].ToString());
                novo.estado = bool.Parse(dados[3].ToString());
                int id_cliente = -1;
                int.TryParse(dados[4].ToString(), out id_cliente);
                novo.idCliente = id_cliente;
                novo.custo_dia = decimal.Parse(dados[5].ToString());
                lista.Add(novo);
            }
            comando.Dispose();

            return lista;
        }
        public List<QuartosModel> listaVazios()
        {
            string sql = "SELECT * FROM Quartos WHERE estado='True'";
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            SqlDataReader dados = comando.ExecuteReader();
            List<QuartosModel> lista = new List<QuartosModel>();

            while (dados.Read())
            {
                QuartosModel novo = new QuartosModel();
                novo.nr = int.Parse(dados[0].ToString());
                novo.piso = int.Parse(dados[1].ToString());
                novo.lotacao = int.Parse(dados[2].ToString());
                novo.estado = bool.Parse(dados[3].ToString());
                int id_cliente = -1;
                int.TryParse(dados[4].ToString(), out id_cliente);
                novo.idCliente = id_cliente;
                novo.custo_dia = decimal.Parse(dados[5].ToString());
                lista.Add(novo);
            }
            comando.Dispose();

            return lista;
        }
        public void adicionarQuarto(QuartosModel novo)
        {
            string sql = "INSERT INTO quartos(piso,lotacao,estado,custo_dia) VALUES";
            sql += " (@piso,@lotacao,@estado,@custo_dia)";
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddWithValue("@piso", novo.piso);
            comando.Parameters.AddWithValue("@lotacao", novo.lotacao);
            comando.Parameters.AddWithValue("@estado", novo.estado);
            comando.Parameters.AddWithValue("@custo_dia",novo.custo_dia);
            comando.ExecuteNonQuery();
            comando.Dispose();
            return;
        }
        public void removerQuarto(int nr)
        {
            string sql = "DELETE FROM quartos WHERE nr=@nr";
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddWithValue("@nr", nr);
            comando.ExecuteNonQuery();
            comando.Dispose();
            return;
        }
        public void atualizarQuarto(QuartosModel quarto)
        {
            string sql = "UPDATE Quartos SET piso=@piso,lotacao=@lotacao,estado=@estado,custo_dia=@custo_dia ";
            sql += " WHERE nr=@nr";
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddWithValue("@piso", quarto.piso);
            comando.Parameters.AddWithValue("@lotacao", quarto.lotacao);
            comando.Parameters.AddWithValue("@estado", quarto.estado);
            comando.Parameters.AddWithValue("@custo_dia", quarto.custo_dia);
            comando.Parameters.AddWithValue("@nr", quarto.nr);
            comando.ExecuteNonQuery();
            comando.Dispose();
            return;
        }
    }
}