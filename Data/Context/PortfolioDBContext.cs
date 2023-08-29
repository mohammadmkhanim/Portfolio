using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data.Entities;

namespace Portfolio.Data.Context
{
    public class PortfolioDBContext : DbContext
    {
        public PortfolioDBContext(DbContextOptions<PortfolioDBContext> options) : base(options)
        {
            
        }

        public DbSet<RecentWork> RecentWorks { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<TechnicalSkill> TechnicalSkills { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Category>().HasMany(x => x.AttributeNames)
            //     .WithMany(x => x.Categories)
            //     .UsingEntity<AttributeNameCategory>(
            // );
        }
    }
}