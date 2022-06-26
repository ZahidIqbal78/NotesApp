using AutoMapper;
using BCrypt.Net;
using NotesApp.API.Data;
using NotesApp.API.DomainModels;
using NotesApp.API.DTOs.User;
using NotesApp.API.Helpers;

namespace NotesApp.API.Services.User
{
    public class UserService : IUserService
    {
        private readonly NotesAppDbContext context;
        private readonly IMapper mapper;
        private readonly IJwtUtility jwtUtility;

        public UserService(NotesAppDbContext context, IMapper mapper, IJwtUtility jwtUtility)
        {
            this.context = context;
            this.mapper = mapper;
            this.jwtUtility = jwtUtility;
        }

        public bool CheckExistingUser(string email)
        {
            var user = this.context.Users.SingleOrDefault(x => x.Email == email);
            
            if (user != null) return true;

            return false;
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return this.context.Users;
        }

        public ApplicationUser? GetUser(string email)
        {
            return this.context.Users.SingleOrDefault(x => x.Email == email);
        }

        public ApplicationUser? GetUserById(int id)
        {
            var user = this.context.Users.Find(id);
            if (user == null) throw new KeyNotFoundException();

            return user;
        }

        public UserLoginResponseDto Login(ApplicationUser user)
        {
            var response = this.mapper.Map<UserLoginResponseDto>(user);
            response.Token = this.jwtUtility.GenerateToken(user);
            return response;
        }

        public void Register(UserRegisterRequestDto newUser)
        {
            
            bool isExistingUser = CheckExistingUser(newUser.Email);
            if(isExistingUser)
            {
                throw new Exception("Email "+ newUser.Email + "is taken.");
            }
            var applicationUser = this.mapper.Map<ApplicationUser>(newUser);

            // hash password
            applicationUser.HashedPassword = BCrypt.Net.BCrypt.HashPassword(newUser.Password);

            this.context.Users.Add(applicationUser);
            this.context.SaveChanges();
        }
    }
}
