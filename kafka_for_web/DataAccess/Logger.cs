using Kafka_for_web.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;

namespace Kafka_for_web.DataAccess;

public static class Logger
{
    public static bool Write(string logPath, Message message)
    {
        try
        {
            var baseDirectory = AppContext.BaseDirectory;

            // FIXME: bruh what is this
            var projectDirectory = Directory.GetParent(baseDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
            if (projectDirectory == null) return false;
            var logDirectory = Path.Combine(projectDirectory, "kafka_for_web", logPath);

            using StreamWriter writer = new(logDirectory, append: true);

            // Get the current time 
            var currentTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");
            var logContent = $"{message.ProducerId}-{currentTime}: {message.Value}";

            writer.WriteLine(logContent);

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    public static string Read(string logPath, int offset)
    {
        try
        {
            var baseDirectory = AppContext.BaseDirectory;

            // ykw, if it works it works.
            var projectDirectory = Directory.GetParent(baseDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
            if (projectDirectory == null) return "Directory was not found. I am not in the correct location";

            var logDirectory = Path.Combine(projectDirectory, "Kafka_for_web", logPath);
            var line = File.ReadLines(logDirectory).Skip(offset).Take(1).First();

            return line;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return "Not Found. Thing was not found.";
        }
    }
}
