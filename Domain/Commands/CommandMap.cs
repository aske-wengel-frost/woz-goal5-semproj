namespace cs.Domain.Commands
{
    using cs.Domain;
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
            //List<MapElement> MapElements = new List<MapElement>
            //{
            //    new MapRoomElement(1, 2, 2, 12, 10, "Entré"),
            //    new MapRoomElement(2, 11, 2, 8, 20, "Værelse 1"),
            //    new MapRoomElement(3, 11, 9, 5, 50, "Gang"),
            //    new MapRoomElement(4, 30, 2, 8, 20, "Værelse 2"),
            //    new MapRoomElement(5, 2, 13, 9, 40, "Køkken alrum"),
            //    new MapRoomElement(6, 49, 2, 8, 12, "Toilet"),
            //    new MapTextElement(7, 40, 10, "Cock and balls :3") {Color = ConsoleColor.Cyan}
            //};

            StoryHandler._UIHandler.DrawMap();
        }
    }
}
