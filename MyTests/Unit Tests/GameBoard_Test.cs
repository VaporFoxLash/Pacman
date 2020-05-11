using NUnit.Framework;
using System;
using System.Windows.Forms;

namespace Pacman
{
    [TestFixture]
    public class GameBoard_Test
    {
        private GameBoard GameBoard = new GameBoard();

        [Test]
        public void NewGameBoardTest()
        {
            // Check default Game Board image has been created
            GameBoard.CreateBoardPicture(new Form(), 1);
            Assert.AreNotEqual(null, GameBoard.BoardPicture.Image);
            Assert.AreEqual(0, GameBoard.BoardPicture.Left);
            Assert.AreEqual(50, GameBoard.BoardPicture.Top);
            Assert.AreEqual("BoardPicture", GameBoard.BoardPicture.Name);
        }

        [Test]
        public void GameBoardMatrixTest()
        {
            // Check Board Matrix for start position
            Assert.AreEqual(new Tuple<int,int> (9,17), GameBoard.InitialBoardMatrix(1));
        }
    }
}
