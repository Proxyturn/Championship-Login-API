using System;
using Championship_Login_API.Models;
using CoreAPI.Util;
using DatabaseProject;
using DatabaseProject.Models.Auth.Request;
using Microsoft.EntityFrameworkCore;

namespace CoreAPI.Repositories
{
	public class UserRepository
	{
        private readonly ChampionContext _dbContext;

        public UserRepository(ChampionContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> GetUserByEmail(String userMail)
        {
            try
            {
                
                    User existUser = _dbContext.Users.Where(w=>w.Email ==userMail).FirstOrDefault();

                    return existUser;
                

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<object> CadastroDeUserAsync(User newUser)
        {
            try
            {
                var existUser = await GetUserByEmail(newUser.Email);

                if (existUser == null)
                {
                    
                    _dbContext.Users.Add(newUser);
                    _dbContext.SaveChanges();
                    existUser = await GetUserByEmail(newUser.Email);
                    return new
                    {
                        existUser?.Id,
                        existUser.Email,
                        existUser.Name,
                        existUser.Age,
                        existUser.IdTeam
                    };
                    
                }
                else
                    throw new Exception("Usuário já existente");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> VerificarUsuarioSenhaAsync(LoginUser loginUser)
        {
            try
            {
                var existUser = await GetUserByEmail(loginUser.Email);
                if (existUser != null)
                {
                    if (existUser.Password == loginUser.Password)
                        return existUser;
                    else
                        throw new Exception("NotCompatible");
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível verificar login do usuário.");
            }
        }
    }
}

