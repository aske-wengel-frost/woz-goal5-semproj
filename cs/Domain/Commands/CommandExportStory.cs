namespace cs.Domain.Commands
{
    using cs.Domain.Story;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CommandExportStory : BaseCommand, ICommand
    {
        public void Execute(StoryHandler storyHandler, string command, string[] parameters)
        {
            //StoryHandler._Data.ExportStoryToFile();
        }

        public string GetDescription()
        {
            return "Eksporterer den aktuelt indlæste historie til en JSON-fil. Første argument er filnavnet.";
        }
    }
}
