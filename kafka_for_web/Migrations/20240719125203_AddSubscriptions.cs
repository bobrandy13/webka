using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kafka_for_web.Migrations
{
    /// <inheritdoc />
    public partial class AddSubscriptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ConsumerGroupId",
                table: "subscription",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_subscription_ConsumerGroupId",
                table: "subscription",
                column: "ConsumerGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_subscription_consumerGroup_ConsumerGroupId",
                table: "subscription",
                column: "ConsumerGroupId",
                principalTable: "consumerGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_subscription_consumerGroup_ConsumerGroupId",
                table: "subscription");

            migrationBuilder.DropIndex(
                name: "IX_subscription_ConsumerGroupId",
                table: "subscription");

            migrationBuilder.DropColumn(
                name: "ConsumerGroupId",
                table: "subscription");
        }
    }
}
