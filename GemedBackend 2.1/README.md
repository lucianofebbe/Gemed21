# Introduction 
Gemed, Sistema para controle de pacientes para clinicas e hospitais

# Getting Started
Sistema desenvolvido utilizando os conceitos de microservicos, Mediator e cqrs. Microservicos se comunicam atraves de menssageria, RabbitMQ.<br>
Midleware onde eh feito o redirecionamento das requisições vindas do front e ou mobile para os devidos microservicos.<br>
Sistema rodando sobre container em docker e orquestrador por kubernetes.<br>
Sistema utiliza base de dados SqlServer para dados chaves como dados do paciente, dados das empresa.<br>
Sistema utilia MongoDb como forma de cache para dados necessarios para o funcionamento do servico como configuraçoes de email, logs de erros, rotas dos microservios autenticacoes.

#Instalação:
##Diretório "GemedBackend 2.1":
	contem um arquivo docker-compose onde o mesmo possui referencias para os Dockerfile de cada microservico.
##Diretório "GemedBackend 2.1":
	contem um arquivo docker-compose-mongo onde o mesmo possui as configurações do dbMongo.

##Diretório "GemedBackend 2.1\_IP.BasesMicroServices":
	contem a estrutura base de todos microServicos, caso futuramente precise criar novos, somente copiar os seguintes diretórios para o novo MicroServiço.
	"Application, Domain, Infrastructure, Services"

##Diretório "GemedBackend 2.1\_IP.BasesMicroServices\BaseDadosCacheMongo":
	Contem o backup dos dados de cada coleção do mongoDb, tambem possui um Dockerfile para a instalação. MongoDb roda sobre um container próprio assim mantem as informações
	em caso do kubernetes subir novos containers dos MicroServicos preservando a autenticacao.
 
###Para rodar, utilize os seguinte commandos:
	Crie a rede que os servicos iram se comunicar:
		docker network create --subnet=172.20.0.0/16 broker-Gemed21
	Rodar os dockers composes
		Servicos: C:\...\Gemed21\GemedBackend 2.1
		Mongo: C:\...\Gemed21\GemedBackend 2.1_MongoDb
			Criar o banco de dados com o nome de "DbCache" para uso em desenvolvimento.
			Para produção pode utilizar o mesmo nome ou criar outro.
			Nao esquecer de remover as portas de acesso externas para utiizar em produção.
			Inserir as seguintes coleções: Autenticacoes, Permissoes, TipoEmail, TipoEmpresa, .ConfigMails e TipoRequisicao.
			Nesse diretório possui os arquivos de importação com as configurações padrões, importar as mesmas.
			ConnectionString padrão: "mongodb://Gemed2022:Gemed2022Teste@localhost:27018/?authSource=admin"

#Dependencias:
##Diretório "GemedBackend 2.1\IP.Infrastructure":
	Contem todas as funcionalidas mais comuns entre os microServicos.
	Foram concentradas nessa solução afim de facilitar a implementacao das funcionalidades e abstrair complexidades desnecessarias dos microServicos
		afim de deixar os mesmos somente com as regras de negócio.
	Dentro do diretório existe um arquivo Bash, onde o mesmo compila os projetos dentro da solução e distribui entre os microServicos.
	Caso precise criar um novo MicroServiço, abrir o bash e editar a lista de destino inserindo o novo microServico.
	Cada microservico contem um diretório onde fica as Dlls da solução. "GemedBackend 2.1\IP.Usuario\Infrastructure\IP.Dlls.Infrastructure"

#Exemplo de requisições:
##EndPoint:
	https://localhost:7255/GatewayHandler
##JsonBody:
{
  "TipoRequisicao": "Autenticacao",
  "TipoEmpresa": "",
  "Empresa":"",
  "EndPoint": "AutenticacaoHandler",
  "Objeto": {
	  "id": 0,
	  "cpf": "5867858",
	  "senha": "zYb618"
		}
}

{
  "TipoRequisicao": "Usuario",
  "TipoEmpresa": "GemedCardio",
  "Empresa":"EcoFetal",
  "EndPoint": "MenuHandler",
  "Token":"660c4780d06037dc70056a9d",
  "RefreshToken":"660c4780d06037dc70056a9c",
  "Objeto": {
	}
}

TipoRequisicao -> Microservico Que precisa ser chamado.<br>
EndPoint -> EndPoint devera ser chamado na requisicao.<br>
TipoEmpresa -> Tipo de empresa o sistema estah fazendo a requisicao.<br>
Empresa -> Empresa esta fazendo a requisicao.<br>
Objeto -> Contem as chaves e valores da requisicao.<br>