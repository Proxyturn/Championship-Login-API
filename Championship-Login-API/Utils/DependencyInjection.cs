using System;
using ChampionshipAPI.Business;
using ChampionshipAPI.Repository;
using CoreAPI.Business;
using CoreAPI.Repositories;
using CoreAPI.Services;
using TeamAPI.Business;
using TeamAPI.Repositories;

namespace CoreAPI.Utils
{
	public static class DependencyInjection
	{
        public static IServiceCollection AddChampionshipDependencies(
             this IServiceCollection services)
        {
            #region Business Layer
            services.AddTransient<UserBusiness>();
            services.AddTransient<ChampionshipBusiness>();
            services.AddTransient<TeamBusiness>();
            #endregion

            #region Repository Layer
            services.AddTransient<UserRepository>();
            services.AddTransient<ChampionshipRepository>();
            services.AddTransient<TeamRepository>();
            #endregion

            services.AddTransient<TokenService>();

            return services;
        }
    }
}

