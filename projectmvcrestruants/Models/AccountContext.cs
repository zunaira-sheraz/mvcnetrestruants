using Microsoft.EntityFrameworkCore;
using projectmvcrestruants.Models;

public class AccountContext:DbContext
{
    public AccountContext(DbContextOptions<AccountContext> options)
        : base(options)
    {
    }

    // DbSet for the users table
    public DbSet<projectmvcrestruants.Models.Account> Accounts { get; set; }
}





