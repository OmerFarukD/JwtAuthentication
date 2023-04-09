using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniApp.WebAPI.Models;

namespace MiniApp.WebAPI.Repository;

public class BaseDbContext: IdentityDbContext<AccountEntity,IdentityRole,string>
{
    public BaseDbContext(DbContextOptions options): base(options)
    {
        
    }

}