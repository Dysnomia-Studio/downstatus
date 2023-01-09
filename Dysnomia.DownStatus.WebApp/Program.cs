
using Dysnomia.Common.Stats;
using Dysnomia.DownStatus.Business;
using Dysnomia.DownStatus.CLI;
using Dysnomia.DownStatus.Common;
using Dysnomia.DownStatus.Monitoring;
using Dysnomia.DownStatus.Persistance;

namespace Dysnomia.DownStatus.WebApp {
#pragma warning disable S1118 // Utility classes should not have public constructors
	public class Program {
#pragma warning restore S1118 // Utility classes should not have public constructors
		public static void Main(string[] args) {
			var rawConfig = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddEnvironmentVariables()
				.AddJsonFile("appsettings.json")
				.AddUserSecrets<Program>()
				.Build();

			var builder = WebApplication.CreateBuilder(args);
			var services = builder.Services;

			var appSettingsSection = rawConfig.GetSection("AppSettings");
			services.Configure<AppSettings>(appSettingsSection);

			services.AddHttpClient("website", o => {
				o.DefaultRequestHeaders.Add("User-Agent", "Downstat.us - Website worker");
			});

			services.ConfigureRepositories();
			services.ConfigureBusinessServices();
			services.ConfigureJobsServices();

			services.AddHostedService<CleanerHostedService>();
			services.AddHostedService<MonitoringHostedService>();

			services.AddControllersWithViews();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment()) {
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();

			if (!app.Environment.IsEnvironment("Testing")) {
				app.Use(async (context, next) => {
					StatsRecorder.PrepareVisit(context);

					await next();

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
					StatsRecorder.NewVisit(context);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
				});
			}

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller}/{action=Index}/{id?}");

			app.MapFallbackToFile("index.html");

			app.Run();
		}
	}
}