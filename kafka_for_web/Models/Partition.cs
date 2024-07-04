using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Kafka_for_web.Models;

public enum PartitionStatus
{
   Online,
   Offline,
   Error,
   Recovery,
   Unknown
}

public class Partition
{
    // One consumer per  partition
    // This id should just be from 1, 2, 3, 4, 5 ...
    public long Id { get; set; }
    
    // The status of the partition
    public PartitionStatus Status { get; init; }
    
    // Stores the most recent messages in an array
    [JsonIgnore]
    public ICollection<Message>? Messages { get; init; } = null!;
    
    public long TopicId { get; set; }
    
    [JsonIgnore]
    public Topic? Topic { get; set; } = null!;
}