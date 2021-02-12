using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace GameOfGoose
{
    public class Game
    {
        public ObservableCollection<Player> players;
        public ObservableCollection<ISquare> squares;
        public Player currentPlayer;
        public ISquare currentSquare;
        private int totalRounds;
        public int diceResult;
        private int firstDie;
        private int secondDie;
        private bool movingBackwards = false;
        public Game()
        {
            players = CreatePlayers(); //move to startgame?
            squares = GenerateGameBoard();
        }

        public void StartGame()
        {
            //assign players to list
            currentPlayer = GetPlayer(1);
        }

        // button event, starts this method for each turn
        private void TurnFlow()
        {
            //First turn only
            if (totalRounds == 1)
            {
                diceResult = RollDice();
                FirstThrowCheck(firstDie, secondDie);
                CheckSquare();
                currentPlayer.IsFirstRound = false;
            }
            //rest of the game
            else
            {
                if (CheckPenalty())
                {
                    diceResult = RollDice();

                    MovePawn(diceResult);
                    //does this in MovePawn -> CheckSquare();
                }
                else
                {
                    //sux to be u
                }
            }
            movingBackwards = false;
            //assign player for next turn
            currentPlayer = GetPlayer(GetNextPlayerId());
        }

        private Player GetPlayer(int id)
        {
            return players.FirstOrDefault(x => x.Id == id);
        }

        private int GetNextPlayerId()
        {
            int nextPlayerId;
            if (currentPlayer.Id < players.Count())
            {
                nextPlayerId = currentPlayer.Id + 1;
            }
            else
            {
                nextPlayerId = 1;
                totalRounds++;
            }
            return nextPlayerId;
        }

        public ISquare GetSquare(int id)
        {
            return squares.FirstOrDefault(x => x.ID == id);
        }

        private bool CheckPenalty()
        {
            if (currentPlayer.TurnPenalty == 0)
            {
                return true;
            }
            else
            {
                currentPlayer.TurnPenalty--;
                return false;
            }
        }

        private void GameOver()
        {
            VictoryScreen victory = new VictoryScreen();
            //victory.Show();
        }

        // button event add player

        private int RollDice() //moet in een klasse apart!
        {
            Random random1 = new Random();
            firstDie = random1.Next(1, 6);

            Random random2 = new Random();
            secondDie = random2.Next(1, 6);

            diceResult = firstDie + secondDie;
            return diceResult;
        }

        private void FirstThrowCheck(int die1, int die2)
        {
            if ((die1 == 5 && die2 == 4) || (die1 == 4 && die2 == 5))
            {
                currentPlayer.PawnLocation = 26;
            }
            else if ((die1 == 6 && die2 == 3) || (die1 == 3 && die2 == 6))
            {
                currentPlayer.PawnLocation = 53;
            }
            else
                MovePawn(diceResult);
        }

        public void MovePawn(int diceTotal)
        {
            int tempValue = diceTotal + currentPlayer.PawnLocation;
            int remainingSteps;
            if (tempValue > 63)
            {
                int squares = 63 - tempValue;
                int steps = diceTotal + squares;
                remainingSteps = squares + steps;
                movingBackwards = true;
            }
            else
            {
                remainingSteps = diceTotal;
            }

            currentPlayer.PawnLocation += remainingSteps;
            currentSquare = GetSquare(currentPlayer.PawnLocation);
            CheckSquare();
        }

        private void CheckSquare()
        {
            //checken op object type
            if (currentSquare is NormalSquare)
            {
                NormalSquare normal = currentSquare as NormalSquare;
                normal.AssignPawnImage();
            }
            else if (currentSquare is GooseSquare)
            {
                GooseSquare goose = currentSquare as GooseSquare;
                goose.AssignPawnImage();
                if (movingBackwards)
                {
                    MovePawn(diceResult * -1);
                }
                MovePawn(diceResult);
            }
            //eigen klasse maken per special square met gedeelde execute methode -> polymorph
            else if (currentSquare is SpecialSquare)
            {
                SpecialSquare special = currentSquare as SpecialSquare;
                switch (currentSquare.Name)
                {
                    case "Bridge":
                    case "Maze":
                    case "Death":
                        currentPlayer.PawnLocation = special.MoveToSpecificSquare(currentPlayer.PawnLocation);
                        break;

                    case "Inn":
                    case "Prison":
                        currentPlayer.TurnPenalty = special.SkipTurns(currentPlayer.PawnLocation);
                        break;

                    case "Well":
                        ResetWellPenalty();
                        currentPlayer.TurnPenalty = special.WaitForOtherPlayer();
                        break;

                    case "End":
                        GameOver();
                        break;

                    default:
                        break;
                }
                special.AssignPawnImage();
            }
        }

        private void ResetWellPenalty()
        {
            foreach (var player in players)
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
                // Row 1
                new NormalSquare(21, "Going Steady"),
                new NormalSquare(20, "Going Steady"),
                new SpecialSquare(19, "Inn", "", "./Images/inn.jpg"),
                new GooseSquare(18, "GOOSE!"),
                new NormalSquare(17, "Going Steady"),
                new NormalSquare(16, "Going Steady"),
                new NormalSquare(15, "Going Steady"),
                new GooseSquare(14, "GOOSE!"),

                // Row 2
                new NormalSquare(22, "Going Steady"),
                new NormalSquare(43, "Going Steady"),
                new SpecialSquare(42, "Maze","","./Images/maze.jpg"),
                new GooseSquare(41, "GOOSE!"),
                new NormalSquare(40, "Going Steady"),
                new NormalSquare(39, "Going Steady"),
                new NormalSquare(38, "Going Steady"),
                new NormalSquare(13, "Going Steady"),

                // Row 3
                new GooseSquare(23, "GOOSE!"),
                new NormalSquare(44, "Going Steady"),
                new NormalSquare(57, "Going Steady"),
                new NormalSquare(56, "Going Steady"),
                new NormalSquare(55, "Going Steady"),
                new GooseSquare(54, "GOOSE!"),
                new NormalSquare(37, "Going Steady"),
                new NormalSquare(12, "Going Steady"),

                // Row 4
                new NormalSquare(24, "Going Steady"),
                new GooseSquare(45, "GOOSE!"),
                new SpecialSquare(58, "Death","","./Images/death.jpg"),
                new SpecialSquare(63, "The End","","./Images/end.jpg"),
                new NormalSquare(62, "Going Steady"),
                new NormalSquare(53, "Going Steady"),
                new GooseSquare(36, "GOOSE!"),
                new NormalSquare(11, "Going Steady"),

                // Row 5
                new NormalSquare(25, "Going Steady"),
                new NormalSquare(46, "Going Steady"),
                new GooseSquare(59, "GOOSE!"),
                new NormalSquare(60, "Going Steady"), //60
                new NormalSquare(61, "Going Steady"),
                new SpecialSquare(52, "Prison","","./Images/prison.jpg"),
                new NormalSquare(35, "Going Steady"),
                new NormalSquare(10, "Going Steady"),

                // Row 6
                new NormalSquare(26, "Going Steady"),
                new NormalSquare(47, "Going Steady"),
                new NormalSquare(48, "Going Steady"),
                new NormalSquare(49, "Going Steady"),
                new GooseSquare(50, "GOOSE!"), //50
                new NormalSquare(51, "Going Steady"),
                new NormalSquare(34, "Going Steady"),
                new GooseSquare(9, "GOOSE!"),

                // Row 7
                new GooseSquare(27, "GOOSE!"),
                new NormalSquare(28, "Going Steady"),
                new NormalSquare(29, "Going Steady"),
                new NormalSquare(30, "Going Steady"), //30
                new SpecialSquare(31, "Well", "" , "./Images/well.jpg"),
                new GooseSquare(32, "GOOSE!"),
                new NormalSquare(33, "Going Steady"),
                new NormalSquare(8, "Going Steady"),

                // Row 8
                new NormalSquare(0, "Start"), //0
                new NormalSquare(1, "Going Steady"),
                new NormalSquare(2, "Going Steady"),
                new NormalSquare(3, "Going Steady"),
                new NormalSquare(4, "Going Steady"),
                new GooseSquare(5, "GOOSE!"), //5
                new SpecialSquare(6, "Bridge", "", "./Images/bridge.jpg"),
                new NormalSquare(7, "Going Steady"),
            };

            return squares;
        }
    }
}