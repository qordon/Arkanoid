using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Lab4_Game
{
    class Brick : EntityDrawing
    {
        private float posX;
        private float posY;

        public MyRectangle rect;

        public int brickWidth;
        public int brickHeight;

        public Brick(ref Image spriteSheet, int posX, int posY) : base(92, 22, 183, 0, ref spriteSheet)
        {
            this.posX = posX;
            this.posY = posY;

            this.brickWidth = 88;
            this.brickHeight = 18;

            rect.upperX = posX;
            rect.upperY = posY;
            rect.lowerX = rect.upperX + brickWidth;
            rect.lowerY = rect.upperY + brickHeight;
        }

        public override void Draw(Graphics g)
        {
            g.DrawImage(spriteSheet, posX, posY, this.basis, GraphicsUnit.Pixel);
        }
    }
}


