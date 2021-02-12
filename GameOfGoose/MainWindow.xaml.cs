﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
         
        private Game game;

        public MainWindow()
        {
            InitializeComponent();
            game = new Game();
            GenerateNewPlayerSelectionScreen();
        }

        private void MenuItemRestartGame_Click(object sender, RoutedEventArgs e)
        {     
            game.ResetPlayers();
            GenerateNewPlayerSelectionScreen();
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void StartButtonClickedInPanel(object sender, EventArgs e)
        {
            btnRollDice.IsEnabled = true;            
        }
        
        private void GenerateNewPlayerSelectionScreen()
        {
            PlayerSelection playerWindow = new PlayerSelection(game);
            playerWindow.StartGameButtonClicked += StartButtonClickedInPanel;
            playerWindow.StartGameButtonClicked += SetPlayerNames;
            mainWindow.NavigationService.Navigate(playerWindow);
        }
        
        private void SetPlayerNames(object sender, EventArgs e)
        {
            int amountOfPlayers = game.players.Count;

            switch (amountOfPlayers)
            {
                case 2:
                    lblPlayer1.Content = game.GetPlayer(1).Name;
                    lblPlayer2.Content = game.GetPlayer(2).Name;
                    lblPlayer3.Visibility = Visibility.Hidden;
                    lblPlayer4.Visibility = Visibility.Hidden;

                    break;
                case 3:
                    lblPlayer1.Content = game.GetPlayer(1).Name;
                    lblPlayer2.Content = game.GetPlayer(2).Name;
                    lblPlayer3.Content = game.GetPlayer(3).Name;
                    lblPlayer4.Visibility = Visibility.Hidden;
                    break;
                case 4:
                    lblPlayer1.Content = game.GetPlayer(1).Name;
                    lblPlayer2.Content = game.GetPlayer(2).Name;
                    lblPlayer3.Content = game.GetPlayer(3).Name;
                    lblPlayer4.Content = game.GetPlayer(4).Name;
                    break;
                default:
                    break;
            }
        }
    }
}
