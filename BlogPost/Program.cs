using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BlogPost
{
    class DataFile
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var fileContent = File.ReadAllText(args[0]);
                var file = JsonConvert.DeserializeObject<DataFile>(fileContent);
                if (file.StartDate > file.EndDate)
                {
                    WriteBuildError("Error", args[0], 1, $"The start date is after the end date");
                    return;
                }
            }
            catch
            {
                WriteBuildError("Error", new Uri(Assembly.GetExecutingAssembly().Location).AbsolutePath, 1, $"This tool expects 1 command-line argument");
            }
        }

        private  static void WriteBuildError(string type, string filePath, int lineNumber, string message)
        {
            // what message does MSBuild recognise?
            var msBuildMessage = string.Format(@"{0}({1}) : {2}: {3}.", filePath, lineNumber, type, message);

            // write out the message with the appropriate colour
            ConsoleColor color = type == "Error" ? ConsoleColor.Red : ConsoleColor.Yellow;
            Console.ForegroundColor = color;
            Console.WriteLine(msBuildMessage);
            Console.ResetColor();
        }
    }
}
