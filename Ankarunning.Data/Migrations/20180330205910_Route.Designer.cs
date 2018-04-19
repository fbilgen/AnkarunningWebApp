﻿// <auto-generated />
using Ankarunning.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Ankarunning.Data.Migrations
{
    [DbContext(typeof(AnkarunningContext))]
    [Migration("20180330205910_Route")]
    partial class Route
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Ankarunning.Data.Route", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<byte[]>("Content")
                        .IsRequired();

                    b.Property<string>("ContentType")
                        .IsRequired();

                    b.Property<short>("Distance");

                    b.Property<string>("FileName")
                        .IsRequired();

                    b.Property<string>("Latitude")
                        .IsRequired();

                    b.Property<string>("Longitute")
                        .IsRequired();

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Route");
                });

            modelBuilder.Entity("Ankarunning.Data.Training", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<DateTime>("DateTime");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<short>("Distance");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<long>("RouteId");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.ToTable("Training");
                });

            modelBuilder.Entity("Ankarunning.Data.TrainingPhoto", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<byte[]>("Content");

                    b.Property<string>("ContentType");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<long>("TrainingId");

                    b.HasKey("Id");

                    b.HasIndex("TrainingId")
                        .IsUnique();

                    b.ToTable("TrainingPhoto");
                });

            modelBuilder.Entity("Ankarunning.Data.Training", b =>
                {
                    b.HasOne("Ankarunning.Data.Route", "Route")
                        .WithMany()
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ankarunning.Data.TrainingPhoto", b =>
                {
                    b.HasOne("Ankarunning.Data.Training", "Training")
                        .WithOne("TrainingPhoto")
                        .HasForeignKey("Ankarunning.Data.TrainingPhoto", "TrainingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
