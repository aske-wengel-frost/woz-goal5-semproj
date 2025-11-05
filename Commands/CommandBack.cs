namespace cs.Commands
{
    using System;

    class CommandBack : BaseCommand, ICommand
    {
        public CommandBack()
        {
            this.description = "Go back to the previous scene";
        }

        public void Execute(StoryHandler StoryHandler, string command, string[] parameters)
        {
            // Implement logic to go back to the previous scene
            // Note to self: Might want to access StoryHandler's scene history
        }
    }
}