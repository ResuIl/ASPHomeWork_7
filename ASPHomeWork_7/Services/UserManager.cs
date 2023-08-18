using ASPHomeWork_7.Data;
using ASPHomeWork_7.Encryptors;
using ASPHomeWork_7.Models;
using System.Text.Json;

namespace ASPHomeWork_7.Services;

public class UserManager : IUserManager
{
    private readonly IHttpContextAccessor accessor;
    private readonly UserDbContext context;

    public UserManager(IHttpContextAccessor accessor, UserDbContext context)
    {
        this.accessor = accessor;
        this.context = context;
    }

    public UserCredentials GetUserCredentials()
    {
        if (accessor is not null && accessor.HttpContext is not null && accessor.HttpContext.Request.Cookies.ContainsKey("auth"))
        {
            var hash = accessor.HttpContext.Request.Cookies["auth"];

            var json = AesEncryptor.DecryptString("b14ca5898a4e4133bbce2ea2315a1916", hash);
            return JsonSerializer.Deserialize<UserCredentials>(json);
        }
        return null;
    }

    public bool Login(string username, string password)
    {
        var passHash = SHA256Encryptor.Encrypt(password);
        var user = context.Users.FirstOrDefault(u => u.Login == username && u.PasswordHash == passHash);
        if (user is not null)
        {
            UserCredentials userCredentials = new()
            {
                Login = user.Login,
                IsAdmin = user.IsAdmin
            };
            var json = JsonSerializer.Serialize(userCredentials);
            var encryptedJson = AesEncryptor.EncryptString("b14ca5898a4e4133bbce2ea2315a1916", json);
            accessor?.HttpContext?.Response.Cookies.Append("auth", encryptedJson);
            return true;
        }
        return false;
    }
    public bool Register(string username, string password, bool isAdmin)
    {
        var exist = context.Users.Any(u => u.Login == username);
        if (!exist)
        {
            context.Users.Add(new User
            {
                Login = username,
                PasswordHash = SHA256Encryptor.Encrypt(password),
                IsAdmin = isAdmin
            });
            context.SaveChanges();
            return true;
        }
        return false;
    }
}
