/* Command interface
 */
namespace cs.Commands
{
    public interface ICommand
    {
        void Execute(StoryHandler StoryHandler, string command, string[] parameters);
        string GetDescription();
    }
}


