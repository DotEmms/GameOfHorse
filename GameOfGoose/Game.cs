using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace GameOfGoose
{
    public class Game
    {
        public ObservableCollection<Player> Players;
        public ObservableCollection<ISquare> Squares;
        public Player CurrentPlayer;
        private ISquare _currentSquare;
        private int _totalRounds;
        public int DiceResult;
        private int _firstDie;
        private int _secondDie;
        private bool _movingBackwards;

        public Game()
        {
            Players = CreatePlayers(); //move to startgame?
            Squares = GenerateGameBoard();
        }

        private void StartGame()
        {
            //assign players to list
            CurrentPlayer = GetPlayer(1);
        }

        // button event, starts this method for each turn
        private void TurnFlow()
        {
            //First turn only
            if (_totalRounds == 1)
            {
                DiceResult = RollDice();
                FirstThrowCheck(_firstDie, _secondDie);
                CheckSquare();
                CurrentPlayer.IsFirstRound = false;
            }
            //rest of the game
            else
            {
                if (CheckPenalty())
                {
                    DiceResult = RollDice();

                    MovePawn(DiceResult);
                    //does this in MovePawn -> CheckSquare();
                }
                else
                {
                    //sux to be u
                }
            }
            _movingBackwards = false;
            //assign player for next turn
            CurrentPlayer = GetPlayer(GetNextPlayerId());
        }

        private Player GetPlayer(int id)
        {
            return Players.FirstOrDefault(x => x.Id == id);
        }

        private int GetNextPlayerId()
        {
            int nextPlayerId;
            if (CurrentPlayer.Id < Players.Count)
            {
                nextPlayerId = CurrentPlayer.Id + 1;
            }
            else
            {
                nextPlayerId = 1;
                _totalRounds++;
            }
            return nextPlayerId;
        }

        private ISquare GetSquare(int id)
        {
            return Squares.FirstOrDefault(x => x.ID == id);
        }

        private bool CheckPenalty()
        {
            if (CurrentPlayer.TurnPenalty == 0)
            {
                return true;
            }
            else
            {
                CurrentPlayer.TurnPenalty--;
                return false;
            }
        }

        private void GameOver()
        {
            VictoryScreen victory = new VictoryScreen();
            //victory.Show();
        }

        // button event add player

        private int RollDice()
        {
            Random random1 = new Random();
            _firstDie = random1.Next(1, 6);

            Random random2 = new Random();
            _secondDie = random2.Next(1, 6);

            DiceResult = _firstDie + _secondDie;
            return DiceResult;
        }

        private void FirstThrowCheck(int die1, int die2)
        {
            if ((die1 == 5 && die2 == 4) || (die1 == 4 && die2 == 5))
            {
                CurrentPlayer.PawnLocation = 26;
            }
            else if ((die1 == 6 && die2 == 3) || (die1 == 3 && die2 == 6))
            {
                CurrentPlayer.PawnLocation = 53;
            }
            else
                MovePawn(DiceResult);
        }

        public void MovePawn(int diceTotal)
        {
            int tempValue = diceTotal + CurrentPlayer.PawnLocation;
            int remainingSteps;
            if (tempValue > 63)
            {
                int squares = 63 - tempValue;
                int steps = diceTotal + squares;
                remainingSteps = squares + steps;
                _movingBackwards = true;
            }
            else
            {
                remainingSteps = diceTotal;
            }

            CurrentPlayer.PawnLocation += remainingSteps;
            _currentSquare = GetSquare(CurrentPlayer.PawnLocation);
            CheckSquare();
        }

        private void CheckSquare()
        {
            if (_currentSquare.Name == "Going Steady")
            {
                NormalSquare normal = _currentSquare as NormalSquare;
                normal?.AssignPawnImage();
            }
            else if (_currentSquare.Name == "GOOSE!")
            {
                GooseSquare goose = _currentSquare as GooseSquare;
                goose?.AssignPawnImage();
                if (_movingBackwards)
                {
                    MovePawn(DiceResult * -1);
                }
                MovePawn(DiceResult);
            }
            else if (_currentSquare.Name == "Bridge" || _currentSquare.Name == "Inn" || _currentSquare.Name == "Well" || _currentSquare.Name == "Maze" || _currentSquare.Name == "Prison" || _currentSquare.Name == "Death" || _currentSquare.Name == "End")
            {
                if (!(_currentSquare is SpecialSquare special)) return;
                switch (_currentSquare.Name)
                {
                    case "Bridge":
                    case "Maze":
                    case "Death":
                        CurrentPlayer.PawnLocation = special.MoveToSpecificSquare(CurrentPlayer.PawnLocation);
                        break;

                    case "Inn":
                    case "Prison":
                        CurrentPlayer.TurnPenalty = special.SkipTurns(CurrentPlayer.PawnLocation);
                        break;

                    case "Well":
                        ResetWellPenalty();
                        CurrentPlayer.TurnPenalty = special.WaitForOtherPlayer();
                        break;

                    case "End":
                        GameOver();
                        break;
                }

                special.AssignPawnImage();
            }
        }

        private void ResetWellPenalty()
        {
            foreach (var player in Players)
            {
                if (player.PawnLocation == 31)
                {
                    player.TurnPenalty = 0;
                }
            }
        }

        private ObservableCollection<Player> CreatePlayers()
        {
            var players = new ObservableCollection<Player>
            {
                new Player("Player 1"),
                new Player("Player 2"),
                //new Player("Player 3"),
                //new Player("Player 4")
            };
            players.Add(new Player("Player 4"));
            return players;
        }

        private ObservableCollection<ISquare> GenerateGameBoard()
        {
            var squares = new ObservableCollection<ISquare>
            {
                new NormalSquare(0, "Start"), //0
                new NormalSquare(1, "Going Steady"),
                new NormalSquare(2, "Going Steady"),
                new NormalSquare(3, "Going Steady"),
                new NormalSquare(4, "Going Steady"),
                new GooseSquare(5, "GOOSE!"), //5
                new SpecialSquare(6, "Bridge", "", "./Images/bridge.jpg"),
                new NormalSquare(7, "Going Steady"),
                new NormalSquare(8, "Going Steady"),
                new GooseSquare(9, "GOOSE!"),
                new NormalSquare(10, "Going Steady"), //10
                new NormalSquare(11, "Going Steady"),
                new NormalSquare(12, "Going Steady"),
                new NormalSquare(13, "Going Steady"),
                new GooseSquare(14, "GOOSE!"),
                new NormalSquare(15, "Going Steady"), //15
                new NormalSquare(16, "Going Steady"),
                new NormalSquare(17, "Going Steady"),
                new GooseSquare(18, "GOOSE!"),
                new SpecialSquare(19, "Inn", "", "./Images/inn.jpg"),
                new NormalSquare(20, "Going Steady"), //20
                new NormalSquare(21, "Going Steady"),
                new NormalSquare(22, "Going Steady"),
                new GooseSquare(23, "GOOSE!"),
                new NormalSquare(24, "Going Steady"),
                new NormalSquare(25, "Going Steady"), //25
                new NormalSquare(26, "Going Steady"),
                new GooseSquare(27, "GOOSE!"),
                new NormalSquare(28, "Going Steady"),
                new NormalSquare(29, "Going Steady"),
                new NormalSquare(30, "Going Steady"), //30
                new SpecialSquare(31, "Well", "" , "./Images/well.jpg"),
                new GooseSquare(32, "GOOSE!"),
                new NormalSquare(33, "Going Steady"),
                new NormalSquare(34, "Going Steady"),
                new NormalSquare(35, "Going Steady"), //35
                new GooseSquare(36, "GOOSE!"),
                new NormalSquare(37, "Going Steady"),
                new NormalSquare(38, "Going Steady"),
                new NormalSquare(39, "Going Steady"),
                new NormalSquare(40, "Going Steady"), //40
                new GooseSquare(41, "GOOSE!"),
                new SpecialSquare(42, "Maze","","./Images/maze.jpg"),
                new NormalSquare(43, "Going Steady"),
                new NormalSquare(44, "Going Steady"),
                new GooseSquare(45, "GOOSE!"), //45
                new NormalSquare(46, "Going Steady"),
                new NormalSquare(47, "Going Steady"),
                new NormalSquare(48, "Going Steady"),
                new NormalSquare(49, "Going Steady"),
                new GooseSquare(50, "GOOSE!"), //50
                new NormalSquare(51, "Going Steady"),
                new SpecialSquare(52, "Prison","","./Images/prison.jpg"),
                new NormalSquare(53, "Going Steady"),
                new GooseSquare(54, "GOOSE!"),
                new NormalSquare(55, "Going Steady"), //55
                new NormalSquare(56, "Going Steady"),
                new NormalSquare(57, "Going Steady"),
                new SpecialSquare(58, "Death","","./Images/death.jpg"),
                new GooseSquare(59, "GOOSE!"),
                new NormalSquare(60, "Going Steady"), //60
                new NormalSquare(61, "Going Steady"),
                new NormalSquare(62, "Going Steady"),
                new SpecialSquare(63, "The End","","./Images/end.jpg"),
            };

            return squares;
        }
    }
}