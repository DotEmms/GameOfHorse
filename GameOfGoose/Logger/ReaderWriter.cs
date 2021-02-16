using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameOfHorse
{
    public class ReaderWriter
    {
        private string pATH_LEADERBOARD = "../../../Logger/leaderboard.txt";

        public string PATH_LEADERBOARD
        {
            get { return pATH_LEADERBOARD; }
        }

        private string dateTime = Convert.ToString(System.DateTime.Now);


        public void WriteDataToFile(string textToWriteToFile)
        {
            WriteDataToFile(textToWriteToFile, PATH_LEADERBOARD);
        }
        public void WriteDataToFile(string textToWriteToFile, string path)
        {
            using (StreamWriter writer = new StreamWriter(path, true))
            {                
                writer.WriteLine($"{dateTime}, {textToWriteToFile}");
            }
        }

        public void WriteDataToFile(string[] lines)
        {
            WriteDataToFile(lines, PATH_LEADERBOARD);
        }
        public void WriteDataToFile(string[] lines, string path)
        {
            using (StreamWriter writer = new StreamWriter(path, true)) //true om nieuwe tekst toe te voegen ipv overschrijven.
            {
                writer.WriteLine();
                writer.Write($"{dateTime} |");
                foreach (string line in lines)
                {
                    writer.Write($" {line}");
                }
            }                       
        }

        public List<string> ReadDataFromFile(string path)
        {
            List<string> lines = new List<string>();
            string line = string.Empty;
            using (StreamReader reader = new StreamReader(path))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            return lines;
        }
    }
}
