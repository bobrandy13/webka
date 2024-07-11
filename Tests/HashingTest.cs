using kafka_for_web.DataAccess;
using Kafka_for_web.DataAccess;
using Kafka_for_web.Models;
using static System.Int32;

namespace Tests;

public class Tests
{
    private const int NumServers = 3;
    private const int TotalData = 1000;

    private static readonly Random Random = new Random();

    private const double MarginOfError = 0.07;
    private const double LowerBound = 1 - MarginOfError;
    private const double UpperBound = 1 + MarginOfError;


    class Person(string name, int age)
    {
        private string Name { get; set; } = name;
        private int Age { get; set; } = age;

        public override string ToString()
        {
            return $"Name: {Name}, Age: {Age}";
        }
    }

    private static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[Random.Next(s.Length)]).ToArray());
    }

    [Test]
    public void EnsureConsistentDistributionWithNoKey()
    {
        var distribution = new int[NumServers];
        for (var i = 0; i < TotalData; ++i)
        {
            var name = RandomString(20);
            var age = Random.Next(1, 32);
            var person = new Person(name, age);

            var hashIndex = HashFunction.Hash(new Message { Value = person.ToString() }, NumServers);

            distribution[Math.Abs(hashIndex) % NumServers] += 1;
        }

        // Ensure that all values are within +- 7%;
        Assert.That(
            distribution.Any(value =>
                value > (double)TotalData / NumServers * LowerBound &&
                value < (double)TotalData / NumServers * UpperBound), Is.True);
    }

    [Test]
    public void EnsureDistributionWithKey()
    {
        var distribution = new int[NumServers];
        for (var i = 0; i < TotalData; ++i)
        {
            var name = RandomString(20);
            var age = Random.Next(1, 32);
            var person = new Person(name, age);

            var hashIndex = HashFunction.Hash(new Message { Key = 1, Value = person.ToString() }, NumServers);

            distribution[Math.Abs(hashIndex) % NumServers] += 1;
        }

        // Ensure that all values go to  the first server.
        Assert.That(distribution[0], Is.EqualTo(TotalData));
    }

    [Test]
    public void EnsureDistributionWithKeyAndNoKeyMixed()
    {
        var distribution = new int[NumServers];

        // half of data should have a key, while the other half should not have a key
        for (var i = 0; i < TotalData / 2; ++i)
        {
            var name = RandomString(20);
            var age = Random.Next(1, 32);
            var person = new Person(name, age);
            
            
            var hashIndex = HashFunction.Hash(new Message
                { Key = Random.Next(0, MaxValue), Value = person.ToString() }, NumServers);
            
            distribution[Math.Abs(hashIndex) % NumServers] += 1;
        }

        for (var j = 0; j < TotalData / 2; ++j)
        {
            var name = RandomString(20);
            var age = Random.Next(1, 32);
            var person = new Person(name, age);

            var hashIndex = HashFunction.Hash(new Message
                { Value = person.ToString() }, NumServers);
            
            distribution[Math.Abs(hashIndex) % NumServers] += 1;
        }
        
        foreach (var a in distribution) Console.WriteLine(a);

        Assert.That(
            distribution.Any(value =>
                value > (double)TotalData / NumServers * LowerBound &&
                value < (double)TotalData / NumServers * UpperBound), Is.True);
    }
}