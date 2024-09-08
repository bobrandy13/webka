using Kafka_for_web.DataAccess;
using Kafka_for_web.Models;

namespace Tests;

public class LoggingTest
{   
    [Test]
    public void TestLogging()
    {
        const string logPath = "Log.txt";
        const string message = "1:51PM 2/7/24 User opened new file";
        Assert.That(Logger.Write(logPath, new Message()), Is.True);
    }

    [Test]
    public void TestReading()
    {
        long avg = 0;
        Console.WriteLine(
            Logger.Read("Buyers", "EccomerceStore",10));
    }

    // Stress Writing
    [Test]
    public void TestMassLogging()
    {
        // Test the logging here.
        Assert.Pass();
    }
    
    // Stress reading
    [Test]
    public void TestMassReading()
    {
        // Test the reading here. 
        Assert.Pass();
    }
}