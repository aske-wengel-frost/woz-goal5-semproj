/* Main class for launching the game
 */
using cs;
using cs.Commands;
using System.Transactions;

namespace cs
{
    using Commands;

    public class Game
    {
        static public StoryHandler storyHandler { get; set; }
        static public IUIHandler UIHandler { get; set; }

        static ICommand fallback = new CommandUnknown();
        static Registry registry {get; set;}

        private static void InitRegistry()
        {
            ICommand cmdExit = new CommandExit();
            registry.Register("hjælp", new CommandHelp(registry));
            registry.Register("afslut", cmdExit); // cmdExit x2?
            registry.Register("bevæg", new CommandMove());
            registry.Register("forlade", cmdExit); // cmdExit x2?
            registry.Register("export", new CommandExportStory());

            // New Commands for the help() function
            registry.Register("tilbage", new CommandBack());
            registry.Register("se", new CommandLook());
            registry.Register("inventar", new CommandInventory());
            registry.Register("inv", new CommandInventory()); // Just a shorter version for inventory (Alias)
            registry.Register("tag", new CommandTake());
            registry.Register("brug", new CommandUse());
        }

        static void Main(string[] args)
        {
            UIHandler = new UITerminal();
            storyHandler = new StoryHandler(UIHandler);
            registry = new Registry(storyHandler, fallback);

            // Welcome message
            Console.WriteLine("---------=======================================================================================---------");
            Console.WriteLine("Detté spil er udviklet for at skabe opmærksomhed omkring psykisk vold mod kvinder.");
            Console.WriteLine("Gennem fortællinger, valg og scenarier får du mulighed for at opleve, hvordan psykisk vold kan opstå,");
            Console.WriteLine("udvikle sig og påvirke et menneske - både synligt og usynligt.");
            Console.WriteLine();
            Console.WriteLine("Du er ikke alene, der findes hjælp - både i og uden for spillet. ");
            Console.WriteLine("Lev Uden Volds Hotline: 1888");
            Console.WriteLine();
            Console.WriteLine("Hvis du bliver i tvivl om, hvordan spillet fungerer, kan du altid skrive kommandoen 'hjælp' ");
            Console.WriteLine("for at se en liste over alle kommandoer med deres beskrivelse");
            Console.WriteLine("---------=======================================================================================---------");
            Console.WriteLine();
            Console.Write("Indtast dit navn: ");
            string? playerName = Console.ReadLine();
            storyHandler.player = new Player (playerName); //Create the player in storyHandler. 
            Console.WriteLine($"Hej {playerName}, tak fordi du vælger at engagere dig i et vigtigt emne.");

            // We call the InitRegistry method
            InitRegistry();

            storyHandler.Start();

            // Game loop  
            while (storyHandler.IsDone() == false)
            {
                Console.Write("> ");
                string? line = Console.ReadLine();
                if (line != null) registry.Dispatch(line);
            }
            Console.WriteLine($"Spillet er nu slut, tak fordi du spillede {playerName}");

        }
    }
}
