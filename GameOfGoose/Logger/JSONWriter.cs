using System;
using System.IO;
using System.Text.Json;

namespace GameOfHorse
{
    class JSONWriter
    {
       
        private string _filePath = "../../../Logger/leaderboard.json";

        public void WriteTo(string winner)
        {
            DateTime dateTime = DateTime.Now;
            string temp = $"{dateTime.ToString()},{winner}";
            string maybe = JsonSerializer.Serialize(temp); ;
            File.AppendAllText(_filePath, maybe);
        }
    }
}
