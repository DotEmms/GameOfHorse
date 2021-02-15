using System;

namespace GameOfGoose
{
    public class Dice
    {
        public int firstDie { get; set; }
        public int secondDie { get; set; }
        public int diceResult { get; set; }

        public int RollDice()
        {
            Random random1 = new Random();
            firstDie = random1.Next(1, 6);

            Random random2 = new Random();
            secondDie = random2.Next(1, 6);

            diceResult = firstDie + secondDie;
            return diceResult;
        }
    }
}