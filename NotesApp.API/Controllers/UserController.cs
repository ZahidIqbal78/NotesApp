using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NotesApp.API.DomainModels;
using NotesApp.API.DTOs.User;
using NotesApp.API.Helpers;
using NotesApp.API.Services.User;
using System.Security.Claims;
using System.Text.Json;

namespace NotesApp.API.Controllers
{
    [AuthorizeAttributeHelper]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly IValidator<UserRegisterRequestDto> registerValidator;
        private readonly IValidator<UserLoginRequestDto> loginValidator;

        public UserController(IUserService userService,
            IMapper mapper,
            IValidator<UserRegisterRequestDto> registerValidator,
            IValidator<UserLoginRequestDto> loginValidator)
        {
            this.userService = userService;
            this.mapper = mapper;
            this.registerValidator = registerValidator;
            this.loginValidator = loginValidator;
        }
        
        /// <summary>
        /// New user registration
        /// </summary>
        /// <param name="userRegisterDto"></param>
        /// <returns></returns>
        [AllowAnonymousAttributeHelper]
        [HttpPost()]
        [Route("register")]
        public IActionResult Register([FromBody] UserRegisterRequestDto userRegisterDto)
        {
            var validationResult = this.registerValidator.Validate(userRegisterDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
            }

            this.userService.Register(userRegisterDto);
            return Ok();
        }

        /// <summary>
        /// User login using email and password
        /// </summary>
        /// <param name="userLoginRequestDto"></param>
        /// <returns></returns>
        [AllowAnonymousAttributeHelper]
        [HttpPost()]
        [Route("login")]
        public IActionResult Login([FromBody] UserLoginRequestDto userLoginRequestDto)
        {
            var validationResult = this.loginValidator.Validate(userLoginRequestDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
            }

            var existingUser = this.userService.GetUser(userLoginRequestDto.Email);
            if (existingUser == null) return NotFound($"User with email '{userLoginRequestDto.Email}' not found");

            var isPasswordVerified = BCrypt.Net.BCrypt.Verify(userLoginRequestDto.Password, existingUser.HashedPassword);
            if (!isPasswordVerified) return BadRequest("Email and password do not match.");
            
            var response = this.userService.Login(existingUser);
            
            return Ok(response);

        }

        /// <summary>
        /// A test action to check authorized attribute,
        /// and whether the user can be retrieved from the
        /// the http context
        /// </summary>
        /// <returns></returns>
        [AllowAnonymousAttributeHelper]
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = this.userService.GetAll();
            return Ok(users);
        }
    }
}
