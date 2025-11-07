namespace cs.Commands
{
    using System;

    class CommandBack : BaseCommand, ICommand
    {
        public CommandBack()
        {
            this.description = "Gå tilbage til tidligere scene";
        }

        public void Execute(StoryHandler StoryHandler, string command, string[] parameters)
        {
            // Check to see if the player can go back to the previous scene
            bool success = StoryHandler.GoBack();
            if (!success)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Du kan ikke gå tilbage herfra"); // Message if bool is false
                Console.ResetColor();
            }
        }
    }
}