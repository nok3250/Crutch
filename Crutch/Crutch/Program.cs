using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Crutch
{
    class Program
    {
        private static string _sourcePath;
        private static string _destinationPath;
        private static string _exePath;
        private static int _delay;

        static void Main(string[] args)
        {
            try
            {
                if (!Directory.Exists("logs"))
                {
                    Directory.CreateDirectory("logs");
                }

                if (args.Length == 4)
                {
                    _sourcePath = args[0];
                    _destinationPath = args[1];
                    _exePath = args[2];
                    if (int.TryParse(args[3], out _delay))
                    {
                        Thread.Sleep(_delay);
                        if (File.Exists(_sourcePath))
                        {
                            File.Copy(_sourcePath, _destinationPath, true);

                            RunAsAdmin(_exePath);
                        }
                        else
                        {
                            throw new FileNotFoundException("Source file not found: " + _sourcePath);
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Argument #4 must be numeric - it's a delay");
                    }
                }
                else
                {
                    throw new ArgumentException("Incorrect number of parameters");
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                LogError(ref msg);
                Console.ReadLine();
            }
        }

        private static void LogError(ref string message)
        {
            var dt = DateTime.Now;
            var sw = new StreamWriter("logs/" + dt.ToShortDateString() + ".txt", true) { AutoFlush = true };
            message = DateTime.Now + "  -  " + message;
            Console.WriteLine(message);
            sw.WriteLine(message);
        }

        private static void RunAsAdmin(string aFileName, string anArguments = null)
        {
            var processInfo = new ProcessStartInfo
            {
                FileName = aFileName,
                UseShellExecute = true,
                Verb = "runas"
            };

            if (anArguments != null)
                processInfo.Arguments = anArguments;

            var process = new Process
            {
                StartInfo = processInfo
            };
            process.Start();
           // process.WaitForExit();
        }
    }
}
