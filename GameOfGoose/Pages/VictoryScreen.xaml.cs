using System.Windows.Controls;

namespace GameOfGoose
{
    /// <summary>
    /// Interaction logic for VictoryScreen.xaml
    /// </summary>
    public partial class VictoryScreen : Page
    {
        private Game game;

        public VictoryScreen(Game game)
        {
            InitializeComponent();
            this.game = game;

            lblWinner.Content = game.winningPlayer.Name;
            lblAmountOfRounds.Content = game.totalRounds;
        }
    }
}