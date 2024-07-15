using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProdutosController : ControllerBase
	{
		private readonly AppDbContext _context;
		public ProdutosController(AppDbContext context)
		{
			_context = context;
		}
		[HttpGet]
		public ActionResult<IEnumerable<Produto>> Get()  //ActionResult, permite retornar um Ienumerable ou qualquer tipo de retorno de Actiom Result 
		{
			try
			{
				var produtos = _context.Produtos.AsNoTracking().ToList();
				if (produtos is null)
				{
					return NotFound("produtos nao encontrados");
				}
				return Ok(produtos);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "erro ao atender a solicitação.");
			}
		}

		[HttpGet("{id:int}", Name = "GetProdutoById")]// parametro que ira chegar no request, e sera  enviado a action ou seja URL
		public ActionResult<Produto> Get(int id)
		{
			try
			{
				var produto = _context.Produtos.AsNoTracking().FirstOrDefault(p => p.ProdutoId == id);
				if (produto is null)
				{
					return NotFound("produto nao encontrado");
				}
				return Ok(produto);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "erro ao atender a solicitação.");
			}
		}

		[HttpPost]
		public ActionResult Post([FromBody] Produto produto) //Nao retorna nada, o retorno sera feito no CreatedAtRouteResult, produto e enviado no corpo do Post
		{
			try
			{
				if (produto is null)
				{
					return BadRequest();
				}
				_context.Produtos.Add(produto);
				_context.SaveChanges();

				return new CreatedAtRouteResult("GetProdutoById", new { id = produto.ProdutoId }, produto);//retorna 201, e o id do produto criado e o produto
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "erro ao atender a solicitação.");
			}
		}

		[HttpPut("{id:int}")]
		public ActionResult Put(int id, [FromBody] Produto produto)
		{
			try
			{
				if (id != produto.ProdutoId)
				{
					return BadRequest();
				}
				_context.Entry(produto).State = EntityState.Modified; // o produto veio de um estado desconectado
																	  //É necessario falar ao EF que produto esta sendo modificado
				_context.SaveChanges();
				return Ok(produto);
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
				var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
				if (produto is null)
				{
					return NotFound("Produto não localizado");
				}
				_context.Produtos.Remove(produto);
				_context.SaveChanges();
				return Ok();
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "erro ao atender a solicitação.");
			}
		}

	}
}
