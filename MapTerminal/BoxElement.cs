namespace cs.MapTerminal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BoxElement : MapElement
    {
        public BoxElement(int x, int y, int height, int width) : base(x, y, height, width)
        {
        }

        public override void InsertIntoBuffer(char[,] buffer)
        {
            // if the box exceeds the size of the buffer, return.
            if (this.GuardInsert(buffer))
            {
                return;
            }

            for (int i = this.Y; i < this.Height + this.Y; i++)
            {
                // if it is the top or bottom line we draw
                if (i == this.Y || i == this.Y + this.Height - 1)
                {
                    for (int j = this.X; j < this.Width + this.X; j++)
                    {
                        buffer[i, j] = '=';
                    }
                }
                else
                {
                    // else we draw the sides:
                    buffer[i, X] = '|';
                    buffer[i, X + Width - 1] = '|';
                }
            }
        }
    }
}
