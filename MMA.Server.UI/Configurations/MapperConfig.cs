using AutoMapper;
using MMA.Server.UI.Services.Base;

namespace MMA.Server.UI.Configurations
{
  public class MapperConfig : Profile
  {
    public MapperConfig()
    {
      CreateMap<DtoClientUpdate, DtoClient>()//.ReverseMap();
        .ForMember(c => c.Contacts, opt => opt.Ignore())
        .ReverseMap();
      CreateMap<DtoClient, DtoClientUpdate>();


      //CreateMap<DtoClientUpdate, DtoClient>().ReverseMap();
      //CreateMap<DtoClientOnly, DtoClientUpdate>().ReverseMap();
      //CreateMap<DtoClient, DtoClientOnly>().ReverseMap();

      //.ForMember(c => c.Contacts, opt => opt.Ignore())
      //.ReverseMap();  

      CreateMap<DtoContactOnly, DtoContactUpdate>().ReverseMap();
      //CreateMap<DtoContact, DtoContactUpdate>().ReverseMap();
      //CreateMap<DtoContact, DtoContactOnly>().ReverseMap();

    }
  }
}
