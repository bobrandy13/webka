using System.ComponentModel.DataAnnotations;

namespace Kafka_for_web.Models;

public class Message
{
    [Key]
    public long Id { get; set; }
    public long? Key { get; set; }
    
    // Should encode the message into a string. Object.ToString()
    [StringLength(300)]
    public string Value { get; set; } = null!;
}