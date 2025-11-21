/* Command for exiting program
 */

namespace cs.Commands
{
    using cs.Domain;

    class CommandExit : BaseCommand, ICommand
    {
        public CommandExit()
        {
            this.description = "Forlad spillet";
        }

        public void Execute(StoryHandler StoryHandler, string command, string[] parameters)
        {
            StoryHandler.ShowEndScene();
        }
    }

}
