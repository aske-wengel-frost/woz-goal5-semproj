/* Command interface
 */
namespace cs.Domain.Commands
{
    using cs.Domain.Story;

    public interface ICommand
    {
        void Execute(StoryHandler storyHandler, string command, string[] parameters);
        string GetDescription();
    }
}


