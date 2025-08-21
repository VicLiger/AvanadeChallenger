using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceChallenger.Domain.Models
{
    public class CatalogoProdutos
    {
        private readonly List<Produto> _produtosCatalogados;

        public IReadOnlyCollection<Produto> ProdutosCatalogados => _produtosCatalogados.ToList().AsReadOnly();

        public CatalogoProdutos()
        {
            _produtosCatalogados = new List<Produto>();
        }

        #region Métodos de domínio
        public void AdicionarProduto(Produto produto)
        {
            if (produto == null)
                throw new ArgumentNullException(nameof(produto));
            if (!_produtosCatalogados.Contains(produto))
                _produtosCatalogados.Add(produto);
        }

        public IEnumerable<Produto> BuscarPorCategoria(Guid categoriaId)
        {
            return _produtosCatalogados.Where(p => p.Ativo && p.ProdutoId != Guid.Empty && p.CategoriaId == categoriaId);
        }

        public IEnumerable<Produto> BuscarAtivos()
        {
            return _produtosCatalogados.Where(p => p.Ativo);
        }
        #endregion
    }
}
