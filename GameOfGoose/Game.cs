
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
        private int totalRounds;
        private int diceResult;
        private int firstDie;
        private int secondDie;

        private void StartGame()
        {
            players = CreatePlayers();
            currentPlayer = GetPlayer(1);
            TurnFlow();
        }

        private void TurnFlow()
        {

            //if(totalRounds == 1)
            //{
            //    FirstThrowCheck(firstDie, secondDie);
            //    diceResult = RollDice();                
            //}
            //else
            
            
            
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
            
            if (CheckTurn())
            {
                MovePawn(diceResult);
            }
            
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

        private bool CheckTurn()
        {
            return currentPlayer.IsTurnPlayer;            
        }

        private bool IsTurnPlayer()
        {
            return false;
        }
        private void StartTurn()
        {
            GetNextPlayerId();
            //Do magic
        }

        private void EndTurn()
        {

        }

        private void GameOver()
        {
            VictoryScreen victory = new VictoryScreen(currentPlayer)
            victory.Show();
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
                EndTurn();
            }
            else if ((die1 == 6 && die2 == 3) || (die1 == 3 && die2 == 6))
            {
                currentPlayer.PawnLocation = 53;
                EndTurn();
            }
        }

        private void MovePawn(int steps)
        {
            steps = RollDice();
            currentPlayer.PawnLocation += steps; 
        }

        // button event next player

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
                new NormalSquare(), //0
                new NormalSquare(),
                new NormalSquare(),
                new NormalSquare(),
                new NormalSquare(),
                new GooseSquare(), //5
                new SpecialSquare(),
                new NormalSquare(),
                new NormalSquare(),
                new GooseSquare(),
                new NormalSquare(), //10
                new NormalSquare(),
                new NormalSquare(),
                new NormalSquare(),
                new GooseSquare(),
                new NormalSquare(), //15
                new NormalSquare(),
                new NormalSquare(),
                new GooseSquare(),
                new SpecialSquare(),
                new NormalSquare(), //20
                new NormalSquare(),
                new NormalSquare(),
                new GooseSquare(),
                new NormalSquare(),
                new NormalSquare(), //25
                new NormalSquare(),
                new GooseSquare(),
                new NormalSquare(),
                new NormalSquare(),
                new NormalSquare(), //30
                new SpecialSquare(),
                new GooseSquare(),
                new NormalSquare(),
                new NormalSquare(),
                new NormalSquare(), //35
                new GooseSquare(),
                new NormalSquare(),
                new NormalSquare(),
                new NormalSquare(),
                new NormalSquare(), //40
                new GooseSquare(),
                new SpecialSquare(),
                new NormalSquare(),
                new NormalSquare(),
                new GooseSquare(), //45
                new NormalSquare(),
                new NormalSquare(),
                new NormalSquare(),
                new NormalSquare(),
                new GooseSquare(), //50
                new NormalSquare(),
                new SpecialSquare(),
                new NormalSquare(),
                new GooseSquare(),
                new NormalSquare(), //55
                new NormalSquare(),
                new NormalSquare(),
                new SpecialSquare(),
                new GooseSquare(),
                new NormalSquare(), //60
                new NormalSquare(),
                new NormalSquare(),
                new SpecialSquare(),

            };

            return squares;
        }
    }
}