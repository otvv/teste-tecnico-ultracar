# Ultracar Etapa Tecnica:

## Objetivo

A criação de um sistema que gerencia uma oficina de veículos.
Precisa criar um sistema para automatizar e digitalizar processos seguindo as seguintes regras:

**Regras**:

* A oficina possui orçamentos, que possuem uma numeração, uma placa de um veículo, o nome do cliente daquele orçamento e as peças daquele orçamento.
* As peças na oficina possuem um estoque, cada peça possui o seu próprio estoque e um nome.
* Ao adicionar a peça num orçamento ela fica em estado de espera.
* Quando há peças em estado de espera num orçamento, é possível realizar a entrega dessa peça ao orçamento, o que irá deduzir o valor de estoque da peça.
* É importante que toda movimentação de estoque seja armazenada.
---

## Documentação da API:

[DOCUMENTACAO.md](DOCUMENTACAO.md)

**Diagrama do banco de dados**:

![diagrama-ultracar-db](/Repo/ultracar_db_diagram.png)

**Dump recente banco de dados**:

[dump-ultracar_db_plain.sql](/Repo/dump-ultracar_db_plain.sql)

[dump-ultracar_db.tar](/Repo/dump-ultracar_db.tar)

_caso necessite autenticação_:

 `USER: postgres` 
 `SENHA: 12345`

**Tecnologias usadas no projeto**:

* Entity Framework como ORM
* PostgresSQL com Docker
* .NET Core 8
* DBeaver para visualizar e realizar o dump do banco de dados.
* Insomnia para visualizar e realizar queries na API.

**Praticas utilizadas**:

* Metodologia Code-First
* Injeção de dependências

Os requisitos detalhados do projeto estao localizados [aqui](/Repo/REQUISITOS.md).
