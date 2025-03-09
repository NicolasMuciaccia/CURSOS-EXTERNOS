using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTOs
{
    /*
     Ao invés de retornar toda a Entidade do nosso sistema, retornamos apenas os DTOs nas consultas do usuário,
    fazendo com que a gente não exponha nossas entidades
    */
    public class CategoriaDTO
    {
        public int CategoriaId { get; set; }

        [Required]
        [StringLength(80, ErrorMessage = "O nome da categoria deve ter entre 2~80 caracteres", MinimumLength = 2)]
        public string? Nome { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "A url da imagem deve conter entre 10~300 caracteres", MinimumLength = 10)]
        public string? ImageUrl
        {
            get; set;
        }
    }
}
