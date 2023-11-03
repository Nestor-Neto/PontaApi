# API REST- Projeto de Exemplo com Aplicação .NET Core/DDD/MVC 📜

## ☕ Escopo: 
A API RESTful que permitirá aos usuários gerenciar uma lista de tarefas, a API deve seguir as melhores práticas de desenvolvimento, incluindo boas práticas de arquitetura, segurança e documentação. 

## 💻 Pré-requisitos
- instalar a Versão donet 6.0 e visual studio (https://visualstudio.microsoft.com/pt-br/vs/community/) ou Visual Studio Code (https://code.visualstudio.com/download)
- Instalar posgresSql [https://www.enterprisedb.com/downloads/postgres-postgresql-downloads].

# posgresSql 
Após instalar e configurar criar uma senha no posgresSql. 
- Abrar o projeto e configure a senha e a porta(padrão 5433) no ConnectionString do arquivo appsettings.json do projeto Application.
- Ex: "ConnectionString": "User ID=postgres;Password=1234;Host=localhost;Port=5432;Database=PontaBD;Pooling=true;Connection Lifetime=0;Include Error Detail=true;",

## 📝 ORM Entity Framework 
O EF funciona com diversos banco de dados. O ORM, facilita o acesso ao banco de dados, mapeando suas tabelas.
- Abra o prompt de comando e ponte para o projeto Infrastruture e execute as migrations, que irá adicionar o banco as tabelas.
- Ex:
|Console            |	Description                                              |	
|-------------------|------------------------------------------------------------|
|add-migration      |	Create a new migration with the specific migration name. |	                                  | 
|update-database    |	Update the database to the latest migration.             | 


# 🚀 Primeiro acesso a API
Com o projeto em execução crie o primeiro usuário no endpoint (/Login/Insert).
- Exemplo: 
-{
-  "nome": "Nestor",
-  "login": "nestor.neto",
-  "senha": "123"
-}

## 🚀 Autenticação e Autorização do usuário
- A API suporta autenticação de usuários. 
- Apenas os criadores das tarefas devem poder atualizá-las ou excluí-las. 
- As tarefas é visíveis a todos os usuários autenticados.
# Token
 Primeiro passo: Com o projeto em execução crie o token no endpoint (/Login/Login), informando o login e a senha, a API retornará seu token.
- Exemplo: 
- Envio
- {
-   "login": "nestor.neto",
-   "senha": "123"
- }
- retorno da API
- {
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwidW5pcXVlX25hbWUiOiJuZXN0b3IubmV0byIsIm5iZiI6MTY5OTAzODgyMSwiZXhwIjoxNjk5MDUzMjIxLCJpYXQiOjE2OTkwMzg4MjF9.oubC2b2e0UBfLBQcwIJ7StiBZWMDogh50zfdFY1HOmE"
}
- Copie o Token.

# Autenticação
Com o projeto em execução, clique no botão authorize da API(icone de cadeado), informe o token no campo (Value)que foi gerado no Primeiro passo. 
Exemplo:(Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwidW5pcXVlX25hbWUiOiJuZXN0b3IubmV0byIsIm5iZiI6MTY5OTAzODgyMSwiZXhwIjoxNjk5MDUzMjIxLCJpYXQiOjE2OTkwMzg4MjF9.oubC2b2e0UBfLBQcwIJ7StiBZWMDogh50zfdFY1HOmE).

## 📝 Documentação: 
•	A API esta configurada e rodando no Swagger ou pode ser configurada em ferramenta similar. 
* https://localhost:7096/swagger/index.html

## CRUDs 
•	Crie(endpoint: /Tarefas/Insert), listar (endpoint: /Tarefas/ListAll), atualize endpoint: /Tarefas/Update) e exclua tarefas endpoint: /Tarefas/Delete). 
•	Cada tarefa tem um título, uma descrição, uma data de criação e um status.
Os status são:
	• 0 – Pendente.
	• 1 – Em Andamento.
	• 2 – Concluído.
•	Os usuários podem listar (endpoint: /Tarefas/ListAll) todas as tarefas.
•	Os usuários podem filtrá-las com base em seu status (endpoint: /Tarefas/SelectStatus). 


## 🚀 Testes Unitários: 
Foi criado o projeto para testes da API. Com a intenção de garantindo a qualidade do código. Eles ajudam a identificar erros, documentam o código, facilitam a refatoração e proporcionam confiabilidade ao software
Projeto:
•	Tests: Contém o código-fonte da camada de aplicação.

## Logs: 
•	Foi implementado logs para rastrear eventos importantes na API, então quanto iniciar a aplicação será gerado o monitoramento e rastreamento de toda aplicação em arquivo.txt


## Estrutura do projeto

## Mecanismos Arquiteturiais

|Análise            |	Design                                |	Implementação      |
|-------------------|-----------------------------------------|------------------|
|Persistência       |	ORM	                                  | Entity Framework   |
|Persistência       |	Banco de dados relacional             | Postgres           |
|Back-end	        |  Arquitetura em camadas                 |	.Net Core 6        |
|Versionamento      |	Versionamento do código das aplicações|	GitHub             |
|Documentação de API|Solução para documentação das APIs da solução|	    Swagger  |

## 📝 Estrutura DDD (Domain-Driven Design)

•	Application: Contém o código-fonte da camada de aplicação, que é responsável por receber as requisições e direcioná-las para as outras camadas.
•	Domain: Contém o código-fonte da camada de Domínio, definido através do padrão Domain Model, aqui foi definido modelo de negócios em termos de classes.
•	Service: Contém o código-fonte da camada de serviço,que contém regras e comportamentos referentes ao Modelo de domínio.
•	Infrastructure: Contém o código-fonte da camada de Infraestrutura. Os repositórios de dados são definidos através de um padrão: o padrão Repository, onde o Modelo do domínio está livre de qualquer definição de infraestrutura de dados.


## 📝 Licença

Esse projeto está sob licença. Veja o arquivo [LICENÇA](LICENSE.md) para mais detalhes.