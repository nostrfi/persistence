﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nostrfi.Persistence;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Nostrfi.Persistence.Migrations
{
    [DbContext(typeof(NostrContext))]
    [Migration("20241129121104_initial_schema")]
    partial class initial_schema
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("nostrfi")
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "uuid-ossp");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Nostrfi.Persistence.Entities.Events", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(65)
                        .HasColumnType("varchar")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<int>("KindId")
                        .HasColumnType("integer")
                        .HasColumnName("kind_id");

                    b.Property<string>("PubKey")
                        .IsRequired()
                        .HasMaxLength(65)
                        .HasColumnType("varchar")
                        .HasColumnName("pubkey");

                    b.Property<string>("Sig")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar")
                        .HasColumnName("sig");

                    b.HasKey("Id")
                        .HasName("id");

                    b.HasIndex("KindId");

                    b.HasIndex("Id", "PubKey")
                        .IsUnique();

                    b.ToTable("events", "nostrfi");
                });

            modelBuilder.Entity("Nostrfi.Persistence.Entities.Kinds", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("varchar")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("kinds", "nostrfi");
                });

            modelBuilder.Entity("Nostrfi.Persistence.Entities.Tags", b =>
                {
                    b.Property<string>("EventId")
                        .HasMaxLength(65)
                        .HasColumnType("varchar")
                        .HasColumnName("event_id");

                    b.Property<string>("Identifier")
                        .HasMaxLength(35)
                        .HasColumnType("varchar")
                        .HasColumnName("identifier");

                    b.PrimitiveCollection<string[]>("Data")
                        .HasColumnType("text[]");

                    b.HasKey("EventId", "Identifier");

                    b.ToTable("tags", "nostrfi");
                });

            modelBuilder.Entity("Nostrfi.Persistence.Entities.Events", b =>
                {
                    b.HasOne("Nostrfi.Persistence.Entities.Kinds", "Kind")
                        .WithMany("Events")
                        .HasForeignKey("KindId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kind");
                });

            modelBuilder.Entity("Nostrfi.Persistence.Entities.Tags", b =>
                {
                    b.HasOne("Nostrfi.Persistence.Entities.Events", "Event")
                        .WithMany("Tags")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("Nostrfi.Persistence.Entities.Events", b =>
                {
                    b.Navigation("Tags");
                });

            modelBuilder.Entity("Nostrfi.Persistence.Entities.Kinds", b =>
                {
                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}