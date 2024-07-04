using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game2
{
    public partial class Form1 : Form
    {
        bool goLeft, goRight, jumping, isGameOver;

        int jumpSpeed = 10;
        int force=8;        
        int playerSpeed = 9;
        int score = 0;
        int si = 0;

        int horizontalSpeed = 10;

        int enemyOneSpeed = 7;
        int enemyTwoSpeed = 5;
        int enemyThreeSpeed = 4;
        int enemyFourSpeed = 5;
        int enemyFiveSpeed = 5;

        public Form1()
        {
            InitializeComponent();
        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {            
            player.Top += jumpSpeed;

            if (goLeft == true)
            {
                player.Left -= playerSpeed;
            }

            if (goRight == true)
            {
                player.Left += playerSpeed;
            }

            if (jumping == true && force < 0)
            {
                jumping = false;
            }

            if (jumping == true)
            {
                jumpSpeed = -7;
                force -= 1;
            }

            else
            {
                jumpSpeed = 12;
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {

                    if ((string)x.Tag == "platform")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            force = 8;
                            player.Top = x.Top - player.Height;

                            if ((string)x.Name == "horizontalPlatform" && goLeft == false ||
                                (string)x.Name == "horizontalPlatform" && goRight == false)
                            {
                                player.Left -= horizontalSpeed;

                            }
                        }

                        x.BringToFront();
                    }

                    if ((string)x.Tag == "i")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            if (si == 0)
                            {
                                ++si;
                                MessageBox.Show("you can't win before collect all coins first coward!");                                
                            }
                            
                        }
                    }


                    if ((string)x.Tag == "coin")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                        {
                            x.Visible = false;
                            score++;
                        }
                    }

                    if ((string)x.Tag == "enemy")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            gameTimer.Stop();
                            isGameOver = true;
                            MessageBox.Show("Game Over Loser :)");
                        }
                    }


                }
            }

            horizontalPlatform.Left -= horizontalSpeed;
            if (horizontalPlatform.Left < 0 ||
                horizontalPlatform.Left + horizontalPlatform.Width > this.ClientSize.Width)
            {
                horizontalSpeed = -horizontalSpeed;
            }

            enemy0.Top -= enemyOneSpeed;
            if (enemy0.Top < pictureBox7.Top + 30 ||
                enemy0.Top > 410)
            {
                enemyOneSpeed = -enemyOneSpeed;
            }

            enemy2.Left -= enemyTwoSpeed;
            if (enemy2.Left < 0 ||
                enemy2.Left + enemy2.Width > this.ClientSize.Width)
            {
                enemyTwoSpeed = -enemyTwoSpeed;
            }

            enemy3.Left -= enemyFourSpeed;
            if (enemy3.Left < 0 ||
                enemy3.Left + enemy3.Width > this.ClientSize.Width)
            {
                enemyFourSpeed = -enemyFourSpeed;
            }

            enemy4.Left -= enemyFiveSpeed;
            if (enemy4.Left < 0 ||
                enemy4.Left + enemy4.Width > this.ClientSize.Width)
            {
                enemyFiveSpeed = -enemyFiveSpeed;
            }

            enemy1.Left -= enemyThreeSpeed;
            if (enemy1.Left < pictureBox7.Left ||
                enemy1.Left + enemy1.Width > pictureBox7.Left + pictureBox7.Width)
            {
                enemyThreeSpeed = -enemyThreeSpeed;
            }


            if (player.Top + player.Height > this.ClientSize.Height + 50)
            {
                gameTimer.Stop();
                isGameOver = true;                
                MessageBox.Show("You fell to your death loser");
            }

            if (player.Bounds.IntersectsWith(door.Bounds) && score == 5)
            {
                gameTimer.Stop();
                isGameOver = true;
                MessageBox.Show("You lost. you will never win actualy!");
                /*txtScore.Text = "score: " + score + Environment.NewLine +
                    "It is just lucky loser";
            }*/
            if (player.Bounds.IntersectsWith(door.Bounds) && score < 5)
            {
                    MessageBox.Show("NO loser, dont be coward collect all coins first"
                                 + Environment.NewLine + "score: " + score);
                }

            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }

            if (e.KeyCode == Keys.Up && jumping == false)
            {
                jumping = true;
            }

            if (e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;
            }

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }

            if (jumping == true)
            {
                jumping = false;
            }

            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                restartGame();
            }


        }

        private void restartGame()
        {
            jumping = false;
            goLeft = false;
            goRight = false;
            isGameOver = false;
            score = 0;
            si = 0;
            

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Visible == false && (string)x.Name != "i")
                {
                    x.Visible = true;
                }
            }

            // reset the position of the player
            player.Left = 190;
            player.Top = 373;

            horizontalPlatform.Left = 185;
            

            gameTimer.Start();            

        }
    }
}
