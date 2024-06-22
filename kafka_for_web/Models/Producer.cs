namespace Kafka_for_web.Models; 

public class Producer {
    public long Id { get; set; }
    public string Name { get; set; } = null!;

    public Message MostRecentMessage { get; set; } = null!;

    // When a user produces a new message 
    public ICollection<Message> Messages { get; set; } = null!;
}