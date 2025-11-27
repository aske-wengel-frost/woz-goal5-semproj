namespace cs.Domain.Commands
{
    using cs.Domain.Story;
    using System;


    //summary
    // This command was only made as an idea for the player to go back to a previous scene.
    // It's not implemented in the game but mearly here as a placeholder for future development.
    // /summary
    class CommandBack : BaseCommand, ICommand
    {
        public CommandBack()
        {
            this.description = "Gå tilbage til tidligere scene (nej)";
        }

        public void Execute(StoryHandler storyHandler, string command, string[] parameters)
        {
            // Check to see if the player can go back to the previous scene
            //bool success = StoryHandler.GoBack();
            //if (!success)
            //{
            //    // How do i keep the red coler? and not use the green coler form DrawInfo? (should i just make a new .DrawInfo?)
            //    Console.ForegroundColor = ConsoleColor.Red;
            //    Console.WriteLine("Du kan ikke g� tilbage herfra"); // Message if bool is false
            //    Console.ResetColor();
            //}
        }
    }
}