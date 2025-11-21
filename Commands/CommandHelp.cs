/* Help command
 */

namespace cs.Commands
{
    using cs.Domain;

    class CommandHelp : BaseCommand, ICommand
    {
        Registry registry;

        public CommandHelp(Registry registry)
        {
            this.registry = registry;
            this.description = "Viser 'hjælp' listen";
        }

        public void Execute(StoryHandler StoryHandler, string command, string[] parameters)
        {
            string[] commandNames = registry.GetCommandNames();
            Array.Sort(commandNames);

            // find max length of command name
            int max = 0;
            foreach (String commandName in commandNames)
            {
                int length = commandName.Length;
                if (length > max) max = length;
            }

            // present list of commands
            StoryHandler._UIHandler.DrawInfo("Commands:");
            foreach (String commandName in commandNames)
            {
                string description = registry.GetCommand(commandName).GetDescription();
                string lineToDraw = string.Format(" - {0,-" + max + "} " + description, commandName);

                StoryHandler._UIHandler.DrawInfo(lineToDraw);
            }
        }
    }
}
