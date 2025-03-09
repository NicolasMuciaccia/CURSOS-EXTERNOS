using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo.Models
{
    // Colocamos entre [] argumentos para mapeamento ideal dos atributos
    [Table("Categorias")]
    public class Categoria
    {
        // Como eu tenho um atributo do tipo collection, é uma boa prática eu criá-lo no constructor!
        public Categoria() 
        {
            Produtos = new Collection<Produto>();
        }

        // O nome "(NomeId) --> ProdutoId" faz com que ele indentifique como chave automaticamente no mapeamento
        [Key]
        public int CategoriaId { get; set; }

        [Required]
        [StringLength(80, ErrorMessage ="O nome da categoria deve ter entre 2~80 caracteres",MinimumLength =2)]
        public string? Nome { get; set; }

        [Required]
        [StringLength(300,ErrorMessage ="A url da imagem deve conter entre 10~300 caracteres" ,MinimumLength =10)]
        public string? ImageUrl { get; set; }

        // Uma entidade de navegação, usada para identificar o relacionamento (ela não aparece como atributo)
        // Por ser do tipo Collection, inicializamos ela no constructor
        [JsonIgnore]
        public ICollection<Produto>? Produtos { get; set; }

    }
}
