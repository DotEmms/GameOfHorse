using System.Windows.Controls;

namespace GameOfGoose
{
    public partial class GameBoard : Page
    {
        private Game game;

        public GameBoard(Game game)
        {
            InitializeComponent();
            this.game = game;

            Board.DataContext = game.players;
        }
    }
}