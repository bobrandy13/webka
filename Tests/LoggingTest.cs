using Kafka_for_web.DataAccess;

namespace Tests;

public class LoggingTest
{   
    [Test]
    public void TestLogging()
    {
        const string logPath = "Log.txt";
        const string message = "1:51PM 2/7/24 User opened new file";
        Assert.That(Logger.Write(logPath, message), Is.True);
    }
}