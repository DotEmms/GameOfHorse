using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfGoose.Squares
{
    public class SpecialSquare
    {
        public void MoveToSpecificSquare() //=> Bridge, Maze, Death
        { 

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
    }
}
