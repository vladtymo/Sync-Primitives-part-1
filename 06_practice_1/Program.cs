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

    // Ways to read text from file
    /* 1 - using FileStream
     * FileStream fs = new FileStream(file, FileMode.Open);
     * fs.Read
      
       2 - using StreamReader
     * StreamReader sr = new StreamReader(file);
     * sr.ReadToEnd();
      
       3 - using File.ReadAllText
     * string text = File.ReadAllText(file);
     */
    class Program
    {
        static void Main(string[] args)
        {
            Stat statistic = new Stat();

            string[] files = Directory.GetFiles("C:/.../folder");

            foreach (var file in files)
            {
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
