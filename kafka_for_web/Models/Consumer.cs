using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using Newtonsoft.Json;

namespace Kafka_for_web.Models;

// Consumers must be part of a consumerGroup

public enum MessageOffset
{
    earliest,
    latest,
    custom,
    none
}

public class Consumer
{
    public long Id { get; set; }

    [StringLength((100))]
    [Index(IsUnique = true)]
    public string Name { get; set; } = null!;

    // // Consumers can push their offset to the saved in the same location as the output. 
    // public long offset;
    [JsonIgnore]
    public ICollection<Subscription>? Subscriptions { get; set; } = [];

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

    [JsonIgnore]
    public Consumer Consumer { get; set; } = null!;

    public long TopicId { get; set; }

    [JsonIgnore]
    public Topic Topic { get; set; } = null!;
}

public class ConsumerOptionalParams
{
    public bool? __from_beginning { get; set; }
    // public bool? __formatter { get; set; }

    // public string? __consumer_property { get; set; } = null!;

    // public bool? __group { get; set; }

    // public bool? __max_messages { get; set; }

    // public bool? __partition;
}

public class ConsumerOffsets
{
    public long ConsumerId;

    [ForeignKey("ConsumerId")]
    [JsonIgnore]
    public Consumer consumer { get; set; } = null!;


    public long offset;

}