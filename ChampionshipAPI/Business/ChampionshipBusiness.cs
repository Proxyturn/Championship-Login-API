using System;
using Championship_Login_API.Models;
using ChampionshipAPI.Repository;
using DatabaseProject.Models.Auth.Request;
using DatabaseProject.Models.Request;
using DatabaseProject.Models.Response;

namespace ChampionshipAPI.Business
{
	public class ChampionshipBusiness
	{
        private ChampionshipRepository _championshipRepository;
        public ChampionshipBusiness(ChampionshipRepository championshipRepository)
		{
			_championshipRepository = championshipRepository;
		}

        public async Task<object> GetAll(Boolean external=true)
        {
            try
            {
                return await _championshipRepository.GetAll(external);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ChampionshipExternalDetail> GetById(Guid id, Boolean external = true)
        {
            try
            {
                return await _championshipRepository.GetById(id, external);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Insert(CreateChampionship createChampionship)
        {
            try
            {
                return await _championshipRepository.Insert(createChampionship);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(UpdateChampionship championship)
        {
            try
            {
                return await _championshipRepository.Update(championship);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                return await _championshipRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> StartChampionship(Guid idChampionship)
        {
            try
            {
                return await _championshipRepository.StartChampionship(idChampionship);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> FinishChampionship(Guid idChampionship)
        {
            try
            {
                return await _championshipRepository.FinishChampionship(idChampionship);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

