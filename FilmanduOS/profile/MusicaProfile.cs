using AutoMapper;
using FilmanduOS.DTO;
using FilmanduOS.models;

namespace FilmanduOS.profile
{
    public class MusicaProfile : Profile
    {
        public MusicaProfile()
        {
            CreateMap<CreateDTOMusicas, Musica>();
            CreateMap<UpdateDTOMusicas, Musica>();
            CreateMap<Musica, UpdateDTOMusicas>();
        }
    }
}
