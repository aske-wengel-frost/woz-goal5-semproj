namespace cs.Domain.Story
{
    using cs.Domain.Player;
    using cs.Persistance;
    using cs.Presentation;

    using System.Xml.Linq;

    using static System.Formats.Asn1.AsnWriter;

    public class StoryHandler
    {
        bool Done = false;
        public Story Story { get; private set; }
        private Scene? CurrentScene { get; set; }
        public IUIHandler _UI { get; private set; }
        public IDataProvider _Data { get; private set; }
        public Player Player { get; set; }
        public bool IsEndScene { get; private set; }

        // New constructor with respect to our design. 
        // With respect to dependency of our UIHandler.
        // Godt eksempel på dependeny injection
        public StoryHandler(IUIHandler uiHandler, IDataProvider dataProvider)
        {
            Player = new Player("");
            _Data = dataProvider;
            _UI = uiHandler;
            this.Story = _Data.GetStory();

        }

        /// <summary>
        /// Entry point for the story, this loads the scenes, gets the initial scene and draws the fi
        /// </summary>
        public void StartStory()
        {
            // Sets the current scene
            Scene? initialScene = Story.GetInitialScene();

            if (initialScene is null)
            {
                _UI.DrawError("Could not find a valid start scene!");
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
            CurrentScene = scene;

            // If the scene is of type ContextScene
            if (scene is ContextScene contextScene)
            {
                HandleContextScene(contextScene);
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

        // Måske flyt denne ind i gå command?
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

            if (CurrentScene is not ContextScene)
            {
                _UI.DrawError($"Den nuværende scene er ikke en context scene!");
                return;
            }

            // We explicitcast to context scene as we know it is one based on guard check
            ContextScene contextScene = (ContextScene)GetCurrentScene();

            // Try to find the contextscene in the list of contextscenes by index based on the users input.
            if (contextScene.Choices.ElementAtOrDefault(usrInpValue - 1) == null)
            {
                _UI.DrawError($"{usrInpValue} er ikke et gyldigt valg!");
                return;
            }

            // Gets the scenechoice object
            SceneChoice sceneChoice = contextScene.Choices[usrInpValue - 1];

            // GUARD CLAUSES
            if (sceneChoice.IsLocked())
            {
                // Try to unlock the sceneChoice
                if (!sceneChoice.Unlock(Player.Inventory))
                {
                    _UI.DrawError($"Du kan ikke gå hertil, du mangler vidst {sceneChoice.KeyItem.Name}");
                    return;
                }
            }

            Player.ModifyScore(sceneChoice.ScorePoints);
            Player.ModifyPartnerAgression(sceneChoice.PartnerAggression);

            // At last transitions to scene
            TransitionToScene(sceneChoice.SceneObj);
        }

        public void MakeDone()
        {
            if (!IsEndScene) return;
            Done = true;
        }

        public bool IsDone()
        {
            return Done;
        }

        // Method to get the current scene of the story
        public Scene GetCurrentScene()
        {
            return CurrentScene;
        }

        // Method to get the player object
        public Player GetPlayer()
        {
            return Player;
        }

        // Method to show end scene
        public void HandleEndScene(EndScene endScene)
        {
            IsEndScene = true;
            _UI.DrawScene(endScene, this);

        }

        /// <summary>
        /// Tries to use an item in the current scene.
        /// </summary>
        /// <param name="item">The item to be used.</param>
        /// <returns>True if the item was used successfully, false otherwise.</returns>
        public bool UseItemInScene(Item item)
        {
            if (CurrentScene is not ContextScene contextScene)
            {
                return false;
            }

            // Finds a choice in the current scene that requires an item
            foreach (SceneChoice choice in contextScene.Choices)
            {
                // Checks if the choice requires the specified item and if it's the right one
                if (choice.IsLocked() && choice.KeyItemId == item.Id)
                {
                    // If TRUE, proceed to the next scene
                    _UI.DrawInfo($"Du brugte: {item.Name}.");

                    // Find the next scene based on the choice - almost like the PerformChoice method
                    Scene? nextScene = Story.FindScene<Scene>(choice.SceneId);
                    if (nextScene != null)
                    {
                        TransitionToScene(nextScene);
                        return true; // True return if the Item was used successfully
                    }
                    else
                    {
                        // UI draws error if next scene could not be found (If scene is null)
                        _UI.DrawError("Could not find the next scene");
                        return false;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Handles what to draw given a specific ContextScene.
        /// And highlights the area related to the scene.
        /// Where if mutliple cut scenes are linked to eachother, it calls recursively.
        /// </summary>
        private void HandleContextScene(ContextScene contextScene)
        {
            _UI.HighlightArea(contextScene.AreaId);
            _UI.DrawScene(contextScene, this);
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
                Scene? nextScene = Story.FindScene<Scene>(cutScene.NextSceneId.Value);
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
            if (!IsEndScene) return;
            // Reset players score and inventory
            GetPlayer().ResetPlayerScore();
            GetPlayer().Inventory.RemoveAllItems();

            // Reset story
            _Data.ReloadStory();
            this.Story = _Data.GetStory();
            StartStory();
            IsEndScene = false;

        }

    }
}