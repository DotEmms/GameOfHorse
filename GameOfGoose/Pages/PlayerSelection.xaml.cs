using System;
using System.Windows;
using System.Windows.Controls;

namespace GameOfGoose
{
    /// <summary>
    /// Interaction logic for PlayerSelection.xaml
    /// </summary>
    public partial class PlayerSelection : Page
    {
        private Game game;
        //private string playerOne;
        //private string playerTwo;
        //private string playerThree;
        //private string playerFour;

        public PlayerSelection(Game game)
        {
            InitializeComponent();
            this.game = game;

        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            //create players
            SetPlayers();
            if (game.players == null)
            {
                MessageBox.Show("You need at least 2 players to play!", "No players found", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (game.players.Count < 2)
            {
                MessageBox.Show("You need at least 2 players to play!", "Not enough players", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                game.StartGame();

                //event to enable button
                OnStartGameButtonClicked(e);
                this.NavigationService.Navigate(new GameBoard(game));
            }
        }

        private void SetPlayers()
        {
            if (game.GetPlayer(1) == null && (txtPlayerOne.Text != ""))
            {
                var newPlayer1 = game.CreatePlayer(txtPlayerOne.Text);
                newPlayer1.DisplayedImagePath = "../Images/bluePawn.png";
                game.AddPlayerToGame(newPlayer1);
            }
            if (game.GetPlayer(2) == null && (txtPlayerTwo.Text != ""))
            {
                var newPlayer2 = game.CreatePlayer(txtPlayerTwo.Text);
                newPlayer2.DisplayedImagePath = "../Images/redPawn.png";
                game.AddPlayerToGame(newPlayer2);
            }
            if (game.GetPlayer(3) == null && (txtPlayerThree.Text != ""))
            {
                var newPlayer3 = game.CreatePlayer(txtPlayerThree.Text);
                newPlayer3.DisplayedImagePath = "../Images/greenPawn.png";
                game.AddPlayerToGame(newPlayer3);
            }
            if (game.GetPlayer(4) == null && (txtPlayerFour.Text != ""))
            {
                var newPlayer4 = game.CreatePlayer(txtPlayerFour.Text);
                newPlayer4.DisplayedImagePath = "../Images/yellowPawn.png";
                game.AddPlayerToGame(newPlayer4);
            }
        }

        //event for RollDice button in main
        public event EventHandler StartGameButtonClicked;

        protected virtual void OnStartGameButtonClicked(EventArgs e)
        {
            StartGameButtonClicked?.Invoke(this, e);
        }

        private void txtPlayerOne_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtPlayerTwo.IsEnabled = true;
        }

        private void txtPlayerTwo_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtPlayerThree.IsEnabled = true;
            btnStartGame.IsEnabled = true;
            btnStartGame.Visibility = Visibility.Visible;
        }

        private void txtPlayerThree_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtPlayerFour.IsEnabled = true;
        }
    }
}