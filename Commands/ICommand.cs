/* Command interface
 */

interface ICommand {
  void Execute (StoryHandler StoryHandler, string command, string[] parameters);
  string GetDescription ();
}

