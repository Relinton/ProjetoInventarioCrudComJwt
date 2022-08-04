using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsumindoApiWebApp.Service
{
    public static class AutenticacaoService
    {
        public static string EfetuarLogin()
        {
            string urlApiGeraToken = "https://localhost:44343/api/token";
            string token = "";

            using (var httpClient = new System.Net.Http.HttpClient())
            {
                string nome = "Pinheiros Development";
                string login = "relintonproande@gmail.com";
                string senha = "12345678";

                var dados = new
                {
                    Nome = nome,
                    Login = login,
                    Senha = senha
                };
                string jsonObjeto = JsonConvert.SerializeObject(dados);
                var content = new StringContent(jsonObjeto, Encoding.UTF8, "application/json");

                var resultado = httpClient.PostAsync(urlApiGeraToken, content).Result;

                if (resultado.IsSuccessStatusCode)
                {
                    var tokenJson = resultado.Content.ReadAsStringAsync();
                    var tokenJsonResult = tokenJson.Result;
                    token = tokenJsonResult;
                }

            }
            return (token);
        }
    }
}
