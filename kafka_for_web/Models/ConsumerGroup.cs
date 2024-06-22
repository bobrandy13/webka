using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kafka_for_web.Models;

public class ConsumerGroup
{
   public long Id { get; set; } 
   
   [StringLength(100)]
   public string Name { get; set; } = null!;
   
   public long ClusterId { get; set; }

   [ForeignKey("ClusterId")] 
   public Cluster Cluster { get; set; } = null!;

   // contain a list of all consumers
   public ICollection<Consumer> Consumers { get; set; } = null!;
}