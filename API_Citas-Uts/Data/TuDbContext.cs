using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class TuDbContext : IdentityDbContext<IdentityUser>
{
    public TuDbContext(DbContextOptions<TuDbContext> options)
        : base(options)
    {
    }
}