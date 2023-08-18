using ASPHomeWork_7.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPHomeWork_7.Data;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions options) : base(options) { }
    public DbSet<User> Users { get; set; }
}
