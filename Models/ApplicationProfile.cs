using AutoMapper;
using WebAPI_Assignment.Models.Dtos;

namespace WebAPI_Assignment.Models;

public class ApplicationProfile : Profile
{
  public ApplicationProfile()
  {
    CreateMap<Category, CategoryDto>();
    CreateMap<Note, NoteDto>();
    CreateMap<User, UserDto>();
  }
}
