using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nostrfi.Relay.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class nip_01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "nostrfi");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "events",
                schema: "nostrfi",
                columns: table => new
                {
                    identifier = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    received = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Event = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("identifier", x => x.identifier);
                });

            migrationBuilder.CreateIndex(
                name: "IX_events_identifier_received",
                schema: "nostrfi",
                table: "events",
                columns: new[] { "identifier", "received" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "events",
                schema: "nostrfi");
        }
    }
}
