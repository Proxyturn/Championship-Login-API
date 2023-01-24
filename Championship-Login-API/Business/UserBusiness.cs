using System;
using Championship_Login_API.Models;
using CoreAPI.Repositories;
using CoreAPI.Util;
using DatabaseProject.Models.Auth.Request;

namespace CoreAPI.Business
{
	public class UserBusiness
	{
		private UserRepository _userRepository;

		public UserBusiness(UserRepository userRepository)
		{
			_userRepository = userRepository;
        }

        public async Task<User> VerificarUsuarioSenhaAsync(LoginUser loginUser)
        {
            try
            {
                loginUser.Password = Criptography.HashValue(loginUser.Password);

                var verificado = await _userRepository.VerificarUsuarioSenhaAsync(loginUser);

                return verificado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<object> CadastroDeUserAsync(User newUser)
        {
            try
            {
                newUser.Password = Criptography.HashValue(newUser.Password);

                var createdUser = await _userRepository.CadastroDeUserAsync(newUser);

                return createdUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}

