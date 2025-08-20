using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceChallenger.Domain.Models
{
    public class Categoria
    {
        private string _nome;
        private string _descricao;
        private ICollection<Produto> _produtos; 

        public Guid ProdutoId { get; }
        public string Nome => _nome;
        public string Descricao => _descricao;
        public IReadOnlyCollection<Produto> Produtos => _produtos.ToList().AsReadOnly();
        public DateTime CriadoEm { get; }
        public DateTime? AtualizadoEm { get; private set; }

        protected Categoria() { }

        public Categoria(string nome, string descricao, Produto produto)
        {
            ProdutoId = Guid.NewGuid();
            CriadoEm = DateTime.UtcNow;

            AlterarNome(nome);
            AlterarDescricao(descricao);
            AdicionarProduto(produto);
        }

        public Categoria(string nome, string descricao, IEnumerable<Produto> produtos)
        {
            ProdutoId = Guid.NewGuid();
            CriadoEm = DateTime.UtcNow;

            AlterarNome(nome);
            AlterarDescricao(descricao);
            AdicionarGrupoProdutos(produtos);
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

        public void AdicionarProduto(Produto produto)
        {
            if (produto == null)
                throw new ArgumentNullException(nameof(produto), "Produto não pode ser nulo.");
            if (_produtos == null)
                _produtos = new List<Produto>();
            _produtos.Add(produto);
            AtualizadoEm = DateTime.UtcNow;
        }

        public void AdicionarGrupoProdutos(IEnumerable<Produto> produtos)
        {

            foreach(var produto in produtos)
            {
                if (produto == null)
                    throw new ArgumentNullException(nameof(produto), "Produto não pode ser nulo.");
                if (_produtos == null)
                    _produtos = new List<Produto>();
                _produtos.Add(produto);
                AtualizadoEm = DateTime.UtcNow;
            }
        }

        #endregion
    }
}
