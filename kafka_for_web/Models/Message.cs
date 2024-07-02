using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Kafka_for_web.Models;

public class Message
{
    [Key]
    public long Id { get; set; }
    public long? Key { get; set; }
    
    // Should encode the message into a string. Object.ToString()
    [StringLength(300)]
    public string Value { get; set; } = null!;
    
    public long PartitionId { get; set; }
    
    [JsonIgnore]
    public Partition? Partition { get; set; } = null!;
    
    public long ProducerId { get; set; }
    
    [JsonIgnore]
    public Producer? Producer { get; set; } = null!;
}