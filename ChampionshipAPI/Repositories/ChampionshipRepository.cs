﻿using System;
using Championship_Login_API.Models;
using DatabaseProject;
using DatabaseProject.Models.Request;
using DatabaseProject.Models.Response;
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

        public async Task<object> GetAll(Boolean external = true)
        {
            try
            {
                List<Championship> championships = _dbContext.Championships.OrderBy(ob=>ob.Id).ThenBy(ob=>ob.StartDate)?.ToList();
                //if (external)
                //{
                    List<ChampionshipExternalListResponse> externalList = new List<ChampionshipExternalListResponse>();
                    foreach (var championship in championships)
                    {
                        externalList.Add(new ChampionshipExternalListResponse()
                        {
                            Id = championship.Id,
                            Title = championship.Title,
                            Description = championship.Description == null? "Não foi registrada uma descrição para esta competição" : championship.Description,
                            StartDate = championship.StartDate.ToString("dd/MM/yyyy HH:mm"),
                            Status = championship.Status
                        });

                    }
                    return externalList;
                //}
                return championships;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<object> GetById(Guid id, Boolean external = true)
        {
            try
            {
                Championship championships = _dbContext.Championships.Where(w=>w.Id == id).FirstOrDefault();
                //if (external)
                //{
                    List<TeamsExternalDetail> subsTeams = new List<TeamsExternalDetail>();
                    List<MatchExternalDetail> matches = new List<MatchExternalDetail>();

                    subsTeams = (from teams in _dbContext.Teams
                                where  teams.IdChampionship == id
                                orderby teams.Wins
                                select new TeamsExternalDetail
                                {
                                    IdTeam = teams.Id,
                                    Name = teams.Name,
                                    Wins = teams.Wins
                                }).ToList();
                    matches = (from matchs in _dbContext.Matchs
                               where matchs.IdChampion == id
                               join users in _dbContext.Users on matchs.IdReferee equals users.Id
                               orderby matchs.PhaseNumber
                               select new MatchExternalDetail
                               {
                                   IdMatch = matchs.Id,
                                   Name = matchs.Name,
                                   PhaseNumber = matchs.PhaseNumber,
                                   TotalTickets = matchs.TotalTickets,
                                   StartDate = matchs.StartDate.ToString("dd/MM/yyyy HH:mm"),
                                   Status = matchs.Status,
                                   IdTeamB = matchs.TeamB,
                                   IdTeamA = matchs.TeamA,
                                   RefereeName = users.Name,
                                   TeamAName = matchs.TeamA == Guid.Empty? "W.O": _dbContext.Teams.Where(w=>w.Id == matchs.TeamA).FirstOrDefault().Name,
                                   TeamBName = matchs.TeamB == Guid.Empty ? "W.O" : _dbContext.Teams.Where(w => w.Id == matchs.TeamB).FirstOrDefault().Name,
                                   SoldTickets = (from tickets in _dbContext.Tickets
                                                  where tickets.IdMatch == matchs.Id
                                                  select tickets
                                                  ).Count()
                               }).ToList();

                    return new ChampionshipExternalDetail()
                    {
                        Title = championships.Title,
                        Description = championships.Description == null ? "Não foi registrada uma descrição para esta competição" : championships.Description,
                        Status = championships.Status,
                        StartDate= championships.StartDate.ToString("dd/MM/yyyy HH:mm"),
                        EndDate = "-",
                        Subscription = subsTeams == null? 0:subsTeams.Count(),
                        Ranking = subsTeams,
                        Matchs = matches
                    };
                //}
                    
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
                        Description = createChampionship.Description,
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

        public async Task<bool> Update(UpdateChampionship championship)
        {
            try
            {
                Championship existChamp = _dbContext.Championships.Where(w => w.Id == championship.Id)?.FirstOrDefault();
                if (existChamp != null)
                {
                    if (championship != null)
                    {
                        existChamp.Title = championship.Title;
                        existChamp.Description = championship.Description;
                        existChamp.StartDate = championship.StartDate;

                        _dbContext.Championships.Update(existChamp);
                        _dbContext.SaveChanges();
                        return true;
                    }
                    else
                        return false;
                }
                else
                    throw new Exception("Championship não encontrada");
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
                var championship = (await GetById(id,false)) as Championship;
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

        public async Task<bool> StartChampionship(Guid idChampionship)
        {
            try
            {
                var championship = (await GetById(idChampionship, false)) as Championship;
                if (championship != null)
                {
                    List<Team> subsTeams = _dbContext.Teams.Where(w => w.IdChampionship == idChampionship).ToList();
                    List<ChampionshipReferee> subsRef = _dbContext.ChampionshipReferees.Where(cref => cref.ChampionshipId == idChampionship).ToList();

                    //impar
                    if (subsTeams.Count() % 2 != 0)
                        subsTeams.Add(new Team()
                        {
                            Id = Guid.Empty,
                            IdChampionship = idChampionship,
                            Name = "W.O."
                        });

                    //phase 1
                    int match = 1;
                    for (int i = 0; i < subsTeams.Count()-1; i = 1+2)
                    {
                        _dbContext.Matchs.Add(new Match()
                        {
                            Id = Guid.NewGuid(),
                            IdChampion = idChampionship,
                            IdReferee = subsRef[Convert.ToInt16(new Random().NextInt64(0, subsRef.Count() - 1))].UserId,
                            Location = "",
                            Name = $"Confronto {match}",
                            PhaseNumber = 1,
                            StartDate = championship.StartDate,
                            TeamA = subsTeams[i].Id,
                            TeamB = subsTeams[i+1].Id,
                            TotalTickets = 100
                        });
                        match++;
                    }

                    var totalTeams = subsTeams.Count() / 2;
                    //phase >2 until championshipTotalPhases
                    for (int i = 2; i <= championship.TotalPhases; i++)
                    {
                        for (int j = 0; j < totalTeams; j = j+2)
                        {
                            _dbContext.Matchs.Add(new Match()
                            {
                                Id = Guid.NewGuid(),
                                IdChampion = idChampionship,
                                IdReferee = subsRef[Convert.ToInt16(new Random().NextInt64(0, subsRef.Count() - 1))].UserId,
                                Location = "",
                                Name = $"Confronto {match}",
                                PhaseNumber = i,
                                StartDate = championship.StartDate,
                                TeamA = Guid.Empty,
                                TeamB = Guid.Empty,
                                TotalTickets = 100
                            });
                            match++;
                        }
                        totalTeams = totalTeams / 2;
                    }
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

        public async Task<bool> FinishChampionship(Guid idChampionship)
        {
            try
            {
                var championship = await GetById(idChampionship, false);
                if (championship != null)
                {
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

