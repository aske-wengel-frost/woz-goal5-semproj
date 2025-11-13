namespace cs.Commands
{
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
            // test areas:
            Dictionary<int, Area> testAreas = new Dictionary<int, Area>
            {
                {1, new Area(1, "Entré"){yStart = 2, xStart = 2, width = 10, height = 12 }},
                {2, new Area(2, "Værelse 1"){yStart = 2, xStart = 11, width = 20, height = 8 }},
                {3, new Area(3, "Gang"){yStart = 9, xStart = 11, width = 50, height = 5 }},
                {4, new Area(4, "Værelse 2"){yStart = 2, xStart = 30, width = 20, height = 8 }},
                {5, new Area(5, "Køkken alrum"){yStart = 13, xStart = 2, width = 40, height = 9 }},
                {6, new Area(6, "Toilet"){yStart = 2, xStart = 49, width = 12, height = 8 }},

            };

            //{ 4, new Area(4, "test4") { yStart = 2, xStart = 40, width = 20, height = 10 }},
            //    { 5, new Area(5, "test5") { yStart = 20, xStart = 40, width = 30, height = 4 }},
            //    { 7, new Area(7, "test6") { yStart = 0, xStart = 0, width = 5, height = 5 }},
            //    { 8, new Area(8, "test7") { yStart = 30, xStart = 95, width = 5, height = 5 }}


            StoryHandler._UIHandler.RefreshMap(testAreas);
        }
    }
}
