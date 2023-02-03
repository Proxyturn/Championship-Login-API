using System;
using Championship_Login_API.Models;
using DatabaseProject;
using DatabaseProject.Models.Request;
using Microsoft.EntityFrameworkCore;

namespace ChampionshipAPI.Repository
{
	public class ChampionshipRepository
	{
        private readonly ChampionContext _dbContext;
        public ChampionshipRepository(ChampionContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Championship>> GetAll(Boolean external = true)
        {
            try
            {
                List<Championship> championships = _dbContext.Championships.OrderBy(ob=>ob.Id).ThenBy(ob=>ob.StartDate)?.ToList();
                if (external)
                    Console.WriteLine("Se houver alguma regra de negócio a ser aplicada neste ponto fica.");
                
                return championships;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Championship?> GetById(Guid id, Boolean external = true)
        {
            try
            {
                Championship championships = _dbContext.Championships.Where(w=>w.Id == id).FirstOrDefault();
                if (external)
                {
                    Console.WriteLine("Se houver alguma regra de negócio a ser aplicada neste ponto fica.");
                }
                    
                return championships;
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
                if (createChampionship != null)
                {
                    _dbContext.Championships.Add(new Championship {
                        Title = createChampionship.Title,
                        StartDate = createChampionship.StartDate,
                        TotalPhases = createChampionship.TotalPhases
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

        public async Task<bool> Update(Championship championship)
        {
            try
            {
                if (championship != null)
                {
                    _dbContext.Championships.Update(championship);
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

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var championship = await GetById(id,false);
                if (championship != null)
                {
                    _dbContext.Championships.Remove(championship);
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

