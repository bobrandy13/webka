using Kafka_for_web.Models;
using Microsoft.EntityFrameworkCore;

namespace Kafka_for_web.DataAccess;

public class KafkaContext : DbContext
{
    public KafkaContext(DbContextOptions<KafkaContext> options) : base (options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=webka;");
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cluster>().ToTable("cluster");
        modelBuilder.Entity<Topic>().ToTable("topic");
        modelBuilder.Entity<Partition>().ToTable("partition");
        modelBuilder.Entity<Consumer>().ToTable("consumer");
        modelBuilder.Entity<Producer>().ToTable("producer");
        modelBuilder.Entity<ConsumerGroup>().ToTable("consumerGroup");
        
        modelBuilder.Entity<Subscription>().ToTable("subscription");
    }

    public DbSet<Cluster> Clusters { get; set; } 
    public DbSet<Topic> Topics { get; set; } 
    public DbSet<Partition> Partitions { get; set; } 
    
    public DbSet<Producer> Producers { get; set; }
    public DbSet<Consumer> Consumers { get; set; }
    
    public DbSet<ConsumerGroup> ConsumerGroups { get; set; }
    
    public DbSet<Subscription> Subscriptions { get; set; }
}