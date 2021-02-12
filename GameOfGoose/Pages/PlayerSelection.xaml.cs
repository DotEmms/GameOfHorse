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

            
            game.StartGame();

            //event to enable button
            OnStartGameButtonClicked(e);
            this.NavigationService.Navigate(new GameBoard(game));            
        }

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
        }
        private void txtPlayerThree_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtPlayerFour.IsEnabled = true;
        }
    }
}
