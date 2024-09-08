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


    private class Person(string name, int age)
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
            var name = RandomString(100);
            var age = Random.Next(1, 100);
            var person = new Person(name, age);


            // Compute the hash index
            var hashIndex = HashFunction.Hash(new Message { Value = person.ToString() }, NumServers);
        
            // Use a more uniform approach to distribute the hash across servers
            var serverIndex = Math.Abs(hashIndex % NumServers);
            distribution[serverIndex] += 1;
        }

        foreach (var d in distribution) Console.WriteLine(d);

        // Ensure that all values are within +- 7%;
        foreach (var value in distribution)
        {
            var lowerBound = (double)TotalData / NumServers * LowerBound;
            var upperBound = (double)TotalData / NumServers * UpperBound;

            Assert.That(value, Is.GreaterThanOrEqualTo(lowerBound)
                    .And.LessThanOrEqualTo(upperBound),
                $"Distribution out of expected range: {value}");
        }
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

        foreach (var d in distribution) Console.WriteLine(d);
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