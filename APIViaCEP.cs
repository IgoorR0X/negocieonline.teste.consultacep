using negocieonline.teste.consultacep.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace negocieonline.teste.consultacep
{
    public class APIViaCEP
    {
        public string BaseURL 
        {
            get 
            {
                return "https://viacep.com.br/ws/";
            }
        }
        public List<Endereco> GetCEP(string cep)
        {
            using (var CEP = new HttpClient())
            {
                string URL = string.Format("{0}/json/", cep);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, BaseURL + URL);
                HttpResponseMessage response = CEP.SendAsync(request).Result;
                List<Endereco> consultarCEPs = new List<Endereco>();
                string cepJson = response.Content.ReadAsStringAsync().Result;
                try
                {
                    dynamic resultado = JsonConvert.DeserializeObject(cepJson);
                    consultarCEPs.Add(new Endereco() { cep = resultado["cep"], logradouro = resultado["logradouro"], complemento = resultado["complemento"], bairro = resultado["bairro"], ddd = resultado["ddd"], gia = resultado["gia"], ibge = resultado["ibge"], localidade = resultado["localidade"], uf = resultado["uf"], siafi = resultado["siafi"] });
                }
                catch(Exception e)
                {
                    
                }
                return consultarCEPs;
            }
        }
    }
}
