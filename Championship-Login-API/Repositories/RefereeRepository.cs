using System;
using Championship_Login_API.Models;
using DatabaseProject;
using DatabaseProject.Models.Auth.Request;

namespace CoreAPI.Repositories
{
	public class RefereeRepository
	{
        private readonly ChampionContext _dbContext;
        public RefereeRepository(ChampionContext dbContext)
		{
			_dbContext = dbContext;
		}

        public async Task<List<User>> GetAll()
        {
            try
            {
                List<User> referee = _dbContext.Users.Where(w=> w.UserType == Championship_Login_API.Enums.UserEnum.Referee)?.ToList();
                
                return referee;
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
                User referee = _dbContext.Users.Where(w => w.Id == id)?.FirstOrDefault();

                return referee;
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


                _dbContext.Users.Add(new User
                {
                    Id = Guid.NewGuid(),
                    Age = newUser.Age,
                    Email = newUser.Email,
                    Name = newUser.Name,
                    Password = newUser.Password,
                    UserType = Championship_Login_API.Enums.UserEnum.Referee
                });
                _dbContext.SaveChanges();
                User referee = _dbContext.Users.Where(w => w.Email == newUser.Email)?.FirstOrDefault();
                return referee;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}

