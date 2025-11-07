namespace cs.Commands
{
    using System;

    class CommandTake : BaseCommand, ICommand
    {
        public CommandTake()
        {
            this.description = "Take the viewed item(s)";
        }

        public void Execute(StoryHandler StoryHandler, string command, string[] parameters)
        {
            // Method to take the viewed item in the current scene


        }
    }
}