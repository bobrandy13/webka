using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kafka_for_web.Migrations
{
    /// <inheritdoc />
    public partial class optional_subscrition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_subscription_consumerGroup_ConsumerGroupId",
                table: "subscription");

            migrationBuilder.AlterColumn<long>(
                name: "ConsumerGroupId",
                table: "subscription",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_subscription_consumerGroup_ConsumerGroupId",
                table: "subscription",
                column: "ConsumerGroupId",
                principalTable: "consumerGroup",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_subscription_consumerGroup_ConsumerGroupId",
                table: "subscription");

            migrationBuilder.AlterColumn<long>(
                name: "ConsumerGroupId",
                table: "subscription",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_subscription_consumerGroup_ConsumerGroupId",
                table: "subscription",
                column: "ConsumerGroupId",
                principalTable: "consumerGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
