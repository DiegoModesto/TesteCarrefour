
## Instalação


Este sistema foi desenvolvido utilizando a IDE Jetbrains Rider, veja as compatibilidades

### Github

Clone o repositório ao seu ambiente de preferência

```path
    SSH: gitclone git@github.com:DiegoModesto/TesteCarrefour.git
    HTTPS: https://github.com/DiegoModesto/TesteCarrefour.git 
```

### Repositórios

Os repositórios Nuget instalado estão com versões fixadas e podem não refletir a atualidade, use esses dados como parâmetros caso deseje atualizar algum repositório.

```path
  dotnet restore 
```

### Docker

O projeto utliza banco de dados Postgres e RabbitMQ, todos podem ser encontrados na pasta

```path
    ./docker-compose.yml
```

Obs.: Não aconselho utilizar 'stack' para rodar o projeto

#### Criando Banco e MQ

Dentro do YML é possível subir o compose completo com os projetos executando internamente, porém, é possível executar separadamente.

Pode-se subir os bancos separados para leitura parcial:
docker-compose.
![compose.png](https://github.com/DiegoModesto/TesteCarrefour/blob/main/Artefacts/compose.png?raw=true)


### MacOs/Linux

O sistema contém 3 EntryPoints:
- DailyReport.WebApi
- DailyEntry.WebApi
- DailyReport.Worker


#### Daily Entry

Este repositório é simplesmente um sistema que recebe valores de consumo diariamente (tanto entrada(+) quanto saída(-)).

Após o cadastro do registro, uma notificação é enviada ao MQ que irá armazenar a informação.

OpenAPI
![DailyEntry.png](https://github.com/DiegoModesto/TesteCarrefour/blob/main/Artefacts/DailyEntry.png?raw=true)

RabbitMQ
![rabbit.png](https://github.com/DiegoModesto/TesteCarrefour/blob/main/Artefacts/rabbit.png?raw=true)

### Daily Report

Este projeto contém dois EntryPoints, um que irá gerar exclusivamente o registro de consumo, e armazenar os dados.

Exclusivamente irá inserir os dados recebidos do MQ em uma tabela espeçifica.
![postgres.png](https://github.com/DiegoModesto/TesteCarrefour/blob/main/Artefacts/postgres.png?raw=true)

O outro EntryPoint serve para buscar os dados "consolidados", apenas com valores segmentados e filtrados por TEMPO.

Note que existem um filtro por DATA, e quando retornar, será feita uma consulta que resultada no total consolidado, e uma lista com os registros cadastrados.
![DailyReport.png](https://github.com/DiegoModesto/TesteCarrefour/blob/main/Artefacts/DailyReport.png?raw=true)


Obs.: Estes registros são meramente exemplificando o uso de mensagerias, e consultas de práticas de desenvolvimento, não aconselhamos o uso em ambiente produto sem prévios testes.


