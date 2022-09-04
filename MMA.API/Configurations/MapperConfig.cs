using AutoMapper;
using MMA.API.Data.Entities;
using MMA.API.Models;
using MMA.API.Models.User;

namespace MMA.API.Configurations
{
  public class MapperConfig : Profile
  {
    public MapperConfig()
    {
      CreateMap<dtoClient, entityClient>().ReverseMap();
      CreateMap<dtoClientOnly, entityClient>().ReverseMap();
      CreateMap<dtoClientCreate, entityClient>().ReverseMap();
      CreateMap<dtoClientUpdate, entityClient>().ReverseMap();

      CreateMap<dtoContact, Contact>().ReverseMap();
      CreateMap<dtoContactOnly, Contact>().ReverseMap();
      CreateMap<dtoContactCreate, Contact>().ReverseMap();
      CreateMap<dtoContactUpdate, Contact>().ReverseMap();
      CreateMap<Contact, dtoContactOnly>()
        .ForMember(q => q.ClientName, d => d.MapFrom(map => $"{map.Client.ClientName}"))
        .ReverseMap();


      CreateMap<ApiUser, UserDto>().ReverseMap();


      //CreateMap<AuthorReadOnlyDto, Author>().ReverseMap();
      //CreateMap<AuthorCreateDto, Author>().ReverseMap();
      //CreateMap<AuthorUpdateDto, Author>().ReverseMap();

      //CreateMap<BookCreateDto, Book>().ReverseMap();
      //CreateMap<BookUpdateDto, Book>().ReverseMap();
      //CreateMap<Book, BookReadOnlyDto>()
      //    .ForMember(q => q.AuthorName, d => d.MapFrom(map => $"{map.Author.FirstName} {map.Author.LastName}"))
      //    .ReverseMap();

      //CreateMap<Book, BookDetailsDto>()
      //    .ForMember(q => q.AuthorName, d => d.MapFrom(map => $"{map.Author.FirstName} {map.Author.LastName}"))
      //    .ReverseMap();


    }
  }
}
