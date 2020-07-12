using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace kolko_krzyzyk
{
    class Grafika
    {
        private Graphics graphics;
        private int Width { get; }
        private int Height { get; }
        private int Size { get; }
        private Pen BlackPen = new Pen(Color.Black);
        public Grafika(Graphics grafika, int width, int height, int size)
        {
            graphics = grafika;
            Width = width;
            Height = height;
            Size = size;
        }

        public void RysujLinie()
        {
            int horizontal = Width / Size;
            int vertical = Height / Size;
            for(int i=horizontal; i < Width; i+=horizontal)
            {
                graphics.DrawLine(BlackPen, new Point(i, 0), new Point(i, Height));
            }
            for (int i = vertical; i < Height; i += vertical)
            {
                graphics.DrawLine(BlackPen, new Point(0, i), new Point(Width, i));
            }
        }

        public void RysujKolko(int x, int y)
        {
            int stepWidth = (Width / Size);
            int stepHeight = (Height / Size);
            int offset = 10;
            graphics.DrawEllipse(BlackPen, stepWidth * x + offset, stepHeight * y + offset, stepWidth - 2 * offset, stepHeight - 2 * offset);
        }
        public void RysujKrzyzyk(int x, int y)
        {
            int stepWidth = (Width / Size);
            int stepHeight = (Height / Size);
            int offset = 10;
            graphics.DrawLine(BlackPen, new Point(stepWidth * x + offset, stepHeight * y + offset), new Point(stepWidth * (x + 1) - offset, stepHeight * (y + 1) - offset));
            graphics.DrawLine(BlackPen, new Point(stepWidth * x + offset, stepHeight * (y + 1) - offset), new Point(stepWidth * (x + 1) - offset, stepHeight * y + offset));
        }
        public void Dispose()
        {
            BlackPen.Dispose();
            graphics.Dispose();
        }
    }
}
