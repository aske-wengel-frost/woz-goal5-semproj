namespace cs.Domain.Story
{
    using cs.Domain.Player;
    using cs.Persistance;
    using cs.Presentation;


    public class StoryHandler
    {
        bool done = false;
        public Story story { get; set; }
        public EndScene EndScene { get; set; }
        private Scene? currentScene { get; set; }
        //public DataProvider dataLoader { get; set; }
        public IUIHandler _UIHandler { get; set; }
        public IDataProvider _Data { get; set; }
        public Player player { get; set; }

        // New constructor with respect to our design. 
        // With respect to dependency of our UIHandler.
        // Godt eksempel på dependeny injection
        public StoryHandler(IUIHandler uiHandler, IDataProvider dataProvider)
        {
            _Data = dataProvider;
            _UIHandler = uiHandler;

            this.story = _Data.getStory();
            EndScene = new EndScene(this);

            //dataLoader = new DataProvider();
            //dataLoader.Load();
            //dataLoader.ExportStoryToFile();

            //this.story = dataLoader.story;

            // Initialize EndScene with the current StoryHandler instance

            // Loads the story
            //dataLoader.LoadAreas();
            //dataLoader.LoadScenesFromFile();

            //DataLoader.LoadScenesNew();
            //dataLoader.LinkScenes();
        }

        /// <summary>
        /// Entry point for the story, this loads the scenes, gets the initial scene and draws the fi
        /// </summary>
        public void StartStory()
        {
            // Sets the current scene
            ContextScene? contextScene = story.getInitialScene();

            if (contextScene is null)
            {
                _UIHandler.DrawError("Could not find start valid start scene!");
                return;
            }

            currentScene = contextScene;

            // Draws the initial scene
            _UIHandler.HighlightArea(contextScene.AreaId);
            _UIHandler.DrawScene(contextScene, player.Score);

        }

        /// <summary>
        /// Checks wether userinput corresponds to any choice, and proceeeds if true, otherwise, DrawError is called.
        /// </summary>
        /// <param name="usrInp"></param>
        public void PerformChoice(string usrInp)
        {
            // If user input cannot be converted to int
            bool isConverted = Int32.TryParse(usrInp, out int usrInpValue);
            if (!isConverted)
            {
                _UIHandler.DrawError("Ikke validt input!");
                return;
            }

            // Checks if any of the scene choices contains the users input (using ASKE MAGIC)
            if (!UITerminal.SceneChoiceAsc.ContainsKey(usrInpValue))
            {
                _UIHandler.DrawError($"{usrInpValue} er ikke et gyldigt valg!");
                return;
            }

            //Gets the sceneobject of scene to switch to
            Scene? scene = story.FindScene<Scene>(UITerminal.SceneChoiceAsc[usrInpValue]);

            // Checks if current scene is contextScene (Propably is always?)
            if(currentScene is ContextScene curContextScene)
            {
                // Gets the scenechoice
                SceneChoice? sceneChoice = curContextScene.Choices.Find(_ => _.SceneId == scene.ID);

                // GUARD CLAUSES
                if (sceneChoice.isLocked())
                {
                    // Try to unlock the sceneChoice
                    if (!sceneChoice.Unlock(player.Inventory))
                    {
                        //_UIHandler.DrawError($"Du kan ikke gå hertil, du mangler vidst {sceneChoice.KeyItem.Name}");
                        _UIHandler.DrawError($"Du kan ikke gå hertil, du mangler vidst {sceneChoice.KeyItem.Name}");
                        return;
                    }
                }
            }
            

            // At last transitions to scene
            TransitionToScene(scene);
        }

        /// <summary>
        /// Helper method for transitioning to a given scene
        /// </summary>
        /// <param name="scene">The scene to transition to</param>
        private void TransitionToScene(Scene scene)
        {
            currentScene = scene;

            // If the scene is of type contextScene
            if (scene is ContextScene contextScene)
            {
                _UIHandler.HighlightArea(contextScene.AreaId);
                player.Score += contextScene.ScenePoints; //Adds the points of the currentScene to the playerScore
                _UIHandler.DrawScene(contextScene, player.Score);
            }

            // if the scene to transition to is of type cutscene
            if(scene is CutScene cutScene)
            {
                HandleCutScene(cutScene);
            }
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
        public ContextScene? GetCurrentScene()
        {
            return currentScene as ContextScene;
        }

        // Method to get the player object
        public Player GetPlayer()
        {
            return player;
        }

        // Method to show end scene
        public void ShowEndScene()
        {
            EndScene.ShowEndScene();
        }

        /// <summary>
        /// Tries to use an item in the current scene.
        /// </summary>
        /// <param name="item">The item to be used.</param>
        /// <returns>True if the item was used successfully, false otherwise.</returns>
        public bool UseItemInScene(Item item)
        {
            if (currentScene is not ContextScene contextScene)
            {
                return false;
            }

            // Finds a choice in the current scene that requires an item
            foreach (SceneChoice choice in contextScene.Choices)
            {
                // Checks if the choice requires the specified item and if it's the right one
                if (choice.isLocked() && choice.KeyItemId == item.ID)
                {
                    // If TRUE, proceed to the next scene
                    _UIHandler.DrawInfo($"Du brugte: {item.Name}.");

                    // Find the next scene based on the choice - almost like the PerformChoice method
                    Scene? nextScene = story.FindScene<Scene>(choice.SceneId);
                    if(nextScene != null)
                    {
                        TransitionToScene(nextScene);
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Handles what to draw given a Scene. If more CutScenes are linked to eachother, it calls recursively.
        /// </summary>
        /// <param name="cutScene"></param>
        public void HandleCutScene(CutScene cutScene)
        {
            _UIHandler.DrawScene(cutScene, player.Score);
            _UIHandler.WaitForKeypress();

            // Check if next scene has id.
            if (cutScene.NextSceneId.HasValue)
            {
                Scene? nextScene = story.FindScene<Scene>(cutScene.NextSceneId.Value);
                if (nextScene != null)
                {
                    // Handle next scene.
                    TransitionToScene(nextScene);
                }
                else
                {
                    MakeDone();
                }
            }
            else
            {
                MakeDone();
            }
        }

    }
}

