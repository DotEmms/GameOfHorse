using GameOfGoose;
using NUnit.Framework;
using System;

namespace GameOfGoose.Test.Squares
{
    [TestFixture]
    public class GooseSquareTests
    {
        [Test]
        public void CheckGooseSquare_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var gooseSquare = new GooseSquare(1, "Angry Goose");
            int dice = 12;
            int location = 52;
            var expectedResult = 10;
            // Act
            var result = gooseSquare.CheckGooseSquare(
                dice,
                location);
            
            // Assert
            Assert.AreEqual(expectedResult ,result);
        }
    }
}
