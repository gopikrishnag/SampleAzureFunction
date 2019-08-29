using ClearData.Service;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(ClearData.Startup))]
namespace ClearData
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
             
            builder.Services.AddSingleton<IDatabaseService, DatabaseService>();
        }
    }
}
 

//https://medium.com/asos-techblog/things-i-learnt-in-my-first-azure-functions-project-a02c0aa5d24e