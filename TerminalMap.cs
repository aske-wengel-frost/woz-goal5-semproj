namespace cs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TerminalMap
    {
        public int Height { get; set; } = 24;

        public int Width { get; set; } = 68;

        public char[,] DrawBuffer { get; set; }


        public TerminalMap()
        {
            // Init size of drawbuffer, make it 2 bigger in both width and height to accomidate borders.
            DrawBuffer = new char[Height, Width];

            this.initBuffer();
        }

        public void initBuffer()
        {
            // populate array with chars
            for (int i = 0; i < DrawBuffer.GetLength(0); i++)
            {
                for (int j = 0; j < DrawBuffer.GetLength(1); j++)
                {
                    DrawBuffer[i, j] = ' ';
                }
            }


            this.InsertBox(0, 0, Height, Width);

            // Insert the header:
            string headerText = "[ Map ]";
            int headerStartXpos = Width / 2 - headerText.Length / 2;
            this.InsertText(headerStartXpos, 0, headerText);

        }

        public void DrawMap(Dictionary<int, Area> areas)
        {
            foreach (Area area in areas.Values)
            {
                // Inserts a box into the buffer
                //InsertBox(area.xStart, area.yStart, area.height, area.width);
                InsertArea(area.xStart, area.yStart, area.height, area.width, area.Name);
            }
            this.InsertText(94, 34, "Helloo");

            // loop over all areas and print map
            for (int row = 0; row < DrawBuffer.GetLength(0); row++)
            {
                // Draw the area on the map.
                for (int col = 0; col < DrawBuffer.GetLength(1); col++)
                {
                    Console.Write(DrawBuffer[row, col]);
                }

                Console.WriteLine();

            }
            

        }

        private void InsertBox(int X, int Y, int Height, int Width)
        {
            // if the box exceedsa the size of the buffer, return.
            if(GuardSize(X, Y, Height, Width))
            {
                return;
            }

            for (int i = Y; i < Height + Y; i++)
            {
                // if it is the top or bottom line we draw
                if (i == Y || i == Y + Height - 1)
                {
                    for (int j = X; j < Width + X; j++)
                    {
                        this.DrawBuffer[i, j] = '=';
                    }
                }
                else
                {
                    // else we draw the sides:
                    this.DrawBuffer[i, X] = '|';
                    this.DrawBuffer[i, X + Width - 1] = '|';
                }
            }

        }

        private void InsertArea(int X, int Y, int Height, int Width, string name = " ")
        {
            this.InsertBox(X, Y, Height, Width);

            // Calculate text offset
            int xOffset = Width / 2 - name.Length / 2;
            int yOffset = Height / 2;

            this.InsertText(X + xOffset, Y + yOffset, name);
        }

        private void InsertText(int X, int Y, string text)
        {
            if (GuardSize(X, Y, 1, text.Length))
            {
                return;
            }

            for (int ch = 0; ch < text.Length; ch++)
            {
                this.DrawBuffer[Y, X + ch] = text[ch];
            }
        }

        /// <summary>
        /// Makes sure that nothing drawn to the map exceeds the size of the 2d array.
        /// </summary>
        /// <param name="x">The start x coordinate</param>
        /// <param name="y">The start y coordinate</param>
        /// <param name="height">the height of the object</param>
        /// <param name="width"> the width of the object</param>
        /// <returns></returns>
        private bool GuardSize(int x, int y, int height, int width)
        {
            if(x + width > Width)
            {
                return true;
            }

            if(y + height > Height)
            {
                return true;
            }

            return false;
        }
    }
}
