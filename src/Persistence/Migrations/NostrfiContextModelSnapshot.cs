﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nostrfi.Database.Persistence;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Nostrfi.Database.Persistence.Migrations
{
    [DbContext(typeof(NostrfiContext))]
    partial class NostrfiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("nostrfi")
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "uuid-ossp");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Nostrfi.Events", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<int>("Kind")
                        .HasColumnType("integer")
                        .HasColumnName("kind");

                    b.Property<string>("PublicKey")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("public_key");

                    b.Property<string>("Signature")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("signature");

                    b.HasKey("Id");

                    b.ToTable("Events", "nostrfi");
                });

            modelBuilder.Entity("Nostrfi.Tags", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("EventId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("event_id");

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("tag");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("Tags", "nostrfi");
                });

            modelBuilder.Entity("Nostrfi.Tags", b =>
                {
                    b.HasOne("Nostrfi.Events", "Event")
                        .WithMany("Tags")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("Nostrfi.Events", b =>
                {
                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}
