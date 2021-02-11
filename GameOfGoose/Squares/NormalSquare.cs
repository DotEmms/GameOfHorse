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

        public NormalSquare(int id, string name)
        {
            ID = id;
            Name = name;
            Description = "Safe zone, well done!";
            Image = "./Images/normal.jpg";
        }
        public void AssignPawnImage()
        {
            throw new NotImplementedException();
        }
    }
}
