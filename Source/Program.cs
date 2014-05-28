using System;

namespace AutoHeader
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var commandlineOptions = new CommandlineOptions(args);
                foreach (var commandlineOption in commandlineOptions)
                {
                    commandlineOption.Execute();
                }
            }
            catch (UnknownOptionException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (ExecutionException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
