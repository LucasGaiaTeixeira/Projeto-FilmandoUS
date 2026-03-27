<h1>**FilmanduOS API**</h1>

<h3>O FilmanduOS é uma Web API robusta desenvolvida em ASP.NET Core para o gerenciamento de catálogos de filmes e músicas. 
  O projeto demonstra a aplicação de padrões modernos de desenvolvimento backend, integração com APIs externas e persistência de dados relacional.</h3>

<h2>Funcionalidades</h2>
# Gerenciamento de Filmes
<p>Cadastro de Filmes: Endpoint POST que recebe dados via corpo da requisição (JSON).</p>
<p>Validação Rigorosa: Uso de Data Annotations para garantir que títulos, durações e gêneros sigam regras de negócio específicas.</p>
<p>Listagem Paginada: Sistema que utiliza Skip e Take para navegar por grandes volumes de dados sem perda de performance.</p>
<p>Busca por ID: Recuperação detalhada que retorna 200 OK para sucessos ou 404 Not Found caso o recurso não exista.</p>

# Gerenciamento de Músicas
<p>Consumo de API Pública: Serviço especializado que busca dados externos para popular o banco de dados automaticamente.</p>
<p></p>Persistência Assíncrona: Uso de Task e await para garantir que as operações de banco de dados não bloqueiem a aplicação.</p>

<h2>Tecnologias e Ferramentas</h2>

<p>Framework: ASP.NET Core 8.0</p>
<p>ORM: Entity Framework Core.</p>
<p>Banco de Dados: MySQL (via Pomelo.EntityFrameworkCore.MySql).</p>
<p>Documentação: Swagger/OpenAPI para testes interativos.</p>
<p>Serialização: System.Text.Json com customização de nomes de propriedades via JsonPropertyName</p>

<h2>Estrutura do Projeto</h2>
<p>/Controllers: Define as rotas e lida com a lógica de entrada/saída HTTP.</p>
<p>/Data: Contém o BancoDados.cs (DbContext), mapeando as classes para tabelas MySQL.</p>
<p>/Models: Entidades Filme e Musica com decorações de validação.</p>
<p>/Service: Camada de serviço (MusicasService) para isolar a lógica de negócio e chamadas externas.</p>
<p>/Migrations: Histórico de versões do esquema do banco de dados.</p>


<h2>Configuração do Banco de Dados (EF Core Migrations)</h2>
<p>Para replicar este projeto, você deve utilizar o Console do Gerenciador de Pacotes (NuGet) ou o Terminal do VS Code:</p>
<h4><strong>Criar a Migração:</strong>
Este comando analisa suas classes de modelo e gera o código C# necessário para criar as tabelas.</h4>
<p>*Add-Migration "NomeDaMigracao" -Context BancoDados</p>
<h4>Atualizar o Banco:
Aplica as mudanças diretamente no seu servidor MySQL.</h4>
<p>Update-Database -Context BancoDados</p>
<h4>Remover Alterações (Opcional):
Caso precise desfazer a última migração antes de aplicá-la.</h4>
<p>Remove-Migration -Context BancoDados</p>

<h2>Como Rodar o projeto</h2>
<p>Instalar Dotnet 8</p>
<p>Primeiramente no arquivo "appsettings.json" irá ter a conectionStrings e la estãos os dados do banco, mude para seu banco</p>
<p>instale os pacotes [Mircrosoft.EntityFrameworkCore - version: 8.0.24], [Microsoft.EntityFrameorkCore.Tools - versions: 8.0.24], [Pomelo.EntityFrameworkCore.MySql - version: 8.0.3]</p>
<p>No visual Studio e so rodar sem depurar</p>


<p>No VSCODE (terminal)</p>
<ol>
  <li>dotnet Restore</li>
  <li>dotner Run</li>
</ol>

<p>para dependencias do nugget</p>
<ol>
  <li>(dotnet tool install --global dotnet-ef) se não tiver</li>
  <li>(dotnet ef database update -c BancoDados) para aplicar as configuraçoes ao banco de dados</li>
</ol>


<p></p>
