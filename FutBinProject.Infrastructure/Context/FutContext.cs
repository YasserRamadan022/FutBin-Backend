using FutBinProject.Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutBinProject.Infrastructure.Context
{
    public class FutContext : IdentityDbContext<ApplicationUser>
    {
        //tryit
        public FutContext(DbContextOptions<FutContext> options) : base(options) { }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<LineUp> LineUps { get; set; }
        public DbSet<LineUpPlayer> LineUpsPlayers { get; set; }
        public DbSet<Player> Players { get; set; }
    }
}
