namespace cs.Domain.Story
{
    using cs.Domain.Player;
    using cs.Persistance;
    using cs.Presentation;

    public class StoryHandler
    {
        bool done = false;
        public Story story { get; set; }
        private Scene? currentScene { get; set; }
        //public DataProvider dataLoader { get; set; }
        public IUIHandler _UI { get; set; }
        public IDataProvider _Data { get; set; }
        public Player player { get; set; }
        public bool isEndScene { get; private set; }
        static Registry? registry { get; set; }

        // New constructor with respect to our design. 
        // With respect to dependency of our UIHandler.
        // Godt eksempel på dependeny injection
        public StoryHandler(IUIHandler uiHandler, IDataProvider dataProvider)
        {
            _Data = dataProvider;
            _UI = uiHandler;
            this.story = _Data.GetStory();

        }

        /// <summary>
        /// Entry point for the story, this loads the scenes, gets the initial scene and draws the fi
        /// </summary>
        public void StartStory()
        {
            // Sets the current scene
            Scene? initialScene = story.GetInitialScene();

            if (initialScene is null)
            {
                _UI.DrawError("Could not a valid start scene!");
                return;
            }
            
            this.TransitionToScene(initialScene);
        }

        /// <summary>
        /// Helper method for transitioning to a given scene
        /// </summary>
        /// <param name="scene">The scene to transition to</param>
        private void TransitionToScene(Scene scene)
        {
            // Sets the current scene
            currentScene = scene;

            // If the scene is of type ContextScene
            if (scene is ContextScene contextScene)
            {
                _UI.HighlightArea(contextScene.AreaId);
                _UI.DrawScene(contextScene, this);
                // The object type is identified so we return as there is no need to check further statements!
                return;
            }

            // If the scene is of type CutScene
            else if (scene is CutScene cutScene)
            {
                HandleCutScene(cutScene);
                return;
            }

            // If the scene is of type EndScene
            else if (scene is EndScene endScene)
            {
                HandleEndScene(endScene);
                return;
            }
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
                _UI.DrawError("Ikke validt input!");
                return;
            }

            if (currentScene is not ContextScene)
            {
                _UI.DrawError($"Den nuværende scene er ikke en context scene!");
                return;
            }

            // We can explicit cast as we have a guard check for if the current scene is a context scene
            ContextScene? contextScene = (ContextScene)GetCurrentScene();


            if (contextScene.Choices.ElementAtOrDefault(usrInpValue - 1) == null)
            {
                _UI.DrawError($"{usrInpValue} er ikke et gyldigt valg!");
                return;
            }

            // Gets the scenechoice with aske stuff
            SceneChoice? sceneChoice = contextScene.Choices[usrInpValue - 1];

            // GUARD CLAUSES
            if (sceneChoice.isLocked())
            {
                // Try to unlock the sceneChoice
                if (!sceneChoice.Unlock(player.inventory))
                {
                    _UI.DrawError($"Du kan ikke gå hertil, du mangler vidst {sceneChoice.KeyItem.Name}");
                    return;
                }
            }

            player.Score += sceneChoice.ScorePoints;
            player.PartnerAggression += sceneChoice.PartnerAggression;

            // At last transitions to scene
            TransitionToScene(sceneChoice.SceneObj);
        }

        public void MakeDone()
        {
            if (!isEndScene) return;
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
        public void HandleEndScene(EndScene endScene)
        {
            isEndScene = true;
            _UI.DrawScene(endScene, this);

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
                    _UI.DrawInfo($"Du brugte: {item.Name}.");

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
        private void HandleCutScene(CutScene cutScene)
        {
            _UI.DrawScene(cutScene, this);
            _UI.WaitForKeypress();

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

        /// <summary>
        /// Creates new instance of story, and restarts game.
        /// </summary>
        public void RestartGame()
        {
            if (!isEndScene) return;
            // Reset players score and inventory
            GetPlayer().Score = 0;
            GetPlayer().inventory.RemoveAllItems();

            // Reset story
            _Data.ReloadStory();
            this.story = _Data.GetStory();
            StartStory();
            isEndScene = false;

        }

    }
}