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
        Logger.Read("", 0);
        for (var i = 0; i < 12; ++i)
        {
            Logger.Read("", i);
        }
    }
}