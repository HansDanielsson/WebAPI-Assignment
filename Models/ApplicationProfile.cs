using AutoMapper;
using WebAPI_Assignment.Models.Dtos;

namespace WebAPI_Assignment.Models;

public class ApplicationProfile : Profile
{
  public ApplicationProfile()
  {
    CreateMap<Category, CategoryDto>().ForMember(dest => dest.NoteCount, opt => opt.MapFrom(src => src.Notes.Count));
    CreateMap<Note, NoteDto>();
    CreateMap<User, UserDto>();
  }
}
