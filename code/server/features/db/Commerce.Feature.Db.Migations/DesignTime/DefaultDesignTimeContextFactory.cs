using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Commerce.Feature.Db.Common.DesignTime
{
    public abstract class
        DefaultDesignTimeContextFactory<TContext> : IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        public TContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<TContext>();
            builder
                .UseSqlServer(
                    "Server=127.0.0.1,1401;Database=master;User Id=SA;Password=kind6iVy;Trusted_Connection=False;MultipleActiveResultSets=true",
                    optionsBuilder => optionsBuilder.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
            return (TContext) Activator.CreateInstance(typeof(TContext), builder.Options);
        }
    }
}