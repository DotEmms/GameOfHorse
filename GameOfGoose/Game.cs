using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace GameOfGoose
{
    public class Game
    {
        public ObservableCollection<Player> players;
        public ObservableCollection<ISquare> squares;
        private readonly Dice dice;
        public Player currentPlayer;
        public Player previousPlayer;
        public ISquare currentSquare;

        public int totalTurns = 0;
        public int totalRounds = 0;
        public int diceResult;
        private bool movingBackwards = false;
        public bool isGameOver = false;
        public Player winningPlayer;

        public Game()
        {
            players = new ObservableCollection<Player>();
            squares = GenerateGameBoard();
            dice = new Dice();
        }

        public void StartGame()
        {
            currentPlayer = GetPlayer(1);
        }

        public void TurnFlow()
        {
            totalTurns++;

            //First turn only
            if (currentPlayer.IsFirstRound)
            {
                diceResult = dice.RollDice();
                FirstThrowCheck(dice.firstDie, dice.secondDie);
                CheckSquare();
                currentPlayer.IsFirstRound = false;
            }
            //rest of the game
            else
            {
                if (CheckPenalty())
                {
                    diceResult = dice.RollDice();

                    MovePawn(diceResult);
                    //does this in MovePawn -> CheckSquare();
                }
                else
                {
                    //message to frontend?
                }
            }
            movingBackwards = false;
            //assign player for next turn
            if (isGameOver != true)
            {
                previousPlayer = currentPlayer;
                currentPlayer = GetPlayer(GetNextPlayerId());
            }
        }

        public Player GetPlayer(int id)
        {
            Player player;
            if (players == null)
            {
                player = null;
            }
            else
            {
                if (id <= players.Count)
                {
                    player = players.FirstOrDefault(x => x.Id == id);
                }
                else
                {
                    player = null;
                }
            }

            return player;
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
                totalTurns++;
            }
            return nextPlayerId;
        }

        private ISquare GetSquare(int id)
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
            isGameOver = true;
            winningPlayer = currentPlayer;
            totalRounds = CalculateTotalRounds();
        }

        private void FirstThrowCheck(int die1, int die2)
        {
            if ((die1 == 5 && die2 == 4) || (die1 == 4 && die2 == 5))
            {
                currentPlayer.PawnLocation = 26;
                currentSquare = GetSquare(currentPlayer.PawnLocation);
                UpdateCoordinates();
            }
            else if ((die1 == 6 && die2 == 3) || (die1 == 3 && die2 == 6))
            {
                currentPlayer.PawnLocation = 53;
                currentSquare = GetSquare(currentPlayer.PawnLocation);
                UpdateCoordinates();
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
            UpdateCoordinates();
            CheckSquare();
        }

        private void UpdateCoordinates()
        {
            currentPlayer.Column = currentSquare.Column;
            currentPlayer.Row = currentSquare.Row;
        }

        private void CheckSquare()
        {
            //checken op object type
            if (currentSquare is GooseSquare)
            {
                if (movingBackwards)
                {
                    MessageBox.Show($"Moving backwards with {diceResult}!", "Landed on 'GOOSE!'");
                    MovePawn(diceResult * -1);
                }
                MessageBox.Show($"Moving forward with {diceResult}!", "Landed on 'GOOSE!'");
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
                        currentSquare = GetSquare(currentPlayer.PawnLocation);
                        UpdateCoordinates();
                        break;

                    case "Inn":
                    case "Prison":
                        currentPlayer.TurnPenalty = special.SkipTurns(currentPlayer.PawnLocation);

                        break;

                    case "Well":
                        ResetWellPenalty();
                        currentPlayer.TurnPenalty = special.WaitForOtherPlayer();

                        break;

                    case "The End":
                        GameOver();
                        break;

                    default:
                        break;
                }
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

        private int CalculateTotalRounds()
        {
            double result = (totalTurns / players.Count);
            return (int)Math.Ceiling(result);
        }

        public Player CreatePlayer(string name)
        {
            var player = new Player(name);
            return player;
        }

        public void AddPlayerToGame(Player player)
        {
            players.Add(player);
        }

        public void ResetPlayers()
        {
            players.Clear();
        }

        private ObservableCollection<ISquare> GenerateGameBoard()
        {
            var squares = new ObservableCollection<ISquare>
            {
                // Row 1
                new NormalSquare(21, 0, 0, "21"),
                new NormalSquare(20, 0, 1,"20"),
                new SpecialSquare(19,0, 2, "Inn", "Skip one turn", "./Images/inn.jpg"),
                new GooseSquare(18,0,3,  "GOOSE!"),
                new NormalSquare(17,0,4, "17"),
                new NormalSquare(16, 0,5,"16"),
                new NormalSquare(15, 0,6,"15"),
                new GooseSquare(14, 0,7,"GOOSE!"),

                // Row 2
                new NormalSquare(22, 1,0,"22"),
                new NormalSquare(43, 1,1,"Going Steady"),
                new SpecialSquare(42, 1,2,"Maze","Go back to square 39","./Images/maze.jpg"),
                new GooseSquare(41, 1,3,"GOOSE!"),
                new NormalSquare(40, 1,4,"40"),
                new NormalSquare(39, 1,5,"39"),
                new NormalSquare(38, 1,6,"38"),
                new NormalSquare(13, 1,7,"13"),

                // Row 3
                new GooseSquare(23, 2,0,"GOOSE!"),
                new NormalSquare(44, 2,1,"44"),
                new NormalSquare(57, 2,2,"57"),
                new NormalSquare(56, 2,3,"56"),
                new NormalSquare(55, 2,4,"55"),
                new GooseSquare(54, 2,5,"GOOSE!"),
                new NormalSquare(37, 2,6,"37"),
                new NormalSquare(12, 2,7,"12"),

                // Row 4
                new NormalSquare(24, 3,0,"24"),
                new GooseSquare(45, 3,1,"GOOSE!"),
                new SpecialSquare(58, 3,2,"Death","Go back to start","./Images/death.jpg"),
                new SpecialSquare(63, 3,3,"The End","YOU WIN!","./Images/end.jpg"),
                new NormalSquare(62, 3,4,"62"),
                new NormalSquare(53, 3,5,"53"),
                new GooseSquare(36, 3,6,"GOOSE!"),
                new NormalSquare(11, 3,7,"11"),

                // Row 5
                new NormalSquare(25, 4,0,"25"),
                new NormalSquare(46, 4,1,"46"),
                new GooseSquare(59, 4,2,"GOOSE!"),
                new NormalSquare(60, 4,3,"60"),
                new NormalSquare(61, 4,4,"61"),
                new SpecialSquare(52, 4,5,"Prison","Skip 3 turns","./Images/prison.jpg"),
                new NormalSquare(35, 4,6,"35"),
                new NormalSquare(10, 4,7,"10"),

                // Row 6
                new NormalSquare(26, 5,0, "26"),
                new NormalSquare(47, 5,1,"47"),
                new NormalSquare(48, 5,2,"48"),
                new NormalSquare(49, 5,3,"49"),
                new GooseSquare(50, 5,4,"GOOSE!"),
                new NormalSquare(51, 5,5,"51"),
                new NormalSquare(34, 5,6,"34"),
                new GooseSquare(9, 5,7,"GOOSE!"),

                // Row 7
                new GooseSquare(27, 6,0,"GOOSE!"),
                new NormalSquare(28, 6,1,"28"),
                new NormalSquare(29, 6,2,"29"),
                new NormalSquare(30, 6,3,"30"),
                new SpecialSquare(31, 6,4,"Well", "Wait for another player" , "./Images/well.jpg"),
                new GooseSquare(32, 6,5,"GOOSE!"),
                new NormalSquare(33, 6,6,"33"),
                new NormalSquare(8, 6,7,"8"),

                // Row 8
                new NormalSquare(0, 7, 0,  "Start"),
                new NormalSquare(1, 7, 1, "1"),
                new NormalSquare(2, 7, 2, "2"),
                new NormalSquare(3, 7, 3, "3"),
                new NormalSquare(4, 7, 4, "4"),
                new GooseSquare(5, 7, 5, "GOOSE!"),
                new SpecialSquare(6, 7, 6, "Bridge", "Move to square 12", "./Images/bridge.jpg"),
                new NormalSquare(7, 7, 7, "7"),
            };

            return squares;
        }
    }
}