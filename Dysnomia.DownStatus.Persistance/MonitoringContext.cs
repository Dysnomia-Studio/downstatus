using Dysnomia.DownStatus.Common;
using Dysnomia.DownStatus.Common.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Dysnomia.DownStatus.Persistance {
	public class MonitoringContext : DbContext {
		private readonly string ConnectionString;

		public DbSet<App> Apps { get; set; }
		public DbSet<MonitoringEntry> MonitoringEntry { get; set; }
		public DbSet<MonitoringEntryHistoryEntry> MonitoringHistory { get; set; }

		public MonitoringContext(IOptions<AppSettings> settings) {
			ConnectionString = settings.Value.ConnectionString;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.Entity<App>()
				.HasKey(e => e.Key);

			modelBuilder.Entity<App>()
				.HasMany(e => e.MonitoringEntries)
				.WithOne(e => e.App)
				.HasForeignKey(e => new { e.AppId });

			modelBuilder.Entity<MonitoringEntry>()
				.HasKey(e => new { e.AppId, e.Name });

			modelBuilder.Entity<MonitoringEntry>()
				.HasMany(e => e.History)
				.WithOne(e => e.MonitoringEntry)
				.HasForeignKey(e => new { e.MonitoringEntryAppId, e.MonitoringEntryName });

			modelBuilder.Entity<MonitoringEntryHistoryEntry>()
				.HasKey(e => new { e.MonitoringEntryAppId, e.MonitoringEntryName, e.Date });
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
				 => optionsBuilder.UseNpgsql(ConnectionString);
	}
}
