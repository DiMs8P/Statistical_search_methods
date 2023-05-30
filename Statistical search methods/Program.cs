using Statistical_search_methods.Core.Assembly;
using Statistical_search_methods.Logging;
using Statistical_search_methods.Settings;
using Environment = Statistical_search_methods.Core.Assembly.Environment;

namespace Statistical_search_methods
{
    class Program
    {
        static void Main(string[] args)
        {
            PrebuildTests tests = new PrebuildTests();
            
            Logger logger = new Logger(Console.WriteLine);
            
            for (int i = 0; i < Config.M.Length; i++)
            {
                logger.BufferItem(Config.M[i].ToString(), 0);
                Environment environment = tests.SecondAlgorithmTest(logger, Config.M[i]);
                
                environment.FindMinimumPoint(Config.Seed);
                
                logger.BufferItem(Config.FuncCount.ToString(), 1);
                Config.FuncCount = 0;
            }
            
            logger.LogBuffer();
            
            // PrebuildTests tests = new PrebuildTests();
            //
            // Logger logger = new Logger(Console.WriteLine);
            //
            // Environment environment = tests.SimpleRandomSearchTest(logger);
            // environment.FindMinimumPoint(Config.Seed);
            //
            // logger.LogBuffer();
        }
    }
}