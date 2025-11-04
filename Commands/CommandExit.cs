/* Command for exiting program
 */

class CommandExit : BaseCommand, ICommand {
  public void Execute (StoryHandler context, string command, string[] parameters) {
    context.MakeDone();
  }
}
