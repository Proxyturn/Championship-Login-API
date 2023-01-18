using System;
using Championship_Login_API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseProject
{
	public class ChampionContext : DbContext
    {
		public ChampionContext(DbContextOptions options) : base(options)
        {
		}

        #region SystemTables
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Championship> Championships { get; set; }
        public DbSet<Match> Competitions { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        #endregion
    }
}

