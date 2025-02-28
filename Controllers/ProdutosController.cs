using Microsoft.AspNetCore.Mvc;
using ApiCatalago.Context;
using ApiCatalago.Models;
using Microsoft.EntityFrameworkCore;
namespace ApiCatalago.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context) {
            
            _context = context;

        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get() {
            var produtos = _context.Produtos.AsNoTracking().Take(10).ToList(); 
            if(!produtos.Any() ) {
                return NotFound("Produtos não encontrados");
             }
            return produtos;


        }

        [HttpGet("Categoria/{id:int}")]
        public ActionResult<IEnumerable<Produto>> GetProdForCateg(int id) {
            var produtos = _context.Produtos.Where(p => p.CategoriaId == id).AsNoTracking().Take(10).ToList();

            if (produtos is null || produtos.Count == 0)
            {
                return NotFound("Nenhum produto encontrado para esta categoria.");
            }

            return Ok(produtos);
        }

        [HttpGet("{id:int}", Name="ObterProduto")]
        public ActionResult<Produto> GetForId(int id)
        {
            var produto = _context.Produtos.AsNoTracking().FirstOrDefault(p => p.ProdutoId == id);
                       

            if (produto is null) {
                return NotFound("Produto Não encontrado");
            }
            
            return produto;

        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if (produto is null) {
                return BadRequest();
             }

            _context.Produtos.Add(produto);
            
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);


        }

        [HttpPut("{id:int}")]

        public ActionResult Put(int id, Produto produto) {
            if (id != produto.ProdutoId) {
                return BadRequest();
            }
            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);

        }
        
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id) {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            
            if (produto is null) {
                return NotFound("Produto não encontrado");
            }
            
            _context.Produtos.Remove(produto);
            
            _context.SaveChanges();

            return Ok(produto);

        }

    }
}
