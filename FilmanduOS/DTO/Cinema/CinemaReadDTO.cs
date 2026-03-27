using FilmanduOS.DTO.Endereco;

namespace FilmanduOS.DTO.Cinema
{
    public class CinemaReadDTO: CinemaBaseDTO
    {
        public EnderecoReadDTO Endereco { get; set; }
    }
}
