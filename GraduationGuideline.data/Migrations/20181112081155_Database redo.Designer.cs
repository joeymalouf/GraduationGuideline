﻿// <auto-generated />
using GraduationGuideline.data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GraduationGuideline.data.Migrations
{
    [DbContext(typeof(GraduationGuidelineContext))]
    [Migration("20181112081155_Database redo")]
    partial class Databaseredo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("GraduationGuideline.data.entities.StepEntity", b =>
                {
                    b.Property<string>("Username");

                    b.Property<string>("StepName");

                    b.Property<bool>("Status");

                    b.HasKey("Username", "StepName");

                    b.ToTable("Step");
                });

            modelBuilder.Entity("GraduationGuideline.data.entities.UserEntity", b =>
                {
                    b.Property<string>("Username")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Admin");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<string>("StudentType");

                    b.HasKey("Username");

                    b.ToTable("User");
                });

            modelBuilder.Entity("GraduationGuideline.data.entities.StepEntity", b =>
                {
                    b.HasOne("GraduationGuideline.data.entities.UserEntity", "UserEntity")
                        .WithMany()
                        .HasForeignKey("Username")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}