# JuntoSeguros
# Intenção

Esse projeto é uma aplicação de teste. De cadastro de pessoa e acesso deste.

## DDD

Esse projeto foi concebido sob a abordagem do que prega o DDD Domain-Driven Design. Houveram duas complexidades: técnica e a de negócio. Pessoa(Person) como domínio principal e Acesso(PersonAcesso) como domínio auxiliar.

## Arquitetura
Pensando em escalabilidade, performance e segurança focando nas diretivas de LGPD, foi elaborada uma arquitetura baseada em micro serviços, com aplicação de Event-Driven e Event-Source. Se observarmos as tabelas do banco, para toda alteração há uma evento aplicado a este e com uma nova versão atribuída a mudança. Para ter maior performance, já que o banco de dados carrega uma complexidade alta, foi adotada um desenho CQRC - Command Query Responsability Segregation, pelo nome, já sabemos que a intenção é o desenvolvimento de api para consulta e outra para alteração de dados diretamente no banco, visando minimizar a concorrência de leitura e escrita na mesma base.

## Banco de Dados
##### Sql Server(user:sa senha:SqlServer2022!) - MongoDB - ElasticSearch

## Mensageria
##### RabbitMQ (user:guest senha:guest)

## Observabilidade
Nesse projeto aplicamos logs e telemetria.
##### Kibana(visualização dos logs) - Jaeger(telemetria)

## Premissas
O projeto foi desenvolvido com aquilo que prega o bom desenvolvimento de software. Houve uma preocupação, não somente com arquitetura da solução, mas também a de software. Observando código, podemos ver uma separação em camadas, onde a domain só é acessada via porta, e quem quer acessar ou fornecer algo que sirva de apoio as regras, deverá se adaptar as portas. O que foi mencionado é a arquitetura hexagonal. Foi aplicado os princípios do SOLID. Além, claro, de uma alta cobertura de teste unitários, e um pequeno teste de integração para validar os repositórios nas operações com banco de dados.

## Considerações
Para apresentar esse projetos, houve pouco tempo, com isso não organizei algumas coisas que vejo como necessárias, tais: cada serviço deveria está numa solution, coisas comuns, deveria está num gerenciador de pacotes interno "da empresa", nesse caso subir uma imagem do nuget no Docker para essa finalidade. Com isso, não ficou, do meu ponto de vista, com um nível bom de organização.

## Conteinerização
A forma como a aplicação foi desenvolvida permite a orquestração de container, por exemplo, com Kubernets. Foi tentado emular essa situação com docker-compose.

## Inicialização do Projeto
Na raiz do projeto, assim que feito o pull request, acesse do diretório Docker, abra o terminal e execute o comando abaixo.
```bash
docker-compose up -d
```
Demora um pouco, pois no momento da execução várias imagem do Docker são inicializadas. Mas a mais custosa é a do SqlServer, é necessário esperar para poder executar o script de criação de base de dados e inserção de dados iniciais. Para garantir o ambiente correto, verifique se a base de dados JuntoSegurosOnboarding foi criada. Verifique também o acesso ao Mongo e ao RabbitMQ. Vale a observação, para acesso ao Rabbit foi utilizado o MassTransit, lib que facilita alguma operações em sistema de mensageria, contudo se o Rabbit for reiniciado, corre o risco de não haver mais consumo dos eventos. Com isso a base de leitura não é atualizada com as alterações ocorridas na commands api.
