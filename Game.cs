/* Main class for launching the game
 */

using cs;

class Game {
  static World    world    = new World();
  static StoryHandler  context  = new StoryHandler(world.GetEntry());
  static ICommand fallback = new CommandUnknown();
  static Registry registry = new Registry(context, fallback);

    #region IUIHandler Members
    public void DrawScene(Scene scene)
    {
        for (int i = 0; i < 80; i++)
        {
            Console.Write("#");
        }
    }

    public string GetUserInput(String input)
    {
        return "";
    }

    public void ClearScreen()
    {
        Console.Clear();
    }

    public void DrawError()
    {
        Console.WriteLine("An error has occurred. The game will restart.");
    }
    #endregion

    private static void InitRegistry () {
    ICommand cmdExit = new CommandExit();
    registry.Register("exit", cmdExit);
    registry.Register("quit", cmdExit);
    registry.Register("bye", cmdExit);
    registry.Register("go", new CommandGo());
    registry.Register("help", new CommandHelp(registry));
  }
  
  static void Main (string[] args) {
    
    context.Start();

    // A welcome message is printed to the console
    Console.WriteLine("Welcome to the World of Zuul!");
    
    // We call the InitRegistry method
    InitRegistry();

    context.GetCurrent().Welcome();
    
    while (context.IsDone()==false) {
        Console.Write("> ");
        string? line = Console.ReadLine();
        if (line!=null) registry.Dispatch(line);
    }
    Console.WriteLine("Game Over 😥");
    }
}
