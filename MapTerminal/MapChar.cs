namespace cs.MapTerminal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MapChar
    {
        public char Char { get; set; }
        public ConsoleColor Color { get; set; }

        public MapChar(char _char)
        {
            Char = _char;
            Color = ConsoleColor.White;
        }

        public MapChar(char _char, ConsoleColor _consoleColor)
        {
            Char = _char;
            Color = _consoleColor;
        }
    }
}
