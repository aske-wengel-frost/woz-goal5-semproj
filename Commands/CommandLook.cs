namespace cs.Commands
{
    using System;

    class CommandLook : BaseCommand, ICommand
    {
        public CommandLook()
        {
             this.description = "Look around in the current scene";
        }

        public void Execute(StoryHandler StoryHandler, string command, string[] parameters)
        {
            // Implementing logic to look around in the current scene
            // Note Self: Access StoryHandler's current scene details
        }
    }
}