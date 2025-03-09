using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using APICatalogo.Validations;

namespace APICatalogo.Models
{
    // Colocamos entre [] argumentos para mapeamento ideal dos atributos
    [Table("Produtos")] 
    public class Produto
    {
        // O nome "(NomeId) --> ProdutoId" faz com que ele indentifique como chave automaticamente no mapeamento
        [Key]
        public int ProdutoId { get; set; }

        [Required]
        [PrimeiraLetraMaiuscula]
        [StringLength(80,ErrorMessage ="O nome deve conter entre 2~80 caracteres" ,MinimumLength =2)]
        public string? Nome { get; set; }

        [Required]
        [StringLength(300, ErrorMessage ="A descrição deve conter entre 1~300 caracteres", MinimumLength =1)]
        public string? Descricao { get; set; }

        [Required]
        [Column(TypeName ="decimal(10,2)")]
        [Range(1, 10000, ErrorMessage ="O valor do produto deve estar entre {1} e {2}")]
        public decimal Preco { get; set; }

        [Required]
        [StringLength(300, MinimumLength =10)]
        public string? ImagemUrl { get; set; }

        public float? Estoque { get; set; }

        public DateTime DataCadastro { get; set; }

        // Esse atributo é a chave estrangeira de Categorias -> "CategoriaId"
        public int CategoriaId { get; set; }

        // Uma entidade de navegação, usada para identificar o relacionamento (ela não aparece como atributo)
        // O termo [JsonIgnore] serve p/ na hora de inserir/atualizar dados eu não preciso informar todos os dados da categoria
        // ou seja, ele ignora esse "atributo" na hora de executar alguns verbos HTTP
        [JsonIgnore]
        public Categoria? Categoria { get; set; }
    }
}
