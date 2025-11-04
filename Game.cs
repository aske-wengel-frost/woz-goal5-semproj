/* Main class for launching the game
 */
using cs;
using cs.Commands;

namespace cs
{
    using UI;
    using Commands;

    public class Game
    {

        static public StoryHandler storyHandler { get; set; }
        static public UII UIHandler { get; set; }

        static World world = new World();
        static ICommand fallback = new CommandUnknown();
        static Registry registry {get; set;}

        private static void InitRegistry()
        {
            ICommand cmdExit = new CommandExit();
            registry.Register("exit", cmdExit);
            registry.Register("quit", cmdExit);
            registry.Register("bye", cmdExit);
            registry.Register("go", new CommandGo());
            registry.Register("help", new CommandHelp(registry));
            registry.Register("move", new CommandMove());
        }

        static void Main(string[] args)
        {
            UIHandler = new UITerm();
            storyHandler = new StoryHandler(UIHandler, world.GetEntry());
            registry = new Registry(storyHandler, fallback);

            // A welcome message is printed to the console
            Console.WriteLine("Welcome to the World of Zuul!");

            // We call the InitRegistry method
            InitRegistry();

            storyHandler.GetCurrent().Welcome();

            storyHandler.Start();

            while (storyHandler.IsDone() == false)
            {
                Console.Write("> ");
                string? line = Console.ReadLine();
                if (line != null) registry.Dispatch(line);
            }
            Console.WriteLine("Game Over 😥");

            //storyHandler.LoadScenes();
            //storyHandler.Start();
            //while (true)
            //{
            //    string? input = UIHandler.GetUserInput();
            //    UIHandler.ClearUI();
            //    storyHandler.PerformChoice(input);

            //}
        }
    }
}
