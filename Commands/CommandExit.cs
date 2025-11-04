/* Command for exiting program
 */

namespace cs.Commands
{
    class CommandExit : BaseCommand, ICommand
    {
        public void Execute(StoryHandler StoryHandler, string command, string[] parameters)
        {
            StoryHandler.MakeDone();
        }
    }

}
