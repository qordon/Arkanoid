using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4_Game
{
    class Paddle : EntityDrawing
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

        public int paddleWidth;
        public int paddleHeight;

        public Paddle(ref Image spriteSheet) : base(121, 20, 0, 86, ref spriteSheet)
        {
            rect.upperX = 300;
            rect.upperY = 520;
            this.paddleHeight = 20;
            this.paddleWidth = 120;
            rect.lowerX = rect.upperX + paddleWidth;
            rect.lowerY = rect.upperY + paddleHeight;


        }

        public void Move(int direction)
        {
            Console.WriteLine(posX);
            if (!(posX <= 8))
            {
                if (direction == -1)
                {
                    rect.upperX += -13.5f;
                    rect.lowerX = rect.upperX + paddleWidth;

                    /*posX += -6.5f;*/
                }
            }
            if (!(posX >= 790 - paddleWidth))
            {
                if (direction == 1)
                {
                    rect.upperX += +13.5f;
                    rect.lowerX = rect.upperX + paddleWidth;

                    /*posX += 6.5f;*/
                }
            }
        }

        public override void Draw(Graphics g)
        {
            g.DrawImage(this.spriteSheet, posX, posY, this.basis, GraphicsUnit.Pixel);
        }
    }
}
