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
        public PlayerSelection(Game game)
        {
            InitializeComponent();
            this.game = game;
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new GameBoard(game));
            
        }
    }
}
