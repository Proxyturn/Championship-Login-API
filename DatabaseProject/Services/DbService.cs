using System;
namespace DatabaseProject.Services
{
	public class DbService
	{
        private ChampionContext _context;
        public DbService(ChampionContext championContext)
		{
            _context = championContext;
        }
	}
}

