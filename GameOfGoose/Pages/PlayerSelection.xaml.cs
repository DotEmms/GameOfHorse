using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            if(game.players == null)
            {
                MessageBox.Show("You need at least 2 players to play!", "No players found", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if(game.players.Count < 2)
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
            if(game.GetPlayer(1) == null && (txtPlayerOne.Text != ""))
            {
                var newPlayer1 = game.CreatePlayer(txtPlayerOne.Text);
                game.AddPlayerToGame(newPlayer1);
            }
            //else
            //{
            //    game.GetPlayer(1).Name = txtPlayerOne.Text;
            //}
            
            if(game.GetPlayer(2) == null && (txtPlayerTwo.Text != ""))
            {
                var newPlayer2 = game.CreatePlayer(txtPlayerTwo.Text);
                game.AddPlayerToGame(newPlayer2);
            }
            //else
            //{
            //    game.GetPlayer(2).Name = txtPlayerTwo.Text;
            //}
            
            if (game.GetPlayer(3) == null && (txtPlayerThree.Text != ""))
            {
                var newPlayer3 = game.CreatePlayer(txtPlayerThree.Text);
                game.AddPlayerToGame(newPlayer3);
            }
            //else
            //{
            //    game.GetPlayer(3).Name = txtPlayerThree.Text;
            //}
            
            if (game.GetPlayer(4) == null && (txtPlayerFour.Text != ""))
            {
                var newPlayer4 = game.CreatePlayer(txtPlayerFour.Text);
                game.AddPlayerToGame(newPlayer4);
            }
            //else
            //{
            //    game.GetPlayer(4).Name = txtPlayerFour.Text;
            //}       
        }



        //event for RollDice button in main
        public event EventHandler StartGameButtonClicked;
        protected virtual void OnStartGameButtonClicked(EventArgs e)
        {
            StartGameButtonClicked?.Invoke(this, e);
        }        
    }
}
