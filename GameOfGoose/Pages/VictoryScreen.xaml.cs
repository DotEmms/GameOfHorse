﻿using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
            PlaySound();
            this.game = game;
            lblWinner.Content = game.winningPlayer.Name;
            lblAmountOfRounds.Content = game.totalRounds;
            imgWinner.Source = new BitmapImage(new Uri(game.winningPlayer.DisplayedImagePath, UriKind.RelativeOrAbsolute));          
        }
        private void PlaySound()
        {
            var uri = new Uri(@"https://www.myinstants.com/media/sounds/victoryff.swf.mp3", UriKind.RelativeOrAbsolute);
            var player = new MediaPlayer();

            player.Open(uri);
            player.Play();
        }
    }
}