using ASPHomeWork_7.Data;
using ASPHomeWork_7.Middlewares;
using ASPHomeWork_7.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<UserDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("default")));

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserManager, UserManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseMiddleware<AuthMiddleware>();

app.UseAuthorization();

app.UseEndpoints(endpoint =>
{
    endpoint.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoint.MapControllerRoute(
        name: "default",
        pattern: "{controller=Auth}/{action=Login}");
});

app.MapRazorPages();

app.Run();
