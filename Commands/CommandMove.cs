namespace cs.Commands
{
    using System;

    class CommandMove : BaseCommand, ICommand
    {
        public CommandMove()
        {
            this.description = "Move to a different scene by entering the scene number";
        }

        public void Execute(StoryHandler StoryHandler, string command, string[] parameters)
        {
                        if (GuardEq(parameters, 1))
            {
                // We dont have 1 parameter!
                Console.WriteLine("For mange argumenter!");
                return;

            }
            else
            {
                StoryHandler.PerformChoice(parameters[0]);
            }
        }
    }
}
