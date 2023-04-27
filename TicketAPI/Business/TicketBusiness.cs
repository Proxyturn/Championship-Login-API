using System;
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
    }
}

