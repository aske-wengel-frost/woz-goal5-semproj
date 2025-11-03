/* StoryHandler class to hold all StoryHandler relevant to a session.
 */

using cs;

class StoryHandler {
    Space current;
    bool done = false;

    private Scene currentScene { get; set; }
    public Dictionary<string, Scene> Scenes{ get; set; }

    public Dictionary<int, Area> Areas { get; set;}

    public StoryHandler (Space node) 
    {
        current = node;
    }


    /// <summary>
    /// Start method that loads scenes
    /// </summary>
    public void Start()
    {
        Initialize();
    }
  
    public Space GetCurrent() 
    {
        return current;
    }
  
    //If the next space is null, prints message and starts the game over
    public void Transition (string direction) 
    {
        Space next = current.FollowEdge(direction);
        if (next==null) {
            Console.WriteLine("You are confused, and walk in a circle looking for '"+direction+"'. In the end you give up 😩");
        } else {
            current.Goodbye();
            current = next;
            current.Welcome();
        }
    }

    public void SwitchScene(string sceneName)
    {
        Scene scene = Scenes[sceneName];
        currentScene = scene;
    }

    public void MakeDone ()
    {
        done = true;
    }
  
    public bool IsDone () 
    {
        return done;
    }


    /// <summary>
    /// Creates scenes and areas and adds them to the Scenes dictionary
    /// </summary>
    private void Initialize()
    {
        // Initialization of Areas
        Areas = new Dictionary<int, Area>
        {
            {1, new Area(1, "Entrance") },
            {2, new Area(2, "Kitchen") },
            {3, new Area(3, "Living Room") },
            {4, new Area(4, "Bathroom") },
            {5, new Area(5, "Bedroom 1") },
            {6, new Area(6, "Bedroom 2") },
            {7, new Area(7, "Bedroom 3") },
        };

        // Creation of scenes
        Scenes = new Dictionary<string, Scene>
        {
            {"Scene1", new Scene(1, "Scene1", "Din", Areas[1], new List<SceneChoice> {new SceneChoice(2, "Go to scene 2"), new SceneChoice(3, "Go to Scene 3") }) },
            {"Scene2", new Scene(2, "Scene2", "mor", Areas[1], new List<SceneChoice> {new SceneChoice(4, "Go to scene 4"), new SceneChoice(5, "Go to Scene 5") }) },
            {"Scene3", new Scene(3, "Scene3", "stinker", Areas[1], new List<SceneChoice> {new SceneChoice(4, "Go to scene 4"), new SceneChoice(1, "Go to Scene 1") }) },
            {"Scene4", new Scene(4, "Scene4", "af", Areas[1], new List<SceneChoice> {new SceneChoice(1, "Go to start scene") })  },
            {"Scene5", new Scene(5, "Scene5", "lort", Areas[1], new List<SceneChoice> {new SceneChoice(1, "Go to start scene") }) }
        };
    }

}

