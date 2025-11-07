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
            // Check to see if the player can go back to the previous scene
            bool success = StoryHandler.GoBack();
            if (!success)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You can't go back from this area."); // Message if bool is false
                Console.ResetColor();
            }
        }
    }
}