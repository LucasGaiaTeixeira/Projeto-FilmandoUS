using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FilmanduOS.DTO;

public class UpdateDTOMusicas
{
    [Required]
    [JsonPropertyName("song")]
    [StringLength(50)]//diz aqui para ter um certo numero de letras que tem que compor a string recebido do json, diferente do [MaxLenght(50)], que passar o tamanho maximo como instrução para o banco de dados dizendo que o varchar(50)

    public string Nome { get; set; }

    [Required]
    [JsonPropertyName("artist")]
    [StringLength(50)]//diz aqui para ter um certo numero de letras que tem que compor a string recebido do json, diferente do [MaxLenght(50)], que passar o tamanho maximo como instrução para o banco de dados dizendo que o varchar(50)

    public string Artista { get; set; }

    [Required]
    [JsonPropertyName("tempo")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]

    public double Duracao { get; set; }//antes era int no banco e int

    [Required]
    [JsonPropertyName("genre")]
    public string Genero { get; set; }

    [Required]
    [JsonPropertyName("year")]
    public string Ano { get; set; }
}
