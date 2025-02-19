using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProdutosAPI.Context;
using ProdutosAPI.Models;

namespace ProdutosAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : Controller
    {

        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "Obter todos os produtos")]
        public async Task<ActionResult<IEnumerable<Produto>>> GetAllProdutos()
        {
            var produtos = await _context.Produtos.ToListAsync();
            if (produtos is null)
            {
                return NotFound("Produtos nao encontrados");
            }
            return produtos;

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return produto;
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> PostProdutos(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            if (produto is null)
            {
                return BadRequest("Dados Inválidos");
            }

            return CreatedAtAction("GetProduto", new { id = produto.ProdutoId }, produto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutProdutos(Produto produto, int id)
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok(produto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Produto>> DeleteProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
            {
                return BadRequest("Dados inválidos");
            }

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return Ok(produto);



        }
















    }
}
