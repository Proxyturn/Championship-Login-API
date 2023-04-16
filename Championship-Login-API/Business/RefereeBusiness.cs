using System;
using Championship_Login_API.Models;
using CoreAPI.Repositories;
using CoreAPI.Util;
using DatabaseProject.Models.Auth.Request;
using DatabaseProject.Models.Request;

namespace CoreAPI.Business
{
	public class RefereeBusiness
	{
		private RefereeRepository _refereeRepository;
        private UserRepository _userRepository;
		public RefereeBusiness(RefereeRepository refereeRepository, UserRepository userRepository)
		{
            _refereeRepository = refereeRepository;
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetAll()
        {
            try
            {
                return await _refereeRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> GetById(Guid id)
        {
            try
            {
                return await _refereeRepository.GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> Insert(NewUser newUser)
        {
            try
            {
                newUser.Password = Criptography.HashValue("Default@1234");

                return await _refereeRepository.Insert(newUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> Update(UpdateUser updateUser)
        {
            try
            {
                return await _userRepository.UpdateUserAsync(updateUser);
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
                return await _userRepository.DeleteUserAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> RegisterChampionship(RegisterRefereeChampionship registerRefereeChampionship)
        {
            try
            {
                return await _userRepository.RegisterChampionship(registerRefereeChampionship);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

