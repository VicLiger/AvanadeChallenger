namespace MicroserviceChallenger.Domain.Models
{
    public class Produto
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal Preco { get; private set; }
        public int QuantidadeEstoque { get; private set; }

        public Produto(string nome, string descricao, decimal preco, int quantidadeEstoque)
        {
            Id = Guid.NewGuid();
            SetNome(nome);
            SetDescricao(descricao);
            SetPreco(preco);
            QuantidadeEstoque = quantidadeEstoque >= 0 ? quantidadeEstoque : throw new ArgumentException("Estoque não pode ser negativo");
        }

        public void SetNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome do produto não pode ser vazio");
            Nome = nome;
        }

        public void SetDescricao(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new ArgumentException("Descrição do produto não pode ser vazia");
            Descricao = descricao;
        }

        public void SetPreco(decimal preco)
        {
            if (preco <= 0)
                throw new ArgumentException("Preço deve ser maior que zero");
            Preco = preco;
        }

        public void AdicionarEstoque(int quantidade)
        {
            if (quantidade <= 0)
                throw new ArgumentException("Quantidade adicionada deve ser maior que zero");
            QuantidadeEstoque += quantidade;
        }
        public void RemoverEstoque(int quantidade)
        {
            if (quantidade <= 0)
                throw new ArgumentException("Quantidade removida deve ser maior que zero");
            if (QuantidadeEstoque - quantidade < 0)
                throw new InvalidOperationException("Estoque insuficiente");
            QuantidadeEstoque -= quantidade;
        }
    }
}
