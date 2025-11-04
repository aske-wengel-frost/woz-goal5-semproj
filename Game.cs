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
        //Console.WriteLine("Welcome to the World of Zuul!");

        Console.WriteLine("=========================================================================================================");
        Console.WriteLine("Detté spil er udviklet for at skabe opmærksomhed omkring psykisk vold mod kvinder.");
        Console.WriteLine("Gennem fortællinger, valg og scenarier får du mulighed for at opleve, hvordan psykisk vold kan opstå,");
        Console.WriteLine("udvikle sig og påvirke et menneske - både synligt og usynligt.");
        Console.WriteLine();
        Console.WriteLine("Du er ikke alene, der findes hjælp - både i og uden for spillet. ");
        Console.WriteLine("Lev uden volds Hotline: 1888");
        Console.WriteLine();
        Console.WriteLine("=========================================================================================================");
        Console.WriteLine();
        Console.Write("Indtast dit navn: ");
        string? playerName = Console.ReadLine();
        Console.WriteLine($"Hej {playerName}, tak fordi du vælger at engagere dig i et vigtigt emne.");




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
