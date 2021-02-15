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
