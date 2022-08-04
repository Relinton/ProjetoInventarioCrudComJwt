using InventarioApiJwt.Conexao;
using InventarioApiJwt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InventarioApiJwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly InventarioContext _inventarioContext;

        public TokenController(IConfiguration configuration, InventarioContext inventarioContext)
        {
            _configuration = configuration;
            _inventarioContext = inventarioContext;
        }


        [HttpPost]
        public async Task<IActionResult> AutenticarUsuario(Usuario usuario)
        {
            //verifica as credenciais do usuário
            if (usuario != null && usuario.Login != null && usuario.Senha != null)
            {
                var usu = await GetUsuario(usuario.Login, usuario.Senha);

                if (usu != null)
                {
                    //cria claims baseado nas informações do usuário
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        //new Claim("Id", usuario.Id.ToString()),
                        //new Claim("Nome", usuario.Nome),
                        new Claim("Login", usuario.Login),
                        new Claim("Senha", usuario.Senha)
                        //new Claim("Ativo", Convert.ToString(usuario.Ativo)),
                        //new Claim("Email", usuario.Email)
                    };

                    //Define a chave usando o aplicano do método SymmetricSecutiryKey e usando a chave secreta;
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));

                    //Define as credenciais;
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    //Gera o token;
                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                             _configuration["Jwt:Audience"], claims,
                             expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Credenciais inválidas");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<Usuario> GetUsuario(string email, string senha)
        {
            return await _inventarioContext.Usuarios.FirstOrDefaultAsync(u => u.Login == email && u.Senha == senha);
        }
    }
}
