using System;
using DatabaseProject.Models.Request;
using DatabaseProject.Models.Response;
using TicketAPI.Repositories;

namespace TicketAPI.Business
{
	public class TicketBusiness
	{
		private TicketRepository _ticketRepository;
		public TicketBusiness(TicketRepository ticketRepository)
		{
			_ticketRepository = ticketRepository;
		}

        public async Task<List<AvailableTicketListResponse>> GetAllAvailable()
        {
            try
            {
                return await _ticketRepository.GetAllAvailable();
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
                return await _ticketRepository.Insert(matchId,userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

