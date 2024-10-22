﻿namespace GameOfGoose
{
    public class GooseSquare : ISquare
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public GooseSquare(int id, int row, int column, string name)
        {
            ID = id;
            Name = name;
            Description = "";
            Row = row;
            Column = column;
        }
    }
}