using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DatabaseProject
{
	public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ChampionContext>
    {
		public DesignTimeDbContextFactory()
		{
		}

        public ChampionContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<ChampionContext>();
            var connectionString = configuration.GetConnectionString("ChampionConnection");
            builder.UseSqlServer(connectionString);
            return new ChampionContext(builder.Options);
        }
    }
}

