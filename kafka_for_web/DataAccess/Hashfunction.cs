using System.Security.Cryptography;
using Kafka_for_web.Models;

namespace kafka_for_web.DataAccess;

// NOTE: We are going to use Round Robin based partitioning, and if a key is provided, then we should hash based on that key. 

public static class HashFunction
{
    private static readonly HashAlgorithm Sha = SHA256.Create();

    private static int _lastPartition = 0;

    /// <summary>
    /// Hash function that takes a message and a number of servers and returns the partition number.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="numServers"></param>
    /// <returns>An integer that represents the partition to send to. </returns>
    public static long Hash(Message message, long numServers = 1)
    {
        // ! ROUND ROBIN PARTITIONING
        if (message.Key == null)
        {
            _lastPartition++;

            if (_lastPartition >= numServers)
            {
                _lastPartition = 0;
            }

            return _lastPartition;
        }

        var data = Sha.ComputeHash(
            System.Text.Encoding.UTF8.GetBytes(message.Key.ToString() ?? throw new InvalidOperationException()));

        return BitConverter.ToInt64(data) % numServers;
    }
}
