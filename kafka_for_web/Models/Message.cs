using System.ComponentModel.DataAnnotations;

namespace Kafka_for_web.Models;

public class Message
{
    [Key]
    public long Id { get; set; }
    public long Key { get; set; }
    public string Value { get; set; }
}