using APICatalogo.Context;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Repositories;
using APICatalogo.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CategoriaDTO = APICatalogo.DTOs.CategoriaDTO;
using APICatalogo.DTOs.Mappings;
using APICatalogo.Pagination;
using Newtonsoft.Json;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly ILogger _logger;
        public CategoriasController(IUnitOfWork uof, ILogger<ApiLoggingFilter> logger)
        {
            _uof= uof;
            _logger = logger;
        }

        [HttpGet("pagination")]
        public ActionResult<IEnumerable<CategoriaDTO>> Get([FromQuery] CategoriaParameters categoriasParameters)
        {
            var categorias = _uof.CategoriaRepository.GetCategorias(categoriasParameters);

            var metadata = new
            {
                categorias.TotalCount,
                categorias.PageSize,
                categorias.CurrentPage,
                categorias.TotalPages,
                categorias.HasNext,
                categorias.HasPrevious
            };

            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

            var categoriasDto = categorias.ToCategoriaDTOList();
            _logger.LogInformation($"========== Get/api/categorias/pagination (SUCCESS) ==========");
            return Ok(categoriasDto);
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoriaDTO>> GetCategorias()
        {
            var categorias = _uof.CategoriaRepository.GetAll();

            if(categorias is null)
            {
                _logger.LogInformation($"========== Get/api/categorias -> (NOT FOUND) ==========");
                return NotFound("Nenhuma categoria foi encontrada!");
            }

            _logger.LogInformation($"========== Get/api/categorias (SUCCESS) ==========");
            var categoriasDto = categorias.ToCategoriaDTOList();
            return Ok(categoriasDto);
        }


        [HttpGet("{id:int:min(1)}", Name="ObterCategoria")]
        public ActionResult<CategoriaDTO> GetCategoria(int id)
        {
            // Usando repositório
            var categoria = _uof.CategoriaRepository.Get(c => c.CategoriaId == id);
            _logger.LogInformation($"========== Get/api/categorias/id = {id} (SUCCESS) ==========");

            if (categoria is null)
            {
                _logger.LogInformation($"========== Get/api/categorias/id = {id} -> (NOT FOUND) ==========");
                return NotFound($"A categoria de Id={id} não foi encontrada!");
            }

            // Mapeando os DTOs manualmente! cria um CategoriaDTO para retornar e reduzir exposição de entidades
            var categoriaDto = categoria.ToCategoriaDTO();
            return Ok(categoriaDto);
        }


        [HttpPost]
        public ActionResult<CategoriaDTO> CreateCategoria(CategoriaDTO categoriaDto)
        {
            if (categoriaDto == null)
            {
                _logger.LogWarning($"========== Post/api/categorias -> Dados Inválidos (NULL) ==========");
                return BadRequest("A categoria que você inseriu é nula, prencha os dados corretamente!");
            }
            // Create espera uma "Categoria" e não DTO, tem que converter
            var categoria = categoriaDto.ToCategoria();
            var categoriaCriada = _uof.CategoriaRepository.Create(categoria);
            _uof.Commit();
            _logger.LogWarning($"========== Post/api/categorias (SUCCESS) ==========");

            var novaCategoriaDto = categoriaCriada.ToCategoriaDTO();
            return new CreatedAtRouteResult("ObterCategoria", new { id = novaCategoriaDto.CategoriaId }, novaCategoriaDto);
        }


        [HttpPut("{id:int:min(1)}")]
        public ActionResult<CategoriaDTO> UpdateCategoria(int id, CategoriaDTO categoriaDto)
        {
            if (id != categoriaDto.CategoriaId)
            {
                _logger.LogWarning($"========== Put/api/categorias -> Dados Inválidos ==========");
                return BadRequest("O Id informado na atualização não condiz com o Id do banco de dados!");
            }

            var categoria = categoriaDto.ToCategoria();
            var categoriaAtualizada = _uof.CategoriaRepository.Update(categoria);
            _uof.Commit();
            _logger.LogInformation($"========== Put/api/categorias (SUCCESS) ==========");

            var AtualizadaCategoriaDto = categoriaAtualizada.ToCategoriaDTO();
            return Ok(AtualizadaCategoriaDto);
        }


        [HttpDelete("{id:int:min(1)}")]
        public ActionResult<CategoriaDTO> DeleteCategoria(int id)
        {
            var categoria = _uof.CategoriaRepository.Get(c => c.CategoriaId == id);
            if (categoria is null)
            {
                _logger.LogWarning($"========== Delete/api/categorias -> Dados Inválidos (NULL) ==========");
                return NotFound($"Categoria de id={id} não foi encontrada no banco de dados!");
            }

            var categoriaExcluida = _uof.CategoriaRepository.Delete(categoria);
            _uof.Commit();
            _logger.LogInformation($"========== Delete/api/categorias (SUCCESS) ==========");

            var DeletadaCategoriaDto = categoriaExcluida.ToCategoriaDTO();
            return Ok(DeletadaCategoriaDto);
        }


    }
}
