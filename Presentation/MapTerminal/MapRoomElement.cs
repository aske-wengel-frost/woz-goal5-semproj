using cs.MapTerminal;

namespace cs.Presentation.MapTerminal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    public class MapRoomElement : MapElement
    {
        public string Name { get; set; }

        public MapRoomElement(int id, int x, int y, int height, int width, string name = "") : base(id, x, y, height, width)
        {
            Name = name;
        }

        public override void InsertIntoBuffer(MapChar[,] buffer)
        {
            // Get the buffers width and height
            int buffHeight = buffer.GetLength(0);
            int buffWidth= buffer.GetLength(1);

            // Inserts the box part of the room
            new MapBoxElement(this.Id, this.X, this.Y, this.Height, this.Width) {Color = this.Color }.InsertIntoBuffer(buffer);

            // Inserts the text
            // Calculate text offset
            int xOffset = this.Width / 2 - this.Name.Length / 2;
            int yOffset = this.Height / 2;
            new MapTextElement(this.Id, this.X + xOffset, this.Y + yOffset, Name) { Color = this.Color}.InsertIntoBuffer(buffer);

        }
    }
}
