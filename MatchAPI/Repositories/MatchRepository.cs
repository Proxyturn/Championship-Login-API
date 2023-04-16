using System;
using Championship_Login_API.Models;
using DatabaseProject;

namespace MatchAPI.Repository
{
	public class MatchRepository
	{
        private readonly ChampionContext _dbContext;
        public MatchRepository(ChampionContext dbContext)
		{
            _dbContext = dbContext;
        }

        public async Task<List<Match>> GetByRefereeId(Guid IdReferee)
        {
            try
            {
                return _dbContext.Matchs.Where(w => w.IdReferee == IdReferee)?.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

