
using Dysnomia.DownStatus.Business;
using Dysnomia.DownStatus.CLI;
using Dysnomia.DownStatus.Common;
using Dysnomia.DownStatus.Monitoring;
using Dysnomia.DownStatus.Persistance;

namespace Dysnomia.DownStatus.WebApp {
	public class Program {
		public static void Main(string[] args) {
			var rawConfig = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
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


			app.MapControllerRoute(
				name: "default",
				pattern: "{controller}/{action=Index}/{id?}");

			app.MapFallbackToFile("index.html");

			app.Run();
		}
	}
}