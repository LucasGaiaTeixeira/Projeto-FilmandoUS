using FilmanduOS.models;
using Microsoft.EntityFrameworkCore;

namespace FilmanduOS.Data;

public class BancoDados: DbContext
{
    public BancoDados(DbContextOptions<BancoDados> options) :base(options)
    {

    }


    public DbSet<Filme> Filmes { get; set; } //esse DbSet é a representação da tabela no banco de dados, onde o nome da tabela será "Filmes" e a classe "Filme" representa as colunas dessa tabela.

    public DbSet<Musica> Musicas { get; set; } //esse DbSet é a representação da tabela no banco de dados, onde o nome da tabela será "Musicas" e a classe "Musica" representa as colunas dessa tabela.
    
    public DbSet<Cinema> Cinemas { get; set; } // esse DbSet e a tabela de  Cinemas

    public DbSet<Endereco> Enderecos { get; set; } // tabela de Enderecos
}
//agr para fazer a migração do programa c# para o banco precisa de o terminal nugget com o comando: Add-Migration "nome da migração" -Context BancoDados, depois disso para atualizar o banco de dados com a migração criada, use o comando "Update-Database" -Context BancoDados para atualizar e inserir no banco de dado as modificações que foram feitas. Esses comandos são usados para criar e aplicar migrações no Entity Framework Core, que é um framework de mapeamento objeto-relacional (ORM) para .NET. A migração é uma maneira de atualizar o esquema do banco de dados para refletir as mudanças feitas nas classes de modelo do aplicativo.
//Remove-Migration "nome da migração" -Context BancoDados, esse comando é usado para remover uma migração específica do projeto. Ele desfaz as alterações feitas pela migração e remove o arquivo de migração correspondente. O parâmetro "nome da migração" deve ser substituído pelo nome da migração que você deseja remover, e o parâmetro "-Context BancoDados" especifica o contexto do banco de dados para o qual a migração foi criada.