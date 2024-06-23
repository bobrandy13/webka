using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Kafka_for_web.Models; 

// Consumers must be part of a consumerGroup

public class Consumer {
    public long Id { get; set; }

    [StringLength((100))] 
    public string Name { get; set; } = null!;

    // // Consumers can push their offset to the saved in the same location as the output. 
    // public long offset;
    public ICollection<Subscription>? Subscriptions { get; set; } = new List<Subscription>();
    
    // All consumers must belong to a group 
    public long ConsumerGroupId { get; set; } 
    
    // Allows the consumer to autocommit their offset to the partition that they are reading from. 
    public bool EnableAutoCommit { get; set; }
}

// Many-to-many relationship
public class Subscription
{
    [Key]
    public long Id { get; set; }
    public long ConsumerId { get; set; }
    
    [ForeignKey("ConsumerId")]
    public Consumer Consumer { get; set; } = null!;
    
    public long TopicId { get; set; }
    
    [ForeignKey("TopicId")]
    public Topic Topic { get; set; } = null!;
}