namespace cs.Commands
{
    using cs.Domain;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CommandExportStory : BaseCommand, ICommand
    {
        public void Execute(StoryHandler StoryHandler, string command, string[] parameters)
        {
            StoryHandler.dataLoader.ExportStoryToFile();
        }

        public string GetDescription()
        {
            return "Eksporterer den aktuelt indlæste historie til en JSON-fil. Første argument er filnavnet.";
        }
    }
}
