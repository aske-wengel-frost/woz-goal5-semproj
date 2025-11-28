namespace cs.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Frame
    {
        public int X { get; set; }
        public int Y { get; set;}
        public int Width { get; set; }
        public int Height { get; set; }

        public Frame(int x, int y, int height, int width)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }
}
