using Clerk.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using NetCore.AutoRegisterDi;

namespace Clerk.Business.Service
{
    public class IocConfigurations
    {
        public static void Initialize(IServiceCollection services, string connectionString)
        {
            
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<DbContext, Data.Model.Models.ClerkDataContext>();
            
            //resolve dependency of processing & object services
            services.RegisterAssemblyPublicNonGenericClasses().Where(c => c.Name.EndsWith("Service")).AsPublicImplementedInterfaces();

            ////context 
            services.AddDbContext<Data.Model.Models.ClerkDataContext>(options => options.UseSqlServer(connectionString), contextLifetime: ServiceLifetime.Transient);

            //repository
            var tt = Assembly.GetAssembly(typeof(Data.Repository.Implementation.MemberRepository));
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.GetAssembly(typeof(Data.Repository.Implementation.MemberRepository))).Where(c => c.Name.EndsWith("Repository")).AsPublicImplementedInterfaces();

        }        
    }
}
