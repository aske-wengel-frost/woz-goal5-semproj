/* StoryHandler class to hold all StoryHandler relevant to a session.
 */
namespace cs
{

    public class StoryHandler
    {
        bool done = false;

        private Scene? currentScene { get; set; }
        StoryBuilder StoryBuilder { get; set; }

        public IUIHandler _UIHandler { get; set; }


        // New constructor with respect to our design. 
        // With respect to dependency of our UIHandler.
        public StoryHandler(IUIHandler uiHandler)
        {
            _UIHandler = uiHandler;
            StoryBuilder = new StoryBuilder();
        }

        /// <summary>
        /// Entry point for the story, this loads the scenes, gets the initial scene and draws the fi
        /// </summary>
        public void Start()
        {
            // Loads the story
            StoryBuilder.LoadScenesFromFile();

            // Sets the current scene
            currentScene = StoryBuilder.getIntiialScene();

            // Draws the initial scene
            _UIHandler.DrawScene(currentScene);

        }
        /// <summary>
        /// Checks wether userinput corresponds to any choice, and proceeeds if true, otherwise, DrawError is called.
        /// </summary>
        /// <param name="usrInp"></param>
        public void PerformChoice(string usrInp)
        {
            int usrInpValue = Int32.Parse(usrInp);
            Scene sceneProxy = StoryBuilder.FindScene(usrInpValue);
            if (currentScene!.Choices.Exists(_ => _.SceneObj.Equals(sceneProxy)))
            {
                currentScene = sceneProxy;
                //_UIHandler.DrawScene(currentScene, this);
                _UIHandler.DrawScene(currentScene);
            }
            else { _UIHandler.DrawError("Scene does not exist.."); }


        }

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

