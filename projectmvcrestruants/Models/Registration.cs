using Microsoft.EntityFrameworkCore;
using projectmvcrestruants.Models;

public class Registration: DbContext
{
    public Registration(DbContextOptions<Registration> options)
        : base(options)
    {
    }

    // DbSet for the users table
    public DbSet<Login> Logins{ get; set; }
}


