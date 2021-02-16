using System.ComponentModel;
using System.Windows.Media;

namespace GameOfGoose
{
    public class Player : INotifyPropertyChanged, IPlayer
    {
        private static int counter = 1;
        private int _id;
        private int _col;
        private int _row;
        private string _name;
        private int _pawnLocation;
        private int _turnPenalty;
        private bool _isFirstRound;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
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
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string DisplayedImagePath { get; set; }

        public int PawnLocation
        {
            get { return _pawnLocation; }
            set { _pawnLocation = value; }
        }

        public int TurnPenalty
        {
            get { return _turnPenalty; }
            set { _turnPenalty = value; }
        }

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
            Row = 7;
            Column = 0;
            DisplayedImagePath = "../Images/bluePawn.png";
            CounterControl();
        }

        private void CounterControl()
        {
            if (counter == 4)
            {
                counter = 1;
            }
            else
            {
                counter++;
            }
        }

        public void ResetId()
        {
            counter = 1;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}