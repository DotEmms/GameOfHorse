using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfGoose
{
    public class SpecialSquare : ISquare
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public SpecialSquare(int id, string name, string description, string image)
        {
            ID = id;
            Name = name;
            Description = description;
            Image = image;
        }
        public int MoveToSpecificSquare(int locatie) //=> Bridge, Maze, Death
        {
            switch (locatie)
            {
                case 6:
                    return 12;
                case 42:
                    return 39;
                case 58:
                    return 0;
                default:
                    return -1;
            }
        } 
        public int SkipTurns(int square) //=> Inn, Prison
        {
            if(square == 19)
            {
                return 1;
            }
            else if(square == 52)
            {
                return 3;
            }
            return -1;
        }

        public int WaitForOtherPlayer() //=> Well -> if an other player reaches square 31 => first player can move, second player must wait
        {
            return -1;
        }

        public void GameOver() //=> player reaches square 63
        {
            //game.GameOver();
        }
    }
}
