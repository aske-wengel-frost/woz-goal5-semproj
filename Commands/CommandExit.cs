/* Command for exiting program
 */

namespace cs.Commands
{
    
    class CommandExit : BaseCommand, ICommand
    {
        public CommandExit()
        {
            this.description = "Exit spillet";
        }

        public void Execute(StoryHandler StoryHandler, string command, string[] parameters)
        {
            StoryHandler.MakeDone();
        }
    }

}
