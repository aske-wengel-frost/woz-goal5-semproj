/* Main class for launching the game
 */

class Game {
  static World    world    = new World();
  static StoryHandler  StoryHandler  = new StoryHandler(world.GetEntry());
  static ICommand fallback = new CommandUnknown();
  static Registry registry = new Registry(StoryHandler, fallback);
  
  private static void InitRegistry () {
    ICommand cmdExit = new CommandExit();
    registry.Register("exit", cmdExit);
    registry.Register("quit", cmdExit);
    registry.Register("bye", cmdExit);
    registry.Register("go", new CommandGo());
    registry.Register("help", new CommandHelp(registry));
  }
  
  static void Main (string[] args) {
    
    StoryHandler.Start();

    // A welcome message is printed to the console
    Console.WriteLine("Welcome to the World of Zuul!");
    
    // We call the InitRegistry method
    InitRegistry();

    StoryHandler.GetCurrent().Welcome();
    
    while (StoryHandler.IsDone()==false) {
        Console.Write("> ");
        string? line = Console.ReadLine();
        if (line!=null) registry.Dispatch(line);
    }
    Console.WriteLine("Game Over 😥");
    }
}
