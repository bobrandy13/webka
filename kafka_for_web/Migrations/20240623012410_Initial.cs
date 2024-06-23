using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kafka_for_web.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cluster",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplicationCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cluster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConsumerGroup",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ClusterId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumerGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsumerGroup_Cluster_ClusterId",
                        column: x => x.ClusterId,
                        principalTable: "Cluster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Topic",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NumPartitions = table.Column<long>(type: "bigint", nullable: false),
                    ReplicationFactor = table.Column<long>(type: "bigint", nullable: false),
                    ClusterId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Topic_Cluster_ClusterId",
                        column: x => x.ClusterId,
                        principalTable: "Cluster",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Consumer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ConsumerGroupId = table.Column<long>(type: "bigint", nullable: false),
                    EnableAutoCommit = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consumer_ConsumerGroup_ConsumerGroupId",
                        column: x => x.ConsumerGroupId,
                        principalTable: "ConsumerGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Partition",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReplicationFactor = table.Column<long>(type: "bigint", nullable: false),
                    LogDir = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TopicId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partition_Topic_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topic",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Subscription",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsumerId = table.Column<long>(type: "bigint", nullable: false),
                    TopicId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscription_Consumer_ConsumerId",
                        column: x => x.ConsumerId,
                        principalTable: "Consumer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subscription_Topic_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<long>(type: "bigint", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartitionId = table.Column<long>(type: "bigint", nullable: true),
                    ProducerId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_Partition_PartitionId",
                        column: x => x.PartitionId,
                        principalTable: "Partition",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Producer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MostRecentMessageId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Producer_Message_MostRecentMessageId",
                        column: x => x.MostRecentMessageId,
                        principalTable: "Message",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consumer_ConsumerGroupId",
                table: "Consumer",
                column: "ConsumerGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumerGroup_ClusterId",
                table: "ConsumerGroup",
                column: "ClusterId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_PartitionId",
                table: "Message",
                column: "PartitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ProducerId",
                table: "Message",
                column: "ProducerId");

            migrationBuilder.CreateIndex(
                name: "IX_Partition_TopicId",
                table: "Partition",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Producer_MostRecentMessageId",
                table: "Producer",
                column: "MostRecentMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_ConsumerId",
                table: "Subscription",
                column: "ConsumerId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_TopicId",
                table: "Subscription",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Topic_ClusterId",
                table: "Topic",
                column: "ClusterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Producer_ProducerId",
                table: "Message",
                column: "ProducerId",
                principalTable: "Producer",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Topic_Cluster_ClusterId",
                table: "Topic");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_Partition_PartitionId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_Producer_ProducerId",
                table: "Message");

            migrationBuilder.DropTable(
                name: "Subscription");

            migrationBuilder.DropTable(
                name: "Consumer");

            migrationBuilder.DropTable(
                name: "ConsumerGroup");

            migrationBuilder.DropTable(
                name: "Cluster");

            migrationBuilder.DropTable(
                name: "Partition");

            migrationBuilder.DropTable(
                name: "Topic");

            migrationBuilder.DropTable(
                name: "Producer");

            migrationBuilder.DropTable(
                name: "Message");
        }
    }
}
