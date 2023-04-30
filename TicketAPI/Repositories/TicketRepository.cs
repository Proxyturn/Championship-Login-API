using System;
using DatabaseProject;
using DatabaseProject.Models.Response;
using DatabaseProject.Enums;
using Microsoft.EntityFrameworkCore;
using Championship_Login_API.Models;
using DatabaseProject.Models.Request;

namespace TicketAPI.Repositories
{
	public class TicketRepository
	{
        private readonly ChampionContext _dbContext;
        public TicketRepository(ChampionContext dbContext)
		{
            _dbContext = dbContext;
        }

        public async Task<List<AvailableTicketListResponse>> GetAllAvailable()
        {
            try
            {
                List<AvailableTicketListResponse> ticketsGroup = (from matches in _dbContext.Matchs.Where(w => w.Status != MatchStatusEnum.Finished)
                                    join championship in _dbContext.Championships on matches.IdChampion equals championship.Id
                                    into champMatch
                                    from cm in champMatch.DefaultIfEmpty()
                                    select new AvailableTicketListResponse
                                    {
                                       IdMatch = matches.Id,
                                       IdChampionship = cm.Id,
                                       ChampioshipTitle = cm.Title,
                                       ChampionshipDescription = cm.Description == null ? "Não foi registrada uma descrição para esta competição" : cm.Description,
                                       ChampionshipStartDate = cm.StartDate.ToString("dd/MM/yyyy HH:mm"),
                                       MatchTeamA = matches.TeamA == Guid.Empty ? "W.O" : _dbContext.Teams.Where(w => w.Id == matches.TeamA).FirstOrDefault().Name,
                                       MatchTeamB = matches.TeamB == Guid.Empty ? "W.O" : _dbContext.Teams.Where(w => w.Id == matches.TeamB).FirstOrDefault().Name,
                                       MatchTotalTicket = matches.TotalTickets,
                                       TotalSold = _dbContext.Tickets.Where(w => w.IdMatch == matches.Id).Count()
                                    }).ToList();
                
                return ticketsGroup;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Insert(Guid matchId, string userId)
        {
            try
            {
                if (matchId != Guid.Empty)
                {
                    _dbContext.Tickets.Add(new Ticket
                    {
                        Id = Guid.NewGuid(),
                        IdMatch = matchId,
                        IdUser = Guid.Parse(userId),
                        TicketNumber = _dbContext.Tickets.Where(w=>w.IdMatch == matchId).Count()+1
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
    }
}

