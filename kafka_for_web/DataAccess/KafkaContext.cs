using Kafka_for_web.Models;
using Microsoft.EntityFrameworkCore;

namespace Kafka_for_web.DataAccess;

public class KafkaContext : DbContext
{
    public KafkaContext(DbContextOptions<KafkaContext> options) : base (options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cluster>().ToTable("Cluster");
        modelBuilder.Entity<Topic>().ToTable("Topic");
        modelBuilder.Entity<Partition>().ToTable("Partition");
        modelBuilder.Entity<Consumer>().ToTable("Consumer");
        modelBuilder.Entity<Producer>().ToTable("Producer");
        modelBuilder.Entity<ConsumerGroup>().ToTable("ConsumerGroup");
    }

    public DbSet<Cluster> Clusters { get; set; } 
    public DbSet<Topic> Topics { get; set; } 
    public DbSet<Partition> Partitions { get; set; } 
    
    public DbSet<Producer> Producers { get; set; }
    public DbSet<Consumer> Consumers { get; set; }
    
    public DbSet<ConsumerGroup> ConsumerGroups { get; set; }
}