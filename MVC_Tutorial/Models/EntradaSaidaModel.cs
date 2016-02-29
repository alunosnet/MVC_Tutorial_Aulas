using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace MVC_Hotel.Models
{
    public class EntradaSaidaModel
    {
        public int id { get; set; }
        public int id_quarto { get; set; }
        public int id_cliente { get; set; }
        public DateTime data_entrada { get; set; }
        public DateTime data_saida { get; set; }
        public decimal valor_pago { get; set; }
    }
    public class EntradaSaidaBD
    {
        string strLigacao;
        SqlConnection ligacaoBD;

        public EntradaSaidaBD()
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

        ~EntradaSaidaBD()
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

        public List<EntradaSaidaModel> listaTodos()
        {
            string sql = "SELECT * FROM entradasaida";
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            SqlDataReader dados = comando.ExecuteReader();
            List<EntradaSaidaModel> lista = new List<EntradaSaidaModel>();

            while (dados.Read())
            {
                EntradaSaidaModel novo = new EntradaSaidaModel();
                novo.id = int.Parse(dados[0].ToString());
                novo.id_quarto = int.Parse(dados[1].ToString());
                novo.id_cliente = int.Parse(dados[2].ToString());
                novo.data_entrada = DateTime.Parse(dados[3].ToString());
                novo.data_saida = DateTime.Parse(dados[4].ToString());
                novo.valor_pago = Decimal.Parse(dados[5].ToString());
                lista.Add(novo);
            }
            comando.Dispose();

            return lista;
        }
        public List<EntradaSaidaModel> listaSaidas()
        {
            string sql = "SELECT * FROM entradasaida WHERE data_saida not null";
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            SqlDataReader dados = comando.ExecuteReader();
            List<EntradaSaidaModel> lista = new List<EntradaSaidaModel>();

            while (dados.Read())
            {
                EntradaSaidaModel novo = new EntradaSaidaModel();
                novo.id = int.Parse(dados[0].ToString());
                novo.id_quarto = int.Parse(dados[1].ToString());
                novo.id_cliente = int.Parse(dados[2].ToString());
                novo.data_entrada = DateTime.Parse(dados[3].ToString());
                novo.data_saida = DateTime.Parse(dados[4].ToString());
                novo.valor_pago = Decimal.Parse(dados[5].ToString());
                lista.Add(novo);
            }
            comando.Dispose();

            return lista;
        }
        public List<EntradaSaidaModel> listaOcupados()
        {
            string sql = "SELECT * FROM entradasaida WHERE data_saida is null";
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            SqlDataReader dados = comando.ExecuteReader();
            List<EntradaSaidaModel> lista = new List<EntradaSaidaModel>();

            while (dados.Read())
            {
                EntradaSaidaModel novo = new EntradaSaidaModel();
                novo.id = int.Parse(dados[0].ToString());
                novo.id_quarto = int.Parse(dados[1].ToString());
                novo.id_cliente = int.Parse(dados[2].ToString());
                novo.data_entrada = DateTime.Parse(dados[3].ToString());
                DateTime data;
                if (DateTime.TryParse(dados[4].ToString(), out data))
                    novo.data_saida = data;
                decimal valor_pago;
                if (Decimal.TryParse(dados[5].ToString(), out valor_pago))
                    novo.valor_pago = valor_pago;
                lista.Add(novo);
            }
            comando.Dispose();

            return lista;
        }
        public List<EntradaSaidaModel> listaOcupados(int id)
        {
            string sql = "SELECT * FROM entradasaida WHERE data_saida is null and id=@id";
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddWithValue("@id", id);
            SqlDataReader dados = comando.ExecuteReader();
            List<EntradaSaidaModel> lista = new List<EntradaSaidaModel>();

            while (dados.Read())
            {
                EntradaSaidaModel novo = new EntradaSaidaModel();
                novo.id = int.Parse(dados[0].ToString());
                novo.id_quarto = int.Parse(dados[1].ToString());
                novo.id_cliente = int.Parse(dados[2].ToString());
                novo.data_entrada = DateTime.Parse(dados[3].ToString());
                //novo.data_saida = DateTime.Parse(dados[4].ToString());
               // novo.valor_pago = Decimal.Parse(dados[5].ToString());
                lista.Add(novo);
            }
            comando.Dispose();

            return lista;
        }
        //entrada
        public void registarEntrada(EntradaSaidaModel novo)
        {
            string sql = "INSERT INTO entradasaida (id_quarto,id_clientes,data_entrada) VALUES ";
            sql += "(@id_quarto,@id_cliente,@data_entrada)";
            SqlCommand comando = new SqlCommand(sql,ligacaoBD);
            comando.Parameters.AddWithValue("@id_quarto", novo.id_quarto);
            comando.Parameters.AddWithValue("@id_cliente", novo.id_cliente);
            comando.Parameters.AddWithValue("@data_entrada", novo.data_entrada);
            comando.ExecuteNonQuery();
            comando.Dispose();
            //atualizar estado do quarto
            sql = "UPDATE quartos SET estado='false' WHERE nr=@nr";
            comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddWithValue("@nr", novo.id_quarto);
            comando.ExecuteNonQuery();
            comando.Dispose();
        }
        //saida
        public void registarSaida(EntradaSaidaModel novo)
        {
            string sql = "UPDATE entradasaida set data_saida=@data_saida,valor_pago=@valor_pago WHERE id=@id";
            
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddWithValue("@data_saida", novo.data_saida);
            comando.Parameters.AddWithValue("@valor_pago", novo.valor_pago);
            comando.Parameters.AddWithValue("@id", novo.id);
            comando.ExecuteNonQuery();
            comando.Dispose();
            //atualizar estado do quarto
            sql = "UPDATE quartos SET estado='true' WHERE nr=@nr";
            comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddWithValue("@nr", novo.id_quarto);
            comando.ExecuteNonQuery();
            comando.Dispose();
        }
    }

}