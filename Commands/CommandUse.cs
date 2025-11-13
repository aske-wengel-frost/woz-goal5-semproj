namespace cs.Commands
{
    using System;

    class CommandUse : BaseCommand, ICommand
    {
        public CommandUse()
        {
            this.description = "Brug et item(s)";
        }

        public void Execute(StoryHandler StoryHandler, string command, string[] parameters)
        {
            // Method to use item in the current scene

        }
    }
}