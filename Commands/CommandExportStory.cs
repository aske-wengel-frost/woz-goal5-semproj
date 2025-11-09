namespace cs.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CommandExportStory : BaseCommand, ICommand
    {
        public void Execute(StoryHandler StoryHandler, string command, string[] parameters)
        {
            StoryHandler.StoryBuilder.ExportScenesToFile();
        }

        public string GetDescription()
        {
            return "Exports the currently loaded story to json file, First argument is filename";
        }
    }
}
