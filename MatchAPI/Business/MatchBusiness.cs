using System;
using Championship_Login_API.Models;
using MatchAPI.Repository;

namespace MatchAPI.Business
{
	public class MatchBusiness
	{
        private MatchRepository _matchRepository;
		public MatchBusiness(MatchRepository matchRepository)
		{
            _matchRepository = matchRepository;
		}

        public async Task<List<Match>> GetByRefereeId(Guid IdReferee)
        {
            try
            {
                return await _matchRepository.GetByRefereeId(IdReferee);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

