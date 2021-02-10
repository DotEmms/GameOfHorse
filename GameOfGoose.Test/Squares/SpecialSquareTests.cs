using GameOfGoose.Squares;
using NUnit.Framework;

namespace GameOfGoose.Test.Squares
{
    [TestFixture]
    public class SpecialSquareTests
    {
        [Test]
        public void MoveToSpecificSquare_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var specialSquare = new SpecialSquare();

            // Act
            specialSquare.MoveToSpecificSquare();

            // Assert
            Assert.Fail();
        }

        [Test]
        public void SkipTurns_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var specialSquare = new SpecialSquare();

            // Act
            specialSquare.SkipTurns();

            // Assert
            Assert.Fail();
        }

        [Test]
        public void WaitForOtherPlayer_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var specialSquare = new SpecialSquare();

            // Act
            specialSquare.WaitForOtherPlayer();

            // Assert
            Assert.Fail();
        }

        [Test]
        public void GameOverCheck_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var specialSquare = new SpecialSquare();

            // Act
            specialSquare.GameOverCheck();

            // Assert
            Assert.Fail();
        }
    }
}
