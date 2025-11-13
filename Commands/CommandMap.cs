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
                {1, new Area(1, "test1"){yStart = 3, xStart = 5, width = 3, height = 3 }},
                {2, new Area(2, "test2"){yStart = 8, xStart = 10, width = 7, height = 4 }},
                {3, new Area(3, "test3"){yStart = 16, xStart = 30, width = 5, height = 10 }},
                {4, new Area(4, "test3"){yStart = 2, xStart = 40, width = 20, height = 10 }},
                {5, new Area(5, "test3"){yStart = 20, xStart = 40, width = 30, height = 4 }},
                {6, new Area(6, "test3"){yStart = 30, xStart = 95, width = 5, height = 5 }}
            };


            StoryHandler._UIHandler.RefreshMap(testAreas);
        }
    }
}
