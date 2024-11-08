﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SM.Match.Infrastructure;

#nullable disable

namespace SM.WebAPI.Migrations.MatchDb
{
    [DbContext(typeof(MatchDbContext))]
    [Migration("20241107124057_Matchesv1")]
    partial class Matchesv1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SM.Match.Domain.Matches.Matches", b =>
                {
                    b.Property<int>("MatchesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MatchesId"));

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("MatchesDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MatchesName")
                        .HasColumnType("int");

                    b.Property<string>("Stadium")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("TeamLose")
                        .HasColumnType("int");

                    b.Property<int>("TeamWin")
                        .HasColumnType("int");

                    b.Property<int>("TournamentId")
                        .HasColumnType("int");

                    b.HasKey("MatchesId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("SM.Match.Domain.Statistic.MatchesStatistic", b =>
                {
                    b.Property<int>("MatchesStatisticId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MatchesStatisticId"));

                    b.Property<int>("ClubId")
                        .HasColumnType("int");

                    b.Property<int>("Fouls")
                        .HasColumnType("int");

                    b.Property<int>("LineUpId")
                        .HasColumnType("int");

                    b.Property<int>("MatchesId")
                        .HasColumnType("int");

                    b.Property<int>("Pass")
                        .HasColumnType("int");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<int>("Shot")
                        .HasColumnType("int");

                    b.HasKey("MatchesStatisticId");

                    b.ToTable("MatchesStatistic");
                });
#pragma warning restore 612, 618
        }
    }
}
