namespace cs.Presentation.MapTerminal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using static System.Net.Mime.MediaTypeNames;

    public class MapTextElement : MapElement
    {
        public string Text { get; set; }

        // Width is set to the length of the text and height is set to 1.
        public MapTextElement(int id, int x, int y, string text ) : base(id, x, y, 1, text.Length)
        {
            Text = text;
        }

        public override void InsertIntoBuffer(MapChar[,] buffer)
        {
            if (this.GuardInsert(buffer))
            {
                return;
            }

            for (int ch = 0; ch < Text.Length; ch++)
            {
                MapChar mapChar = buffer[Y, X + ch];

                mapChar.Char = Text[ch];
                mapChar.Color = this.Color;
            }
        }
    }
}
