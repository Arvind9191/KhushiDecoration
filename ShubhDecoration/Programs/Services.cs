using Shubhdecoration.DataAccess.Dapper;
using Shubhdecoration.Repository.Dapper.Account;
using Shubhdecoration.Repository.Dapper.Common;
using Shubhdecoration.Repository.Dapper.Decoration;
using Shubhdecoration.Repository.Master;
using Shubhdecoration.Services.Account;

namespace ShubhDecoration.Programs
{
    public class Services
    {
        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAccountRepo, AccountRepo>();
            services.AddScoped<IConnecctions, Connection>();
            services.AddScoped<IAccountServices, AccountServices>();
            services.AddScoped<IDecorationRepository, DecorationRespository>();
            services.AddScoped<ICommonRepository, CommonRepository>();
            services.AddScoped<IMasterRepository, MasterRepository>();
            return services;
        }
    }
}
