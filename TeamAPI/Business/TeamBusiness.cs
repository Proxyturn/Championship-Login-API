using System;
using DatabaseProject.Models.Request;
using TeamAPI.Repositories;

namespace TeamAPI.Business
{
	public class TeamBusiness
	{
        private TeamRepository _teamRepository;
        public TeamBusiness(TeamRepository teamRepository)
		{
            _teamRepository = teamRepository;
        }

		public object GetUserTeam()
		{
			return "PAO";
		}

        public async Task<bool> Insert(CreateTeam createTeam, string userId)
        {
            try
            {
                return await _teamRepository.Insert(createTeam, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

