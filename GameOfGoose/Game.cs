using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace GameOfGoose
{
    public class Game
    {
        private ObservableCollection<Player> players;
        private Player currentPlayer;
        private int totalRounds;

        private void StartGame()
        {
            players = CreatePlayers();

            //turn structure
            //TurnFlow();
            StartTurn();
        }

        private void TurnFlow(int id)
        {
            
            if (CheckTurn())
            {
                MovePawn(RollDice());
            }
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
            DecideTurnPerRound();
            //Do magic
        }

        private void EndTurn()
        {
        }

        private void GameOver()
        {
            VictoryScreen.Show();
        }

        // button event add player

        private int RollDice()
        {
            Random random1 = new Random();
            int dice1 = random1.Next(1, 6);

            Random random2 = new Random();
            int dice2 = random2.Next(1, 6);

            int result = dice1 + dice2;
            return result;
        }

        private void MovePawn(int steps)
        {
            steps = RollDice();
            player.PawnLocation += steps;
        }

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
    }
}