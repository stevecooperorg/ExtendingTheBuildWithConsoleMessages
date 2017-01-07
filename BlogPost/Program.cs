using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteBuildError("Error", @"c:\src\BlogPost\BlogPost\Program.cs", 12, "It's bad, jim!");
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
