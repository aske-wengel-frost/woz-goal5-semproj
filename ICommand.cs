/* Command interface
 */

interface ICommand {
  void Execute (StoryHandler context, string command, string[] parameters);
  string GetDescription ();
}

