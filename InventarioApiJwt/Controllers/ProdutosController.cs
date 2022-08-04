using InventarioApiJwt.Models;
using InventarioApiJwt.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioApiJwt.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutosController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }



        [AllowAnonymous]
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "ProdutosController :: Acessado em : " + DateTime.Now.ToLongDateString();
        }

        [HttpGet("todos")]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            var produtos = await _produtoRepository.GetAll();
            if (produtos == null)
            {
                return BadRequest();
            }
            return Ok(produtos.ToList());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {
            var produto = await _produtoRepository.GetById(id);

            if (produto == null)
            {
                return NotFound("Produto não encontrado pelo id informado");
            }
            return Ok(produto);
        }


        [HttpPost]
        public async Task<IActionResult> PostProduto([FromBody] Produto produto)
        {
            if (produto == null)
            {
                return BadRequest("Produto é null");
            }

            await _produtoRepository.Insert(produto);

            return CreatedAtAction(nameof(GetProduto), new { Id = produto.Id }, produto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(int id, Produto produto)
        {
            if (id != produto.Id)
            {
                return BadRequest($"O código do produto {id} não confere");
            }

            try
            {
                await _produtoRepository.Update(id, produto);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return Ok("Atualização do produto realizada com sucesso");
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Produto>> DeleteProduto(int id)
        {
            var produto = await _produtoRepository.GetById(id);
            if (produto == null)
            {
                return NotFound($"Produto de id {id} não foi encontrado");
            }

            await _produtoRepository.Delete(id);
            return Ok(produto);
        }
    }
}
