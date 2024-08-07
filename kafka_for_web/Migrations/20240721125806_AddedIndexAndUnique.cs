using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kafka_for_web.Migrations
{
    /// <inheritdoc />
    public partial class AddedIndexAndUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "offset",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConsumerId = table.Column<long>(type: "bigint", nullable: false),
                    TopicId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_offset", x => x.Id);
                    table.ForeignKey(
                        name: "FK_offset_consumer_ConsumerId",
                        column: x => x.ConsumerId,
                        principalTable: "consumer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_offset_topic_TopicId",
                        column: x => x.TopicId,
                        principalTable: "topic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_topic_Name",
                table: "topic",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_producer_Name",
                table: "producer",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_consumerGroup_Name",
                table: "consumerGroup",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_consumer_Name",
                table: "consumer",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cluster_Name",
                table: "cluster",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_offset_ConsumerId",
                table: "offset",
                column: "ConsumerId");

            migrationBuilder.CreateIndex(
                name: "IX_offset_TopicId",
                table: "offset",
                column: "TopicId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "offset");

            migrationBuilder.DropIndex(
                name: "IX_topic_Name",
                table: "topic");

            migrationBuilder.DropIndex(
                name: "IX_producer_Name",
                table: "producer");

            migrationBuilder.DropIndex(
                name: "IX_consumerGroup_Name",
                table: "consumerGroup");

            migrationBuilder.DropIndex(
                name: "IX_consumer_Name",
                table: "consumer");

            migrationBuilder.DropIndex(
                name: "IX_cluster_Name",
                table: "cluster");
        }
    }
}
