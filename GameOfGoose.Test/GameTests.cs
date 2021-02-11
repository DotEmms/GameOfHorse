using GameOfGoose;
using NUnit.Framework;

namespace GameOfGoose.Test
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void MovePawn_NormalMovement_ValueAddsUp()
        {
            // Arrange
            var game = new Game();
            game.currentPlayer = new Player("test");
            game.currentPlayer.PawnLocation = 50;
            int expectedResult = 57;
            // Act
            game.MovePawn(7);
            int result = game.currentPlayer.PawnLocation;

            // Assert
            Assert.AreEqual(expectedResult,result);
        }
        [Test]
        public void MovePawn_BackwardsMovement_ValueGoesDownAfter63()
        {
            // Arrange
            var game = new Game();
            game.currentPlayer = new Player("test");
            game.currentPlayer.PawnLocation = 60;
            int expectedResult = 59;
            // Act
            game.MovePawn(7);
            int result = game.currentPlayer.PawnLocation;

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
        [Test]
        public void MovePawn_Movement_ValueFails()
        {
            // Arrange
            var game = new Game();            
            game.currentPlayer = new Player("test");
            game.currentPlayer.PawnLocation = 60;
            int expectedResult = 59;
            // Act
            game.MovePawn(8);
            int result = game.currentPlayer.PawnLocation;

            // Assert
            Assert.AreNotEqual(expectedResult, result);
        }
        [Test]
        public void MovePawn_NegativeMovementValue_ValueCountsDown()
        {
            // Arrange
            var game = new Game();
            game.currentPlayer = new Player("test");
            game.currentPlayer.PawnLocation = 59;
            int expectedResult = 51;
            // Act
            game.MovePawn(-8);
            int result = game.currentPlayer.PawnLocation;

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}
