namespace Kafka_for_web.Models;

public class Broker
{
    private long id;
    private long clusterId;
    private string host;
    private int port;
    private string rack; 
    
    public Broker(long id, string host, int port, long clusterId, string rack)
    {
        this.id = id;
        this.host = host;
        this.port = port;
        this.clusterId = clusterId;
        this.rack = rack;
    }
}