namespace cs.Commands
{
    using cs.MapTerminal;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CommandMap : BaseCommand, ICommand
    {
        public CommandMap()
        {
             this.description = "Viser kortet";
        }

        public void Execute(StoryHandler StoryHandler, string command, string[] parameters)
        {
            TerminalMap tm = new TerminalMap();

            //// test areas:
            //Dictionary<int, Area> testAreas = new Dictionary<int, Area>
            //{
            //    {1, new Area(1, "Entré"){yStart = 2, xStart = 2, width = 10, height = 12 }},
            //    {2, new Area(2, "Værelse 1"){yStart = 2, xStart = 11, width = 20, height = 8 }},
            //    {3, new Area(3, "Gang"){yStart = 9, xStart = 11, width = 50, height = 5 }},
            //    {4, new Area(4, "Værelse 2"){yStart = 2, xStart = 30, width = 20, height = 8 }},
            //    {5, new Area(5, "Køkken alrum"){yStart = 13, xStart = 2, width = 40, height = 9 }},
            //    {6, new Area(6, "Toilet"){yStart = 2, xStart = 49, width = 12, height = 8 }},
            //    //{7, new Area(6, "Diller rummet"){yStart = 100, xStart = 100, width = 10, height = 8 }},

            //};

            List<MapElement> MapElements = new List<MapElement>
            {
                new MapRoomElement(1, 2, 2, 12, 10, "Entré"),
                new MapRoomElement(2, 11, 2, 8, 20, "Værelse 1"),
                new MapRoomElement(3, 11, 9, 5, 50, "Gang"),
                new MapRoomElement(4, 30, 2, 8, 20, "Værelse 2"),
                new MapRoomElement(5, 2, 13, 9, 40, "Køkken alrum"),
                new MapRoomElement(6, 49, 2, 8, 12, "Toilet"),
                new MapTextElement(7, 40, 10, "Cock and balls :3") {Color = ConsoleColor.Cyan}
            };

            // Adds elements to buffer and refreshes buffer
            tm.Elements = MapElements;

            tm.HighlightElement(4, ConsoleColor.White);

            tm.DrawMap();

            //StoryHandler._UIHandler.RefreshMap(testAreas);
        }
    }
}
