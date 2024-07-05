using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.Models;

[Table("Categorias")]
public class Categoria
{
    public Categoria()
    {
        Produtos = new Collection<Produto>();
    }// boa pratica, porque e responsabilidade da classe onde tem o tipo colecão, iniciar a colecao
    [Key]
    public int CategoraId               { get; set; }

    [Required]
    [StringLength(80)]
    public string? CategoriaNome        { get; set; }

    [Required]
    [StringLength(100)]
    public string? CategoriaImgUrl      { get; set; }
    
    public ICollection<Produto>?Produtos{ get; set; }
}
