# QuestorTeste API

![Code Coverage](./coverage.svg)[![Build and Push to GHCR](https://github.com/brunomokan/questorsistemas/actions/workflows/deploy-devel.yaml/badge.svg?branch=main)](https://github.com/brunomokan/questorsistemas/actions/workflows/deploy-devel.yaml)

## Stack da Solucao

| Tecnologia | Versao | Finalidade |
|---|---|---|
| C# / .NET | 10.0 (preview) | Linguagem e runtime |
| ASP.NET Core | 10.0 | Framework Web API |
| Entity Framework Core | 10.0.9 | ORM para acesso a dados |
| PostgreSQL | - | Banco de dados relacional |
| Npgsql | 10.0.3 | Provider PostgreSQL para .NET |
| AutoMapper | 16.1.1 | Mapeamento entre DTOs, Models e Entities |
| Swashbuckle | 10.2.1 | Documentacao Swagger/OpenAPI |
| xUnit | 2.9.3 | Framework de testes unitarios |
| Moq | 4.20.72 | Mocking para testes |
| FluentAssertions | 8.10.0 | Assertions fluentes para testes |
| Coverlet | 6.0.4 | Cobertura de codigo |
| Docker | Multi-stage build | Containerizacao da aplicacao |
| GitHub Actions | - | CI/CD (testes, cobertura e deploy) |
| GitHub Container Registry (GHCR) | - | Registro de imagens Docker |

## Arquitetura da Solucao

A aplicacao segue a **Arquitetura Hexagonal** (Ports & Adapters), organizada por entidade de dominio. Cada entidade possui sua propria estrutura de portas, servicos e adaptadores, garantindo o desacoplamento entre as camadas.

[Arquitetura hexagonal - por Márcio Krüger](https://medium.com/@marcio.kgr/arquitetura-hexagonal-8958fb3e5507)

### Estrutura por Entidade

```
Entities/
  {Entidade}/
    Ports/
      I{Entidade}InputPort       # Porta primaria (casos de uso)
      I{Entidade}OutputPort      # Porta secundaria (persistencia)
    Domain/
      Models/                    # Modelos de dominio
      Services/                  # Implementacao dos casos de uso (InputPort)
    Adapters/
      Inbound/
        Controllers/             # Controllers ASP.NET (adaptador primario)
        Dtos/                    # DTOs de request/response
        Mappers/                 # AutoMapper: DTO <-> Model
      Outbound/
        Entities/                # Entidades de banco de dados
        Mappers/                 # AutoMapper: Model <-> Entity
        {Entidade}PersistenceAdapter  # Implementacao do OutputPort
```

### Fluxo de Dados

```
HTTP Request
    |
    v
[Controller] (Adaptador Inbound)
    | DTO -> Model (AutoMapper)
    v
[InputPort] (Interface)
    | implementado por
    v
[Service] (Dominio)
    | chama
    v
[OutputPort] (Interface)
    | implementado por
    v
[PersistenceAdapter] (Adaptador Outbound)
    | Model -> Entity (AutoMapper), EF Core
    v
[PostgreSQL]
```

### Entidades de Dominio

- **Banco**: Representa uma instituicao bancaria (`NomeDoBanco`, `CodigoDoBanco`, `PercentualDeJuros`).
- **Boleto**: Representa um boleto de cobranca, vinculado a um Banco. O servico aplica automaticamente o calculo de juros quando o boleto esta vencido, com base no `PercentualDeJuros` do banco associado.

### Endpoints da API

| Metodo | Rota | Descricao |
|---|---|---|
| POST | `/api/v1/banco` | Criar banco |
| GET | `/api/v1/banco/{codigo}` | Buscar banco por codigo |
| GET | `/api/v1/banco` | Listar todos os bancos |
| POST | `/api/v1/boleto` | Criar boleto |
| GET | `/api/v1/boleto/{boletoId}` | Buscar boleto por ID |

### Deploy

A aplicacao e containerizada via **Docker** (multi-stage build) e publicada no **GitHub Container Registry (GHCR)** atraves de pipeline **GitHub Actions**, que executa testes, coleta cobertura de codigo e faz o build/push da imagem.
mago@Debian13:~/temp/questorsistemas$