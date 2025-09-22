
# FCG - FIAP Cloud Games

## Resumo

Este repositório contém um microsserviço desenvolvido em .NET para o gerenciamento de jogos na FGC (FIAP Cloud Games). O projeto adota a Arquitetura Hexagonal (Ports & Adapters), promovendo desacoplamento entre regras de negócio e infraestrutura, facilitando testes, manutenção e integração com diferentes tecnologias. Utiliza ElasticSearch como mecanismo de persistência e expõe uma API REST para operações relacionadas a jogos.

---

**Microsserviço para gerenciamento de Jogos na FGC**

## Índice

	 - [Resumo](#resumo)
	 - [Visão Geral](#visão-geral)
	 - [Camadas do Projeto](#camadas-do-projeto)
	 - [Estrutura de Pastas](#estrutura-de-pastas)
	 - [Principais Componentes](#principais-componentes)
	 - [Testes](#testes)
	 - [Como Executar](#como-executar)
	 - [Observações](#observações)
	 - [Referências](#referências)

## Visão Geral

Este projeto é um microsserviço responsável pelo gerenciamento de jogos na FGC, desenvolvido em .NET. Ele segue os princípios da Arquitetura Hexagonal (Ports & Adapters), promovendo separação de responsabilidades, testabilidade e flexibilidade para integração com diferentes tecnologias.


### Camadas do Projeto

- **Domínio (`FCG.Games.Domain`)**: Contém as entidades de negócio, regras e interfaces (ports) que definem contratos para operações essenciais, como repositórios.
- **Aplicação (`FCG.Games.Application`)**: Implementa os casos de uso (use cases) do sistema, orquestrando as operações do domínio e validando dados de entrada.
- **Infraestrutura (`FCG.Games.Infrastructure.ElasticSearch`)**: Implementa os adaptadores (adapters) para tecnologias externas, como persistência em ElasticSearch, seguindo os contratos definidos no domínio.
- **API (`FCG.Games.Api`)**: Camada de apresentação, expõe endpoints HTTP, recebe requisições, faz a orquestração dos casos de uso e retorna respostas.

### Estrutura de Pastas


| Pasta                                 | Papel na Arquitetura Hexagonal                | Tipo         |
|----------------------------------------|-----------------------------------------------|--------------|
| `FCG.Games.Domain/`                    | Núcleo do domínio, entidades, regras, ports   | Domínio/Port |
| `FCG.Games.Application/`               | Casos de uso, validações                      | Aplicação    |
| `FCG.Games.Infrastructure.ElasticSearch/` | Integrações externas, persistência, adapters | Adapter      |
| `FCG.Games.Api/`                       | API REST, controllers, entrada do sistema     | Adapter      |

> As pastas de infraestrutura e API representam os "adapters" da arquitetura hexagonal, conectando o núcleo do domínio a tecnologias externas (bancos, frameworks, protocolos, etc) e à interface de entrada (HTTP/API).

> O projeto também possui uma pasta `tests/` na raiz, contendo testes unitários e de integração organizados por contexto.

## Principais Componentes

- **Entidades**: Representam os objetos de negócio (ex: `Game`, `Catalog`, `User`).
- **Use Cases**: Implementam operações como criação de jogos (`CreateGameUseCase`).
- **Repositórios**: Interfaces no domínio e implementações na infraestrutura (ex: `IGameRepository`, `GameRepository`).
- **Controllers**: Exposição dos endpoints REST (ex: `GamesController`).
- **Validações**: Garantem integridade dos dados de entrada.

## Testes

O projeto possui testes unitários e de integração localizados na pasta `tests/`, cobrindo casos de uso, validações e endpoints da API.

## Como Executar

1. Clone o repositório:
	```sh
	git clone https://github.com/PauloBusch/fcg-games-microservice
	```
2. Restaure as dependências:
	```sh
	dotnet restore
	```
3. Execute a aplicação:
	```sh
	dotnet run --project src/FCG.Games.Api/FCG.Games.Api.csproj
	```
4. Acesse a API via:
	```
	http://localhost:5000
	```

## Observações

- O projeto utiliza ElasticSearch como mecanismo de persistência.
- Configurações adicionais podem ser feitas nos arquivos `appsettings.*.json`.

### Referências

- Este projeto foi derivado e evoluído a partir do MVP disponível em: [leticia-kojima/tech-challenge-net-phase-1](https://github.com/leticia-kojima/tech-challenge-net-phase-1)

---
Desenvolvido seguindo boas práticas de arquitetura para facilitar manutenção, testes e evolução.
