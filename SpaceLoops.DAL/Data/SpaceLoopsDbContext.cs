using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpaceLoops.DAL.Entities;
using SpaceLoops.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SpaceLoops.DAL.Data
{
    public class SpaceLoopsDbContext : IdentityDbContext
    {
        public SpaceLoopsDbContext(DbContextOptions<SpaceLoopsDbContext> options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<UserRegistration> UserRegistration { get; set; }
        public DbSet<Image> Image { get; set; }
    }
}
