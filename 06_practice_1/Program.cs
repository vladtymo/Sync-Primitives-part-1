using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace _06_practice_1
{
    class Stat
    {
        public int Letters { get; set; }
        public int Digits { get; set; }
        //...
    }
    class Program
    {
        static void Main(string[] args)
        {
            Stat statistic = new Stat();

            string[] files = Directory.GetFiles("C:/.../folder");

            foreach (var file in files)
            {
                //FileStream fs = new FileStream(file, FileMode.Open);
                //fs.Read

                //StreamReader sr = new StreamReader(file);
                //sr.ReadToEnd();

                string text = File.ReadAllText(file);

                Thread thread = new Thread(TextAnalyse);
                thread.Start(statistic);
                //Task.Run(() => TextAnalyse(text, statistic));
            }        

            // show total statistic
        }

        static void TextAnalyse(object stat)
        {
            // text analyse how many letters, digits etc.

        }
    }
}
