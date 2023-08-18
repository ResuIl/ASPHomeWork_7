using ASPHomeWork_7.Models;
using ASPHomeWork_7.Services;
using Microsoft.AspNetCore.Identity;

namespace ASPHomeWork_7.Middlewares;

public class AuthMiddleware
{
    private RequestDelegate next;

    public AuthMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        IUserManager? userManager = context.RequestServices.GetService<IUserManager>();
        UserCredentials? userCredentials = userManager?.GetUserCredentials();
        //await Console.Out.WriteLineAsync((userCredentials is not null).ToString());
        if (userCredentials is not null)
            await next.Invoke(context);
        else if (!context.Request.Path.StartsWithSegments("/Auth/Register"))
            context.Response.Redirect("Auth/Register");
        else
            await context.Response.WriteAsync("Unauthorized");
    }
}
