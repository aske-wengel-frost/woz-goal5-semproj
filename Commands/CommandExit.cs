/* Command for exiting program
 */

class CommandExit : BaseCommand, ICommand {
  public void Execute (StoryHandler StoryHandler, string command, string[] parameters) {
    StoryHandler.MakeDone();
  }
}
