/* StoryHandler class to hold all StoryHandler relevant to a session.
 */
namespace cs
{
    using UI;

    public class StoryHandler
    {
        Space current;
        bool done = false;

        private Scene? currentScene { get; set; }
        StoryBuilder StoryBuilder { get; set; }

        public UII UIHandler { get; set; }


        // New constructor with respect to our design. 
        // With respect to dependency of our UIHandler.
        public StoryHandler(UII uiHandler, Space node)
        {
            UIHandler = uiHandler;
            StoryBuilder = new StoryBuilder();
            current = node;
        }

        public Space GetCurrent()
        {
            return current;
        }

        //If the next space is null, prints message and starts the game over
        public void Transition(string direction)
        {
            Space next = current.FollowEdge(direction);
            if (next == null)
            {
                Console.WriteLine("You are confused, and walk in a circle looking for '" + direction + "'. In the end you give up 😩");
            }
            else
            {
                current.Goodbye();
                current = next;
                current.Welcome();
            }
        }

        /// <summary>
        /// Sets current scene to intial scene, and draws it.
        /// </summary>
        public void Start()
        {
            //currentScene = StoryBuilder.getIntiialScene();
            //UIHandler.DrawScene(currentScene, this);
            StoryBuilder.LoadScenes();
            currentScene = StoryBuilder.getIntiialScene();

        }
        /// <summary>
        /// Checks wether userinput corresponds to any choice, and proceeeds if true, otherwise, DrawError is called.
        /// </summary>
        /// <param name="usrInp"></param>
        public void PerformChoice(string usrInp)
        {
            int usrInpValue = Int32.Parse(usrInp);
            Scene sceneProxy = StoryBuilder.FindScene(usrInpValue);
            if (currentScene!.Choices.Exists(_ => _.SceneObj == sceneProxy))
            {
                currentScene = sceneProxy;
                UIHandler.DrawScene(currentScene, this);
            }
            else { UIHandler.DrawError("Scene does not exist.."); }


        }


        // public void SwitchScene(string sceneName)
        // {
        //   Scene scene = Scenes[sceneName];
        //   currentScene = scene;
        // }

        public void MakeDone()
        {
            done = true;
        }

        public bool IsDone()
        {
            return done;
        }

    }
}

