/* Command for transitioning between spaces
 */

namespace cs.Commands 
{

    class CommandGo : BaseCommand, ICommand
    {
        public CommandGo()
        {
            description = "Follow an exit";
        }

        public void Execute(StoryHandler StoryHandler, string command, string[] parameters)
        {
            if (GuardEq(parameters, 1))
            {
                Console.WriteLine("I don't seem to know where that is 🤔");
                return;
            }
            StoryHandler.Transition(parameters[0]);
        }
    }
}
