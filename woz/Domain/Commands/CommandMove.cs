namespace woz.Domain.Commands
{
    using woz.Domain.Story;

    using System;

    /// <summary>
    /// Command to move to another scene by specifying the scene number.
    /// </summary> 
    class CommandMove : BaseCommand, ICommand
    {
        public CommandMove()
        {
            this.description = "Bevæge til en anden scene ved at skrive scene nummer";
        }

        public void Execute(StoryHandler storyHandler, string command, string[] parameters)
        {
            if (GuardEq(parameters, 1))
            {
                // We dont have 1 parameter!
                storyHandler.UI.DrawError("For mange argumenter!");
                return;

            }

            // If user input cannot be converted to int
            bool isConverted = Int32.TryParse(parameters[0], out int usrInpValue);
            if (!isConverted)
            {
                storyHandler.UI.DrawError("Ikke validt input!");
                return;
            }

            storyHandler.PerformChoice(usrInpValue);
        }
    }
}
