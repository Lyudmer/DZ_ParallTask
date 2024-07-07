using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using System.Threading.Tasks;
using static System.Net.WebRequestMethods;


namespace DZ_ParallTask
{
    public class Program
    {
        private static void Main()
        {
          
            var inputval = new ConsoleInput();
            string PathDir = inputval.GetInputVal();
            string[]files = Directory.GetFiles(PathDir);

            Console.WriteLine($"Тест 1 : проверка 3 файлов из папки {PathDir}");
            var test_p1 = GetHendler(files,3);
            test_p1.ProcessQueue();
            
            Console.WriteLine($"Тест 2 : проверка всех файлов из папки {PathDir}");
            var test_p2 = GetHendler(files, files.Count());
            test_p2.ProcessQueue();

            Console.ReadKey();
        }

        private static IHandler GetHendler(string[] files, int cFiles)
        {
            return new TasksHandler(files,cFiles);
        }
        
    }
    
}
