namespace Kafka_for_web.DataAccess;

public static class Logger
{
    public static bool Write(string logPath, string message)
    {
        try
        {
            var baseDirectory = AppContext.BaseDirectory;
            
            // ykw, if it works it works.
            var projectDirectory = Directory.GetParent(baseDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
            if (projectDirectory == null) return false;
            var logDirectory = Path.Combine(projectDirectory, "Kafka_for_web", "logs", "users", "log1.txt");
            
            using StreamWriter writer = new(logDirectory, append: true);
            
            writer.WriteLine(message);

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    public static bool Read(string logPath, int offset)
    {
        try
        {
            using StreamReader reader = new(logPath);
            var contents = reader.ReadLine(); 
            Console.WriteLine(contents);

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }
}