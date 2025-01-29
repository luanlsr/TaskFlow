using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Infrastructure.Data.EntityFramework.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, UserGroup, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
                => ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        public DbSet<WorkItem> WorkItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
