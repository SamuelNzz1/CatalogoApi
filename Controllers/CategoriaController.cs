using ApiCatalago.Context;
using ApiCatalago.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalago.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriaController(AppDbContext context) {
            _context = context;
        }

        


        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get() {
            try
            {

               var categorias = _context.Categorias.AsNoTracking().Take(10).ToList();
               return categorias;
            }
            catch(Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");

                
            }
                
           

        }
        [HttpGet("Produtos")]

        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos() {

            try { 
                var categoria = _context.Categorias.Include(p => p.Produtos).AsNoTracking().Take(10).ToList();
                if (categoria is null) {
                    return NotFound("Não foi possivel encontrar categoria nem produto");
                    
                }
                return categoria;
            }
            catch(Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
            }

        }
        

        [HttpGet("{id:int}", Name = "ObterCategoria")]

        public ActionResult<Categoria> GetForId(int id){
            var categoria = _context.Categorias.AsNoTracking().FirstOrDefault(p => p.CategoriaId == id);
            if (categoria is null) {
                return NotFound("Categoria não encontrada");
            }

            return categoria;

        }

        [HttpPost]

        public ActionResult Post(Categoria categoria) {
            if (categoria is null) {
                return BadRequest();

            }

            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);

        }

        [HttpPut("{id:int}")]

        public ActionResult<Categoria> Put(int id, Categoria categoria) {
            
            if (categoria.CategoriaId != id) {
                return BadRequest();
            }

            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(categoria);
        }
        [HttpDelete("{id:int}")]

        public ActionResult Delete(int id) {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
            if (categoria is null) {
                return NotFound("Categoria não encontrada");
            }

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return Ok(categoria);

        }

    }
}
