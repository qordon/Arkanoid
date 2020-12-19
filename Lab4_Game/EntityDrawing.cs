using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_Game
{
    public abstract class EntityDrawing
    {
        protected int frameWidth;
        protected int frameHeight;

        protected int framePosX;
        protected int framePosY;

        protected Rectangle basis;

        protected Image spriteSheet;

        public EntityDrawing(int frameWidth, int frameHeight, int framePosX, int framePosY, ref Image spriteSheet)
        {
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.framePosX = framePosX;
            this.framePosY = framePosY;
            this.spriteSheet = spriteSheet;

            basis = new Rectangle(new Point(framePosX, framePosY), new Size(frameWidth, frameHeight));
        }

        public abstract void Draw(Graphics g);
    }
}
