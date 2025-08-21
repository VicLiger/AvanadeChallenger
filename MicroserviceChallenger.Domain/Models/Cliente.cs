using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceChallenger.Domain.Models
{
    public class Cliente
    {
        private string _nome;
        private string _sobrenome;
        private string _password;
        private string _telefone;
        private string _endereco;

        private readonly List<Pedido> _pedidos;

        public Guid ClienteId { get; }
        public string Nome => _nome;
        public string Sobrenome => _sobrenome;
        public string Telefone => _telefone;
        public string Endereco => _endereco;
        public DateTime DataNascimento { get; }
        public bool Ativo { get; private set; }
        public DateTime CriadoEm { get; }
        public DateTime? AtualizadoEm { get; private set; }

        public IReadOnlyCollection<Pedido> Pedidos => _pedidos.ToList().AsReadOnly();

        protected Cliente() { }

        public Cliente(string nome, string sobrenome, string password, string telefone, string endereco, DateTime dataNascimento)
        {
            ClienteId = Guid.NewGuid();
            CriadoEm = DateTime.UtcNow;
            Ativo = true;
            _pedidos = new List<Pedido>();

            AlterarNome(nome, sobrenome);
            DefinirPassword(password);
            AlterarTelefone(telefone);
            AlterarEndereco(endereco);
            DataNascimento = dataNascimento;
        }

        #region Métodos de domínio
        public void AlterarNome(string nome, string sobrenome)
        {
            if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(sobrenome))
                throw new ArgumentException("Nome e sobrenome são obrigatórios.");
            _nome = nome;
            _sobrenome = sobrenome;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void DefinirPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
                throw new ArgumentException("Senha deve conter no mínimo 6 caracteres.");
            _password = password;
            AtualizadoEm = DateTime.UtcNow;
        }

        public bool ValidarSenha(string senha) => senha == _password;

        public void AlterarTelefone(string telefone)
        {
            if (string.IsNullOrWhiteSpace(telefone))
                throw new ArgumentException("Telefone inválido.");
            _telefone = telefone;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void AlterarEndereco(string endereco)
        {
            if (string.IsNullOrWhiteSpace(endereco))
                throw new ArgumentException("Endereço inválido.");
            _endereco = endereco;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void Desativar()
        {
            Ativo = false;
            AtualizadoEm = DateTime.UtcNow;
        }

        public Pedido CriarPedido()
        {
            var pedido = new Pedido(this);
            _pedidos.Add(pedido);
            return pedido;
        }
        #endregion
    }
}
