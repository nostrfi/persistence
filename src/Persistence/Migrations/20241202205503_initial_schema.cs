using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Nostrfi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial_schema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "nostrfi");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "kinds",
                schema: "nostrfi",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar", maxLength: 35, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kinds", x => x.id);
                });
    migrationBuilder.InsertData(
                table: "kinds",
                columns: new[] { "id", "name", "description" },
                values: new object[,]
                {
                    { 0, "Metadata" , "NIP 01, NIP 24" },
                    { 1, "Text", "NIP 01" },
                    { 2, "RecommendRelay", "" },
                    { 3, "Contacts", "NIP 02 , NIP 24 "},
                    { 4 , "EncryptedDirectMessage", "" },
                    { 5, "EventDeletion", "" },
                    { 6, "Repost", "" },
                    { 7, "Reaction", "" },
                    { 8 , "BadgeAward", "" },
                    { 9, "GroupChat", "NIP 29" },
                    { 11, "GroupNote", "NIP 29" },
                    { 12, "GroupReply", "NIP 29" },
                    { 16, "GenericRepost", "" },
                    { 40 , "ChannelCreation", "" },
                    { 41 , "ChannelMetadata", "" },
                    { 42, "ChannelMessage", "" },
                    { 43, "ChannelHideMessage", "" },
                    { 44, "ChannelMuteUser", "" },
                    { 1063 , "Media", "" },
                    { 1984 , "Report", "" },
                    { 1985 , "Label", "" },
                    { 5000, "DVMReqTextExtraction" , "NIP 90"},
                    { 5001, "DVMReqTextSummarization", "NIP 90" },
                    { 5002, "DVMReqTextTranslation", "NIP 90"},
                    { 5050, "DVMReqTextGeneration", "NIP 90" },
                    { 5100, "DVMReqImageGeneration", "NIP 90 Image : 5100 - 5199"},
                    { 5250, "DVMReqTextToSpeech", "NIP 90 - Text-to-Speech: 5200-5299"}, 
                    { 5300, "DVMReqDiscoveryNostrContent", "NIP 90 - Discovery: 5300 - 5301" },
                    { 5301, "DVMReqDiscoveryNostrPeople", "NIP 90" }, 
                    { 5900, "DVMReqTimestamping", "" },
                    { 5905, "DVMEventSchedule", "" },
                    { 7000, "DVMJobFeedback", "" },
                    { 7001, "Subscribe", ""},
                    { 7002, "Unsubscribe", ""},
                    { 7003, "SubscriptionReceipt", "" },
                    { 7373 , "CashuReserve", "Cashu Wallet" },
                    { 7374 , "CashuQuote", "Cashu Wallet" },
                    { 7375,  "CashuToken", "Cashu Wallet" },
                    { 7376, "WalletChange", "Cashu Wallet" },
                    { 9000, "GroupAdminAddUser", "NIP 29" }, 
                    { 9001, "GroupAdminRemoveUser", "NIP 29" },
                    { 9002, "GroupAdminEditMetadata", "NIP 29" },
                    { 9006, "GroupAdminEditStatus", "NIP 29" },
                    { 9007, "GroupAdminCreateGroup", "NIP 29" },
                    { 9021, "GroupAdminRequestJoin", "NIP 29" },
                    { 10000, "MuteList", "" },
                    { 10001, "PinList", "" },
                    { 10002, "RelayList", "" },
                    { 10003, "BookmarkList", "" },
                    { 10004, "CommunityList", "" },
                    { 10005, "PublicChatList", "" },
                    { 10006, "BlockRelayList", "" },
                    { 10007, "SearchRelayList", "" },
                    { 10009, "SimpleGroupList", "" },
                    { 10015, "InterestList", "" },
                    { 10019, "CashuMintList", "" },
                    { 10030, "EmojiList", "" },
                    { 10050, "DirectMessageReceiveRelayList", "" },
                    { 10063, "BlossomList", "" },
                    { 13194, "NostrWaletConnectInfo", "" },
                    { 17000, "TierList", "" },
                    { 30000, "FollowSet", "" },
                    { 30001, "CategorizedBookmarkList", "" },
                    { 30002, "RelaySet", "" },
                    { 30003, "BookmarkSet", "" },
                    { 30004, "ArticleCurationSet", "" },
                    { 30005, "InterestSet", "" },
                    { 30030, "EmojiSet", "" },
                    { 30040, "ModularArticle", "" },
                    { 30041, "ModularArticleItem", "" },
                    { 30818, "Wiki", "" },
                    { 31234, "Draft", "" },
                    { 37001, "SubscriptionTier", "" },
                    { 38000, "EcashMintRecommendation", "" },
                    { 39802, "HighlightSet", "" },
                    { 9321,  "Nutzap", "" },
                    { 9734,  "ZapRequest", "" }, 
                    { 9735,  "Zap", "" }, 
                    { 9802,  "Highlight", "" },
                    { 22242, "ClientAuth", "" },
                    { 23194, "NostrWalletConnectReq", "" },
                    { 23195, "NostrWalletConnectRes", "" },
                    { 24133, "NostrConnect", "" },
                    { 24242, "BlossomUpload", "" },
                    { 27235, "HttpAuth", "" },
                    { 30008, "ProfileBadge", "" },
                    { 30009, "BadgeDefinition", "" },
                    { 30017, "MarketStall", "" },
                    { 30018, "MarketProduct", "" },
                    { 30023, "Article", "" },
                    { 30078, "AppSpecificData", "" },
                    { 30402, "Classified", "" },
                    { 34235, "HorizontalVideo", "" },
                    { 34236, "VerticalVideo", "" },  
                    { 37375, "CashuWallet", "" },  
                    { 39000, "GroupMetadata", "NIP 29" }, 
                    { 39001, "GroupAdmins", "NIP 29" }, 
                    { 39002, "GroupMembers", "NIP 29" }, 
                    { 31989, "AppRecommendation", "NIP 89" },
                    { 31990, "AppHandler", "NIP 89" },
                }
            );
    
            migrationBuilder.CreateTable(
                name: "events",
                schema: "nostrfi",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar", maxLength: 65, nullable: false),
                    publickey = table.Column<string>(type: "varchar", maxLength: 65, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    kind_id = table.Column<int>(type: "integer", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    signature = table.Column<string>(type: "varchar", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.id);
                    table.ForeignKey(
                        name: "FK_events_kinds_kind_id",
                        column: x => x.kind_id,
                        principalSchema: "nostrfi",
                        principalTable: "kinds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                schema: "nostrfi",
                columns: table => new
                {
                    event_id = table.Column<string>(type: "varchar", maxLength: 65, nullable: false),
                    identifier = table.Column<string>(type: "varchar", maxLength: 35, nullable: false),
                    Data = table.Column<string[]>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => new { x.event_id, x.identifier });
                    table.ForeignKey(
                        name: "FK_tags_events_event_id",
                        column: x => x.event_id,
                        principalSchema: "nostrfi",
                        principalTable: "events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_events_id_publickey",
                schema: "nostrfi",
                table: "events",
                columns: new[] { "id", "publickey" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_events_kind_id",
                schema: "nostrfi",
                table: "events",
                column: "kind_id");

            migrationBuilder.CreateIndex(
                name: "IX_kinds_name",
                schema: "nostrfi",
                table: "kinds",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tags",
                schema: "nostrfi");

            migrationBuilder.DropTable(
                name: "events",
                schema: "nostrfi");

            migrationBuilder.DropTable(
                name: "kinds",
                schema: "nostrfi");
        }
    }
}
