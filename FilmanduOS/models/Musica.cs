using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FilmanduOS.models;

public class Musica
{

    [Required]
    [Key]
    public int Id { get; set; }

    [Required]
    [JsonPropertyName("song")]
    public string Nome { get; set; }

    [Required]
    [JsonPropertyName("artist")]
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
