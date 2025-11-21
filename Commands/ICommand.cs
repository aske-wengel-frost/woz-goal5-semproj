/* Command interface
 */
namespace cs.Commands
{
    using cs.Domain;

    public interface ICommand
    {
        void Execute(StoryHandler StoryHandler, string command, string[] parameters);
        string GetDescription();
    }
}


