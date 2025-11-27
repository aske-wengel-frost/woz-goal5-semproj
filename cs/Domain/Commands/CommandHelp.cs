/* Help command
 */

namespace cs.Domain.Commands
{
    using cs.Domain;
    using cs.Domain.Story;

    class CommandHelp : BaseCommand, ICommand
    {
        Registry registry;

        public CommandHelp(Registry registry)
        {
            this.registry = registry;
            this.description = "Viser 'hj�lp' listen";
        }

        public void Execute(StoryHandler storyHandler, string command, string[] parameters)
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
            storyHandler._UI.DrawInfo("Commands:");
            foreach (String commandName in commandNames)
            {
                string description = registry.GetCommand(commandName).GetDescription();
                string lineToDraw = string.Format(" - {0,-" + max + "} " + description, commandName);

                storyHandler._UI.DrawInfo(lineToDraw);
            }
        }
    }
}
