using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Kafka_for_web.Models;

public class Topic
{
   public long Id { get; set; }

   [StringLength(100)]
   [Index(IsUnique = true)]
   public string Name { get; set; } = null!;

   // the number of partitions each topic should have 
   public long NumPartitions { get; set; }
   public long ReplicationFactor { get; set; }

   public long ClusterId { get; set; }

   [JsonIgnore]
   public Cluster? Cluster { get; set; } = null!;

   [JsonIgnore]
   public ICollection<Partition>? Partitions { get; set; } = null!;

   public TimeSpan RetentionPeriod;
}
