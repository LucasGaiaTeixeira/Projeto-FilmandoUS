using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FilmanduOS.models
{
    public class Filme
    {
        [Key]//aqui no caso o atributo Key é usado para indicar que a propriedade Id é a chave primária da entidade Filme. Isso significa que o valor da propriedade Id será único para cada instância de Filme e será usado para identificar de forma exclusiva cada filme no banco de dados ou em qualquer outra estrutura de armazenamento que esteja sendo utilizada. O atributo Key é comumente usado em frameworks de mapeamento objeto-relacional (ORM) para definir a chave primária de uma entidade.
        [Required]
        public int Id { get; set; }


        //esse Required é um atributo de validação que indica que a propriedade Nome é obrigatória. Isso significa que, ao tentar criar ou atualizar um objeto do tipo Filme, o valor da propriedade Nome deve ser fornecido e não pode ser nulo ou vazio. Se o valor for nulo ou vazio, a validação falhará e uma mensagem de erro será gerada indicando que o campo Nome é obrigatório.
        //esse maxLength é outro atributo de validação que especifica o comprimento máximo permitido para a propriedade Nome. No caso, o valor máximo é 65 caracteres. Se o valor fornecido para a propriedade Nome exceder esse limite, a validação falhará e uma mensagem de erro será gerada indicando que o tamanho do nome está muito grande e solicitando que seja inserido o nome correto.
        //esse range é um atributo de validação que especifica um intervalo válido para a propriedade Duracao. No caso, o valor mínimo permitido é 70 e o valor máximo permitido é 300. Se o valor fornecido para a propriedade Duracao estiver fora desse intervalo, a validação falhará e uma mensagem de erro será gerada indicando que o tempo de duração é inválido e solicitando que seja inserido um valor dentro do intervalo permitido.
        [Required(ErrorMessage ="precisa do titulo")]
        [MaxLength(65, ErrorMessage ="o tamanho do nome esta muito grande ensira o nome correto")]
        //outra coisa quando for no dto que para maxLenght é para dizer qual o tamanho do banco em ORM usamos no dto [StringLength(65, errorMessage = "")] que e para dizer que a string tem que ter esse tamanho mas,não tem poder de alterar  em nada o banco de dados diferente da que esta porque não fiz o DTO
        [JsonPropertyName("Name")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "precisa da descrição")]
        [MaxLength(500, ErrorMessage = "o tamanho da descricao esta muito grande ensira a descricao correta")]
        [JsonPropertyName("Description")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "precisa do tempo de duração")]
        [Range(70, 300, ErrorMessage ="tempo não permitido, ou e muito curto ou muito longo o filme por favor inserir o tempo certo")]
        [JsonPropertyName("Time")]
        public int Duracao { get; set; }

        [Required(ErrorMessage = "precisa do genero")]
        [JsonPropertyName("Gender")]
        public string Genero { get; set; }
        

        [JsonPropertyName("Actors")]
        public List<string>? Atores { get; set; }

        
        

    }
}
