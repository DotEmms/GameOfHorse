using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameOfHorse
{
    public class FileManager
    {
        public void CreateFile(string file)
        {
            if (!File.Exists(file))
            {
                FileStream fileStream = File.Create(file);
                fileStream.Close();
            }
        }
    }
}
