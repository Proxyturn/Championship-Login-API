﻿using System;
using Championship_Login_API.Models;
using DatabaseProject;
using DatabaseProject.Models.Request;
using DatabaseProject.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace MatchAPI.Repository
{
	public class MatchRepository
	{
        private readonly ChampionContext _dbContext;
        public MatchRepository(ChampionContext dbContext)
		{
            _dbContext = dbContext;
        }

        public async Task<List<MatchListResponse>> GetByRefereeId(Guid IdReferee)
        {
            try
            {
                List<MatchListResponse> matchListResponse = new List<MatchListResponse>();
                var dbMatches = _dbContext.Matchs.Where(w => w.IdReferee == IdReferee)?.ToList();

                foreach (var match in dbMatches)
                {
                    matchListResponse.Add(new MatchListResponse()
                    {
                        Id = match.Id,
                        Name = match.Name,
                        PhaseNumber = match.PhaseNumber,
                        StartDate = match.StartDate.ToString("dd/MM/yyyy HH:mm"),
                        ChampionshipName = _dbContext.Championships.Where(w => w.Id == match.IdChampion)?.FirstOrDefault()?.Title,
                        TotalTickets = match.TotalTickets,
                        SoldTickets = _dbContext.Tickets.Where(w => w.IdMatch == match.Id) == null ? 0 : _dbContext.Tickets.Where(w => w.IdMatch == match.Id).Count(),
                        RefereeName = _dbContext.Users.Where(w => w.Id == match.IdReferee)?.FirstOrDefault()?.Name,
                        IdTeamA = match.TeamA,
                        TeamAName = match.TeamA == Guid.Empty? "W.O": _dbContext.Teams.Where(w => w.Id == match.TeamA)?.FirstOrDefault().Name,
                        IdTeamB = match.TeamB,
                        TeamBName = match.TeamB == Guid.Empty ? "W.O" : _dbContext.Teams.Where(w => w.Id == match.TeamB)?.FirstOrDefault().Name,
                        Status = match.Status,
                    }) ;

                }
                return matchListResponse;
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
                Match match = _dbContext.Matchs.Where(w => w.Id == IdMatch)?.FirstOrDefault();
                if (match != null)
                {
                    Championship existChampionship = _dbContext.Championships.Where(w => w.Id == match.IdChampion)?.FirstOrDefault();
                    return new MatchDetailResponse()
                    {
                        Id = match.Id,
                        Name = match.Name,
                        PhaseNumber = match.PhaseNumber,
                        StartDate = match.StartDate.ToString("dd/MM/yyyy HH:mm"),
                        TotalTickets = match.TotalTickets,
                        SoldTickets = _dbContext.Tickets.Where(w => w.IdMatch == match.Id) == null ? 0 : _dbContext.Tickets.Where(w => w.IdMatch == match.Id).Count(),
                        RefereeName = _dbContext.Users.Where(w => w.Id == match.IdReferee)?.FirstOrDefault()?.Name,
                        IdTeamA = match.TeamA,
                        TeamAName = match.TeamA == Guid.Empty ? "W.O" : _dbContext.Teams.Where(w => w.Id == match.TeamA)?.FirstOrDefault().Name,
                        IdTeamB = match.TeamB,
                        TeamBName = match.TeamB == Guid.Empty ? "W.O" : _dbContext.Teams.Where(w => w.Id == match.TeamB)?.FirstOrDefault().Name,
                        Status = match.Status,
                        ChampionshipName = existChampionship?.Title,
                        ChampionshipDetail = existChampionship?.Description == null? "Não foi registrada uma descrição para esta competição" : existChampionship.Description,
                        MatchOcurrence = null
                    };
                }
                else
                    return null;
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
                Match match = _dbContext.Matchs.Where(w => w.Id == updateMatchReferee.IdMatch)?.FirstOrDefault();
                if (match != null)
                {
                    User existRefereee = _dbContext.Users.Where(w => w.Id == updateMatchReferee.IdReferee)?.FirstOrDefault();
                    if (existRefereee != null)
                    {
                        match.IdReferee = existRefereee.Id;
                        _dbContext.Matchs.Update(match);
                        _dbContext.SaveChanges();
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
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
                Match match = _dbContext.Matchs.Where(w => w.Id == startMatch.IdMatch)?.FirstOrDefault();
                if (match != null)
                {
                    match.StartDate = DateTime.Now;
                    match.Status = DatabaseProject.Enums.MatchStatusEnum.OnGoing;
                    //Criar nova ocorrencia de atividade
                    _dbContext.Matchs.Update(match);
                    _dbContext.SaveChanges();
                    return true;
                }
                else
                    throw new Exception("Não foi encontrado confronto informado");
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
                Match match = _dbContext.Matchs.Where(w => w.Id == finishMatch.IdMatch)?.FirstOrDefault();
                if (match != null)
                {
                    match.FinishDate = DateTime.Now;
                    match.IdWinner = finishMatch.IdWinner;
                    match.Status = DatabaseProject.Enums.MatchStatusEnum.Finished;

                    Team winnerTeam = _dbContext.Teams.Where(w => w.IdChampionship == match.IdChampion && w.Id == finishMatch.IdWinner)?.FirstOrDefault();
                    if (winnerTeam != null)
                    {
                        winnerTeam.Wins++;

                        List<Match> nextMatches = _dbContext.Matchs.Where(w => w.IdChampion == match.IdChampion && w.PhaseNumber== finishMatch.MatchPhase+1).OrderBy(ob=>ob.Name).ToList();
                        for (int i = 0; i < nextMatches.Count(); i++)
                        {
                            if (nextMatches[i].TeamA == Guid.Empty)
                            {
                                nextMatches[i].TeamA = winnerTeam.Id;
                                _dbContext.Matchs.Update(nextMatches[i]);
                                break;
                            }
                            else if (nextMatches[i].TeamB == Guid.Empty)
                            {
                                nextMatches[i].TeamB = winnerTeam.Id;
                                _dbContext.Matchs.Update(nextMatches[i]);
                                break;
                            }
                        }

                        //Criar nova ocorrencia de atividade
                        _dbContext.Matchs.Update(match);
                        _dbContext.Teams.Update(winnerTeam);
                        _dbContext.SaveChanges();
                        return true;
                    }
                    else
                        throw new Exception("Vencedor informado não foi encontrado");
                    
                }
                else
                    throw new Exception("Não foi encontrado confronto informado");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}