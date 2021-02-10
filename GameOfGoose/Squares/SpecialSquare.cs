using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfGoose
{
    public class SpecialSquare : ISquare
    {
        public int ID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void MoveToSpecificSquare(int locatie) //=> Bridge, Maze, Death
        {
            switch (locatie)
            {
                case 6:
                case 42:
                case 58:
                default:
                    break;
            }
        } 
        public void SkipTurns() //=> Inn, Prison
        {

        }
        public void WaitForOtherPlayer() //=> Well -> if an other player reaches square 31 => first player can move, second player must wait
        {

        }
        public void GameOverCheck() //=> player reaches square 63
        {
            //game.GameOver();
        }

        public void AssignPawnImage()
        {
            throw new NotImplementedException();
        }
    }
}
