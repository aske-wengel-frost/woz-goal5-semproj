/* Fallback for when a command is not implemented
 */
namespace cs.Domain.Commands
{
    using cs.Domain;

    using System;

    class CommandUnknown : BaseCommand, ICommand
    {
        public void Execute(StoryHandler StoryHandler, string command, string[] parameters)
        {
            StoryHandler._UIHandler.DrawError("Woopsie, forstår ikke '" + command + "' 😕");
        }
    }
}
