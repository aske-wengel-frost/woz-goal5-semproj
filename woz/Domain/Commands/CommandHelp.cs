namespace woz.Domain.Commands
{
    using woz.Domain;
    using woz.Domain.Story;

    /// <summary>
    /// CommandHelp class provides help information about available commands to the user.
    /// </summary>
    class CommandHelp : BaseCommand, ICommand
    {
        Registry registry;

        public CommandHelp(Registry registry)
        {
            this.registry = registry;
            this.description = "Viser 'hjælp' listen";
        }

        /// <summary>
        /// Executes the help-command and displays a list of available commands
        /// for the user along with their descriptions.
        /// </summary>
        public void Execute(StoryHandler storyHandler, string command, string[] parameters)
        {
            storyHandler.UI.DrawHelp(registry.GetCommands());

        }
    }
}
