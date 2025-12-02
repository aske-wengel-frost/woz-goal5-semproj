/* Fallback for when a command is not implemented
 */
namespace woz.Domain.Commands
{
    using woz.Domain.Story;

    using System;

    /// <summary>
    /// A simple command that notifies the player if they give/type an unknown command.
    /// </summary>
    class CommandUnknown : BaseCommand, ICommand
    {
        public void Execute(StoryHandler storyHandler, string command, string[] parameters)
        {
            // Draws this message to the UI when an unknown command is given
            storyHandler._UI.DrawError("Woopsie, forstår ikke '" + command + "' 😕");
        }
    }
}
