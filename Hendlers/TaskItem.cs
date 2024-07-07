using System.Threading.Tasks;

namespace DZ_ParallTask
{
    public class TaskItem
    {
        public string FileName { get; set; }
        public Task Task { get; set; }

        public int CountSpace { get; set; }

        public TaskItem(string fileName)
        {
           FileName = fileName;
        }


    }
}