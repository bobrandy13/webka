namespace Kafka_for_web.Models;

public class Cluster
{
   public long Id { get; set; }
   public string Name { get; set; } = null!;
   
   // The number of brokers
   // private int num_brokers;
   // private List<Broker> brokers;

   // How many servers the cluster should be represented to; 
   // Default number is 2
   public int ReplicationCount { get; set; }  
      
   // each cluster should have a relation to any number of topics; 
   // private List<Topic> _topics;
   public ICollection<Topic> Topics { get; set; } = null!;
}
