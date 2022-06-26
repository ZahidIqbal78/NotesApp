using AutoMapper;
using NotesApp.API.DomainModels;
using NotesApp.API.DTOs.User;

namespace NotesApp.API.Mappers
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            //UserRegisterRequestDto -> ApplicationUser
            CreateMap<UserRegisterRequestDto, ApplicationUser>();

            //User -> UserLoginResponseDto
            CreateMap<ApplicationUser, UserLoginResponseDto>();
        }
    }
}
