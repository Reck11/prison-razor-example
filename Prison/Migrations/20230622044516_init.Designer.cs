﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Prison.Data;

#nullable disable

namespace Prison.Migrations
{
    [DbContext(typeof(PrisonContext))]
    [Migration("20230622044516_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Prison.Models.Crime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("MinimalPunishment")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SecurityLevel")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Crime");
                });

            modelBuilder.Entity("Prison.Models.Prisoner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int>("CrimeId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ImprisonmentEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ImprisonmentStartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RegistrationNumber")
                        .HasColumnType("int");

                    b.Property<int>("SecurityLevel")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CrimeId");

                    b.ToTable("Prisoner");
                });

            modelBuilder.Entity("Prison.Models.ReeducationMeeting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("MaximumNumberOfPrisoners")
                        .HasColumnType("int");

                    b.Property<int>("ProgramId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ProgramId");

                    b.ToTable("ReeducationMeeting");
                });

            modelBuilder.Entity("Prison.Models.ReeducationProgram", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ReeducationProgram");
                });

            modelBuilder.Entity("Prison.Models.ReeducationStaff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QualificationsType")
                        .HasColumnType("int");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ReeducationStaff");
                });

            modelBuilder.Entity("Prison.Models.Warden", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<DateTime>("EmploymentStartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Warden");
                });

            modelBuilder.Entity("PrisonerReeducationMeeting", b =>
                {
                    b.Property<int>("MeetingsId")
                        .HasColumnType("int");

                    b.Property<int>("PrisonersId")
                        .HasColumnType("int");

                    b.HasKey("MeetingsId", "PrisonersId");

                    b.HasIndex("PrisonersId");

                    b.ToTable("PrisonerReeducationMeeting");
                });

            modelBuilder.Entity("ReeducationProgramReeducationStaff", b =>
                {
                    b.Property<int>("ProgramsId")
                        .HasColumnType("int");

                    b.Property<int>("ReeducationStaffId")
                        .HasColumnType("int");

                    b.HasKey("ProgramsId", "ReeducationStaffId");

                    b.HasIndex("ReeducationStaffId");

                    b.ToTable("ReeducationProgramReeducationStaff");
                });

            modelBuilder.Entity("Prison.Models.Prisoner", b =>
                {
                    b.HasOne("Prison.Models.Crime", "Crime")
                        .WithMany()
                        .HasForeignKey("CrimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Crime");
                });

            modelBuilder.Entity("Prison.Models.ReeducationMeeting", b =>
                {
                    b.HasOne("Prison.Models.ReeducationProgram", "Program")
                        .WithMany("Meetings")
                        .HasForeignKey("ProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Program");
                });

            modelBuilder.Entity("PrisonerReeducationMeeting", b =>
                {
                    b.HasOne("Prison.Models.ReeducationMeeting", null)
                        .WithMany()
                        .HasForeignKey("MeetingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Prison.Models.Prisoner", null)
                        .WithMany()
                        .HasForeignKey("PrisonersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReeducationProgramReeducationStaff", b =>
                {
                    b.HasOne("Prison.Models.ReeducationProgram", null)
                        .WithMany()
                        .HasForeignKey("ProgramsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Prison.Models.ReeducationStaff", null)
                        .WithMany()
                        .HasForeignKey("ReeducationStaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Prison.Models.ReeducationProgram", b =>
                {
                    b.Navigation("Meetings");
                });
#pragma warning restore 612, 618
        }
    }
}
