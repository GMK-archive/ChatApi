using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace WebApplication9
{
    public class BearerMidleware
    {
        private readonly RequestDelegate _next;
        //przekarz dekegata dalej
        public BearerMidleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.Equals("/users") || context.Request.Path.Equals("/user/me"))
            {
                await _next(context);
                return;
            }
            string authHeader = context.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorised: missing header");
                return;
            }
            string token = authHeader.Substring("Bearer ".Length).Trim();
            if (!Tokens.UserTokens.TryGetValue(token, out User user))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Unauthorised: missing header");
                return;
            }
            context.Items["User"] = user;
            await _next(context);
        }
    }
}
