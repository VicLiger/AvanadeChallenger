using MicroserviceChallenger.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceChallenger.Domain.Models
{
    public class Pedido
    {
        private readonly List<ItemPedido> _itens;

        public Guid PedidoId { get; }
        public Guid ClienteId { get; }
        public Cliente Cliente { get; }
        public IReadOnlyCollection<ItemPedido> Itens => _itens.ToList().AsReadOnly();
        public StatusPedido Status { get; private set; }
        public bool PedidoConfirmado { get; private set; }
        public decimal ValorTotal => _itens.Sum(i => i.Subtotal);
        public DateTime CriadoEm { get; }
        public DateTime? AtualizadoEm { get; private set; }

        protected Pedido() { }

        public Pedido(Cliente cliente)
        {
            PedidoId = Guid.NewGuid();
            ClienteId = cliente.ClienteId;
            Cliente = cliente;
            CriadoEm = DateTime.UtcNow;
            Status = StatusPedido.Criado;
            PedidoConfirmado = false;
            _itens = new List<ItemPedido>();
        }

        #region Métodos de domínio
        public void AdicionarItem(Produto produto, int quantidade)
        {
            if (!produto.Ativo)
                throw new InvalidOperationException("Produto inativo.");
            produto.RemoverEstoque(quantidade);

            var item = new ItemPedido(this, produto, quantidade);
            _itens.Add(item);

            AtualizadoEm = DateTime.UtcNow;
        }

        public void Confirmar()
        {
            if (!_itens.Any())
                throw new InvalidOperationException("Pedido sem itens não pode ser confirmado.");
            PedidoConfirmado = true;
            Status = StatusPedido.Processando;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void Cancelar()
        {
            Status = StatusPedido.Cancelado;
            AtualizadoEm = DateTime.UtcNow;
        }
        #endregion
    }
}
