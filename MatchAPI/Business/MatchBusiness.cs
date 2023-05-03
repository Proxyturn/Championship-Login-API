using System;
using Championship_Login_API.Models;
using DatabaseProject.Models.Request;
using DatabaseProject.Models.Response;
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

        public async Task<List<MatchListResponse>> GetByRefereeId(Guid IdReferee)
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

        public async Task<MatchDetailResponse> GetMatchById(Guid IdMatch)
        {
            try
            {
                return await _matchRepository.GetMatchById(IdMatch);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateMatchReferee(UpdateMatchReferee updateMatchReferee)
        {
            try
            {
                return await _matchRepository.UpdateMatchReferee(updateMatchReferee);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> StartMatch(StartMatch startMatch)
        {
            try
            {
                return await _matchRepository.StartMatch(startMatch);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> FinishMatch(FinishMatch finishMatch)
        {
            try
            {
                return await _matchRepository.FinishMatch(finishMatch);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

