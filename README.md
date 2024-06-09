# JuntoSeguros
# Intenção

Esse projeto é uma aplicação de teste. De cadastro de pessoa e acesso deste.

## DDD

Esse projeto foi concebido sob a abordagem do que prega o DDD Domain-Driven Design. Houveram duas complexidades: técnica e a de negócio. Pessoa(Person) como domínio principal e Acesso(PersonAcesso) como domínio auxiliar.

## Arquitetura
Pensando em escalabilidade, performance e segurança focando em normativas do BACEN, foi elaborada uma arquitetura baseada em micro serviços, com aplicação de Event-Driven e Event-Source. Se observarmos as tabelas do banco, para toda alteração há uma evento aplicado a este e com uma nova versão atribuída a mudança. Para ter maior performance, já que o banco de dados carrega uma complexidade alta, foi adotada um desenho CQRC - Command Query Responsability Segregation, pelo nome, já sabemos que a intenção é o desenvolvimento de api para consulta e outra para alteração de dados diretamente no banco, visando minimizar a concorrência de leitura e escrita na mesma base.

## Banco de Dados
* Sql Server
   - user:sa
   - senha:SqlServer2022!
* MongoDB
* ElasticSearch

## Mensageria
* RabbitMQ
    * user:guest
    * senha:guest

## Observabilidade
Nesse projeto aplicamos logs e telemetria com:
* Kibana
    * Visualização dos logs
* Jaeger
    * Telemetria

## Premissas
O projeto foi desenvolvido com aquilo que prega o bom desenvolvimento de software. Houve uma preocupação, não somente com arquitetura da solução, mas também a de software. Observando código, podemos ver uma separação em camadas, onde a domain só é acessada via porta, e quem quer acessar ou fornecer algo que sirva de apoio as regras, deverá se adaptar as portas. O que foi mencionado é a arquitetura hexagonal. Foi aplicado os princípios do SOLID. Além, claro, de uma alta cobertura de teste unitários, e um pequeno teste de integração para validar os repositórios nas operações com banco de dados.

## Considerações
Para apresentar esse projetos, houve pouco tempo, com isso não organizei algumas coisas que vejo como necessárias, tais: cada serviço deveria está numa solution, coisas comuns, deveria está num gerenciador de pacotes interno "da empresa", nesse caso subir uma imagem do nuget no Docker para essa finalidade. Com isso, não ficou, do meu ponto de vista, com um nível bom de organização.
Outro ponto é que a aplicação está orientada a event-driven, ou seja cada alteração gerará um novo estado que seré persistido em base de dados (event-sourcing), esse alteração gerará eventos, e pontando terá subscribes, no nosso caso consumers. No desenvolvimento desses consumers não foi adicionada resiliência de negócia, ou seja se ocorrer um erro, não há regras de retentativas. Desse modo, como a intenção é atualizar uma base nosql, então na ocorrência do erro, esse dado, atualizado, não estará disponível nas consultas das query-apis. Lembrete caso seja visto.
#### Nesse projeto, por conta do tempo, não foram adicionado:
* Worker em .net core para funcionar como consumer 
* API Service para fonercer serviços de altenticação de usuário
* BFF em NodeJs que validaria a autenticação do usuário para realizar a orquestração da Command e Query API.

## Conteinerização
A forma como a aplicação foi desenvolvida permite a orquestração de container, por exemplo, com Kubernets. Foi tentado emular essa situação com docker-compose.

## Inicialização do Projeto
Na raiz do projeto, assim que feito o pull request, acesse do diretório Docker, abra o terminal e execute o comando abaixo.
```bash
docker-compose up -d
```

Depois de subir o container é necessário validar alguns logs dos containers, sendo:
* Para garantir que o SqlServer esteja iniciado e com o banco de dados JuntoSegurosOnboarding, verifique no container **mssqltools** os seguintes logs:
![mssqltools](img/database_alredy.png)

* Acessando o banco de dados via Manangement Studio teremos as seguintes relações de tabelas:
![mssqltools](img/tables_database.png)
Caso o banco não existe, há duas formas de criar, ou colocando o container **mssqltools** para iniciar novamente, até que ele termine o processo, ou abrir o diretório *Docker\MSTools\init\\* e executar no Management Studio:
    * JuntoSegurosOnboarding_Creation.sql
    * JuntoSegurosOnboarding_Insertions.sql

* É necessario que o RabbitMQ estaja iniciado e plugado as aplicações, para isso devermos verificar no container **rabbitmq** os seguintes logs:
![mssqltools](img/connection_rabbit_ok.png)

