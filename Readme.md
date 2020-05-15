## 1. Domain Driven Design

O DDD é uma metodologia de design de software. Essa metodologia tenta o entendimento comum entre os envolvidos no desenvolvimento de software.
Através dela a comunicação entre os especialistas de negócio (POs por exemplo) e o time de desenvolvimento fica mais assertiva.


## 2. Microservices

O que são: É um estilo de arquitetura que decompõe o domínio de uma solução em uma coleção de serviços, os quais, trabalham de forma isolada.

Por exemplo, em uma solução de e-commerce poderiamos ter dois microservices: "Carrinho de Compras" e "Estoque". A comunicação entre eles podem ocorrer, porém as regras de negócio não são compartilhadas entre eles.

* Ganhos:

1. Isolamento de Falhas: As funcionalidades de uma solução ficam mais desacopladas, o que minimiza o impacto negativo entre elas. Por exemplo: caso um microservice fique indisponível os outros não serão afetados diretamente, o que normalmente ocorre em uma aplicação monolítica.

2. Deploys mais rápidos: Os microservices alterados ou criados podem ser publicados no ambiente (DEV/UAT/PRD/etc.) de forma independente, ou seja, não é necessário fazer deploy de toda a solução.

3. Soluções mais escaláveis: Em um momento de workload, por exemplo uma black friday, não é preciso fazer o scaling de toda aplicação, somente dos microservices mais utilizados.

4. Soluções mais heterogeneas: É possível aplicar a tecnologia que mais se aproxima a cada resolução de um problema do microservice.

* Desafios:

1. Monitoramento: Dependendo da solução, podemos ter dezenas, centenas e até mesmo milhares de microservices. O monitoramento passa a ser um item fundamental na gestão de microservices.

2. Modelagem: O tamanho de um microservice pode variar muito, o que pode gerar muitos microservices. Por isso é importante se atentar ao scopo da solução.

3. Comunicação entre serviços: Dependeno da solução, cada milisegundo é necessário. Logo, é importante se preocupar com a latência na comunicação entre os microservices.

4. Fault Tolerance: É importante ter em mente, que em uma arquitetura de microservies pode ocorrer falhas. É importante pensar em utilizar ferramentas que fazerm políticas de retry, circuit breakers, osquestradores de microservices (Kubernets) e services mesh (Istio).

5. Consistência de Dados: Como cada microservice pode ter a sua base de dados, é importante garantir que elas estejam sincronizadas. Por exemplo um microservice "Carrinho de Compras" e "Estoque" podem utilizar o mesmo ID de produto. Se houver uma inconsistência entre eles, a compra em um e-commerce pode não ser realizada.


## 3. Comunicação Sincrona vs Assincrona

Em uma comunicação Sincrona o Recurso A realiza um "request" para o Recurso B, e mantém essa conexão entre eles até que A obtenha o seu "response". O melhor cenário para utilizar esse tipo de comunicação é quando o tempo de resposta é PEQUENO entre os recursos.

Em uma comunicacao Assincrona, o Recuros A realiza um "request" para o Recurso B, porém encerra a conexão estabelecida entre eles. Após o B terminar o processamento desse "request", ele devolve o "response" para A. O melhor cenário para utilizar esse tipo de comunicação é quando o tempo de resposta é ALTO entre os recursos.

## 4. Para executar a Aplicação

* Tenha um banco de dados SQL Server.
* Dentro do projeto *SuperDigital.API* edite a variável de ambiente com connection string do SQL Server.
