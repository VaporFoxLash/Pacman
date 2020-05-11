using NUnit.Framework;
using System.Windows.Forms;

namespace Pacman
{
    [TestFixture]
    public class Ghost_Test
    {
        private Ghost GhostItem = new Ghost();

        public Ghost_Test()
        {
            GhostItem.CreateGhostsImages(new Form());
        }

        [Test]
        public void NewGhostTest()
        {
            // Check default Ghost image has been created
            Assert.AreNotEqual(null, GhostItem.GhostImage[0].Image);
        }
    }
}
