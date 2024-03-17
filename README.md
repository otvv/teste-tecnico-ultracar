# Ultracar Etapa Tecnica:

### Objetivo

A criação de um sistema que gerencia uma oficina de veículos.
Precisa criar um sistema para automatizar e digitalizar processos seguindo as seguintes regras:

**Regras**:

* A oficina possui orçamentos, que possuem uma numeração, uma placa de um veículo, o nome do cliente daquele orçamento e as peças daquele orçamento.
* As peças na oficina possuem um estoque, cada peça possui o seu próprio estoque e um nome.
* Ao adicionar a peça num orçamento ela fica em estado de espera.
* Quando há peças em estado de espera num orçamento, é possível realizar a entrega dessa peça ao orçamento, o que irá deduzir o valor de estoque da peça.
* É importante que toda movimentação de estoque seja armazenada.
---

**Tecnologias usadas no projeto:**

* Entity Framework como ORM
* PostgresSQL com Docker
* .NET Core 8
* DBeaver para visualizar e realizar o dump do banco de dados.

**Praticas utilizadas:**

* Code-First
* Metodologia Code-First
* Injeção de dependências

Os requisitos detalhados do projeto estao localizados [aqui](/Repo/Requisitos.md).
