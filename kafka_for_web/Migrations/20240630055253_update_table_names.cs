using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kafka_for_web.Migrations
{
    /// <inheritdoc />
    public partial class update_table_names : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consumer_ConsumerGroup_ConsumerGroupId",
                table: "Consumer");

            migrationBuilder.DropForeignKey(
                name: "FK_ConsumerGroup_Cluster_ClusterId",
                table: "ConsumerGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_Partition_PartitionId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_Producer_ProducerId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Partition_Topic_TopicId",
                table: "Partition");

            migrationBuilder.DropForeignKey(
                name: "FK_Producer_Message_MostRecentMessageId",
                table: "Producer");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscription_Consumer_ConsumerId",
                table: "Subscription");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscription_Topic_TopicId",
                table: "Subscription");

            migrationBuilder.DropForeignKey(
                name: "FK_Topic_Cluster_ClusterId",
                table: "Topic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Topic",
                table: "Topic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subscription",
                table: "Subscription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Producer",
                table: "Producer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Partition",
                table: "Partition");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConsumerGroup",
                table: "ConsumerGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Consumer",
                table: "Consumer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cluster",
                table: "Cluster");

            migrationBuilder.RenameTable(
                name: "Topic",
                newName: "topic");

            migrationBuilder.RenameTable(
                name: "Subscription",
                newName: "subscription");

            migrationBuilder.RenameTable(
                name: "Producer",
                newName: "producer");

            migrationBuilder.RenameTable(
                name: "Partition",
                newName: "partition");

            migrationBuilder.RenameTable(
                name: "ConsumerGroup",
                newName: "consumerGroup");

            migrationBuilder.RenameTable(
                name: "Consumer",
                newName: "consumer");

            migrationBuilder.RenameTable(
                name: "Cluster",
                newName: "cluster");

            migrationBuilder.RenameIndex(
                name: "IX_Topic_ClusterId",
                table: "topic",
                newName: "IX_topic_ClusterId");

            migrationBuilder.RenameIndex(
                name: "IX_Subscription_TopicId",
                table: "subscription",
                newName: "IX_subscription_TopicId");

            migrationBuilder.RenameIndex(
                name: "IX_Subscription_ConsumerId",
                table: "subscription",
                newName: "IX_subscription_ConsumerId");

            migrationBuilder.RenameIndex(
                name: "IX_Producer_MostRecentMessageId",
                table: "producer",
                newName: "IX_producer_MostRecentMessageId");

            migrationBuilder.RenameIndex(
                name: "IX_Partition_TopicId",
                table: "partition",
                newName: "IX_partition_TopicId");

            migrationBuilder.RenameIndex(
                name: "IX_ConsumerGroup_ClusterId",
                table: "consumerGroup",
                newName: "IX_consumerGroup_ClusterId");

            migrationBuilder.RenameIndex(
                name: "IX_Consumer_ConsumerGroupId",
                table: "consumer",
                newName: "IX_consumer_ConsumerGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_topic",
                table: "topic",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_subscription",
                table: "subscription",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_producer",
                table: "producer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_partition",
                table: "partition",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_consumerGroup",
                table: "consumerGroup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_consumer",
                table: "consumer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cluster",
                table: "cluster",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_consumer_consumerGroup_ConsumerGroupId",
                table: "consumer",
                column: "ConsumerGroupId",
                principalTable: "consumerGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_consumerGroup_cluster_ClusterId",
                table: "consumerGroup",
                column: "ClusterId",
                principalTable: "cluster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_partition_PartitionId",
                table: "Message",
                column: "PartitionId",
                principalTable: "partition",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_producer_ProducerId",
                table: "Message",
                column: "ProducerId",
                principalTable: "producer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_partition_topic_TopicId",
                table: "partition",
                column: "TopicId",
                principalTable: "topic",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_producer_Message_MostRecentMessageId",
                table: "producer",
                column: "MostRecentMessageId",
                principalTable: "Message",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_subscription_consumer_ConsumerId",
                table: "subscription",
                column: "ConsumerId",
                principalTable: "consumer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_subscription_topic_TopicId",
                table: "subscription",
                column: "TopicId",
                principalTable: "topic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_topic_cluster_ClusterId",
                table: "topic",
                column: "ClusterId",
                principalTable: "cluster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_consumer_consumerGroup_ConsumerGroupId",
                table: "consumer");

            migrationBuilder.DropForeignKey(
                name: "FK_consumerGroup_cluster_ClusterId",
                table: "consumerGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_partition_PartitionId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_producer_ProducerId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_partition_topic_TopicId",
                table: "partition");

            migrationBuilder.DropForeignKey(
                name: "FK_producer_Message_MostRecentMessageId",
                table: "producer");

            migrationBuilder.DropForeignKey(
                name: "FK_subscription_consumer_ConsumerId",
                table: "subscription");

            migrationBuilder.DropForeignKey(
                name: "FK_subscription_topic_TopicId",
                table: "subscription");

            migrationBuilder.DropForeignKey(
                name: "FK_topic_cluster_ClusterId",
                table: "topic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_topic",
                table: "topic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_subscription",
                table: "subscription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_producer",
                table: "producer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_partition",
                table: "partition");

            migrationBuilder.DropPrimaryKey(
                name: "PK_consumerGroup",
                table: "consumerGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_consumer",
                table: "consumer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cluster",
                table: "cluster");

            migrationBuilder.RenameTable(
                name: "topic",
                newName: "Topic");

            migrationBuilder.RenameTable(
                name: "subscription",
                newName: "Subscription");

            migrationBuilder.RenameTable(
                name: "producer",
                newName: "Producer");

            migrationBuilder.RenameTable(
                name: "partition",
                newName: "Partition");

            migrationBuilder.RenameTable(
                name: "consumerGroup",
                newName: "ConsumerGroup");

            migrationBuilder.RenameTable(
                name: "consumer",
                newName: "Consumer");

            migrationBuilder.RenameTable(
                name: "cluster",
                newName: "Cluster");

            migrationBuilder.RenameIndex(
                name: "IX_topic_ClusterId",
                table: "Topic",
                newName: "IX_Topic_ClusterId");

            migrationBuilder.RenameIndex(
                name: "IX_subscription_TopicId",
                table: "Subscription",
                newName: "IX_Subscription_TopicId");

            migrationBuilder.RenameIndex(
                name: "IX_subscription_ConsumerId",
                table: "Subscription",
                newName: "IX_Subscription_ConsumerId");

            migrationBuilder.RenameIndex(
                name: "IX_producer_MostRecentMessageId",
                table: "Producer",
                newName: "IX_Producer_MostRecentMessageId");

            migrationBuilder.RenameIndex(
                name: "IX_partition_TopicId",
                table: "Partition",
                newName: "IX_Partition_TopicId");

            migrationBuilder.RenameIndex(
                name: "IX_consumerGroup_ClusterId",
                table: "ConsumerGroup",
                newName: "IX_ConsumerGroup_ClusterId");

            migrationBuilder.RenameIndex(
                name: "IX_consumer_ConsumerGroupId",
                table: "Consumer",
                newName: "IX_Consumer_ConsumerGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Topic",
                table: "Topic",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subscription",
                table: "Subscription",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Producer",
                table: "Producer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Partition",
                table: "Partition",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConsumerGroup",
                table: "ConsumerGroup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Consumer",
                table: "Consumer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cluster",
                table: "Cluster",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumer_ConsumerGroup_ConsumerGroupId",
                table: "Consumer",
                column: "ConsumerGroupId",
                principalTable: "ConsumerGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumerGroup_Cluster_ClusterId",
                table: "ConsumerGroup",
                column: "ClusterId",
                principalTable: "Cluster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Partition_PartitionId",
                table: "Message",
                column: "PartitionId",
                principalTable: "Partition",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Producer_ProducerId",
                table: "Message",
                column: "ProducerId",
                principalTable: "Producer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Partition_Topic_TopicId",
                table: "Partition",
                column: "TopicId",
                principalTable: "Topic",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Producer_Message_MostRecentMessageId",
                table: "Producer",
                column: "MostRecentMessageId",
                principalTable: "Message",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscription_Consumer_ConsumerId",
                table: "Subscription",
                column: "ConsumerId",
                principalTable: "Consumer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscription_Topic_TopicId",
                table: "Subscription",
                column: "TopicId",
                principalTable: "Topic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Topic_Cluster_ClusterId",
                table: "Topic",
                column: "ClusterId",
                principalTable: "Cluster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
