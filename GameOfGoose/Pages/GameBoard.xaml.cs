
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
