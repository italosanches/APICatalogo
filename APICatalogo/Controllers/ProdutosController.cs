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
			var produtos =  _context.Produtos.AsNoTracking().ToList();
			if(produtos is null) 
			{
				return NotFound("produtos nao encontrados");
			}
			return Ok(produtos);
		}

		[HttpGet("{id:int}", Name= "GetProdutoById")]// parametro que ira chegar no request, e sera  enviado a action ou seja URL
		public ActionResult<Produto> Get(int id)
		{
			var produto = _context.Produtos.AsNoTracking().FirstOrDefault(p => p.ProdutoId == id);
			if (produto is null)
			{
				return NotFound("produto nao encontrado");
			}
			return Ok(produto);
		}

		[HttpPost]
		public ActionResult Post(Produto produto) //Nao retorna nada, o retorno sera feito no CreatedAtRouteResult, produto e enviado no corpo do Post
		{ 
			if(produto is null)
			{
				return BadRequest();
			}
			_context.Produtos.Add(produto);
			_context.SaveChanges();

			return new CreatedAtRouteResult("GetProdutoById",new {id = produto.ProdutoId},produto);//retorna 201, e o id do produto criado e o produto
		}

		[HttpPut("{id:int}")]
		public ActionResult Put(int id,Produto produto) 
		{
			if(id != produto.ProdutoId)
			{
				return BadRequest();
			}
			_context.Entry(produto).State = EntityState.Modified; // o produto veio de um estado desconectado
																  //É necessario falar ao EF que produto esta sendo modificado
			_context.SaveChanges();
			return Ok(produto);
		}
		[HttpDelete("{id:int}")]
		public ActionResult Delete(int id) 
		{
			var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
			if(produto is null)
			{
				return NotFound("Produto não localizado");
			}
			_context.Produtos.Remove(produto);
			_context.SaveChanges();
			return Ok();
		}

	}
}
