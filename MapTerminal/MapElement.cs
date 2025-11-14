namespace cs.MapTerminal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class MapElement
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public ConsoleColor Color { get; set; }
        public char VerticalBorderChar { get; set; }
        public char HorizontalBorderChar { get; set; }

        protected MapElement(int x, int y, int height, int width)
        {
            // Set properties
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;

            // Set defaults
            Color = ConsoleColor.White;
            VerticalBorderChar = '|';
            HorizontalBorderChar = '=';

        }

        public abstract void InsertIntoBuffer(char[,] buffer);

        public bool GuardInsert(char[,] buffer)
        {
            // Gets length of rows
            int mapHeight = buffer.GetLength(0);
            int mapWidth = buffer.GetLength(1);

            if(this.X + this.Width > mapWidth)
            {
                return true;
            }

            if(this.Y + this.Height > mapHeight)
            {
                return true;
            }

            return false;

        }

    }
}
