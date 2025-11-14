/* StoryHandler class to hold all StoryHandler relevant to a session.
 */
namespace cs
{

    public class StoryHandler
    {
        bool done = false;

        private Scene? currentScene { get; set; }
        public StoryBuilder StoryBuilder { get; set; }

        public IUIHandler _UIHandler { get; set; }

        public Player player { get; set; } //Make a player property.  

        // New constructor with respect to our design. 
        // With respect to dependency of our UIHandler.
        // Godt eksempel p� dependeny injection
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
            StoryBuilder.LoadAreas();
            StoryBuilder.LoadScenesFromFile();
            StoryBuilder.LinkScenes();
            //StoryBuilder.LoadScenesNew();
            //StoryBuilder.ExportScenesToFile();

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
            string ErrorMsg = "Ikke et validt valg. Prøv igen.";
            Int32.TryParse(usrInp, out int usrInpValue);
            if (UITerminal.SceneChoiceAsc.ContainsKey(usrInpValue))
            {
                Scene? sceneProxy = StoryBuilder.FindScene(UITerminal.SceneChoiceAsc[usrInpValue]);

                if (sceneProxy != null && currentScene!.Choices.Exists(_ => _.SceneObj.Equals(sceneProxy)))
                {
                    currentScene = sceneProxy;
                    _UIHandler.DrawScene(currentScene);
                }
                else { _UIHandler.DrawError(ErrorMsg); }
            }
            else { _UIHandler.DrawError(ErrorMsg); }
        }

        public void MakeDone()
        {
            done = true;
        }

        public bool IsDone()
        {
            return done;
        }

        // Method to get the current scene of the story
        public Scene GetCurrentScene()
        {
            return currentScene;
        }

        // Method to get the player object
        public Player GetPlayer()
        {
            return player;
        }

        /// <summary>
        /// Tries to use an item in the current scene.
        /// </summary>
        /// <param name="item">The item to be used.</param>
        /// <returns>True if the item was used successfully, false otherwise.</returns>
        public bool UseItemInScene(Item item)
        {
            // Finds a choice in the current scene that requires an item
            foreach (SceneChoice choice in currentScene.Choices)
            {
                // Checks if the choice requires the specified item and if it's the right one
                if (choice.RequiredItemId.HasValue && choice.RequiredItemId.Value == item.ID)
                {
                    // If TRUE, proceed to the next scene
                    _UIHandler.DrawInfo($"Du brugte: {item.Name}.");

                    // Find the next scene based on the choice - almost like the PerformChoice method
                    Scene? nextScene = StoryBuilder.FindScene(choice.SceneId);

                    // Goes to the next scene if found
                    if (nextScene != null)
                    {
                        currentScene = nextScene;
                        _UIHandler.DrawScene(currentScene);
                        return true;
                    }
                }
            }
            return false;
        }

        // Method to go back to the previous scene
        public bool GoBack()
        {
            // Logic to go back to the previous scene
            // Note to Self: maybe a "sceneHistory" list to hold the scenes??
            // sceneHistory.RemoveAt(sceneHistory.Count - 1);

            /* if (sceneHistory.Count > 0)
            {
                Scene previousScene = sceneHistory[sceneHistory.Count - 1];
                currentScene = previousScene;
                UIHandler.DrawScene(currentScene);
                return true;
            }*/
            return false;
        }
    }
}

