using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfGoose
{
    public class Player
    {
        private static int counter = 1;
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }        

        //int value for position on the board
        private int _pawnLocation;

        public int PawnLocation
        {
            get { return _pawnLocation; }
            set { _pawnLocation = value; }
        }

        private bool _isTurnPlayer;

        public bool IsTurnPlayer
        {
            get { return _isTurnPlayer; }
            set { _isTurnPlayer = value; }
        }

        public Player(string name)
        {
            Id = counter;
            Name = name;
            PawnLocation = 0;
            IsTurnPlayer = false;

            counter++;
        }
       
    }
}
