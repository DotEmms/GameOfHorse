
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
            SetLabels();
            var grid = GenerateGrid(game.squares);
            MyPanel.Children.Add(grid);

        }

        private void SetLabels()
        {
            var content = game.squares.FirstOrDefault(x => x.ID == 1);

        }


        private Grid GenerateGrid(IList<ISquare> squares, int amountOfColumns = 8)
        {
            var dynamicGrid = new Grid();
            int amountOfRows = squares.Count / amountOfColumns;

            for (int i = 0; i < amountOfRows; i++)
            {
                var row = new RowDefinition();
                dynamicGrid.RowDefinitions.Add(row);
            }

            for (int i = 0; i < amountOfColumns; i++)
            {
                var column = new ColumnDefinition();
                dynamicGrid.ColumnDefinitions.Add(column);
            }

            dynamicGrid = SetGridParameters(dynamicGrid, squares, amountOfColumns);
            return dynamicGrid;
        }

        private Grid SetGridParameters(Grid grid, IList<ISquare> squares, int amountOfColumns)
        {
            for (int i = 0; i < grid.RowDefinitions.Count; i++)
            {
                ISquare[] squaresInRow = squares.Skip(i * amountOfColumns).Take(amountOfColumns).ToArray();

                for (int j = 0; j < grid.ColumnDefinitions.Count; j++)
                {
                    var myLabel = new Label { Content = squaresInRow[j].ID };                    
                    Grid.SetRow(myLabel, i);
                    Grid.SetColumn(myLabel, j);
                    grid.Children.Add(myLabel);
                }
            }
            
            return grid;
        }
    }
}
