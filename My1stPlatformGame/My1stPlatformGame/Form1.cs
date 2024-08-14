using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My1stPlatformGame
{
    public partial class Form1 : Form
    {
        bool goLeft, goRight, jumping, isGameOver;

        int jumpSpeed;
        int force;
        int score = 0;
        int playerSpeed = 7;

        int horizontalSpeed = 5;
        int verticalSpeed = 3;

        int enemyOneSpeed = 3;
        int enemyTwoSpeed = 5;
        
        
        public Form1()
        {
            InitializeComponent();
        }

        private void tickTimer(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score;

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
                jumpSpeed = -8;
                force -= 1;
            }
            else { jumpSpeed = 10; }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if((string)x.Tag == "platform")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            force = 8;
                            player.Top = x.Top - player.Height;

                            if ((string)x.Name == "horizontalplatform" && goLeft == false || (string)x.Name == "horizontalplatform" && goRight == false)
                            {
                                player.Left -= horizontalSpeed;
                            }
                        }

                        x.BringToFront();
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
                        txtScore.Text = "Score: " + score + Environment.NewLine + "You were killed in your journey!";
                    }
                }
            }

            horizontalplatform.Left -= horizontalSpeed;

            if (horizontalplatform.Left < 0 || horizontalplatform.Left + horizontalplatform.Width > this.ClientSize.Width)
            {
                horizontalSpeed = -horizontalSpeed;
            }

            verticalplatform.Top += verticalSpeed;

            if (verticalplatform.Top < 178 || verticalplatform.Top > 564)
            {
                verticalSpeed = -verticalSpeed;
            }

            EnemyOne.Left -= enemyOneSpeed;

            if (EnemyOne.Left < pictureBox5.Left || EnemyOne.Left + EnemyOne.Width > pictureBox5.Left + pictureBox5.Width)
            {
                enemyOneSpeed = -enemyOneSpeed;
            }

            EnemyTwo.Left += enemyTwoSpeed;

            if (EnemyTwo.Left < pictureBox2.Left || EnemyTwo.Left + EnemyTwo.Width > pictureBox2.Left + pictureBox2.Width)
            {
                enemyTwoSpeed = -enemyTwoSpeed;
            }

            if (player.Top + player.Width > this.ClientSize.Height + 50)
            {
                gameTimer.Stop();
                isGameOver = true;
                txtScore.Text = "Score: " + score + Environment.NewLine + "You fell to your death!";
            }

            if (player.Bounds.IntersectsWith(door.Bounds) && score == 21)
            {
                gameTimer.Stop();
                isGameOver = true;
                txtScore.Text = "Score: " + score + Environment.NewLine + "Your quest is complete!";
            }

            else { txtScore.Text = "Score: " + score + Environment.NewLine + "Collect all the coins"; }
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
                RestartGame();
            }
        }

        private void RestartGame()
        {
            jumping = false;
            goLeft = false;
            goRight = false;
            isGameOver = false;
            score = 0;

            txtScore.Text = "Score: " + score;

            foreach(Control x in this.Controls)
            {
                if (x is PictureBox && x.Visible == false)
                {
                    x.Visible = true;
                }
            }

            // reseting position player, platform and enemies

            player.Left = 66;
            player.Top = 669;

            EnemyOne.Left = 247;
            EnemyTwo.Left = 393;

            horizontalplatform.Left = 349;
            verticalplatform.Top = 281;

            gameTimer.Start();

        }
    }
}
