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
    class Ball : EntityDrawing
    {
        public MyRectangle rect;
        public float posX
        {
            get
            {
                return rect.upperX;
            }
            set
            {
                rect.upperX = value;
            }
        }
        public float posY
        {
            get
            {
                return rect.upperY;
            }
            set
            {
                rect.upperY = value;
            }
        }

        private int ballSize;

        public float dx = 6.8f;
        public float dy = -6.8f;

        public Ball(ref Image spriteSheet) : base(30, 30, 276, 73, ref spriteSheet)
        {
            rect.upperX = 300;
            rect.upperY = 300;

            this.ballSize = 24;

            rect.lowerX = rect.upperX + ballSize;
            rect.lowerY = rect.upperY + ballSize;


        }

        public override void Draw(Graphics g)
        {
            g.DrawImage(this.spriteSheet, posX, posY, this.basis, GraphicsUnit.Pixel);
        }

        public bool MoveReturnTrueIfGoOut()
        {

            if (posX > 770 - this.ballSize || posX < 0)
            {
                dx = -dx;
            }
            else if (posY < 0)
            {
                dy = -dy;
            }
            else if (posY > 550)
            {
                dx = 6.8f;
                dy = -6.8f;
                return true;
                
            }

            rect.upperX += dx;
            rect.upperY += dy;
            rect.lowerX = rect.upperX + ballSize;
            rect.lowerY = rect.upperY + ballSize;

            return false;
        }

        public void MoveWhenGameNotStart(float x, float y)
        {
            /*Числа после выражения непонятно откуда взявшаяся корреляция*/
            rect.upperX = x - ballSize;
            rect.upperY = y - ballSize;
            rect.lowerX = rect.upperX + ballSize;
            rect.lowerY = rect.upperY + ballSize;
        }

        public void ChangeDirectionByCollideWithBrick(Brick brick)
        {
            float cP = brick.rect.upperX + (brick.brickWidth / 2);
            float cB = this.rect.upperX + (this.ballSize / 2);

            float r = (cP - cB);
            if (Math.Abs(r) > brick.brickWidth / 2)
            {
                dx = -dx;
            }
            else
            {
                dy = -dy;
            }
        }

        public void ChangeDirectionByCollideWithPaddle(ref Paddle paddle)
        {
            float cP = paddle.rect.upperX + (paddle.paddleWidth / 2);
            float cB = this.rect.upperX + (this.ballSize / 2);

            float r = (cP - cB);
            float dev = 0;

            if (Math.Abs(r) > paddle.paddleWidth / 2)
            {
                dx = -dx;
            }
            else
            {
                /*Слагаемое перед - это немного больше квадрата множителя во 2-ой строке*/
                dev = Math.Abs(r / (paddle.paddleWidth / 2));
                dev *= 6.9f;
                dy = (float)(-1 * Math.Sqrt(49 - Math.Pow(dev, 2)));
                if (r > 0) dx = -dev;
                else dx = dev;
            }
        }
    }
}
