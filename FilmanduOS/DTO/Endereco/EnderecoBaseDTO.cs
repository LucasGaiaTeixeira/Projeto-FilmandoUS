using System.ComponentModel.DataAnnotations;

namespace FilmanduOS.DTO.Endereco
{
    public class EnderecoBaseDTO
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string Logradouro { get; set; }
    }
}
