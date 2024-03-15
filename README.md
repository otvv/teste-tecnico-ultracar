# Ultracar Etapa Tecnica:

**Objetivo:**

Num cenário onde você possui uma oficina de manutenção de veículos e precisa criar um sistema para automatizar e digitalizar alguns processos.

---
* A oficina possui orçamentos, que possuem uma numeração, uma placa de um veículo, o nome do cliente daquele orçamento e as peças daquele orçamento.
* As peças na oficina possuem um estoque, cada peça possui o seu próprio estoque e um nome.
* Ao adicionar a peça num orçamento ela fica em estado de espera.
* Quando há peças em estado de espera num orçamento, é possível realizar a entrega dessa peça ao orçamento, o que irá deduzir o valor de estoque da peça.
* É importante que toda movimentação de estoque seja armazenada.

---

**Tecnologias usadas no projeto:**

* Entity Framework como ORM
* PostgresSQL + Docker
* .NET Core 8

**Praticas utilizadas:**

* Metodologia Code-First
* Injeção de dependências

Os requisitos detalhados do projeto estao localizados [aqui](/Repo/Requisitos.md).
