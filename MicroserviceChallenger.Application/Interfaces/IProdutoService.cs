using MicroserviceChallenger.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceChallenger.Application.Interfaces
{
    public interface IProdutoService
    {
        Task AdicionarProduto(Produto produto);
        Task<IEnumerable<string>> ConsultarCatalogoProdutosQuantidade();
        Task AtualizarEstoque(Produto produto, int quantidadeComprada);



    }
}
