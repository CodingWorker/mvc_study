using System;
using System.Linq;
using System.IO;

namespace CopyToCreationTimeDir
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourceDir = System.Configuration.ConfigurationSettings.AppSettings["SourceDir"];
            string destDir = System.Configuration.ConfigurationSettings.AppSettings["DestDir"];
            string timeFormat = System.Configuration.ConfigurationSettings.AppSettings["TimeFormat"];
            string directoryPrefix = System.Configuration.ConfigurationSettings.AppSettings["DirectoryPrefix"];
            string dayGapStr = System.Configuration.ConfigurationSettings.AppSettings["DayGap"];
            
            if(string.IsNullOrEmpty(sourceDir) || string.IsNullOrEmpty(destDir))
            {
                Console.WriteLine("请配置合法的源文件目录和目标文件目录！！！");
                Console.WriteLine("按任意键退出 . . .");
                Console.ReadKey();
                return;
            }
            
            string[] filesWithPath = Directory.GetFiles(sourceDir);
            if(filesWithPath==null || filesWithPath.Count() == 0)
            {
                Console.WriteLine("源文件目录没有可被操作的文件?!");
                Console.WriteLine("按任意键退出 . . .");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("删除过时的文件目录");
            int dayGap=0;
            int.TryParse(dayGapStr, out dayGap);
            foreach(var dir in Directory.GetDirectories(destDir))
            {
                if (File.GetCreationTime(dir) >= DateTime.Now.AddDays(dayGap))
                {
                    Directory.Delete(dir, true);
                }
            }

            Console.WriteLine("开始拷贝");
            foreach (var file in filesWithPath)
            {
                if (File.GetCreationTime(file) < DateTime.Now.AddDays(dayGap))
                {
                    continue;
                }
                string creationTS = File.GetCreationTime(file).ToString(timeFormat);
                string directoryPath = Path.Combine(destDir, directoryPrefix+creationTS);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                
                File.Copy(file, directoryPath+"\\"+ Path.GetFileName(file), true);
                Console.WriteLine(Path.GetFileName(file)+"  success");
            }

            Console.WriteLine("操作成功");
            Console.WriteLine("按任意键退出 . . .");
            Console.ReadKey();
        }
    }
}
