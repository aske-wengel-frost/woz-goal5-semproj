/* Command for exiting program
 */

namespace cs.Domain.Commands
{
    using cs.Domain.Story;

    class CommandExit : BaseCommand, ICommand
    {
        public CommandExit()
        {
            this.description = "Forlad spillet";
        }

        public void Execute(StoryHandler storyHandler, string command, string[] parameters)
        {
            storyHandler.ShowEndScene();
        }
    }

}
