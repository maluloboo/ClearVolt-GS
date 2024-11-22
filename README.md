
# ClearVolt
#### Sistema de Limpeza Automática para Placas Solares

## Instruções
1. Copie o link do repositório.
2. Cole o link: `https://github.com/maluloboo/ClearVolt-GS.git`.
3. Clone o repositório para sua IDE.
4. Instale as dependências necessárias com o comando `dotnet restore`.
5. Em "appsettings.json" altere "Id" e o "Password" para suas credenciais.
6. Localize seu Package Manager Console digite `update-database` e pressione enter.
7. No arquivo ClearVoltIAService dentro de DataIA, na linha 18, atlere o caminho para o correspondente a sua máquina.
8. Rode a API.
9. É necessário começar o create com Pessoa, Role, Usuario, ConfiguracaoDeColeta, Dispositivo, DadoColetado

As mudanças climáticas e a busca por fontes de energia renováveis têm colocado as placas solares como uma das principais alternativas para um futuro mais sustentável. No entanto, fatores como acúmulo de sujeira, poeira, poluição e altas temperaturas podem reduzir significativamente a eficiência energética desses equipamentos. Foi com esse cenário em mente que desenvolvemos o ClearVolt, um Sistema de limpeza automática para placas solares, uma solução completa e inovadora para otimizar a performance e o gerenciamento das placas solares.
## Nossa Solução
O ClearVolt é composto por dispositivos inteligentes que são acoplados às placas solares e possuem sensores para monitorar a temperatura e a umidade do ambiente em tempo real. Com base nos dados coletados, o sistema toma medidas automáticas para garantir o melhor desempenho das placas. As principais funcionalidades incluem:

- Limpeza Automática: Disparos programados de jatos de água ou ar para remover sujeira acumulada, evitando manutenções frequentes e o desgaste dos equipamentos.
- Resfriamento Inteligente: Em locais com temperaturas muito elevadas, o dispositivo ativa jatos de água para resfriar as placas, preservando sua eficiência.

Além disso, o ClearVoltpermite um alto grau de personalização para atender às necessidades específicas de cada cliente:
- Definição de Rotinas: O cliente pode programar ciclos de limpeza semanais diretamente pelo site ou aplicativo.
- Configurações Personalizadas: É possível ajustar os níveis de temperatura ou umidade que acionam automaticamente os jatos de água ou ar.
Programação Pré-definida: Para clientes que preferem praticidade, o sistema vem com rotinas otimizadas de fábrica, eliminando a necessidade de ajustes manuais.

## Plataformas de Monitoramento
Nosso sistema é integrado a uma plataforma web e a um aplicativo móvel, que proporcionam total controle e acompanhamento em tempo real. Por meio dessas ferramentas, o cliente pode:
- Acompanhar o status das placas solares.
- Receber notificações sobre a ativação dos jatos ou mudanças climáticas que possam impactar o sistema.
- Configurar rotinas ou ajustes de forma prática e intuitiva, seja no desktop ou na palma da mão.
## Impacto e Benefícios
Com o ClearVolt, proporcionamos um aumento expressivo na eficiência energética das placas solares, reduzindo o desperdício de recursos naturais e os custos operacionais associados à manutenção manual. Nosso sistema também contribui para a preservação dos equipamentos, prolongando sua vida útil e tornando o uso da energia solar mais acessível e sustentável.

## Apoio à Sustentabilidade e Inovação
O desenvolvimento do ClearVolt está alinhado aos Objetivos de Desenvolvimento Sustentável (ODS), especialmente no que diz respeito à energia limpa e acessível (ODS 7). A inovação tecnológica da solução reforça nosso compromisso em promover um impacto positivo na sociedade e no meio ambiente.

# Integração de IA no Projeto ClearVolt

## Como a IA Funciona

A Inteligência Artificial (IA) utilizada no projeto **ClearVolt** tem como objetivo prever condições ambientais baseadas em dados históricos de temperatura e umidade. A IA é alimentada com dados de sensores e utiliza um modelo de **regressão** para prever o comportamento de variáveis como **temperatura** e **umidade** no futuro. A principal aplicação dessa previsão é em alertas e ajustes de controle automático em ambientes monitorados.

### Fluxo de Funcionamento

1. **Dados de Treinamento**:
   O modelo é treinado com dados históricos coletados de sensores. Esses dados incluem valores de **temperatura**, **umidade** e o **label**, que é a variável que queremos prever. Por exemplo:
   - Temperatura = 22°C, Umidade = 60%, Label = 25 (Valor esperado para a variável de previsão)

2. **Modelo de Regressão**:
   Utilizamos o algoritmo **SDCA (Stochastic Dual Coordinate Ascent)**, que é uma técnica de regressão, para construir um modelo de previsão. Este modelo analisa as relações entre a **temperatura** e a **umidade** para prever o valor de uma variável dependente (o **Label**).

3. **Treinamento e Validação**:
   Durante o processo de treinamento, o modelo ajusta seus parâmetros com base nos dados fornecidos. O objetivo é minimizar o erro de previsão para que ele consiga fazer previsões precisas para dados futuros.

4. **Predição**:
   Após o treinamento, o modelo pode ser utilizado para prever a temperatura e a umidade com base em novas entradas de dados. Quando novos dados de **temperatura** e **umidade** são fornecidos, o modelo gera previsões para essas variáveis.

5. **Geração de Alertas**:
   A IA também é utilizada para gerar **alertas** baseados nas previsões feitas. Por exemplo:
   - Se a previsão de **temperatura** exceder um limite máximo configurado, um alerta será gerado.
   - Se a previsão de **umidade** estiver abaixo de um limite mínimo, outro alerta será gerado.

## Como a IA Agrega Valor ao Projeto

A implementação da IA no **ClearVolt** traz diversos benefícios que agregam valor ao projeto e melhoram a eficiência operacional:

1. **Previsão Antecipada**:
   Com a IA, podemos prever com antecedência mudanças nas condições ambientais. Isso permite que as equipes de operação ajustem os controles ou implementem ações corretivas antes que os valores se tornem críticos, otimizando o tempo de resposta.

2. **Automatização de Alertas**:
   O sistema de **alertas automáticos** baseado em IA torna a operação mais proativa. Ao identificar condições fora dos parâmetros estabelecidos, o sistema envia notificações em tempo real, o que ajuda a evitar falhas e danos aos equipamentos.

3. **Melhoria na Eficiência Operacional**:
   A previsão precisa de variáveis como **temperatura** e **umidade** permite ajustes automáticos em sistemas de climatização, irrigação ou outros sistemas baseados nas condições do ambiente, melhorando a eficiência energética e operacional.

4. **Redução de Custos**:
   Ao permitir o monitoramento remoto e a automação, a IA pode reduzir a necessidade de intervenção humana constante, economizando tempo e recursos. Além disso, previne falhas de equipamentos e minimiza a necessidade de manutenção emergencial.

5. **Otimização de Recursos**:
   O uso da IA pode ajudar a identificar padrões e otimizar os recursos consumidos, como energia e água, ajustando automaticamente as condições ambientais para manter a operação dentro dos parâmetros ideais.

6. **Escalabilidade e Adaptabilidade**:
   A IA é altamente escalável, permitindo que o modelo se adapte facilmente a novos dados e cenários. Isso torna o sistema robusto e preparado para lidar com diferentes ambientes e mudanças nas condições ao longo do tempo.

## Conclusão

A integração da **Inteligência Artificial** no **ClearVolt** não só torna o sistema mais inteligente e autônomo, mas também aprimora a capacidade de prever e reagir a condições ambientais, o que resulta em uma operação mais eficiente, segura e econômica. A previsão de variáveis como **temperatura** e **umidade**, juntamente com a geração de alertas automáticos, contribui significativamente para o sucesso do projeto e a otimização dos recursos.

# Design Patterns

## Controller
Dentro do padrão MVC (Model-View-Controller), as **Controllers** funcionam como a interface entre as requisições dos usuários e a lógica de negócios. Elas são responsáveis por receber as solicitações HTTP, interagir com os serviços apropriados e devolver a resposta adequada ao cliente.

## Service Layer
A **Camada de Serviço** centraliza a lógica de negócios, isolando-a das outras camadas da aplicação. Isso ajuda a manter as **Controllers** mais simples, delegando funções complexas para serviços especializados. Cada serviço geralmente implementa uma interface própria, garantindo uma estrutura desacoplada e facilitando a realização de testes.

## Repository
O padrão **Repository** busca separar a lógica de acesso a dados da lógica de negócios. Embora não existam repositórios explícitos no projeto, os serviços desempenham essa função, acessando o banco de dados através do **AppDbContext** e fornecendo métodos de alto nível para manipulação e persistência de dados.

## Dependency Injection
A **Injeção de Dependência** facilita a criação e o gerenciamento de objetos dentro da aplicação, garantindo que os componentes necessários sejam injetados de forma transparente. Este padrão promove o desacoplamento entre as classes e facilita a realização de testes, pois as dependências podem ser simuladas ou substituídas durante os testes.

## DTO
O padrão **DTO (Data Transfer Object)** é usado para transferir dados entre as camadas da aplicação. Ele define objetos de dados que são trocados entre o frontend e o backend, controlando quais informações são expostas e assegurando que apenas dados necessários sejam enviados ou recebidos pela API.

## Models
No padrão MVC, os **Models** representam os dados e a lógica de negócios. Eles são responsáveis por interagir diretamente com o banco de dados através do **AppDbContext**, encapsulando a estrutura dos dados que serão manipulados pela aplicação e garantindo uma comunicação eficiente com o banco.

# Integrantes
- Gabriel Eduardo De Paiva Oliveira - RM: 98843
- Macirander Souza De Miranda Filho - RM: 551416
- Maria Luiza de Oliveira Lobo      - RM: 552169
- Matheus Felipe Camarinha Duarte   - RM: 552295
- Munir Jamil Mahmoud Ayoub         - RM: 555893
