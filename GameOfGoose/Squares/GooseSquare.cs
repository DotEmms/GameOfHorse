using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfGoose
{
    class GooseSquare : ISquare
    {
        public int ID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void AssignPawnImage()
        {
            throw new NotImplementedException();
        }

        private void CheckGooseSquare()
        {
            switch (squareCounter) //5, 9, 14, 18, 23, 27, 32, 36, 41, 45, 50, 54, 59
            {
                case 5:
                case 9:
                case 14:
                case 18:
                case 23:
                case 27:
                case 32:
                case 36:
                case 41:
                case 45:
                case 50:
                case 54:
                case 59:
                default:
                    return 
            }
        }
        //if((check roll(4) + squareCounter(55)) == 63)
        //{
	       // GameOver();
        //} else if((check roll(4) + squareCounter(55)) > 63)
        //{
	       // set movement backwards => turn + into -
        //}
        //else
        //MovePawn(roll dice);
    }
}
