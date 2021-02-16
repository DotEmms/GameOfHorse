using System;
using System.Windows;
using System.Windows.Media;

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
            game.ResetGame();            
            ResetHeaderState();
            GenerateNewPlayerSelectionScreen();
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItemRules_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(SetRules(), "Game Rules", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MenuItemAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(SetAbout(), "About the game", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void StartButtonClickedInPanel(object sender, EventArgs e)
        {
            btnRollDice.IsEnabled = true;
            btnRollDice.Visibility = Visibility.Visible;
        }

        private void GenerateNewPlayerSelectionScreen()
        {
            PlayerSelection playerWindow = new PlayerSelection(game);
            playerWindow.StartGameButtonClicked += StartButtonClickedInPanel;
            playerWindow.StartGameButtonClicked += SetPlayerNames;
            mainWindow.NavigationService.Navigate(playerWindow);
        }

        private void ResetHeaderState()
        {
            lblPlayer1.Visibility = Visibility.Hidden;
            lblPlayer2.Visibility = Visibility.Hidden;
            lblPlayer3.Visibility = Visibility.Hidden;
            lblPlayer4.Visibility = Visibility.Hidden;
            btnRollDice.IsEnabled = false;
            btnRollDice.Visibility = Visibility.Hidden;
        }

        private void SetPlayerNames(object sender, EventArgs e)
        {
            int amountOfPlayers = game.players.Count;

            switch (amountOfPlayers)
            {
                case 2:
                    lblPlayer1.Content = game.GetPlayer(1).Name;
                    lblPlayer2.Content = game.GetPlayer(2).Name;
                    lblPlayer1.Foreground = new SolidColorBrush(Colors.Blue);
                    lblPlayer2.Foreground = new SolidColorBrush(Colors.Red);
                    lblPlayer1.Visibility = Visibility.Visible;
                    lblPlayer2.Visibility = Visibility.Visible;

                    break;

                case 3:
                    lblPlayer1.Content = game.GetPlayer(1).Name;
                    lblPlayer2.Content = game.GetPlayer(2).Name;
                    lblPlayer3.Content = game.GetPlayer(3).Name;
                    lblPlayer1.Foreground = new SolidColorBrush(Colors.Blue);
                    lblPlayer2.Foreground = new SolidColorBrush(Colors.Red);
                    lblPlayer3.Foreground = new SolidColorBrush(Colors.Green);
                    lblPlayer1.Visibility = Visibility.Visible;
                    lblPlayer2.Visibility = Visibility.Visible;
                    lblPlayer3.Visibility = Visibility.Visible;
                    break;

                case 4:
                    lblPlayer1.Content = game.GetPlayer(1).Name;
                    lblPlayer2.Content = game.GetPlayer(2).Name;
                    lblPlayer3.Content = game.GetPlayer(3).Name;
                    lblPlayer4.Content = game.GetPlayer(4).Name;
                    lblPlayer1.Foreground = new SolidColorBrush(Colors.Blue);
                    lblPlayer2.Foreground = new SolidColorBrush(Colors.Red);
                    lblPlayer3.Foreground = new SolidColorBrush(Colors.Green);
                    lblPlayer4.Foreground = new SolidColorBrush(Colors.Gold);
                    lblPlayer1.Visibility = Visibility.Visible;
                    lblPlayer2.Visibility = Visibility.Visible;
                    lblPlayer3.Visibility = Visibility.Visible;
                    lblPlayer4.Visibility = Visibility.Visible;
                    break;

                default:
                    break;
            }
        }
        
        private void btnRollDice_Click(object sender, RoutedEventArgs e)
        {
            game.TurnFlow();           

            if(game.isGameOver)
            {
                btnRollDice.IsEnabled = false;
                mainWindow.NavigationService.Navigate(new VictoryScreen(game));
            }
        }



        private string SetAbout()
        {
            string about = "Game of the Goose\n\nThe Game of Goose, sometimes known as the Royal Game of Goose, is the earliest commercially produced board game - recorded in Italy as early as the end of the 15th Century.Over hundreds of years, it has appeared in a myriad variations of rules and illustrative designs.\nMany of the boards reflect politics or social situations of the time and some are incredibly beautiful and creative. \nThe basic form of the rules has remained remarkably consistent over the years.\nWe give the standard basic rules that are as applicable to boards produced today as they are to boards produced 400 years ago.\nWith thanks to board games historian, Adrian Seville.";
            return about;
        }

        private string SetRules()
        {
            string rules = $"Play\n\nPlayers take turns to roll the dice and moved their piece forward by the sum of the two dice." +
                $"\nIf your first throw is six and three, move to space 26." +
                $"\nIf your first throw is five and four, move to space 53." + 
                $"\nIf a piece lands on a space with a picture of a horse, it moves forward by same amount again.If this causes the piece to land on another horse, it moves forward again in the same way." +
                $"\nThe following spaces are called Hazard spaces and are usually illustrated to match their name.If a piece lands on the space indicated, that piece must follow the stated rule." +
                $"\n\n6 - The Bridge - Go to space 12" +
                $"\n19 - The Hotel - Stay for one turn" +
                $"\n31 - The Well - Wait until someone comes to pull you out (they then take your place)" +
                $"\n42 - The Maze - Go back to space 39" +
                $"\n52 - The Prison - Stay for three turns" +
                $"\n58 - Death - Return your piece to start" +
                $"\n\nWinning the Game\n" +
                $"\nTo win the game, a piece must land exactly on space 63." +
                $"\nIf a player throws too many, the piece counts the extra points backwards from the winning space.If you then land on a goose space, you must continue moving backwards by the amount of your throw until you land on a space with no goose space.If you land on the Death space, you must start again.";
            return rules;
        }


    }
}