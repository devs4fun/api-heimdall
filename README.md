# api-heimdall

Criado por: @robsonjunior1994
24/11/2020

#INICIO DO PROJETO
Começamos o projeto api-heimdall com intuito de obter mais conhecimento em programação e criar uma rotina de estudos, iniciamos a primeira semana criando diagramas de fluxo, criamos um diagrama de fluxo geral, para dar uma ideia realmente geral da API e um diagrama de fluxo para cada endpoint que conseguimos pensar inicialmente:

- CADASTRO DE USUÁRIO;
- GERAR TOKEN DE ATIVAÇÃO DE CONTA;
- ATIVAR USUÁRIO;
- FAZER LOGIN;
- VALIDAR KEY DE LOGIN.

#A IDEIA 
É bem simples, criar uma API responsável por cadastrar, ativar e fazer login de usuários em uma só API, que vai ser responsável por outros projetos nossos que vão precisar de login e cadastro. A API é simples o usuário vai enviar os dados como, NOME COMPLETO, APELIDO (USERNAME), E-MAIL e SENHA. 
Ao enviar corretamente esses dados o usuário ainda vai precisar validar a conta dele, iremos enviar um link para a caixa de mensagem do e-mail do usuário, se o e-mail for válido e o usuário clicar nesse link ele estará ativo, em seguida ele poderá fazer login e esse usuário só pode logar em uma máquina/navegador por vez, porque se ele fizer login em outra máquina ou navegador a sua key atual que é armazenada no cliente (navegador), será renovada e perderá o login naquele cliente.

#REQUISITOS 
Para rodar o projeto:
- Asp.net core 3.1;
- MySQL;
- MySQL Workbench 8.0 CE instalado na máquina;

#SEGUNDA ETAPA
Foi iniciado a Issue para configuração inicial do projeto que contempla criar as classes, interfaces e classes de repository definidas na etapa de desenho do projeto e já deixar pronto a injeção de dependência, que nesse momento vamos usar a opção de SCOPED pôs quero que uma instância seja criada a cada requisição para o aplicativo.
Referências:
- https://medium.com/@nelson.souza/net-core-dependency-injection-1c1900d1bef
-https://www.treinaweb.com.br/blog/o-que-e-http-request-get-post-response-200-404/
