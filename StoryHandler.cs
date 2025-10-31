/* StoryHandler class to hold all context relevant to a session.
 */

using cs;

class StoryHandler {
  Space current;
  bool done = false;
  
  public Dictionary<string, Scene> Scenes{ get; set; }

  public StoryHandler (Space node) {
    current = node;
  }


    /// <summary>
    /// Start method that loads scenes
    /// </summary>
    public void Start()
    {
        LoadScenes();
    }
  
  public Space GetCurrent() {
    return current;
  }
  
    //If the next space is null, prints message and starts the game over
  public void Transition (string direction) {
    Space next = current.FollowEdge(direction);
    if (next==null) {
      Console.WriteLine("You are confused, and walk in a circle looking for '"+direction+"'. In the end you give up 😩");
    } else {
      current.Goodbye();
      current = next;
      current.Welcome();
    }
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
  /// Creates scenes and adds them to the Scenes dictionary
  /// </summary>
  private void LoadScenes()
  {
        Scenes = new Dictionary<string, Scene>
        {
            {"Scene1", new Scene(1, "Scene1", "Din") },
            {"Scene2", new Scene(2, "Scene2", "mor") },
            {"Scene3", new Scene(3, "Scene3", "stinker") },
            {"Scene4", new Scene(4, "Scene4", "af") },
            {"Scene5", new Scene(5, "Scene5", "lort") }
        };
  }
}

