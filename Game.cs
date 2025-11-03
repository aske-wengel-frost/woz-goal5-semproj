/* Main class for launching the game
 */

// using cs.Commands;
using cs.UI;

class Game
{

  static public StoryHandler storyHandler { get; set; }
  static public UII UIHandler { get; set; }

  // static World world = new World();
  // static StoryHandler StoryHandler = new StoryHandler(world.GetEntry());
  // static ICommand fallback = new CommandUnknown();
  // static Registry registry = new Registry(StoryHandler, fallback);

  // private static void InitRegistry()
  // {
  //   ICommand cmdExit = new CommandExit();
  //   registry.Register("exit", cmdExit);
  //   registry.Register("quit", cmdExit);
  //   registry.Register("bye", cmdExit);
  //   registry.Register("go", new CommandGo());
  //   registry.Register("help", new CommandHelp(registry));
  //   registry.Register("move", new CommandMove());
  // }

  static void Main(string[] args)
  {
    UIHandler = new UITerm();
    storyHandler = new StoryHandler(UIHandler);
    Console.WriteLine("Velkommen til dinmor stinker");
    storyHandler.LoadScenes();
    storyHandler.Start();
    while(true)
    {
      string? input = UIHandler.GetUserInput();
      UIHandler.ClearUI();
      storyHandler.PerformChoice(input);
      
    }
  //   StoryHandler.Start();

  //   // A welcome message is printed to the console
  //   Console.WriteLine("Welcome to the World of Zuul!");

  //   // We call the InitRegistry method
  //   InitRegistry();

  //   StoryHandler.GetCurrent().Welcome();

  //   while (StoryHandler.IsDone() == false)
  //   {
  //     Console.Write("> ");
  //     string? line = Console.ReadLine();
  //     if (line != null) registry.Dispatch(line);
  //   }
  //   Console.WriteLine("Game Over 😥");
  }
}
