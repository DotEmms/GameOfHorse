using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for GameBoard.xaml
    /// </summary>
    public partial class GameBoard : Page
    {
        private Game game;
        public GameBoard(Game game)
        {
            InitializeComponent();
            this.game = game;
            SetLabels();
        }

        private void SetLabels()
        {
            var content = game.squares.FirstOrDefault(x => x.ID == 1);


            one.Content = content.Name;
        }
    }
}
