using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace RagApp.DAL
{
    public class PostgresDbContext : IdentityDbContext<ApplicationUser>
    {
        public PostgresDbContext(DbContextOptions<PostgresDbContext> options)
            : base(options)
        {
        }
       
    }
}
