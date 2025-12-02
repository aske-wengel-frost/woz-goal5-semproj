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
                storyHandler._UI.DrawError("For mange argumenter!");
                return;

            }
            storyHandler.PerformChoice(parameters[0]);
        }
    }
}
