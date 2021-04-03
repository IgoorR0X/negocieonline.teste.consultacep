using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using negocieonline.teste.consultacep.Models;
using negocieonline.teste.consultacep.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace negocieonline.teste.consultacep.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Search(string cep)
        {
            using (var retornoCEP = new HttpClient())
            {
                APIViaCEP CEP = new APIViaCEP();
                try
                {
                    var result = CEP.GetCEP(cep);
                    return View("Index", result);
                }
                catch
                {
                    return View("Index", "Não foi encontrado resultado para o CEP informado. ");
                }
            }
        }

        [HttpGet]
        public IActionResult ConsultarDB(string cep)
        {
            using (var retornoCEPDB = new HttpClient())
            { 
                DBHelper db = new DBHelper();
                var result = db.ConsultaDB(cep);
                if (result.Count > 0)
                {
                    return View("Index", result);
                }
                return View("Index");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            using (var retornoCEP = new HttpClient())
            {
                DBHelper db = new DBHelper();
                Endereco endereco = new Endereco();
                string cep = endereco.cep;
                string logradouro = endereco.logradouro;
                string complemento = endereco.complemento;
                string bairro = endereco.bairro;
                string localidade = endereco.localidade;
                string uf = endereco.uf;
                int ibge = endereco.ibge;
                int gia = endereco.gia;
                int ddd = endereco.ddd;
                int siafi = endereco.siafi;

                var result = db.Create(cep, logradouro, complemento, bairro, localidade, uf, ibge, gia, ddd, siafi);
                if(result == "Informacoes inseridas com sucesso")
                {
                    return View("Index");
                }
                return View("Index");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
