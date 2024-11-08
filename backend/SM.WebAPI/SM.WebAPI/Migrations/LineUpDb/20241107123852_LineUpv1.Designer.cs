﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SM.LineUp.Infrastructure;

#nullable disable

namespace SM.WebAPI.Migrations.LineUpDb
{
    [DbContext(typeof(LineUpDbContext))]
    [Migration("20241107123852_LineUpv1")]
    partial class LineUpv1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SM.LineUp.Domain.LineUp.LineUpBase", b =>
                {
                    b.Property<int>("LineUpId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LineUpId"));

                    b.Property<int>("ClubId")
                        .HasColumnType("int");

                    b.Property<string>("LineUpName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LineUpType")
                        .HasColumnType("int");

                    b.Property<int>("MatchId")
                        .HasColumnType("int");

                    b.Property<int>("MatchType")
                        .HasColumnType("int");

                    b.Property<int>("StadiumBackGroud")
                        .HasColumnType("int");

                    b.Property<int>("TournamentId")
                        .HasColumnType("int");

                    b.HasKey("LineUpId");

                    b.ToTable("LineUpBase");
                });

            modelBuilder.Entity("SM.LineUp.Domain.LineUp.PlayerLineUp", b =>
                {
                    b.Property<int>("PlayerLineUpId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlayerLineUpId"));

                    b.Property<int>("ClubId")
                        .HasColumnType("int");

                    b.Property<bool>("IsCaptain")
                        .HasColumnType("bit");

                    b.Property<int>("LineUpId")
                        .HasColumnType("int");

                    b.Property<int>("PlayTime")
                        .HasColumnType("int");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("PlayerPosition")
                        .HasColumnType("int");

                    b.HasKey("PlayerLineUpId");

                    b.ToTable("PlayerLineUp");
                });
#pragma warning restore 612, 618
        }
    }
}
