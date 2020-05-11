using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pacman
{
    public class Ghost
    {
        private const int GhostsQuantity = 4;

        public int Ghosts = GhostsQuantity;
        private ImageList GhostsImages = new ImageList();
        public PictureBox[] GhostImage = new PictureBox[GhostsQuantity];
        public int[] State = new int[GhostsQuantity];
        private Timer KillAbleTimer = new Timer();
        private Timer StateTimer = new Timer();
        private Timer HomeTimer = new Timer();
        public int[] X_Coordinate = new int[GhostsQuantity];
        public int[] Y_Coordinate = new int[GhostsQuantity];
        private int[] X_Start = new int[GhostsQuantity];
        private int[] Y_Start = new int[GhostsQuantity];
        public int[] GhostDirections = new int[GhostsQuantity];
        private Random randm = new Random();
        private bool GhostActive = false;

        public Ghost()
        {
            GhostsImages.ImageSize = new Size(27, 28);

            timer.Interval = 100;
            timer.Enabled = true;
            timer.Tick += new EventHandler(timer_Tick);

            KillAbleTimer.Interval = 200;
            KillAbleTimer.Enabled = false;
            KillAbleTimer.Tick += new EventHandler(killabletimer_Tick);

            StateTimer.Interval = 10000;
            StateTimer.Enabled = false;
            StateTimer.Tick += new EventHandler(statetimer_Tick);

            HomeTimer.Interval = 5;
            HomeTimer.Enabled = false;
            HomeTimer.Tick += new EventHandler(hometimer_Tick);
        }

        public void CreateGhostsImages(Form formInstance)
        {
            // Create Ghost Image
            for (int x=0; x<Ghosts; x++)
            {
                GhostImage[x] = new PictureBox();
                GhostImage[x].Name = "GhostImage" + x.ToString();
                GhostImage[x].SizeMode = PictureBoxSizeMode.AutoSize;
                formInstance.Controls.Add(GhostImage[x]);
                GhostImage[x].BringToFront();
            }
            ResetGhosts();
        }

        public void ResetGhosts()
        {
            // Reset Ghost States
            for (int x=0; x<GhostsQuantity; x++)
            {
                X_Coordinate[x] = X_Start[x];
                Y_Coordinate[x] = Y_Start[x];
                GhostImage[x].Location = new Point(X_Start[x] * 16 - 3, Y_Start[x] * 16 + 43);
                GhostImage[x].Image = GhostsImages.Images[x * 4];
                GhostDirections[x] = 0;
                State[x] = 0;
            }
        }

        private void statetimer_Tick(object sender, EventArgs e)
        {
            // Turn Ghosts back
            for (int x=0; x<GhostsQuantity; x++)
            {
                State[x] = 0;
            }
            StateTimer.Enabled = false;
            //killabletimer.Enabled = false;
        }

        private void hometimer_Tick(object sender, EventArgs e)
        {
            // Move ghosts to their home positions
            for (int x=0; x<GhostsQuantity; x++)
            {
                if (State[x] == 2)
                {
                    int xpos = X_Start[x] * 16 - 3;
                    int ypos = Y_Start[x] * 16 + 43;
                    if (GhostImage[x].Left > xpos) { GhostImage[x].Left--; }
                    if (GhostImage[x].Left < xpos) { GhostImage[x].Left++; }
                    if (GhostImage[x].Top  > ypos) { GhostImage[x].Top--; }
                    if (GhostImage[x].Top < ypos) { GhostImage[x].Top++; }
                    if (GhostImage[x].Top == ypos && GhostImage[x].Left == xpos)
                    {
                        State[x] = 0;
                        X_Coordinate[x] = X_Start[x];
                        Y_Coordinate[x] = Y_Start[x];
                        GhostImage[x].Left = X_Start[x] * 16 - 3;
                        GhostImage[x].Top = Y_Start[x] * 16 + 43;
                    }
                } 
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            // Keep moving the ghosts
            for (int x = 0; x < Ghosts; x++)
            {
                if (State[x] > 0) { continue; }
                MoveGhosts(x);
            }
            GhostActive = !GhostActive; //then look for pacman
        }

        private void killabletimer_Tick(object sender, EventArgs e)
        {
            // Keep moving the ghosts
            for (int x = 0; x < Ghosts; x++)
            {
                if (State[x] != 1) { continue; }
                MoveGhosts(x);
            }
        }

        private void MoveGhosts(int x)
        {
            // Move the ghosts
            if (GhostDirections[x] == 0)
            {
                if (randm.Next(0, 5) == 3) { GhostDirections[x] = 1;}
            }
            else
            {
                bool CanMove = false;

                while (!CanMove)
                {
                    CanMove = check_direction(GhostDirections[x], x);
                    if (!CanMove) { Change_Direction(GhostDirections[x], x); }

                }

                if (CanMove)
                {
                    switch (GhostDirections[x])
                    {
                        case 1: GhostImage[x].Top -= 16; Y_Coordinate[x]--; break;
                        case 2: GhostImage[x].Left += 16; X_Coordinate[x]++; break;
                        case 3: GhostImage[x].Top += 16; Y_Coordinate[x]++; break;
                        case 4: GhostImage[x].Left -= 16; X_Coordinate[x]--; break;
                    }
                    switch (State[x])
                    {
                        case 0: GhostImage[x].Image = GhostsImages.Images[x * 4 + (GhostDirections[x] - 1)]; break;
                        case 1:
                            if (GhostActive) { GhostImage[x].Image = GhostsImages.Images[17];} else { GhostImage[x].Image = GhostsImages.Images[16]; };
                            break;
                        case 2: GhostImage[x].Image = GhostsImages.Images[18]; break;
                    }
                }
            }
            
        }

        private bool check_direction(int direction, int ghost)
        {
            // Check if ghost can move to space
            return true; // for now
        }

        private void Change_Direction(int direction, int ghost)
        {
            // Change the direction of the ghost
            int which = randm.Next(0, 2);
            switch (direction)
            {
                case 1: case 3: if (which == 1) { GhostDirections[ghost] = 2; } else { GhostDirections[ghost] = 4; }; break;
                case 2: case 4: if (which == 1) { GhostDirections[ghost] = 1; } else { GhostDirections[ghost] = 3; }; break;
            }
        }


        public void ChangeGhostState()
        {
            // Change the state off all of the ghosts so that they can be eaten
            for (int x=0; x<GhostsQuantity; x++)
            {
                if (State[x] == 0)
                {
                    State[x] = 1;
                    GhostImage[x].Image = GhostsImages.Images[16];
                }
            }
            KillAbleTimer.Stop();
            KillAbleTimer.Enabled = true;
            KillAbleTimer.Start();
            StateTimer.Stop();
            StateTimer.Enabled = true;
            StateTimer.Start();
        }

        // Look for pacman and chase him
        // TO-DO

        private Timer timer = new Timer();

    }
}
