
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace GameOfGoose
{
    public class Game
    {
        private ObservableCollection<Player> players;
        private ObservableCollection<ISquare> squares;
        private Player currentPlayer;
        private ISquare currentSquare;
        private int totalRounds;
        private int diceResult;
        private int firstDie;
        private int secondDie;
        private bool isGameOngoing = true;

        public Game()
        {
            players = CreatePlayers();
            squares = GenerateGameBoard();
        }

        private void StartGame()
        {
            currentPlayer = GetPlayer(1);
            TurnFlow();
        }

        private void TurnFlow()
        {
            //infinite loop while game is ongoing
            while (isGameOngoing)
            {
                //First turn only
                if (totalRounds == 1)
                {
                    FirstThrowCheck(firstDie, secondDie);
                    diceResult = RollDice();
                }
                //rest of the game
                else
                {
                    //can current player play?(not in well/inn/jail)
                    // checkstatus?
                    if (CheckPenalty())
                    {
                        diceResult = RollDice();
                        //wait until button is pressed

                        MovePawn(diceResult);
                        CheckSquare();
                                                
                    }                    
                }
                //assign player for next turn
                currentPlayer = GetPlayer(GetNextPlayerId());
            }

            // click event btnNextPlayer_Click of btnEndTurn_click roept deze methode aan

            /* STANDARD TURN FLOW
             
            Mag ik spelen? (geen beurt overslaan van Inn/Jail/Well)
            
            ja? 
            { 
             StartTurn();
            // click event btnRollDice_Click => diceResult = RollDice() => pop-up met aantal ogen dat gegooid is

            MovePawn(diceResult)
                extra effect? => show info about the special location (new screen/pop-up)
                -> MovePawnToSpecificSquare(currentPlayer.PawnLocation);
                -> special square uitvoeren specialMove.methodeVanToepassing() of goose.MovePawnToSpecificLocation()

            show relaxed goose in pop-up
            Set next player => GetNextPlayerId(); => include in btn event next player
            }    
            
            neen?
            {
            show info why not. => new screen/pop-up
            Set next player.      
            }       
            */
            
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
        private ISquare GetSquare(int id)
        {
            return squares.FirstOrDefault(x => x.ID == id);
        }

        private bool CheckPenalty()
        {
            if(currentPlayer.TurnPenalty == 0)
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
            isGameOngoing = false;
            VictoryScreen victory = new VictoryScreen();
            //victory.Show();
        }

        // button event add player

        private int RollDice()
        {
            Random random1 = new Random();
            int die1 = random1.Next(1, 6);

            Random random2 = new Random();
            int die2 = random2.Next(1, 6);

            firstDie = die1;
            secondDie = die2;

            diceResult = die1 + die2;
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
        }

        private void MovePawn(int steps)
        {
            var value = currentPlayer.PawnLocation + steps;
            
                currentPlayer.PawnLocation += value;
                currentSquare = GetSquare(value);
            
        }
        private void CheckSquare()
        {
            if (currentSquare.Name == "Going Steady")
            {
                NormalSquare normal = currentSquare as NormalSquare;
                normal.AssignPawnImage();
            }else if(currentSquare.Name == "GOOSE!")
            {
                GooseSquare goose = currentSquare as GooseSquare;
                goose.AssignPawnImage();
                MovePawn(goose.CheckGooseSquare(diceResult, currentPlayer.PawnLocation));
            }
            else if (currentSquare.Name == "Bridge" || currentSquare.Name == "Inn" || currentSquare.Name == "Well" || currentSquare.Name == "Maze" || currentSquare.Name == "Prison" || currentSquare.Name == "Death" || currentSquare.Name == "End")
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
                if(player.PawnLocation == 31)
                {
                    player.TurnPenalty = 0;
                }
            }
        }

        // button event roll dice

        private ObservableCollection<Player> CreatePlayers()
        {
            var players = new ObservableCollection<Player>
            {
                new Player("Player 1"),
                new Player("Player 2"),
                new Player("Player 3"),
                new Player("Player 4")
            };

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