﻿using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    // Terá apenas lógica de acesso a dados, sem regras de negócios (regras ficam no controlador)
    {
        // Herda o context da base (Repository)
        public ProdutoRepository(AppDbContext context) : base (context)
        {
        }

        public PagedList<Produto> GetProdutos(ProdutosParameters produtosParams)
        {
            var produtos = GetAll().OrderBy(p =>  p.ProdutoId).AsQueryable();
            var produtosOrdenados = PagedList<Produto>.ToPagedList(produtos, produtosParams.PageNumber, produtosParams.PageSize);
            return produtosOrdenados;
        }

        public PagedList<Produto> GetProdutosFiltroPreco(ProdutoFiltroPreco produtosFiltroParams)
        {
            var produtos = GetAll().AsQueryable();

            if (produtosFiltroParams.Preco.HasValue && !string.IsNullOrEmpty(produtosFiltroParams.PrecoCriterio))
            {
                if(produtosFiltroParams.PrecoCriterio.Equals("maior", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(p => p.Preco > produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
                } else if(produtosFiltroParams.PrecoCriterio.Equals("menor", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(p => p.Preco < produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
                } else if(produtosFiltroParams.PrecoCriterio.Equals("igual", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(p => p.Preco == produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);

                }
            }
            var produtosFiltrados = PagedList<Produto>.ToPagedList(produtos, produtosFiltroParams.PageNumber, produtosFiltroParams.PageSize);

            return produtosFiltrados;
        }

        public IEnumerable<Produto> GetProdutosPorCategoria(int id)
        {
            return GetAll().Where(c => c.CategoriaId == id);
        }
    }
}
