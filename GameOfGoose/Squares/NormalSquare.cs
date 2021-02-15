using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfGoose
{
    public class NormalSquare : ISquare
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public NormalSquare(int id, int row, int column, string name)
        {
            ID = id;
            Name = name;
            Description = "Safe zone, well done!";
            Image = "./Images/normal.jpg";
            Row = row;
            Column = column;
        }
    }
}
