using GameOfGoose;
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
using System.Windows.Shapes;

namespace GameOfHorse
{
    /// <summary>
    /// Interaction logic for LeaderBoard.xaml
    /// </summary>
    public partial class LeaderBoard : Window
    {
        private Game game;
        public LeaderBoard(Game game)
        {
            InitializeComponent();
            this.game = game;
            listViewLeaderBoard.ItemsSource = game.leaderBoard;
            this.DataContext = game;
        }
    }
}
