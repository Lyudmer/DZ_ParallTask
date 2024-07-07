using System.IO;
using System.Linq;
using System;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;

namespace DZ_ParallTask
{
    public class ConsoleInput : IInputVal
    {
        public string GetInputVal()
        {
            string[] files;
            string PathDir;
           
            while (true)
            {
                Console.Write("Укажите папку для проверки файлов:");
              
                PathDir = Console.ReadLine();
                if (Directory.Exists(PathDir))
                {
                    files = Directory.GetFiles(PathDir);
                    if (files != null && files.Count() > 0) break;
                   
                }
                
                Console.WriteLine($"Папка {PathDir} не найдена или пустая.Укажите другую папку");
            }
            
            return PathDir;
        }

    }
}