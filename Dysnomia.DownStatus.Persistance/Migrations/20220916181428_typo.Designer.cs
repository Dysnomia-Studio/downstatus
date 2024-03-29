﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dysnomia.DownStatus.Persistance.Migrations {
	[DbContext(typeof(MonitoringContext))]
	[Migration("20220916181428_Typo")]
	partial class Typo {
		protected override void BuildTargetModel(ModelBuilder modelBuilder) {
#pragma warning disable 612, 618
			modelBuilder
				.HasAnnotation("ProductVersion", "6.0.9")
				.HasAnnotation("Relational:MaxIdentifierLength", 63);

			NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

			modelBuilder.Entity("Dysnomia.DownStatus.Common.Models.App", b => {
				b.Property<string>("Key")
					.HasColumnType("text");

				b.Property<string>("Description")
					.HasColumnType("text");

				b.Property<string>("Name")
					.IsRequired()
					.HasColumnType("text");

				b.Property<string>("Website")
					.HasColumnType("text");

				b.HasKey("Key");

				b.ToTable("Apps");
			});

			modelBuilder.Entity("Dysnomia.DownStatus.Common.Models.MonitoringEntry", b => {
				b.Property<string>("AppId")
					.HasColumnType("text");

				b.Property<string>("Name")
					.HasColumnType("text");

				b.Property<string>("Target")
					.IsRequired()
					.HasColumnType("text");

				b.Property<int>("Type")
					.HasColumnType("integer");

				b.HasKey("AppId", "Name");

				b.ToTable("MonitoringEntry");
			});

			modelBuilder.Entity("Dysnomia.DownStatus.Common.Models.MonitoringEntryHistoryEntry", b => {
				b.Property<string>("MonitoringEntryAppId")
					.HasColumnType("text");

				b.Property<string>("MonitoringEntryName")
					.HasColumnType("text");

				b.Property<DateTime>("Date")
					.HasColumnType("timestamp with time zone");

				b.Property<string>("Message")
					.IsRequired()
					.HasColumnType("text");

				b.Property<int>("Status")
					.HasColumnType("integer");

				b.HasKey("MonitoringEntryAppId", "MonitoringEntryName", "Date");

				b.ToTable("MonitoringHistory");
			});

			modelBuilder.Entity("Dysnomia.DownStatus.Common.Models.MonitoringEntry", b => {
				b.HasOne("Dysnomia.DownStatus.Common.Models.App", "App")
					.WithMany("MonitoringEntries")
					.HasForeignKey("AppId")
					.OnDelete(DeleteBehavior.Cascade)
					.IsRequired();

				b.Navigation("App");
			});

			modelBuilder.Entity("Dysnomia.DownStatus.Common.Models.MonitoringEntryHistoryEntry", b => {
				b.HasOne("Dysnomia.DownStatus.Common.Models.MonitoringEntry", "MonitoringEntry")
					.WithMany("History")
					.HasForeignKey("MonitoringEntryAppId", "MonitoringEntryName")
					.OnDelete(DeleteBehavior.Cascade)
					.IsRequired();

				b.Navigation("MonitoringEntry");
			});

			modelBuilder.Entity("Dysnomia.DownStatus.Common.Models.App", b => {
				b.Navigation("MonitoringEntries");
			});

			modelBuilder.Entity("Dysnomia.DownStatus.Common.Models.MonitoringEntry", b => {
				b.Navigation("History");
			});
#pragma warning restore 612, 618
		}
	}
}
