using APICatalogo.Context;
using APICatalogo.Filters;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriasController : ControllerBase
	{
		private readonly AppDbContext _context;
		public CategoriasController(AppDbContext context)
		{
			_context = context;
		}

		[HttpGet("produtos")]
		public ActionResult<IEnumerable<Categoria>> GetCategoriasAndProdutos()
		{
			try
			{
				return _context.Categorias.AsNoTracking().Include(b => b.Produtos).ToList();
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "erro ao atenter a solicitação");
			}
		}

		[HttpGet]
		[ServiceFilter<ApiLogginFilter>]
		public ActionResult<IEnumerable<Categoria>> Get()
		{
			try
			{
				IEnumerable<Categoria> categorias = _context.Categorias.AsNoTracking().ToList();
				if (!categorias.Any() || categorias is null)
				{
					return NotFound("Não há categorias.");
				}
				return Ok(categorias);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "erro ao atenter a solicitação.");
			}
		}

		[HttpGet("{id:int}", Name = "GetCategoriaById")]
		public ActionResult<Categoria> Get(int id)
		{
			try
			{
				var categoria = _context.Categorias.AsNoTracking().FirstOrDefault(x => x.CategoraId == id);
				if (categoria == null)
				{
					return NotFound("Categoria não encontrada.");
				}
				return Ok(categoria);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "erro ao atender a solicitação.");
			}
		}

		[HttpPost]
		public ActionResult<Categoria> Post([FromBody] Categoria categoria)
		{
			try
			{
				if (categoria is null)
				{
					return BadRequest();
				}
				_context.Categorias.Add(categoria);
				_context.SaveChanges();
				return new CreatedAtRouteResult("GetCategoriaById", new { id = categoria.CategoraId }, categoria);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "erro ao atender a solicitação.");
			}
		}
		[HttpPut("{id:int}")]
		public ActionResult Put(int id, [FromBody] Categoria categoria)
		{
			try
			{
				if (id != categoria.CategoraId)
				{
					return BadRequest();
				}
				_context.Entry(categoria).State = EntityState.Modified;
				_context.SaveChanges();
				return Ok(categoria);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "erro ao atender a solicitação.");
			}
		}

		[HttpDelete("{id:int}")]
		public ActionResult Delete(int id)
		{
			try
			{
				var categoria = _context.Categorias.FirstOrDefault(x => x.CategoraId == id);
				if (categoria is null)
				{
					return NotFound("Categoria não encontrada.");
				}
				_context.Categorias.Remove(categoria);
				_context.SaveChanges();
				return Ok(categoria);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "erro ao atender a solicitação.");
			}
		}
	}
}
