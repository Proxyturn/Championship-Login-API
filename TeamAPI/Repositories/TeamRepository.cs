using System;
using Championship_Login_API.Models;
using DatabaseProject;
using DatabaseProject.Models.Request;

namespace TeamAPI.Repositories
{
	public class TeamRepository
	{
        private readonly ChampionContext _dbContext;
        public TeamRepository(ChampionContext dbContext)
		{
            _dbContext = dbContext;
        }

        public async Task<bool> Insert(CreateTeam createTeam, string userId)
        {
            try
            {
                if (createTeam != null)
                {
                    _dbContext.Teams.Add(new Team
                    {
                        Id = Guid.NewGuid(),
                        IdLeader = Guid.Parse(userId),
                        Name = createTeam.Name,
                        IdChampionship = createTeam.IdChampionship
                    });
                    _dbContext.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

