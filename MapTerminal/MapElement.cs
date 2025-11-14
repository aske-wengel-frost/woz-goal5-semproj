namespace cs.MapTerminal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    [JsonDerivedType(typeof(MapTextElement), typeDiscriminator: "text")]
    [JsonDerivedType(typeof(MapBoxElement), typeDiscriminator: "box")]
    [JsonDerivedType(typeof(MapRoomElement), typeDiscriminator: "Room")]
    public abstract class MapElement
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public ConsoleColor Color { get; set; } = ConsoleColor.DarkGray;
        public char VerticalBorderChar { get; set; }
        public char HorizontalBorderChar { get; set; }

        protected MapElement(int id, int x, int y, int height, int width)
        {
            // Set properties
            this.Id = id;
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            //this.Color = color;
            // Set defaults

            VerticalBorderChar = '|';
            HorizontalBorderChar = '=';

        }

        public abstract void InsertIntoBuffer(MapChar[,] buffer);

        public bool GuardInsert(MapChar[,] buffer)
        {
            // Gets dimensions of buffer
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
