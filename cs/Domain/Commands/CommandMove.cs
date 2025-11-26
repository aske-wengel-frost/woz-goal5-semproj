namespace cs.Domain.Commands
{
    using cs.Domain.Story;

    using System;

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
                storyHandler._UIHandler.DrawError("For mange argumenter!");
                return;

            }
            storyHandler.PerformChoice(parameters[0]);
        }
    }
}
