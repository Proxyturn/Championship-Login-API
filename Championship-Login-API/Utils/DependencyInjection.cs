using System;
using ChampionshipAPI.Business;
using ChampionshipAPI.Repository;
using CoreAPI.Business;
using CoreAPI.Repositories;
using CoreAPI.Services;
using MatchAPI.Business;
using MatchAPI.Repository;
using TeamAPI.Business;
using TeamAPI.Repositories;
using TicketAPI.Business;
using TicketAPI.Repositories;

namespace CoreAPI.Utils
{
	public static class DependencyInjection
	{
        public static IServiceCollection AddChampionshipDependencies(
             this IServiceCollection services)
        {
            #region Business Layer
            services.AddTransient<UserBusiness>();
            services.AddTransient<RefereeBusiness>();
            services.AddTransient<ChampionshipBusiness>();
            services.AddTransient<TeamBusiness>();
            services.AddTransient<MatchBusiness>();
            services.AddTransient<TicketBusiness>();
            #endregion

            #region Repository Layer
            services.AddTransient<UserRepository>();
            services.AddTransient<RefereeRepository>();
            services.AddTransient<ChampionshipRepository>();
            services.AddTransient<TeamRepository>();
            services.AddTransient<MatchRepository>();
            services.AddTransient<TicketRepository>();
            #endregion

            services.AddTransient<TokenService>();

            return services;
        }
    }
}

