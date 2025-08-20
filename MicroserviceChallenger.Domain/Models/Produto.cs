using System;

namespace MicroserviceChallenger.Domain.Models
{
    public class Produto
    {
        // 🔒 Campos privados (estado interno)
        private string _nome;
        private string _descricao;
        private decimal _preco;
        private int _quantidadeEstoque;

        // 🔓 Propriedades públicas (apenas leitura)
        public Guid Id { get; }
        public string Nome => _nome;
        public string Descricao => _descricao;
        public decimal Preco => _preco;
        public int QuantidadeEstoque => _quantidadeEstoque;
        public bool Ativo { get; private set; }
        public DateTime CriadoEm { get; }
        public DateTime? AtualizadoEm { get; private set; }

        // ✅ Constante que estava faltando
        private const decimal PRECO_MINIMO = 1.0m;

        protected Produto() { } // EF Core

        public Produto(string nome, string descricao, decimal preco, int quantidadeEstoque)
        {
            Id = Guid.NewGuid();
            CriadoEm = DateTime.UtcNow;
            Ativo = true;

            AlterarNome(nome);
            AlterarDescricao(descricao);
            AlterarPreco(preco);
            DefinirEstoqueInicial(quantidadeEstoque);
        }

        #region Metodos de domínio
        public void AlterarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome do produto não pode ser vazio.");
            _nome = nome;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void AlterarDescricao(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new ArgumentException("Descrição do produto não pode ser vazia.");
            _descricao = descricao;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void AlterarPreco(decimal novoPreco)
        {
            if (novoPreco < PRECO_MINIMO)
                throw new ArgumentException($"Preço deve ser maior ou igual a {PRECO_MINIMO:C}");
            if (_preco > 0 && novoPreco < _preco * 0.5m)
                throw new InvalidOperationException("Redução de preço não permitida.");

            _preco = novoPreco;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void AdicionarEstoque(int quantidade)
        {
            if (quantidade <= 0)
                throw new ArgumentException("Quantidade adicionada deve ser maior que zero.");

            _quantidadeEstoque += quantidade;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void RemoverEstoque(int quantidade)
        {
            if (quantidade <= 0)
                throw new ArgumentException("Quantidade removida deve ser maior que zero.");
            if (_quantidadeEstoque - quantidade < 0)
                throw new InvalidOperationException("Estoque insuficiente.");

            _quantidadeEstoque -= quantidade;
            AtualizadoEm = DateTime.UtcNow;
        }

        private void DefinirEstoqueInicial(int quantidade)
        {
            if (quantidade < 0)
                throw new ArgumentException("Estoque inicial não pode ser negativo.");
            _quantidadeEstoque = quantidade;
        }
        #endregion
    }
}
