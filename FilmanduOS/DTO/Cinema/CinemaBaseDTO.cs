using System.ComponentModel.DataAnnotations;

namespace FilmanduOS.DTO.Cinema;

public class CinemaBaseDTO
{

    [Required]
    [StringLength(100, ErrorMessage = "texto acima do limite possivel, por favor inserir um texto menor")]
    public string Nome { get; set; }
}
