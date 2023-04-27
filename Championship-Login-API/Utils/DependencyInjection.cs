using System;
using CoreAPI.Business;
using CoreAPI.Repositories;
using CoreAPI.Services;
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
            #endregion

            #region Repository Layer
            services.AddTransient<UserRepository>();
            services.AddTransient<RefereeRepository>();
            #endregion

            services.AddTransient<TokenService>();

            return services;
        }
    }
}

