using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
    [Key]
    public long Id { get; set; }
    
    // Set the max to 5, default to 3; 
    // Dictates how many different partitions will store the same data 
    public long ReplicationFactor { get; set; }

    // Stores the current size in bytes of the partition;
    public long Size;

    // Log file name
    [StringLength(100)]
    public string LogDir { get; set; } = null!;

    // The status of the partition
    public PartitionStatus Status { get; init; }
    
    // Stores the most recent messages in an array
    public ICollection<Message> Messages { get; init; } = null!;
}