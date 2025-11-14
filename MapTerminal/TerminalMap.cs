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

        public MapChar[,] Buffer { get; set; }
        
        private List<MapElement> elements;
        public List<MapElement> Elements
        {
            get { return elements;}
            set { elements = value; this.RefreshBuffer();}
        }

        public TerminalMap()
        {
            // Init list of MapElements
            Elements = new List<MapElement>();

            // Init size of drawbuffer, make it 2 bigger in both width and height to accomidate borders.
            Buffer = new MapChar[Height, Width];

            initBuffer();
        }

        public void initBuffer()
        {
            // populate array with chars
            for (int i = 0; i < Buffer.GetLength(0); i++)
            {
                for (int j = 0; j < Buffer.GetLength(1); j++)
                {
                    // sets default color to white for all chars in buffer
                    Buffer[i, j] = new MapChar(' ', ConsoleColor.White);
                }
            }

            // Add the border
            this.AddElement(new MapBoxElement(0, 0, 0, this.Height, this.Width) { Color = ConsoleColor.White});
            // Add the title in the center of the top border
            string titleText = "[ Kort ]";
            int xStartCord = this.Width / 2 - titleText.Length / 2;
            this.AddElement(new MapTextElement(0, xStartCord, 0, titleText) { Color = ConsoleColor.White });


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
                    MapChar mapChar = Buffer[row, col];

                    // Need to add some color defining stuff here
                    Console.ForegroundColor = mapChar.Color;            
                    Console.Write(mapChar.Char);
                    
                }

                Console.WriteLine();

            }
            Console.ResetColor();
        }

        public void AddElement(MapElement element)
        {
            this.Elements.Add(element);
            this.RefreshBuffer();
        }

        public void HighlightElement(int ID, ConsoleColor highlightColor = ConsoleColor.Red)
        {
            // Make sure the element we want highlighted is last in the list as this will force it to be drawn last. and thereby on top of all other elements
            MapElement? element = Elements.Find(x => x.Id == ID);

            if(element != null)
            {
                // Set the color of the element to the desired highlighted color
                element.Color = highlightColor;
                // Removes it from its current location in the list
                Elements.Remove(element);
                // Appends it to the end of the list
                Elements.Add(element);
            }

            this.RefreshBuffer();
        }

    }
}
