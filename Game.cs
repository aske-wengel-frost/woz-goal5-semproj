/* Main class for launching the game
 */

<<<<<<< HEAD
using cs;
=======
using cs.Commands;
>>>>>>> 3a11fe8ba251e1b8333de16bdce494932fcf66bf

class Game {
  static World    world    = new World();
  static StoryHandler  StoryHandler  = new StoryHandler(world.GetEntry());
  static ICommand fallback = new CommandUnknown();
<<<<<<< HEAD
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
=======
  static Registry registry = new Registry(StoryHandler, fallback);
  
  private static void InitRegistry () {
>>>>>>> 3a11fe8ba251e1b8333de16bdce494932fcf66bf
    ICommand cmdExit = new CommandExit();
    registry.Register("exit", cmdExit);
    registry.Register("quit", cmdExit);
    registry.Register("bye", cmdExit);
    registry.Register("go", new CommandGo());
    registry.Register("help", new CommandHelp(registry));
    registry.Register("move", new CommandMove());
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
