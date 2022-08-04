using ConsumindoApiWebApp.Models;
using ConsumindoApiWebApp.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsumindoApiWebApp.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IConfiguration _configuration;

        public ProdutosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var result = AutenticacaoService.EfetuarLogin();

            List<Produto> listaDeProdutos = new List<Produto>();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new
                    System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result);

                using (var response = await httpClient.GetAsync(_configuration.GetValue<string>("API_Access:UrlBase")))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    listaDeProdutos = JsonConvert.DeserializeObject<List<Produto>>(apiResponse);
                }
            }
            return View(listaDeProdutos);
        }


        public IActionResult BuscarProduto()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            Produto produto = new Produto();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_configuration.GetValue<string>("API_Access:UrlBase") + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    produto = JsonConvert.DeserializeObject<Produto>(apiResponse);
                }
            }
            return View(produto);
        }

        public IActionResult CadastrarProduto()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CadastrarProduto(Produto produto)
        {
            var result = AutenticacaoService.EfetuarLogin();

            Produto produtoNovo = new Produto();
            using (var httpClient = new HttpClient())
            {

                httpClient.DefaultRequestHeaders.Authorization = new
                    System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result);

                StringContent content = new StringContent(JsonConvert.SerializeObject(produto),
                                                  Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(_configuration.GetValue<string>("API_Access:UrlBasePost"), content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (apiResponse.Contains("401"))
                    {
                        ViewBag.Result = apiResponse;
                        return View();
                    }
                    else
                    {
                        produtoNovo = JsonConvert.DeserializeObject<Produto>(apiResponse);
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> AtualizarProduto(int id)
        {
            var result = AutenticacaoService.EfetuarLogin();

            Produto produto = new Produto();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new
                    System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result);

                using (var response = await httpClient.GetAsync(_configuration.GetValue<string>("API_Access:UrlBasePost") + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    produto = JsonConvert.DeserializeObject<Produto>(apiResponse);
                }
            }
            return View(produto);
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarProduto(Produto produto)
        {
            var result = AutenticacaoService.EfetuarLogin();
            Produto prod = new Produto();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new
                    System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result);

                StringContent content = new StringContent(JsonConvert.SerializeObject(produto),
                                  Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync(_configuration.GetValue<string>("API_Access:UrlBasePost") + "/" + produto.Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Produto atualizado com sucesso";
                }
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> DeletarProduto(int ProdutoId)
        {
            var result = AutenticacaoService.EfetuarLogin();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new
                    System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result);

                using (var response = await httpClient.DeleteAsync(_configuration.GetValue<string>("API_Access:UrlBasePost") + "/" + ProdutoId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
