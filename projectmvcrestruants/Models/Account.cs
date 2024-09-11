using Microsoft.EntityFrameworkCore;
using projectmvcrestruants.Models;

public class Account: Microsoft.EntityFrameworkCore.DbContext
{
    public Account(DbContextOptions<Account> options)
        : base(options)
    {
    }

    // DbSet for the users table
    public DbSet<projectmvcrestruants.Models.AccountContext> Accounts{ get; set; }
}


