using System;

namespace GameOfGoose
{
    public class GooseSquare : ISquare
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public GooseSquare(int id, string name)
        {
            ID = id;
            Name = name;
            Description = "";
            Image = "./Images/goose.png";
        }

        public void AssignPawnImage()
        {
            throw new NotImplementedException();
        }

        //public int CheckGooseSquare(int dice, int location)
        //{
        //    switch (location)
        //    {
        //        case 5:
        //        case 9:
        //        case 14:
        //        case 18:
        //        case 23:
        //        case 27:
        //        case 32:
        //        case 36:
        //        case 41:
        //        case 45:
        //        case 50:
        //        case 54:
        //        case 59:
        //            return dice;

        //        default:
        //            return 0;
        //    }
        //}
    }
}