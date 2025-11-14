namespace cs.MapTerminal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TerminalMap
    {
        public int Height { get; set; } = 24;
        public int Width { get; set; } = 68;

        public char[,] Buffer { get; set; }
        public List<MapElement> Elements;

        public TerminalMap()
        {
            // Init list of MapElements
            Elements = new List<MapElement>();

            // Init size of drawbuffer, make it 2 bigger in both width and height to accomidate borders.
            Buffer = new char[Height, Width];

            initBuffer();
        }

        public void initBuffer()
        {
            // populate array with chars
            for (int i = 0; i < Buffer.GetLength(0); i++)
            {
                for (int j = 0; j < Buffer.GetLength(1); j++)
                {
                    Buffer[i, j] = ' ';
                }
            }

            // Add the border
            this.AddElement(new MapBoxElement(0, 0, this.Height, this.Width));
            // Add the title in the center of the top border
            string titleText = "[ Kort ]";
            int xStartCord = this.Width / 2 - titleText.Length / 2;
            this.AddElement(new MapTextElement(xStartCord, 0, titleText));


        }

        public void RefreshBuffer()
        {
            // Populates the buffer with elements from elements list
            foreach (MapElement element in Elements)
            {
                // Insert the area into the  buffer
                element.InsertIntoBuffer(this.Buffer);
            }
        }

        public void DrawMap()
        {
            // loop over buffer and print
            for (int row = 0; row < Buffer.GetLength(0); row++)
            {
                // Draw the area on the map.
                for (int col = 0; col < Buffer.GetLength(1); col++)
                {
                    Console.Write(Buffer[row, col]);
                }

                Console.WriteLine();

            }
        }

        public void AddElement(MapElement element)
        {
            this.Elements.Add(element);
            this.RefreshBuffer();
        }

    }
}
