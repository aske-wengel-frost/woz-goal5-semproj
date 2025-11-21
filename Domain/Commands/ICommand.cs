/* Command interface
 */
namespace cs.Domain.Commands
{
    using cs.Domain;

    public interface ICommand
    {
        void Execute(StoryHandler StoryHandler, string command, string[] parameters);
        string GetDescription();
    }
}


