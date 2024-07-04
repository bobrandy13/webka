using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Kafka_for_web.Models;

public class Cluster
{
   public long Id { get; set; }

   [StringLength((100))]
   [Index(IsUnique = true)]
   public string Name { get; set; } = null!;

   // The number of brokers
   // private int num_brokers;
   // private List<Broker> brokers;

   // How many servers the cluster should be represented to; 
   // Default number is 2, and can be set otherwise
   public int ReplicationCount { get; set; }

   // each cluster should have a relation to any number of topics; 
   // private List<Topic> _topics;
   [JsonIgnore]
   public ICollection<Topic>? Topics { get; set; } = [];
}
