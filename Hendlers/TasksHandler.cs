using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Runtime.InteropServices;


namespace DZ_ParallTask
{
        public class TasksHandler : IHandler
        {
        private readonly List<TaskItem> _queueTasks = new List<TaskItem>();

        public TasksHandler(string[] files, int cFiles)
        {
            Files = files;
            CFiles = cFiles;
        }

        public string[] Files { get; }
        public int CFiles { get; }

        public void ProcessQueue()
        {
            var stopWatch = new Stopwatch();

            Console.WriteLine("Старт теста...");
            stopWatch.Start();

            StartAsTasks();

            stopWatch.Stop();


            Console.WriteLine($"Тест выполнялся : {stopWatch.Elapsed}...");
        }

        private void StartAsTasks()
        {
            string[] files;
            if (CFiles!= Files.Count())
            {
                files = new string[CFiles];
                Array.Copy(Files, 0, files, 0, CFiles);
            }
            else files = Files;
            foreach (var item in files)
            {
                var taskitem = new TaskItem(item);
                _queueTasks.Add(taskitem);
            }
            _queueTasks.ForEach(RunTask);
            
            WaitAllTasks();
            
        }

        private void WaitAllTasks()
        {
            var tasksForWait = GetTasksForWait();
            Task.WaitAll(tasksForWait);

            foreach (var result in _queueTasks)
            { 
                if (result.CountSpace == -1)
                    Console.WriteLine($"Файл: {result.FileName} найден");
                else
                    Console.WriteLine($"В файле: {result.FileName} найдено пробелов : {result.CountSpace}");
            }
        }

        private Task[] GetTasksForWait()
        {
            return _queueTasks
               .Select(t => t.Task)
               .ToArray();
        }

        private  void RunTask(TaskItem item)
        {
            item.Task = Task.Run(async() => item.CountSpace = await ReadFileAndCountSpacesAsync(item.FileName));
        }

        static async Task<int> ReadFileAndCountSpacesAsync(string filePath)
        {
            int spacesCount = 0;
            if (!File.Exists(filePath)) return -1;
            var stream = new StreamReader(filePath);
            while (!stream.EndOfStream)
            {
                var line = await stream.ReadLineAsync();
                if (line != null)
                    spacesCount += line.Count(c => c == ' ');
                
            }
            return spacesCount;
        }


    }

  
}