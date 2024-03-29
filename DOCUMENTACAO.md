## Ultracar API Usage:

A simple documentation for the _Ultracar API_ with it's possible routes _(endpoints)_ alongiside with their respective request methods.

All data shown below was generated using the API's seeder. 

Located at `Ultracar/Seeder.cs`. These are the same data that's going to be fed to the DB with the migrations.

_(might have some minor differences inside the seeder, but all basic functionality of the API is documented here with up to date examples, if something doesn't work please let me know.)_


### ROOT (/)

**Method type**: `GET`

**Endpoint**:

`https://localhost:5153/`

**Description:**

The root endpoint _(usually this route is used to check if the API is running.)_

**Expected result**: `200: Ok`

```txt
[Ultracar] - API Running.
```
---

### ORCAMENTOS (/orcamentos)

**Method type**: `GET`

**Endpoint**:

`https://localhost:5153/orcamentos`

**Description:**

The endpoint that displays all available quotes (orcamentos) inside the _**Orcamentos**_ table in the DB.

**Expected result**: `200: Ok`

```json
[
	{
		"id": 1,
		"numeracaoOrcamento": "112",
		"placaVeiculo": "ABC1234",
		"nomeCliente": "John Doe",
		"pecas": [
			{
				"id": 1,
				"estoqueId": 1,
				"nomePeca": "Peca1",
				"quantidade": 1,
				"pecaEntregue": false
			},
			{
				"id": 3,
				"estoqueId": 3,
				"nomePeca": "Peca3",
				"quantidade": 2,
				"pecaEntregue": true
			}
		]
	} ...
]
```

### GET ORCAMENTO BY ID (/orcamentos/{id})

**Method type**: `GET`

**Endpoint**:

`https://localhost:5153/orcamentos/{id}`

**Description:**

The endpoint that can display a single quote (orcamento) based on its _`id`_ from the **Orcamento** table inside the DB.

**Example usage:**

`https://localhost:5153/orcamentos/1`

**Expected result**: `200: Ok`

```json
{
	"id": 1,
	"numeracaoOrcamento": "112",
	"placaVeiculo": "ABC1234",
	"nomeCliente": "John Doe",
	"pecas": [
		{
			"id": 1,
			"estoqueId": 1,
			"nomePeca": "Peca1",
			"quantidade": 1,
			"pecaEntregue": false
		},
		{
			"id": 3,
			"estoqueId": 3,
			"nomePeca": "Peca3",
			"quantidade": 2,
			"pecaEntregue": true
		}
	]
}
```

### GET ORCAMENTO BY CLIENT NAME (/orcamentos/cliente/{nomeCliente})

**Method type**: `GET`

**Endpoint**:

`https://localhost:5153/orcamentos/cliente/{nomeCliente}`

**Description:**

The endpoint that can display one or more quote(s) based in the _`nomeCliente`_ inside the **Orcamento** table in the DB.

**Example usage:**

`https://localhost:5153/orcamentos/cliente/John%20Doe` _(%20 is the ' ' equivalent.)_

**Expected result**: `200: Ok`

```json
[
	{
		"id": 1,
		"numeracaoOrcamento": "112",
		"placaVeiculo": "ABC1234",
		"nomeCliente": "John Doe",
		"pecas": [
			{
				"id": 1,
				"estoqueId": 1,
				"nomePeca": "Peca1",
				"quantidade": 1,
				"pecaEntregue": false
			},
			{
				"id": 3,
				"estoqueId": 3,
				"nomePeca": "Peca3",
				"quantidade": 2,
				"pecaEntregue": true
			}
		]
	},
	{
		"id": 3,
		"numeracaoOrcamento": "334",
		"placaVeiculo": "ABC1234",
		"nomeCliente": "John Doe",
		"pecas": [
			{
				"id": 4,
				"estoqueId": 4,
				"nomePeca": "Peca4",
				"quantidade": 1,
				"pecaEntregue": true
			}
		]
	}
]
```

### GET ORCAMENTO BY LICENSE PLATE (/orcamentos/veiculo/{placaVeiculo})

**Method type**: `GET`

**Endpoint**:

`https://localhost:5153/orcamentos/veiculo/{placaVeiculo}`

**Description:**

The endpoint that can display one or more quote(s) based in the _`placaVeiculo`_ inside the **Orcamento** table in the DB.

**Example usage:**

`https://localhost:5153/orcamentos/veiculo/XYZ5678`

**Expected result**: `200: Ok`

```json
[
	{
		"id": 2,
		"numeracaoOrcamento": "223",
		"placaVeiculo": "XYZ5678",
		"nomeCliente": "Jane Smith",
		"pecas": [
			{
				"id": 2,
				"estoqueId": 2,
				"nomePeca": "Peca2",
				"quantidade": 3,
				"pecaEntregue": true
			}
		]
	}
]
```

### GET ORCAMENTO BY UNIQUE IDENTIFIER (/orcamentos/numero/{numeracaoOrcamento})

**Method type**: `GET`

**Endpoint**:

`https://localhost:5153/orcamentos/numero/{numeracaoOrcamento}`

**Description:**

The endpoint that can display a single quote based in the _`numeracaoOrcamento`_ inside the **Orcamento** table in the DB.

**Example usage:**

`https://localhost:5153/orcamentos/numero/334`

**Expected result**: `200: Ok`

```json
{
	"id": 3,
	"numeracaoOrcamento": "334",
	"placaVeiculo": "ABC1234",
	"nomeCliente": "John Doe",
	"pecas": [
		{
			"id": 4,
			"nomePeca": "Peca4",
			"quantidade": 1,
			"pecaEntregue": true
		}
	]
}
```

### UPDATE ORCAMENTO INFO (/orcamentos/{id}/info/update)

**Method type**: `PUT`

**Endpoint**:

`https://localhost:5153/orcamentos/{id}/info/update`

**Description:**

The endpoint that can update a single quote based on its _`id`_ inside the **Orcamento** table in the DB.
This endpoint can only update the quote info, such as client's name, vehicle license plate and so on...

**Example usage:**

`https://localhost:5153/orcamentos/3/info/update`

`request body:`
```json
	{
		"numeracaoOrcamento": "334",
		"placaVeiculo": "HHH222", // value being updated
		"nomeCliente": "John Dove", // value being updated
	}
```

**Expected result**: `200: Ok`

```json
	{
		"id": 3,
		"numeracaoOrcamento": "334",
		"placaVeiculo": "HHH222",
		"nomeCliente": "John Doe",
		"pecas": [
			{
				"id": 4,
				"estoqueId": 4,
				"nomePeca": "Peca4",
				"quantidade": 3,
				"pecaEntregue": true
			}
		]
	}
```

### UPDATE PARTS IN ORCAMENTO (/orcamentos/{id}/peca/update)

**Method type**: `POST`

**Endpoint**:

`https://localhost:5153/orcamentos/{id}/part/update`

**Description:**

The endpoint that can update one or multiple parts in a single quote based on its _`id`_ inside the **Orcamento** table in the DB.

If one or more parts are being update in a clients quote, it's info gets altered in the **_Estoque_** table as well, such as stock quantity and its current state.

NOTE: _if 'pecaEntregue' is true it means that it's Reserved to one or multiple quotes, false otherwise_.

NOTE: _You can't add new parts with this method, use the _`AddPartsInOrcamento`_ function for that._

**Example usage:**

`https://localhost:5153/orcamentos/3/part/update`

`request body:`
```json
[ // needs to be an array even if its only one part being updated
	{
		"estoqueId": 4, // you can't change this info (to avoid conflicts in the Estoque table.)
		"nomePeca": "Peca4",
		"quantidade": 21, // value being updated
		"pecaEntregue": true
	}
]
```

**Expected result**: `201: Created` _NOTE_: This return type will be changed later.

```json
	{
		"id": 3,
		"numeracaoOrcamento": "334",
		"placaVeiculo": "HHH222",
		"nomeCliente": "John Doe",
		"pecas": [
			{
				"id": 4,
				"estoqueId": 4,
				"nomePeca": "Peca4",
				"quantidade": 21,
				"pecaEntregue": true
			}
		]
	}
```

### CREATE ORCAMENTO INFO (/orcamentos/)

**Method type**: `POST`

**Endpoint**:

`https://localhost:5153/orcamentos/`

**Description:**

The endpoint that can create a single quote inside the **Orcamento** table in the DB.
You can only create the quote without the parts, which means that  you can only create the quote with basic info, such as names, vehicle license plate and unique id. 

If you want to add parts in a quote check the _AddPartsInOrcamento_ function documentation.

**Example usage:**

`https://localhost:5153/orcamentos/`

`request body:`
```json
{
	"numeracaoOrcamento": "66XDS9", // must be unique
	"placaVeiculo": "XXX2157",
	"nomeCliente": "Yan Ping",
}
```

### ADD PARTS IN ORCAMENTO (/orcamentos/{id}/peca/add)

**Method type**: `POST`

**Endpoint**:

`https://localhost:5153/orcamentos/{id}/part/add`

**Description:**

The endpoint that can add one or multiple parts in a single quote based on its _`id`_ inside the **Orcamento** table in the DB.

If a part is added in a clients quote, it's info gets altered in the **_Estoque_** table as well, such as stock quantity and its current state.

NOTE: _When adding parts, just make sure that it exists in the _**Estoque*_ table first, otherwise the API will throw an error saying that the part doesn't exist._

NOTE: _if 'pecaEntregue' is true it means that it's Reserved to one or multiple quotes, false otherwise_.

**Example usage:**

`https://localhost:5153/orcamentos/3/part/add`

`request body:`
```json
[ // needs to be an array even if its only one part being added
	{
		"estoqueId": 4, // this id needs to be valid otherwise the API will throw an error
		"nomePeca": "Peca4",
		"quantidade": 1,
		"pecaEntregue": true
	}
]
```

**Expected result**: `201: Created`

```json
	{
		"id": 3,
		"numeracaoOrcamento": "334",
		"placaVeiculo": "HHH222",
		"nomeCliente": "John Doe",
		"pecas": [
			{
				"id": 4,
				"estoqueId": 4,
				"nomePeca": "Peca4",
				"quantidade": 1,
				"pecaEntregue": true
			}
		]
	}
```

### DELETE ORCAMENTO BY ID (/orcamentos/{id}})

**Method type**: `DELETE`

**Endpoint**:

`https://localhost:5153/orcamentos/{id}`

**Description:**

The endpoint that can delete a quote based on its _`id`_ inside the **Orcamento** table in the DB.

NOTE: _Deleting a quote will restore all parts that where in it to the **Estoque** table. The parts quantity and its statuses._

**Example usage:**

`https://localhost:5153/orcamentos/4`

`request body:`
```txt
empty
```

**Expected result**: `204: No Content`

```json
empty
```
---

### ESTOQUE (/estoque)

**Method type**: `GET`

**Endpoint**:

`https://localhost:5153/estoque`

**Description:**

The endpoint that displays all parts currently in stock (estoque) and its availability inside the _**Estoque**_ table in the DB.

**Expected result**: `200: Ok`

```json
[
	{
		"id": 1,
		"nomePeca": "Peca1",
		"estoquePeca": 100,
		"tipoMovimentacao": 0
	},
	{
		"id": 2,
		"nomePeca": "Peca2",
		"estoquePeca": 200,
		"tipoMovimentacao": 1
	},
	{
		"id": 3,
		"nomePeca": "Peca3",
		"estoquePeca": 2,
		"tipoMovimentacao": 1
	},
	{
		"id": 4,
		"nomePeca": "Peca4",
		"estoquePeca": 0,
		"tipoMovimentacao": 2
	}
]
```

### GET PART IN ESTOQUE BY ID (/estoque/{id})

**Method type**: `GET`

**Endpoint**:

`https://localhost:5153/estoque/{id}`

**Description:**

The endpoint that can display a single part from the stock (estoque) based on its _`id`_ from the **Estoque** table inside the DB.

**Example usage:**

`https://localhost:5153/estoque/1`

**Expected result**: `200: Ok`

```json
{
	"id": 1,
	"nomePeca": "Peca1",
	"estoquePeca": 100,
	"tipoMovimentacao": 0
}
```

### GET PART IN ESTOQUE BY NAME (/estoque/peca/{nomePeca})

**Method type**: `GET`

**Endpoint**:

`https://localhost:5153/estoque/peca/{nomePeca}`

**Description:**

The endpoint that can display one part inside the stock (estoque) based on _`nomePeca`_ inside the **Estoque** table in the DB.

**Example usage:**

`https://localhost:5153/estoque/peca/Peca1`

**Expected result**: `200: Ok`

```json
{
	"id": 1,
	"nomePeca": "Peca1",
	"estoquePeca": 100,
	"tipoMovimentacao": 0
}
```

### GET PART IN ESTOQUE BY STATE (/estoque/peca/{tipoMovimentacao})

**Method type**: `GET`

**Endpoint**:

`https://localhost:5153/estoque/estado/{tipoMovimentacao}`

**Description:**

The endpoint that can display one or more parts inside the stock (estoque) based on _`tipoMovimentacao`_ inside the **Estoque** table in the DB.

**Expected result**: `200: Ok`

**Example Usage**:
`https://localhost:5153/estoque/estado/1`

**Available part states:** (tipoMovimentacao)

```c#
// from Ultracar/Models/Estoque.cs
enum ActionTypes : int
{
  InStock, // 0
  Reserved, // 1
  OutOfStock, // 2
}
```

**Expected result**:

```json
[
	{
		"id": 2,
		"nomePeca": "Peca2",
		"estoquePeca": 200,
		"tipoMovimentacao": 1
	},
	{
		"id": 3,
		"nomePeca": "Peca3",
		"estoquePeca": 2,
		"tipoMovimentacao": 1
	},
	{
		"id": 4,
		"nomePeca": "Peca4",
		"estoquePeca": 0,
		"tipoMovimentacao": 2
	}
]
```

### UPDATE PART IN ESTOQUE BY ID (/estoque/{id})

**Method type**: `PUT`

**Endpoint**:

`https://localhost:5153/estoque/{id}`

**Description:**

The endpoint that can update a part information based on its _`id`_ inside the **Estoque** table in the DB.

**Expected result**: `200: Ok`

**Example Usage**:

`https://localhost:5153/estoque/1`

`request body:`
```json
{
	"nomePeca": "Correia Dentada",
	"estoquePeca": 100,
	"tipoMovimentacao": 0
}
```

**Available part states:** (tipoMovimentacao)

```c#
// from Ultracar/Models/Estoque.cs
enum ActionTypes : int
{
  InStock, // 0
  Reserved, // 1
  OutOfStock, // 2
}
```

**Expected result**:

```json
{
	"id": 1,
	"nomePeca": "Correia Dentada", // value being updated
	"estoquePeca": 100,
	"tipoMovimentacao": 0
}
```

### UPDATE ESTOQUE (/estoque)

**Method type**: `PUT`

**Endpoint**:

`https://localhost:5153/estoque/{id}`

**Description:**

The endpoint that can update information about an entire **Estoque** (stock) table in the DB.

**Expected result**: `200: Ok`

**Example usage**:

`https://localhost:5153/estoque/1`

`request body:`
```json
[
  {
		"id": 1,
		"nomePeca": "Correia Dentada", // value being updated
		"estoquePeca": 68, // value being updated
		"tipoMovimentacao": 0
	}
	{
		"id": 2,
		"nomePeca": "Peca2",
		"estoquePeca": 200,
		"tipoMovimentacao": 1
	},
	{
		"id": 3,
		"nomePeca": "Correia Acessorios", // value being updated
		"estoquePeca": 2,
		"tipoMovimentacao": 1
	},
	{
		"id": 4,
		"nomePeca": "Peca4",
		"estoquePeca": 0,
		"tipoMovimentacao": 2
	},
]
```

**Available part states:** (tipoMovimentacao)

```c#
// from Ultracar/Models/Estoque.cs
enum ActionTypes : int
{
  InStock, // 0
  Reserved, // 1
  OutOfStock, // 2
}
```

**Expected result**:

```json
[
  {
		"id": 1,
		"nomePeca": "Correia Dentada",
		"estoquePeca": 68,
		"tipoMovimentacao": 0
	}
	{
		"id": 2,
		"nomePeca": "Peca2",
		"estoquePeca": 200,
		"tipoMovimentacao": 1
	},
	{
		"id": 3,
		"nomePeca": "Correia Acessorios",
		"estoquePeca": 2,
		"tipoMovimentacao": 1
	},
	{
		"id": 4,
		"nomePeca": "Peca4",
		"estoquePeca": 0,
		"tipoMovimentacao": 2
	},
]
```

### ADD MORE STOCK QUANTITY TO A PART (/estoque/peca/{id}/add?{query})

**Method type**: `PUT`

**Endpoint**:

`https://localhost:5153/estoque/peca{id}/add?{query}`

**Description:**

The endpoint that can add stock quantity into a specific part using its _`id`_ from the **Estoque** (stock) table in the DB.

**Expected result**: `200: Ok`

**Example usage**

`http://localhost:5123/estoque/peca/4/add?quantity=10`

`request query:`
```txt
quantity=10
```

**Available part states:** (tipoMovimentacao)

```c#
// from Ultracar/Models/Estoque.cs
enum ActionTypes : int
{
  InStock, // 0
  Reserved, // 1
  OutOfStock, // 2
}
```

**Expected result**:

```json
{
	"id": 4,
	"nomePeca": "Peca4",
	"estoquePeca": 10, // stock quantity changed
	"tipoMovimentacao": 0 // changed the part status because it's now in stock
},
```

### REMOVE STOCK QUANTITY FROM A PART (/estoque/peca/{id}/remove?{query})

**Method type**: `PUT`

**Endpoint**:

`https://localhost:5153/estoque/peca{id}/remove?{query}`

**Description:**

The endpoint that can remove a specific part stock quantity using its _`id`_ from the **Estoque** (stock) table in the DB.

**Expected result**: `200: Ok`

**Example usage**

`http://localhost:5123/estoque/peca/4/remove?quantity=10`

`request query:`
```txt
quantity=10
```

**Available part states:** (tipoMovimentacao)

```c#
// from Ultracar/Models/Estoque.cs
enum ActionTypes : int
{
  InStock, // 0
  Reserved, // 1
  OutOfStock, // 2
}
```

**Expected result**:

```json
{
	"id": 4,
	"nomePeca": "Peca4",
	"estoquePeca": 0, // stock quantity changed
	"tipoMovimentacao": 2 // changed the part status back to 2 because it's now out of stock
},
```

### ADD PART IN ESTOQUE (/estoque/)

**Method type**: `POST`

**Endpoint**:

`https://localhost:5153/estoque/`

**Description:**

The endpoint that can add a new part inside the **Estoque** table in the DB.

**Expected result**: `201: Created`

**Example Usage**:

`https://localhost:5153/estoque/`

`request body:`
```json
{
	"nomePeca": "Parafuso",
	"estoquePeca": 10, // initial stock quantity
	"tipoMovimentacao": 0 // this will always be 0 since its a new part being added, it can't really be in a client's quote
}
```

**Expected result**:

```json
{
	"id": 5,
	"nomePeca": "Parafuso", // value being updated
	"estoquePeca": 10,
	"tipoMovimentacao": 0
}
```

### REMOVE PART IN ESTOQUE BY ID (/estoque/{id})

**Method type**: `DELETE`

**Endpoint**:

`https://localhost:5153/estoque/{id}`

**Description:**

The endpoint that can delete a specific part based on its _`id`_ inside the **Estoque** table in the DB.

**Expected result**: `204: No Content`

**Example Usage**:

`https://localhost:5153/estoque/5`

`request body:`
```txt
empty
```

**Expected result**:

```json
empty
```
---
