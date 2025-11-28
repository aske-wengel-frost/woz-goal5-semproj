/* Command interface
 */
namespace cs.Domain.Commands
{
    using cs.Domain.Story;

    /// <summary>
    /// The Command interface that defines the structure for all command classes.
    /// All command classes must implement the Execute method to perform their specific actions
    /// </summary>
    public interface ICommand
    {
        void Execute(StoryHandler storyHandler, string command, string[] parameters);
        string GetDescription();
    }
}


