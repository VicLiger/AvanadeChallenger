using MicroserviceChallenger.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceChallenger.Domain.Interfaces
{
    public interface IPedidoRepository
    {
        Task AdicionarAsync(Pedido pedido);
        Task AtualizarAsync(Pedido pedido);
        Task<Pedido?> ObterPorIdAsync(Guid pedidoId);
        Task<IEnumerable<Pedido>> ObterTodosAsync();
    }
}
