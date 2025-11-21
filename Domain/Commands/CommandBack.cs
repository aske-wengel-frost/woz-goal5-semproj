namespace cs.Commands
{
    using cs.Domain;

    using System;

    class CommandBack : BaseCommand, ICommand
    {
        public CommandBack()
        {
            this.description = "Gå tilbage til tidligere scene (nej)";
        }

        public void Execute(StoryHandler StoryHandler, string command, string[] parameters)
        {
            // Check to see if the player can go back to the previous scene
            //bool success = StoryHandler.GoBack();
            //if (!success)
            //{
            //    // How do i keep the red coler? and not use the green coler form DrawInfo? (should i just make a new .DrawInfo?)
            //    Console.ForegroundColor = ConsoleColor.Red;
            //    Console.WriteLine("Du kan ikke gå tilbage herfra"); // Message if bool is false
            //    Console.ResetColor();
            //}
        }
    }
}