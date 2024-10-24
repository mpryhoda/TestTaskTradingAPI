using Asset.Domain;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace FinancialTask.Infrastructure
{
    public class AssetContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "asset";
        public DbSet<AssetEntity> Assets { get; set; }

        public AssetContext(DbContextOptions<AssetContext> options) : base(options) 
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AssetEntity>().HasNoKey();
        }
    }
}