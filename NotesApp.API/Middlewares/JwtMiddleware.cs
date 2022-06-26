using NotesApp.API.Helpers;
using NotesApp.API.Services.User;

namespace NotesApp.API.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate next;

        public JwtMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtility jwtUtility)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtility.ValidateToken(token);
            if (userId != null)
            {
                var userObj = userService.GetUserById(userId.Value);
                context.Items["User"] = userObj;
                context.Items["UserId"] = userObj.Id;
            }

            await this.next(context);
        }
    }
}
