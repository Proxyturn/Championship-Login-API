using System;
using Championship_Login_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DatabaseProject
{
	public class ChampionContext : DbContext
    {
		public ChampionContext(DbContextOptions<ChampionContext> options) : base(options)
        {
		}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("ChampionConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        #region SystemTables
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Championship> Championships { get; set; }
        public DbSet<Match> Competitions { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<ChampionshipReferee> ChampionshipReferees { get; set; }
        #endregion
    }
}

