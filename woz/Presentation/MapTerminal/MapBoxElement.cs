namespace woz.Presentation.MapTerminal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MapBoxElement : MapElement
    {
        public MapBoxElement(int id, int x, int y, int height, int width) : base(id, x, y, height, width)
        {
        }

        public override void InsertIntoBuffer(MapChar[,] buffer)
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
                        MapChar mapChar = buffer[i, j];

                        mapChar.Char = '=';
                        mapChar.Color = this.Color;
                    }
                }
                else
                {
                    // else we draw the sides:
                    MapChar mapCharLeft = buffer[i, X];
                    MapChar mapCharRight = buffer[i, X + Width - 1];
                    
                    mapCharLeft.Char = '|';
                    mapCharLeft.Color = this.Color;

                    mapCharRight.Char = '|';
                    mapCharRight.Color = this.Color;
                }
            }
        }
    }
}
