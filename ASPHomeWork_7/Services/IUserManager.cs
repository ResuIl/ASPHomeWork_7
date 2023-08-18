using ASPHomeWork_7.Models;

namespace ASPHomeWork_7.Services;

public interface IUserManager
{
    bool Register(string username, string password, bool isAdmin);
    bool Login(string username, string password);
    UserCredentials GetUserCredentials();
}
