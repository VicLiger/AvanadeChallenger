# Entendendo Desafio Técnico - Microserviços

## Descrição do Desafio
Desenvolver uma aplicação com **arquitetura de microserviços** para gerenciamento de estoque de produtos e vendas em uma plataforma de e-commerce.  

O sistema será composto por dois microserviços:  
- **Estoque de Produtos**: gerencia o inventário e informações dos produtos.  
- **Vendas**: gerencia pedidos e transações de clientes.  

A comunicação entre os serviços será feita através de um **API Gateway**.  

## Tecnologias Utilizadas
- **Linguagem & Framework**: .NET Core, C#  
- **ORM**: Entity Framework  
- **APIs**: RESTful API  
- **Mensageria**: RabbitMQ (para comunicação entre microserviços)  
- **Autenticação**: JWT  
- **Banco de Dados**: relacional  
- **Cache**: Redis (gerenciado via Docker)  
- **Contêineres**: Docker (contendo o banco de dados e o Redis)

---

## Arquitetura Proposta

### Microserviço 1 (Gestão de Estoque)
Responsável por cadastrar produtos, controlar o estoque e fornecer informações sobre a quantidade disponível.  

### Microserviço 2 (Gestão de Vendas)
Responsável por gerenciar os pedidos e interagir com o serviço de estoque para verificar a disponibilidade de produtos ao realizar uma venda.  

### API Gateway
Roteamento das requisições para os microserviços adequados. Este serviço atua como o ponto de entrada para todas as chamadas de API.  

### RabbitMQ
Usado para comunicação assíncrona entre os microserviços, como notificações de vendas que impactam o estoque.  

### Autenticação com JWT
Garantir que somente usuários autenticados possam realizar ações de vendas ou consultar o estoque.

---

## Funcionalidades Requeridas

### Microserviço 1 (Gestão de Estoque)
- **Cadastro de Produtos**: Adicionar novos produtos com nome, descrição, preço e quantidade em estoque.  
- **Consulta de Produtos**: Permitir que o usuário consulte o catálogo de produtos e a quantidade disponível em estoque.  
- **Atualização de Estoque**: O estoque deve ser atualizado quando ocorrer uma venda (integração com o Microserviço de Vendas).  

### Microserviço 2 (Gestão de Vendas)
- **Criação de Pedidos**: Permitir que o cliente faça um pedido de venda, com a validação do estoque antes de confirmar a compra.  
- **Consulta de Pedidos**: Permitir que o usuário consulte o status dos pedidos realizados.  
- **Notificação de Venda**: Quando um pedido for confirmado, o serviço de vendas deve notificar o serviço de estoque sobre a redução do estoque.  

### Comum aos dois microserviços
- **Autenticação via JWT**: Apenas usuários autenticados podem interagir com os sistemas de vendas ou consultar o estoque.  
- **API Gateway**: Centralizar o acesso à API, garantindo que as requisições sejam direcionadas ao microserviço correto.

---

## Contexto do Negócio
A aplicação simula um sistema para uma plataforma de e-commerce, onde empresas precisam gerenciar seu estoque de produtos e realizar vendas de forma eficiente. A solução deve ser **escalável** e **robusta**, com separação clara entre as responsabilidades de estoque e vendas, utilizando boas práticas de arquitetura de microserviços.  

Esse tipo de sistema é comum em empresas que buscam flexibilidade e alta disponibilidade em ambientes com grande volume de transações.  

---

## Requisitos Técnicos
- **Tecnologia**: .NET Core (C#) para construir as APIs.  
- **Banco de Dados**: Entity Framework com banco de dados relacional (SQL Server ou outro).  

### Microserviços
- **Gestão de Estoque**: cadastrar produtos, consultar estoque e atualizar quantidades.  
- **Gestão de Vendas**: validar a disponibilidade de produtos, criar pedidos e reduzir o estoque.  

### Comunicação entre Microserviços
- RabbitMQ para comunicação assíncrona, especialmente para notificações de vendas e atualizações de estoque.  

### Autenticação
- JWT para proteger os endpoints e garantir que apenas usuários autorizados possam realizar ações.  

### API Gateway
- Redirecionar as requisições de clientes para os microserviços corretos.  

### Boas Práticas
- Utilização de **RESTful APIs**, tratamento adequado de exceções e validações de entrada.  

---

## Critérios de Aceitação
- Cadastro de produtos no microserviço de estoque.  
- Criação de pedidos no microserviço de vendas, com validação de estoque.  
- Comunicação eficiente entre microserviços usando RabbitMQ.  
- API Gateway funcionando corretamente.  
- Segurança com autenticação via JWT.  
- Código bem estruturado, com separação de responsabilidades e boas práticas de POO.  

---

## Extras
- **Testes Unitários**: Criar testes para cadastro de produtos e criação de pedidos.  
- **Monitoramento e Logs**: Implementar monitoramento básico de logs para rastrear falhas e transações.  
- **Escalabilidade**: Sistema capaz de escalar facilmente com novos microserviços (ex: pagamento ou envio).  
