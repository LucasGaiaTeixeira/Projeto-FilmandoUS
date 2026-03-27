using AutoMapper;
using FilmanduOS.DTO.Cinema;
using FilmanduOS.models;

namespace FilmanduOS.profile;

public class CinemaProfile: Profile
{
    public CinemaProfile()
    {
        CreateMap<CinemaCreateDTO, Cinema>();
        CreateMap<Cinema, CinemaReadDTO>();
            //.ForMember(cinemaDTO => cinemaDTO.EnderecoReadDTO, opt => opt.MapFrom(cinema => cinema.Endereco));
                //caso precise passar para o mapper um nome difente que não esta para ele mapear outro objeto
        CreateMap<CinemaUpdateDTO, Cinema>();
    }
}
