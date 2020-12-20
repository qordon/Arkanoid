using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4_Game
{
    public partial class ArkanoidForm : Form
    {
        public Image spriteSheet;
        public Image background;
        public Image menuBackground;
        public Image gameOverMenu;

        Game game;
        int score;

        public ArkanoidForm()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ArkanoidForm_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ArkanoidForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ArkanoidForm_KeyUp);
            this.KeyPreview = true;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.timer1.Tick += new EventHandler(this.Update);

            Init();
        }

        void Init()
        {
            spriteSheet = new Bitmap("sprites/spritesheet1.png");
            this.TransparencyKey = Color.Blue;
            background = new Bitmap("sprites/background-sky.png");
            game = new Game(ref spriteSheet);

            menuBackground = new Bitmap("sprites/NewBackgroundMenu.png");
            gameOverMenu = new Bitmap("sprites/GameOver.png");

            this.timer1.Tick += new System.EventHandler(this.Update);
            this.timer1.Interval = 5;
            timer1.Start();
        }

        private void ArkanoidForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if(game.gameState == GameState.started)
            {
                g.DrawImage(background, 0, 0, new Rectangle(new Point(0, 0), new Size(1200, 720)), GraphicsUnit.Pixel);
                game.Draw(g);
            }
            else if(game.gameState == GameState.gameOver)
            {
                g.DrawImage(menuBackground, 0, 0);
                g.DrawImage(gameOverMenu, 140, 90, new Rectangle(new Point(806, 0), new Size(807, 457)), GraphicsUnit.Pixel);
                if(score < 10) g.DrawString(Convert.ToString(score), new Font("MS Reference Sans Serif", 36f), Brushes.Black, new Point(440, 220));
                else if(score < 100) g.DrawString(Convert.ToString(score), new Font("MS Reference Sans Serif", 36f), Brushes.Black, new Point(425, 220));
                else if(score > 99) g.DrawString(Convert.ToString(score), new Font("MS Reference Sans Serif", 36f), Brushes.Black, new Point(412, 220));
            }
            else if(game.gameState == GameState.gameWon)
            {
                g.DrawImage(menuBackground, 0, 0);
                g.DrawImage(gameOverMenu, 140, 90, new Rectangle(new Point(0, 458), new Size(807, 457)), GraphicsUnit.Pixel);
                if (score < 100) g.DrawString(Convert.ToString(score), new Font("MS Reference Sans Serif", 36f), Brushes.Black, new Point(420, 220));
                else if (score > 99) g.DrawString(Convert.ToString(score), new Font("MS Reference Sans Serif", 36f), Brushes.Black, new Point(400, 220));
            }
           
            
        }

        private void Update(object sender, EventArgs e)
        {
            if(game.gameState == GameState.started)
            {
                score = game.Update();
            }
            Invalidate();
            Refresh();
        }

        private void ArkanoidForm_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine(e.KeyCode);
            if (e.KeyCode == Keys.A)
            {
                game.MovePaddle(-1);
            }
            else if (e.KeyCode == Keys.D)
            {
                game.MovePaddle(1);
            }

            else if (e.KeyCode == Keys.Space)
            {
                game.isBallMoving = true;
            }
            else if (e.KeyCode == Keys.R)
            {
                game = new Game(ref spriteSheet);
            }
        }

        private void ArkanoidForm_KeyUp(object sender, KeyEventArgs e)
        {
            Console.WriteLine(e.KeyCode);
            if (e.KeyCode == Keys.A)
            {
                game.MovePaddle(0);
            }
            else if (e.KeyCode == Keys.D)
            {
                game.MovePaddle(0);
            }
        }
    }
}
