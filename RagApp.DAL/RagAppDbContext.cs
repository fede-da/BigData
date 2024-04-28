using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace RagApp.DAL
{
    public class RagAppDbContext : IdentityDbContext<ApplicationUser>
    {
        public RagAppDbContext(DbContextOptions<RagAppDbContext> options)
            : base(options)
        {
        }
       
    }
}
