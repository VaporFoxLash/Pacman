using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;

namespace Pacman.Classes
{
    public class Pacman
    {
        // Initialise variables
        public int xCoordinate = 0;
        public int yCoordinate = 0;
        // Initial position
        private int xStart = 0;
        private int yStart = 0;
        public int currentDirection = 0;
        public int nextDirection = 0;
        private Timer timer = new Timer();

        private int imageOn = 0;

        public Pacman()
        {
            timer.Interval = 100;
            timer.Enabled = true;
            timer.Tick += new EventHandler(timer_Tick);

           // add player
        }


        public void MovePacman(int direction)
        {
            // Move Pacman
            bool CanMove = check_direction(nextDirection);
            if (!CanMove) { CanMove = check_direction(currentDirection); direction = currentDirection; } else { direction = nextDirection; }
            if (CanMove) { currentDirection = direction; }

            if (CanMove)
            {
                //move in the respective direction
            }
        }

        private void CheckPacmanPosition()
        {
            // Check Pacmans position
            // Will check on the board
        }


        private bool check_direction(int direction)
        {
            // Check if pacman can move to space
            switch (direction)
            {
                case 1: return isDirection(xCoordinate, yCoordinate - 1);
                case 2: return isDirection(xCoordinate + 1, yCoordinate);
                case 3: return isDirection(xCoordinate, yCoordinate + 1);
                case 4: return isDirection(xCoordinate - 1, yCoordinate);
                default: return false;
            }
        }

        private bool isDirection(int x, int y)
        {
            // Check if board space can be used
            // Get pacman image then check if the direction is valid
            return true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            // Keep moving pacman
            MovePacman(currentDirection);
        }

    }
}
