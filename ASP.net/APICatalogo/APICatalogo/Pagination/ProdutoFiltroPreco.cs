namespace APICatalogo.Pagination
{
    public class ProdutoFiltroPreco : QueryStringParameters
    {
        public decimal? Preco { get; set; }
        public string? PrecoCriterio {  get; set; } // maior, menor ou igual
    }
}
