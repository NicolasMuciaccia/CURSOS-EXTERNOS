using APICatalogo.Validations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTOs
{
    public class ProdutoDTO
    {
        public int ProdutoId { get; set; }

        [Required]
        [StringLength(80, ErrorMessage = "O nome deve conter entre 2~80 caracteres", MinimumLength = 2)]
        public string? Nome { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "A descrição deve conter entre 1~300 caracteres", MinimumLength = 1)]
        public string? Descricao { get; set; }

        [Required]
        [Range(1, 10000, ErrorMessage = "O valor do produto deve estar entre {1} e {2}")]
        public decimal Preco { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 10)]
        public string? ImagemUrl { get; set; }

        // Esse atributo é a chave estrangeira de Categorias -> "CategoriaId"
        public int CategoriaId { get; set; }
    }
}
