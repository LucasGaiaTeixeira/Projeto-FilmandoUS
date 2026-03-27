using AutoMapper;
using FilmanduOS.DTO.Endereco;
using FilmanduOS.models;

namespace FilmanduOS.profile;

public class EnderecoProfile : Profile
{
    public EnderecoProfile()
    {
        CreateMap<EnderecoCreateDTO, Endereco>();
        CreateMap<Endereco, EnderecoReadDTO>();
        CreateMap<EnderecoUpdateDTO, Endereco>();
    }
}
