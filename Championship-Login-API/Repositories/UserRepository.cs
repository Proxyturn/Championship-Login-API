using System;
using System.Data.Entity;
using Championship_Login_API.Models;
using CoreAPI.Util;
using DatabaseProject;
using DatabaseProject.Models.Auth.Request;
using DatabaseProject.Models.Request;
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

        public async Task<User> GetUserById(Guid id)
        {
            try
            {
                var existUser = _dbContext.Users.Where(w => w.Id == id)?.FirstOrDefault();

                if (existUser != null)
                    return existUser;                
                else
                    throw new Exception("Usuário informado não foi encontrado");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            try
            {
                var existUser = _dbContext.Users.Where(w => w.Id == id)?.FirstOrDefault();

                if (existUser != null)
                {
                    _dbContext.Users.Remove(existUser);
                    _dbContext.SaveChanges();
                    return true;
                }
                else
                    throw new Exception("Usuário informado não foi encontrado");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> RegisterChampionship(RegisterRefereeChampionship registerRefereeChampionship)
        {
            try
            {
                var existUser = _dbContext.Users.Where(w => w.Id == registerRefereeChampionship.IdReferee)?.FirstOrDefault();
                var existChampionship = _dbContext.Championships.Where(w => w.Id == registerRefereeChampionship.IdChampionship)?.FirstOrDefault();
                if(existUser != null && existChampionship != null)
                {
                    _dbContext.ChampionshipReferees.Add(new ChampionshipReferee()
                    {
                        Id = Guid.NewGuid(),
                        UserId = registerRefereeChampionship.IdReferee,
                        ChampionshipId = registerRefereeChampionship.IdChampionship
                    });
                    _dbContext.SaveChanges();
                }

                if (existUser == null)
                    throw new Exception("Usuário não encontrado");
                if (existChampionship == null)
                    throw new Exception("Campeonato não encontrado");

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> UpdateUserAsync(UpdateUser updateUser)
        {
            try
            {
                var existUser = _dbContext.Users.Where(w => w.Id == updateUser.Id)?.FirstOrDefault();

                if (existUser != null)
                {
                    existUser.Age = updateUser.Age;
                    existUser.Name = updateUser.Name;
                    existUser.UserType = updateUser.UserType;
                    _dbContext.SaveChanges();
                    existUser = _dbContext.Users.Where(w => w.Id == updateUser.Id)?.FirstOrDefault();
                    return existUser;
                }
                else
                    throw new Exception("Usuário informado não foi encontrado");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

