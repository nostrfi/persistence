﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nostrfi.Relay.Persistence;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Nostrfi.Relay.Persistence.Migrations
{
    [DbContext(typeof(NostrContext))]
    partial class NostrContextModelSnapshot : ModelSnapshot
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

            modelBuilder.Entity("Nostrfi.Relay.Persistence.Entities.Events", b =>
                {
                    b.Property<Guid>("Identifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("identifier")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<DateTimeOffset>("Received")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("received");

                    b.HasKey("Identifier")
                        .HasName("identifier");

                    b.HasIndex("Identifier", "Received")
                        .IsUnique();

                    b.ToTable("events", "nostrfi");
                });

            modelBuilder.Entity("Nostrfi.Relay.Persistence.Entities.Events", b =>
                {
                    b.OwnsOne("Nostrfi.Relay.Persistence.Entities.Nostr.Event", "Event", b1 =>
                        {
                            b1.Property<Guid>("EventsIdentifier")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uuid");

                            b1.Property<string>("Content")
                                .HasColumnType("text");

                            b1.Property<DateTimeOffset>("CreatedAt")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<string>("Id")
                                .HasColumnType("text");

                            b1.Property<int>("Kind")
                                .HasColumnType("integer");

                            b1.Property<string>("PublicKey")
                                .HasColumnType("text");

                            b1.Property<string>("Sig")
                                .HasColumnType("text");

                            b1.HasKey("EventsIdentifier");

                            b1.ToTable("events", "nostrfi");

                            b1.ToJson("Event");

                            b1.WithOwner()
                                .HasForeignKey("EventsIdentifier");

                            b1.OwnsOne("System.Collections.Generic.List<string[]>", "Tags", b2 =>
                                {
                                    b2.Property<Guid>("EventsIdentifier")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Capacity")
                                        .HasColumnType("integer");

                                    b2.HasKey("EventsIdentifier");

                                    b2.ToTable("events", "nostrfi");

                                    b2.ToJson("Tags");

                                    b2.WithOwner()
                                        .HasForeignKey("EventsIdentifier");
                                });

                            b1.Navigation("Tags");
                        });

                    b.Navigation("Event");
                });
#pragma warning restore 612, 618
        }
    }
}
