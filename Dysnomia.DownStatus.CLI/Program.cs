using Dysnomia.DownStatus.Business;
using Dysnomia.DownStatus.CLI;
using Dysnomia.DownStatus.Common;
using Dysnomia.DownStatus.Monitoring;
using Dysnomia.DownStatus.Persistance;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Dysnomia.AchieveGames.DatabaseFillerService {
	class Program {
		static async Task Main(string[] args) {
			var rawConfig = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddUserSecrets<Program>()
				.Build();

			var host = Host.CreateDefaultBuilder(args)
				.ConfigureServices(services => {
					var appSettingsSection = rawConfig.GetSection("AppSettings");
					services.Configure<AppSettings>(appSettingsSection);

					services.AddHttpClient("cli", o => {
						o.DefaultRequestHeaders.Add("User-Agent", "Downstat.us - Monitoring Status bot");
					});

					services.ConfigureRepositories();
					services.ConfigureBusinessServices();
					services.ConfigureJobsServices();

					services.AddHostedService<MonitoringHostedService>();
				})
				.Build();

			await host.RunAsync();
		}
	}
}
