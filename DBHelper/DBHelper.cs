using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace negocieonline.teste.consultacep.Models
{
    public class DBHelper
    {
        private static readonly string ConnectionString = "Host=no-db-dev-101.negocieonline.com.br;Database=db_selecao_imdb;Username=test;Password=Sacapp@2020";

        public string Create(string cep, string logradouro, string complemento, string bairro, string localidade, string uf, int ibge, int gia, int ddd, int siafi)
        {
            using (var CEP = new HttpClient())
            {
                using var conn = new Npgsql.NpgsqlConnection(ConnectionString);
                conn.Open();

                try
                {
                    var sql = String.Format("INSERT INTO public.address_consult(cep, logradouro, complemento, bairro, localidade, uf, ibge, gia, ddd, siafi) VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', {6}, {7}, {8}, {9});", cep, logradouro, complemento, bairro, localidade, uf, ibge, gia, ddd, siafi);
                    using var cmd = new Npgsql.NpgsqlCommand(sql, conn);

                    cmd.ExecuteNonQuery();

                    conn.Close();

                    return "Informacoes inseridas com sucesso";
                }
                catch (Exception e)
                {
                    conn.Close();
                    return e.Message;
                }
            }
        }

        public string Consulta(string cep)
        {
            using var conn = new NpgsqlConnection(ConnectionString);
            conn.Open();
            string result = "teste";

            try
            {
                var sql = String.Format("select * from address_consult where cep='" + cep + "'");
                using var cmd = new NpgsqlCommand(sql, conn);

                using NpgsqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                string cepDB = reader["cep"].ToString();
                string logradouro = reader["logradouro"].ToString();
                string complemento = reader["complemento"].ToString();
                string bairro = reader["bairro"].ToString();
                string localidade = reader["localidade"].ToString();
                string estado = reader["uf"].ToString();
                string ibge = reader["ibge"].ToString();
                string gia = reader["gia"].ToString();
                string ddd = reader["ddd"].ToString();
                string siafi = reader["siafi"].ToString();

                result = cepDB + "," + logradouro + "," + complemento + "," + bairro + "," + localidade + "," + estado + "," + ibge + "," + gia + "," + ddd + "," + siafi;

                conn.Close();

                return result;
            }
            catch (Exception e)
            {
                conn.Close();
                return result;
            }
        }
        public List<Endereco> ConsultaDB(string cep)
        {
            Consulta(cep);
            using (var CEP = new HttpClient())
            {
                string[] resultado = Consulta(cep).Split(",");
                List<Endereco> consultarDB = new List<Endereco>();
                try
                {
                    consultarDB.Add(new Endereco() { cep = resultado[0], logradouro = resultado[1], complemento = resultado[2], bairro = resultado[3], localidade = resultado[4], uf = resultado[5], ibge = Int32.Parse(resultado[6]), gia = Int32.Parse(resultado[7]), ddd = Int32.Parse(resultado[8]), siafi = Int32.Parse(resultado[9]) });
                    //consultarDB.Add(new EnderecoDB() { cep = resultado[0], logradouro = resultado[1],  });
                }
                catch (Exception e)
                {

                }
                return consultarDB;
            }
        }
    }
}
