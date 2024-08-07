using Kafka_for_web.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Kafka_for_web.DataAccess;

public class KafkaContext : DbContext
{
    public DbSet<Cluster> Clusters { get; set; }
    public DbSet<Topic> Topics { get; set; }
    public DbSet<Partition> Partitions { get; set; }

    public DbSet<Producer> Producers { get; set; }
    public DbSet<Consumer> Consumers { get; set; }

    public DbSet<ConsumerGroup> ConsumerGroups { get; set; }

    public DbSet<Subscription> Subscriptions { get; set; }

    public DbSet<Message> Messages { get; set; }

    public DbSet<ConsumerOffsets> Offsets { get; set; }

    public KafkaContext(DbContextOptions<KafkaContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=webka;");
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cluster>().ToTable("cluster").HasIndex(cluster => cluster.Name).IsUnique();
        modelBuilder.Entity<Topic>().ToTable("topic").HasIndex(topic => topic.Name).IsUnique();
        modelBuilder.Entity<Partition>().ToTable("partition");
        modelBuilder.Entity<Consumer>().ToTable("consumer").HasIndex(consumer => consumer.Name).IsUnique();
        modelBuilder.Entity<Producer>().ToTable("producer").HasIndex(producer => producer.Name).IsUnique();
        modelBuilder.Entity<ConsumerGroup>().ToTable("consumerGroup").HasIndex(group => group.Name).IsUnique();

        modelBuilder.Entity<Message>().ToTable("message");

        modelBuilder.Entity<Subscription>().ToTable("subscription");

        modelBuilder.Entity<ConsumerOffsets>().ToTable("offset");
    }

}
