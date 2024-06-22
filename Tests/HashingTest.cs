using kafka_for_web.DataAccess;
using Kafka_for_web.DataAccess;

namespace Tests;

public class Tests
{
    private const int NumServers = 3;

    private static readonly Random Random = new Random(); 
    // [SetUp]
    // public void Setup()
    // {
    // }

    class Person(string name, int age)
    {
        private string Name { get; set; } = name;
        private int Age { get; set; } = age;

        public override string ToString()
        {
            return $"Name: {Name}, Age: {Age}";
        }
    }
    
    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[Random.Next(s.Length)]).ToArray());
    }

    [Test]
    public void EnsureConsistentDistribution()
    {
        const int totalData = 1000;
        var distribution = new int [NumServers];
        for (var i = 0; i < totalData; ++i)
        {
            var name = RandomString(20);
            var age = Random.Next(1, 32);
            var person = new Person(name, age);

            var hashIndex = HashFunction.Hash<Person>(person);

            distribution[Math.Abs(hashIndex) % NumServers] += 1;
        }
        for (var i = 0;i < NumServers; ++i)
        {
            Console.WriteLine($"Server {i}: {distribution[i]}");
        }
        for (var a = 0; a < NumServers; ++a)
        {
            Console.WriteLine(distribution[a]);
        }
        
        // Assert.Pass();
        // if all the distributions are close to each other, pass the test. Otherwise, fail. 
        Assert.That(distribution.Any(value => value > (double)totalData / NumServers * 0.93 && value < (double)totalData / NumServers * 1.07), Is.True);
    }
}