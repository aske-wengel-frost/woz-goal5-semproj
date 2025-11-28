namespace cs.Domain.Commands
{
    using cs.Domain;
    using cs.Domain.Story;

    /// <summary>
    /// CommandHelp class which implements the ICommand interface
    /// and provides help information about available commands to the user.
    /// </summary>
    class CommandHelp : BaseCommand, ICommand
    {
        Registry registry;

        public CommandHelp(Registry registry)
        {
            this.registry = registry;
            this.description = "Viser 'hjælp' listen";
        }

        // summary>
        // Executes the help-command and displays a list of available commands
        // for the user along with their descriptions.
        // /summary>
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
