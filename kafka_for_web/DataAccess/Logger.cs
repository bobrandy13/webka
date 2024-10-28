using Kafka_for_web.Models;

namespace Kafka_for_web.DataAccess;

public static class Logger
{
    public static bool Write(string logPath, Message message)
    {
        try
        {
            var baseDirectory = AppContext.BaseDirectory;

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

    // This function is only reading from the first partition (partition 0)
    public static string? Read(string topicName, string clusterName, int offset)
    {
        try
        {
            // if the offset is something out of bounds, return null;
            if (offset == -1) return null;

            var logPath = $"logs/{clusterName}/{topicName}/log.txt";
            var baseDirectory = AppContext.BaseDirectory;

            // ykw, if it works it works.
            // NOTE: may break in production
            var projectDirectory = Directory.GetParent(baseDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
            if (projectDirectory == null) return "Directory was not found. I am not in the correct location";

            var logDirectory = Path.Combine(projectDirectory, "Kafka_for_web", logPath);
            var line = File.ReadLines(logDirectory);

            var enumerable = line as string[] ?? line.ToArray();
            return offset >= enumerable.Length ? null : enumerable.Skip(offset).Take(1).First();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return "Not Found. Thing was not found.";
        }
    }

    /// <summary>
    /// Since topicID is unique, we can find the cluster name based on the topic id
    /// </summary>
    public static string GetLogPath(long topicId)
    {
        return "";
    }


    /// <summary>
    /// This method returns the number of lines of the specified log file 
    /// <example>
    /// For example:
    /// <code>
    /// long length = Logger.GetSize("/Eccormce/Buyers");
    /// </code>
    /// results in <c>length</c>'s having the value of the number of events inside the log file.
    /// </example>
    /// </summary>
    /// TODO: 
    public static long GetSize(string logPath)
    {
        var length = File.ReadLines(logPath).Count();
        return length;
    }

    /// <summary>
    /// This method scans through the log file and then deletes any old logs. 
    /// <example>
    /// For example:
    /// <code>
    /// clean(logPath)
    /// p.Translate(-1,3);
    /// </code>
    /// results in <c>p</c>'s having the value (2,8).
    /// </example>
    /// </summary>
    // TODO: This function should get rid of any logs that are out of date. IE go through every single log and clear from the end. 
    // If the log is out of date, then delete the log. 
    public static void Clean(string logPath)
    {


        return;
    }
}