using APICatalogo.DTOs;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public ProdutosController(IUnitOfWork uof, IMapper mapper, ILogger<ApiLoggingFilter> logger)
        {
            _uof = uof;
            _mapper = mapper;
            _logger = logger;
        }

        // Método criado para evitar repetição de código
        private ActionResult<IEnumerable<ProdutoDTO>> ObterProdutos(PagedList<Produto> produtos)
        {
            var metadata = new
            {
                produtos.TotalCount,
                produtos.PageSize,
                produtos.CurrentPage,
                produtos.TotalPages,
                produtos.HasNext,
                produtos.HasPrevious
            };

            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));
            var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

            return Ok(produtosDto);
        }

        [HttpGet("Pagination")]
        public ActionResult<IEnumerable<ProdutoDTO>> Get ([FromQuery] ProdutosParameters produtosParameters)
        {
            var produtos = _uof.ProdutoRepository.GetProdutos(produtosParameters);
            _logger.LogInformation($"========== Get/api/produtos/Pagination (SUCCESS) ==========");

            return ObterProdutos(produtos);
        }

        [HttpGet("filter/preco/pagination")]
        public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosFiltroPreco([FromQuery] ProdutoFiltroPreco produtoFiltroParameters)
        {
            var produtos = _uof.ProdutoRepository.GetProdutosFiltroPreco(produtoFiltroParameters);
            _logger.LogInformation($"========== Get/api/produtos/filter/preco/pagination (SUCCESS) ==========");

            return ObterProdutos(produtos);
        }


        [HttpGet("Categorias/{id}")]
        public ActionResult <IEnumerable<ProdutoDTO>> GetProdutosCategoria(int id)
        {
            var produtos = _uof.ProdutoRepository.GetProdutosPorCategoria(id);
            if(produtos is null)
            {
                _logger.LogWarning($"========== Get/api/produtos/Categorias/id = {id} -> Nada foi encontrado (NULL) ==========");
                return NotFound($"Nada foi encontrado com o id={id}");
            }
            
            var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
            _logger.LogInformation($"========== Get/api/produtos/Categorias/id = {id} (SUCCESS) ==========");

            return Ok(produtosDTO);
        }


        [HttpGet]
        public ActionResult<IEnumerable<Produto>> GetProdutos() 
        {
            var produtos = _uof.ProdutoRepository.GetAll().ToList();
            if (produtos is null)
            {
                _logger.LogWarning($"========== Get/api/produtos -> Nada foi encontrado (NULL) ==========");
                return NotFound();
            }

            var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
            _logger.LogInformation($"========== Get/api/produtos (SUCCESS) ==========");

            return Ok(produtosDTO);
        }

        [HttpGet("{id:int:min(1)}", Name="ObterProduto")]
        public ActionResult<Produto> GetProduto(int id)
        {
            var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);
            if (produto is null)
            {
                _logger.LogWarning($"========== Get/api/produtos/id -> Dados Inválidos (NULL) ==========");
                return NotFound("Produto não foi encontrado!");
            }

            var produtoDTO = _mapper.Map<ProdutoDTO>(produto);
            _logger.LogInformation($"========== Post/api/produtos/id (SUCCESS) ==========");

            return Ok(produtoDTO);
        }


        [HttpPost]
        public ActionResult CreateProduto(Produto produto)
        {
            if (produto is null)
            {
                _logger.LogWarning($"========== Post/api/produtos -> Dados Inválidos (NULL) ==========");
                return BadRequest("Produto não foi encontrado!");
            }

            var novoProduto = _uof.ProdutoRepository.Create(produto);
            _uof.Commit();
            _logger.LogInformation($"========== Post/api/produtos (SUCCESS) ==========");

            return new CreatedAtRouteResult("ObterProduto", new { id = novoProduto.ProdutoId }, novoProduto);
        }


        [HttpPut("{id:int:min(1)}")]
        public ActionResult Update(int id, Produto produto)
        {   
            // Já verifica null no Repository
            if (id != produto.ProdutoId)
            {
                _logger.LogWarning($"========== Put/api/produtos -> Dados Inválidos ==========");
                return BadRequest("Produto não foi encontrado!");
            }

            var produtoAtualizado = _uof.ProdutoRepository.Update(produto);
            _uof.Commit();
            _logger.LogInformation($"========== Put/api/produtos (SUCCESS) ==========");

            var produtoAtualizadoDTO = _mapper.Map<ProdutoDTO>(produtoAtualizado);
            return Ok(produtoAtualizadoDTO);
        }


        [HttpDelete("{id:int:min(1)}")]
        public ActionResult DeleteProduto(int id)
        {
            var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);
            if(produto is null)
            {
                _logger.LogWarning($"========== Delete/api/produtos -> Dados Inválidos (NULL) ==========");
                return NotFound($"Nada foi encontrado no id={id}");
            }

            var produtoDeletado = _uof.ProdutoRepository.Delete(produto);
            _uof.Commit();
            _logger.LogInformation($"========== Delete/api/produtos (SUCCESS) ==========");

            var produtoDeletadoDTO = _mapper.Map<ProdutoDTO>(produtoDeletado);
            return Ok(produtoDeletadoDTO);
        }
    }
}
