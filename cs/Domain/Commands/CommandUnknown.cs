/* Fallback for when a command is not implemented
 */
namespace cs.Domain.Commands
{
    using cs.Domain.Story;

    using System;

    class CommandUnknown : BaseCommand, ICommand
    {
        public void Execute(StoryHandler storyHandler, string command, string[] parameters)
        {
            storyHandler._UIHandler.DrawError("Woopsie, forstår ikke '" + command + "' 😕");
        }
    }
}
