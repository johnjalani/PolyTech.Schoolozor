using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Blazor.IndexedDB.WebAssembly;
using Serilog;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Schoolozor.Services;
using Schoolozor.Repository;
using Serilog.Core;

namespace Schoolozor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();          
            builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
            builder.Services.AddScoped<ISchoolAuthenticationStateProvider, ApiAuthenticationStateProvider>();
            builder.Services.AddScoped<IClientUserService, ClientUserService>();
            builder.Services.AddScoped<CurrentUserInfo>();

            builder.Services.AddScoped<IGenericRepository, GenericRepository>();

            var levelSwitch = new LoggingLevelSwitch();
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(levelSwitch)
                .WriteTo.RollingFile($"{builder.Configuration.GetSection("LogPath").Value}\\log.txt")
                .Enrich.WithProperty("InstanceId", Guid.NewGuid().ToString("n"))
                .CreateLogger();

            Log.Information("Initialized!!!");

            builder.Services.AddLogging(l => l.AddSerilog(dispose: true));
            builder.Services.AddSingleton<IIndexedDbFactory, IndexedDbFactory>();

            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetSection("ApiBaseUrl").Value) });

            await builder.Build().RunAsync();
        }
    }
}
