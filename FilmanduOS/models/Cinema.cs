using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmanduOS.models;

public class Cinema
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(100, ErrorMessage = "Nome de filme acima do limite esperado, passar um novo nome por favor")]
    public string Nome { get; set; }

    
    public int EnderecoId { get; set; }
    [ForeignKey("EnderecoId")]//pode usar o Foreign Key passando o nome correto da propriedade, porque o entity framework ele ler a propriedade e detecta que é da classe Endereco e tem tbm id entao, vem da classe Endereco e tem uma propriedade id ficando EnderecoId ou passando pela foreign Key o nome correto no virtual para mostrar para o framework qual a propriedade vai ser a coluna de Foreign Key mas tem que passar o nome da propriedade na [ForeignKey("")] se nao o entity nao acha
    public virtual Endereco Endereco { get; set; } // quer dizer que ele vai disparar quando for acessada a tabela Cinema vai puxar os dados da tabela Endereco com o id da coluna FK que tem que no caso é a EnderecoId
    
}
