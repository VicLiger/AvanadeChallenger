using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceChallenger.Domain.Models
{
    public class ItemPedido
    {
        public Guid ItemId { get; }
        public Produto Produto { get; }
        public decimal ValorUnitario { get; }
        public int QuantidadeComprada { get; }
        public Guid PedidoId { get; }
        public decimal Subtotal => QuantidadeComprada * ValorUnitario;

        protected ItemPedido() { }

        public ItemPedido(Pedido pedido, Produto produto, int quantidade)
        {
            if (pedido == null) throw new ArgumentNullException(nameof(pedido));
            if (produto == null) throw new ArgumentNullException(nameof(produto));
            if (quantidade <= 0) throw new ArgumentException("Quantidade deve ser maior que zero.");

            ItemId = Guid.NewGuid();
            PedidoId = pedido.PedidoId;
            Produto = produto;
            QuantidadeComprada = quantidade;
            ValorUnitario = produto.Preco;
        }
    }
}
