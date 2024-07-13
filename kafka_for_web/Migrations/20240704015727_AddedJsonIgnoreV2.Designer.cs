﻿// <auto-generated />
using System;
using Kafka_for_web.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kafka_for_web.Migrations
{
    [DbContext(typeof(KafkaContext))]
    [Migration("20240704015727_AddedJsonIgnoreV2")]
    partial class AddedJsonIgnoreV2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Kafka_for_web.Models.Cluster", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("ReplicationCount")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("cluster", (string)null);
                });

            modelBuilder.Entity("Kafka_for_web.Models.Consumer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long?>("ConsumerGroupId")
                        .HasColumnType("bigint");

                    b.Property<bool>("EnableAutoCommit")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("ConsumerGroupId");

                    b.ToTable("consumer", (string)null);
                });

            modelBuilder.Entity("Kafka_for_web.Models.ConsumerGroup", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ClusterId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("ClusterId");

                    b.ToTable("consumerGroup", (string)null);
                });

            modelBuilder.Entity("Kafka_for_web.Models.Message", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long?>("Key")
                        .HasColumnType("bigint");

                    b.Property<long>("ProducerId")
                        .HasColumnType("bigint");

                    b.Property<long>("TopicId")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.HasKey("Id");

                    b.HasIndex("ProducerId");

                    b.HasIndex("TopicId");

                    b.ToTable("message", (string)null);
                });

            modelBuilder.Entity("Kafka_for_web.Models.Partition", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<long>("TopicId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.ToTable("partition", (string)null);
                });

            modelBuilder.Entity("Kafka_for_web.Models.Producer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("producer", (string)null);
                });

            modelBuilder.Entity("Kafka_for_web.Models.Subscription", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ConsumerId")
                        .HasColumnType("bigint");

                    b.Property<long>("TopicId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ConsumerId");

                    b.HasIndex("TopicId");

                    b.ToTable("subscription", (string)null);
                });

            modelBuilder.Entity("Kafka_for_web.Models.Topic", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ClusterId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<long>("NumPartitions")
                        .HasColumnType("bigint");

                    b.Property<long>("ReplicationFactor")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ClusterId");

                    b.ToTable("topic", (string)null);
                });

            modelBuilder.Entity("Kafka_for_web.Models.Consumer", b =>
                {
                    b.HasOne("Kafka_for_web.Models.ConsumerGroup", null)
                        .WithMany("Consumers")
                        .HasForeignKey("ConsumerGroupId");
                });

            modelBuilder.Entity("Kafka_for_web.Models.ConsumerGroup", b =>
                {
                    b.HasOne("Kafka_for_web.Models.Cluster", "Cluster")
                        .WithMany()
                        .HasForeignKey("ClusterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cluster");
                });

            modelBuilder.Entity("Kafka_for_web.Models.Message", b =>
                {
                    b.HasOne("Kafka_for_web.Models.Producer", "Producer")
                        .WithMany("Messages")
                        .HasForeignKey("ProducerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kafka_for_web.Models.Partition", "Topic")
                        .WithMany("Messages")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producer");

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("Kafka_for_web.Models.Partition", b =>
                {
                    b.HasOne("Kafka_for_web.Models.Topic", "Topic")
                        .WithMany("Partitions")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("Kafka_for_web.Models.Subscription", b =>
                {
                    b.HasOne("Kafka_for_web.Models.Consumer", "Consumer")
                        .WithMany("Subscriptions")
                        .HasForeignKey("ConsumerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kafka_for_web.Models.Topic", "Topic")
                        .WithMany()
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Consumer");

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("Kafka_for_web.Models.Topic", b =>
                {
                    b.HasOne("Kafka_for_web.Models.Cluster", "Cluster")
                        .WithMany("Topics")
                        .HasForeignKey("ClusterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cluster");
                });

            modelBuilder.Entity("Kafka_for_web.Models.Cluster", b =>
                {
                    b.Navigation("Topics");
                });

            modelBuilder.Entity("Kafka_for_web.Models.Consumer", b =>
                {
                    b.Navigation("Subscriptions");
                });

            modelBuilder.Entity("Kafka_for_web.Models.ConsumerGroup", b =>
                {
                    b.Navigation("Consumers");
                });

            modelBuilder.Entity("Kafka_for_web.Models.Partition", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("Kafka_for_web.Models.Producer", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("Kafka_for_web.Models.Topic", b =>
                {
                    b.Navigation("Partitions");
                });
#pragma warning restore 612, 618
        }
    }
}
