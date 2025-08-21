using MicroserviceChallenger.Domain.Interfaces;
using MicroserviceChallenger.Domain.Models;
using MicroserviceChallenger.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroserviceChallenger.Infraestructure.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;

        public PedidoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Pedido pedido)
        {
            await _context.Pedidos.AddAsync(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Pedido pedido)
        {
            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task<Pedido?> ObterPorIdAsync(Guid pedidoId)
        {
            return await _context.Pedidos
                                 .Include(p => p.Itens) // já carrega os itens
                                 .FirstOrDefaultAsync(p => p.PedidoId == pedidoId);
        }

        public async Task<IEnumerable<Pedido>> ObterTodosAsync()
        {
            return await _context.Pedidos
                                 .Include(p => p.Itens)
                                 .ToListAsync();
        }
    }
}
