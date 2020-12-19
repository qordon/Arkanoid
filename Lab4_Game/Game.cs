using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_Game
{
    enum GameState
    {
        started,
        gameWon,
        gameOver
    }
    struct MyRectangle
    {
        public float upperX;
        public float upperY;

        public float lowerX;
        public float lowerY;
    }
    class Game
    {
        private Paddle paddle;
        private Ball ball;
        private List<Brick> bricks;

        private int mapWidth = 9;
        private int mapHeight = 11;

        private string[] map =
        {
            "sssssssss",
            "         ",
            "  s      ",
            "  s      ",
            "sssssss  ",
            "         ",
            "         ",
            "         ",
            "         ",
            "         ",
            "  s      "
        };

        public int score;
        public int life = 2;

        public bool isBallMoving = false;

        public GameState gameState;

        public Game(ref Image spriteSheet)
        {
            paddle = new Paddle(ref spriteSheet);
            ball = new Ball(ref spriteSheet);
            bricks = new List<Brick>();

            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    if (map[i][j] == 's')
                    {
                        bricks.Add(new Brick(ref spriteSheet, 25 + j * 80, 10 + i * 30));
                        
                    }
                        
                }
            }
        }

        public void MovePaddle(int direction)
        {
            paddle.Move(direction);
        }

        public void Draw(Graphics g)
        {
            paddle.Draw(g);
            ball.Draw(g);

            foreach(var brick in bricks)
            {
                brick.Draw(g);
            }

            g.DrawString("Score\n", new Font("Arial", 18f), Brushes.Black, new Point(800, 100));
            if (score < 10) g.DrawString(Convert.ToString(score), new Font("Arial", 18f), Brushes.Black, new Point(825, 130));
            else if (score < 100) g.DrawString(Convert.ToString(score), new Font("Arial", 18f), Brushes.Black, new Point(817, 130));
            else if (score > 99) g.DrawString(Convert.ToString(score), new Font("Arial", 18f), Brushes.Black, new Point(810, 130));

            /*g.DrawString(Convert.ToString(score), new Font("Arial", 18f), Brushes.Black, new Point(820, 130));*/

            g.DrawString("Life", new Font("Arial", 18f), Brushes.Black, new Point(810, 300));
            g.DrawString(Convert.ToString(life), new Font("Arial", 18f), Brushes.Black, new Point(820, 330));
        }

        public int Update()
        {
            if (!isBallMoving)
            {
                ball.MoveWhenGameNotStart(paddle.rect.upperX + paddle.paddleWidth / 2, paddle.rect.upperY);
            }
            else
            {
                if (ball.MoveReturnTrueIfGoOut())
                {
                    isBallMoving = false;
                    life--;
                    if (life == -1)
                    {
                        gameState = GameState.gameOver;
                        return score;
                    }
                    
                }
                if (IsCollide(ball.rect, paddle.rect))
                {
                    ball.ChangeDirectionByCollideWithPaddle(ref paddle);
                }

                for (int i = 0; i < bricks.Count; i++)
                {
                    if (IsCollide(ball.rect, bricks[i].rect))
                    {
                        ball.ChangeDirectionByCollideWithBrick(bricks[i]);
                        score += 10;
                        bricks.RemoveAt(i);

                        if (bricks.Count() == 0)
                        {
                            gameState = GameState.gameWon;
                            return score;
                        }
                        break;
                    }
                }

                
            }

            gameState = GameState.started;
            return score;
        }

        public bool IsCollide(MyRectangle a, MyRectangle b)
        {
            return (
                (
                  (
                    (a.upperX >= b.upperX && a.upperX <= b.lowerX) || (a.lowerX >= b.upperX && a.lowerX <= b.lowerX)
                  ) && (
                    (a.upperY >= b.upperY && a.upperY <= b.lowerY) || (a.lowerY >= b.upperY && a.lowerY <= b.lowerY)
                  )
                ) || (
                  (
                    (b.upperX >= a.upperX && b.upperX <= a.lowerX) || (b.lowerX >= a.upperX && b.lowerX <= a.lowerX)
                  ) && (
                    (b.upperY >= a.upperY && b.upperY <= a.lowerY) || (b.lowerY >= a.upperY && b.lowerY <= a.lowerY)
                  )
                )
              ) || (
                (
                  (
                    (a.upperX >= b.upperX && a.upperX <= b.lowerX) || (a.lowerX >= b.upperX && a.lowerX <= b.lowerX)
                  ) && (
                    (b.upperY >= a.upperY && b.upperY <= a.lowerY) || (b.lowerY >= a.upperY && b.lowerY <= a.lowerY)
                  )
                ) || (
                  (
                    (b.upperX >= a.upperX && b.upperX <= a.lowerX) || (b.lowerX >= a.upperX && b.lowerX <= a.lowerX)
                  ) && (
                    (a.upperY >= b.upperY && a.upperY <= b.lowerY) || (a.lowerY >= b.upperY && a.lowerY <= b.lowerY)
                  )
                )
              );
        }
    }
}
