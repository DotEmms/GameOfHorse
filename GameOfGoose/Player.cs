using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Media;

namespace GameOfGoose
{
    public class Player : INotifyPropertyChanged
    {
        private static int counter = 1;
        private int _id;

        private int _col;

        public int Column
        {
            get 
            { 
                return _col; 
            }
            set 
            {
                _col = value;
                OnPropertyChanged();
            }
        }

        private int _row;

        public int Row
        {
            get 
            { 
                return _row;
            }
            set 
            { 
                _row = value;
                OnPropertyChanged();
            }
        }

        public Brush Fill { get; set; }


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

        private int _turnPenalty;

        public int TurnPenalty
        {
            get { return _turnPenalty; }
            set { _turnPenalty = value; }
        }

        private bool _isFirstRound;

       

        public bool IsFirstRound
        {
            get { return _isFirstRound; }
            set { _isFirstRound = value; }
        }


        public Player(string name)
        {
            Id = counter;
            Name = name;
            PawnLocation = 0;
            TurnPenalty = 0;
            IsFirstRound = true;

            CounterControl();
        }

        private void CounterControl()
        {
            if(counter == 4)
            {
                counter = 1;
            }
            else
            {
                counter++;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
