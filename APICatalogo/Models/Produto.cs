using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.Models;

[Table("Produtos")]
public class Produto
{
	[Key]
    public int      ProdutoId           { get; set; }
	public int      CategoriaId			{ get; set; }
	[Required]
	[StringLength(80)]
	public string?  ProdutoNome         { get; set; }

	[Required]
	[StringLength(300)]
	public string?  ProdutoDescricao    { get; set; }

	[Required]
	[Column(TypeName ="decimal(10,2)")]
	public decimal  ProdutoPreco        { get; set; }

	[Required]
	[StringLength(300)]
	public string?  ProdutoImgUrl       { get; set; }
	public float    ProdutoEstoque      { get; set; }
	public DateTime ProdutoDataCadastro { get; set; }

	
	[ForeignKey("CategoriaId")]
	public virtual Categoria? Categoria { get; set; }
}
