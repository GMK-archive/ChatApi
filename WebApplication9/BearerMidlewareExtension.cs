namespace WebApplication9
{
    public static class BearerMidlewareExtension
    {
        public static IApplicationBuilder UseBearerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BearerMidleware>();
        }
    }
}
