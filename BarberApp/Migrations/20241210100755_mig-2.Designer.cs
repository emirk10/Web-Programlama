﻿// <auto-generated />
using System;
using BarberApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BarberApp.Migrations
{
    [DbContext(typeof(BarberDbContext))]
    [Migration("20241210100755_mig-2")]
    partial class mig2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BarberApp.Models.Admin", b =>
                {
                    b.Property<int>("AdminID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AdminID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("AdminID");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("BarberApp.Models.Appointment", b =>
                {
                    b.Property<int>("AppointmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AppointmentID"));

                    b.Property<DateTime>("AppointmentDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("BarberID")
                        .HasColumnType("integer");

                    b.Property<int>("CustomerID")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("AppointmentID");

                    b.HasIndex("BarberID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("BarberApp.Models.Barber", b =>
                {
                    b.Property<int>("BarberID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BarberID"));

                    b.Property<int?>("AdminID")
                        .HasColumnType("integer");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Rating")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Specialization")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("BarberID");

                    b.HasIndex("AdminID");

                    b.ToTable("Barbers");
                });

            modelBuilder.Entity("BarberApp.Models.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CategoryID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CategoryID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("BarberApp.Models.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CustomerID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CustomerID");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("BarberApp.Models.Expanse", b =>
                {
                    b.Property<int>("ExpanseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ExpanseID"));

                    b.Property<int?>("AdminID")
                        .HasColumnType("integer");

                    b.Property<float>("ExpanseAmount")
                        .HasColumnType("real");

                    b.Property<string>("ExpanseCategory")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly>("ExpanseDate")
                        .HasColumnType("date");

                    b.Property<string>("ExpanseDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ExpanseID");

                    b.HasIndex("AdminID");

                    b.ToTable("Expanses");
                });

            modelBuilder.Entity("BarberApp.Models.Review", b =>
                {
                    b.Property<int>("ReviewID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ReviewID"));

                    b.Property<int>("BarberID")
                        .HasColumnType("integer");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CustomerID")
                        .HasColumnType("integer");

                    b.Property<string>("Rating")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ReviewDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("ReviewID");

                    b.HasIndex("BarberID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("BarberApp.Models.Schedule", b =>
                {
                    b.Property<int>("ScheduleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ScheduleID"));

                    b.Property<int>("BarberID")
                        .HasColumnType("integer");

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("integer");

                    b.Property<TimeOnly>("EndTime")
                        .HasColumnType("time without time zone");

                    b.Property<TimeOnly>("StartTime")
                        .HasColumnType("time without time zone");

                    b.HasKey("ScheduleID");

                    b.HasIndex("BarberID");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("BarberApp.Models.Service", b =>
                {
                    b.Property<int>("ServiceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ServiceID"));

                    b.Property<int?>("AdminID")
                        .HasColumnType("integer");

                    b.Property<int>("CategoryID")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Duration")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("ServiceID");

                    b.HasIndex("AdminID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("BarberApp.Models.ServiceAppointment", b =>
                {
                    b.Property<int>("AppointmentID")
                        .HasColumnType("integer");

                    b.Property<int>("ServiceID")
                        .HasColumnType("integer");

                    b.HasKey("AppointmentID", "ServiceID");

                    b.HasIndex("ServiceID");

                    b.ToTable("ServiceAppointments");
                });

            modelBuilder.Entity("BarberApp.Models.Appointment", b =>
                {
                    b.HasOne("BarberApp.Models.Barber", "Barber")
                        .WithMany("Appointments")
                        .HasForeignKey("BarberID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BarberApp.Models.Customer", "Customer")
                        .WithMany("Appointments")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Barber");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("BarberApp.Models.Barber", b =>
                {
                    b.HasOne("BarberApp.Models.Admin", null)
                        .WithMany("Barbers")
                        .HasForeignKey("AdminID");
                });

            modelBuilder.Entity("BarberApp.Models.Expanse", b =>
                {
                    b.HasOne("BarberApp.Models.Admin", null)
                        .WithMany("Expanses")
                        .HasForeignKey("AdminID");
                });

            modelBuilder.Entity("BarberApp.Models.Review", b =>
                {
                    b.HasOne("BarberApp.Models.Barber", "Barber")
                        .WithMany("Reviews")
                        .HasForeignKey("BarberID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BarberApp.Models.Customer", "Customer")
                        .WithMany("Reviews")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Barber");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("BarberApp.Models.Schedule", b =>
                {
                    b.HasOne("BarberApp.Models.Barber", "Barber")
                        .WithMany("Schedules")
                        .HasForeignKey("BarberID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Barber");
                });

            modelBuilder.Entity("BarberApp.Models.Service", b =>
                {
                    b.HasOne("BarberApp.Models.Admin", null)
                        .WithMany("Services")
                        .HasForeignKey("AdminID");

                    b.HasOne("BarberApp.Models.Category", "Category")
                        .WithMany("Services")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("BarberApp.Models.ServiceAppointment", b =>
                {
                    b.HasOne("BarberApp.Models.Appointment", "Appointment")
                        .WithMany("ServiceAppointments")
                        .HasForeignKey("AppointmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BarberApp.Models.Service", "Service")
                        .WithMany("ServiceAppointments")
                        .HasForeignKey("ServiceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("BarberApp.Models.Admin", b =>
                {
                    b.Navigation("Barbers");

                    b.Navigation("Expanses");

                    b.Navigation("Services");
                });

            modelBuilder.Entity("BarberApp.Models.Appointment", b =>
                {
                    b.Navigation("ServiceAppointments");
                });

            modelBuilder.Entity("BarberApp.Models.Barber", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Reviews");

                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("BarberApp.Models.Category", b =>
                {
                    b.Navigation("Services");
                });

            modelBuilder.Entity("BarberApp.Models.Customer", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("BarberApp.Models.Service", b =>
                {
                    b.Navigation("ServiceAppointments");
                });
#pragma warning restore 612, 618
        }
    }
}
