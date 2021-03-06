﻿// <auto-generated />
using System;
using GraduationGuideline.data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GraduationGuideline.data.Migrations
{
    [DbContext(typeof(GraduationGuidelineContext))]
    [Migration("20181126031256_Creating database with type, graduation, and deadlines")]
    partial class Creatingdatabasewithtypegraduationanddeadlines
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("GraduationGuideline.data.entities.DeadlineEntity", b =>
                {
                    b.Property<string>("Semester");

                    b.Property<int>("year");

                    b.Property<DateTime>("Audit");

                    b.Property<DateTime>("Commencement");

                    b.Property<DateTime>("FinalExam");

                    b.Property<DateTime>("FinalVisit");

                    b.Property<DateTime>("GS8");

                    b.Property<DateTime>("Graduation");

                    b.Property<DateTime>("Hooding");

                    b.Property<DateTime>("ProQuest");

                    b.Property<DateTime>("Survey");

                    b.HasKey("Semester", "year");

                    b.ToTable("Deadline");
                });

            modelBuilder.Entity("GraduationGuideline.data.entities.StepEntity", b =>
                {
                    b.Property<string>("Username");

                    b.Property<string>("StepName");

                    b.Property<string>("Description");

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

                    b.Property<string>("Semester");

                    b.Property<string>("StudentType");

                    b.Property<int>("year");

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
