namespace woz.Domain.Story
{
    using woz.Domain.Player;
    using woz.Persistance;
    using woz.Presentation;

    using static System.Formats.Asn1.AsnWriter;

    public class StoryHandler
    {
        // flag for completion of story
        bool Done = false;
        public Story Story { get; private set; }
        private Scene? CurrentScene { get; set; }
        public IUIHandler UI { get; private set; }
        public IDataProvider Data { get; private set; }
        public Player Player { get; set; }
        public bool IsEndScene { get; private set; }


        // Dependency injection - the UIHandler and DataProvider is passed in as a parameter, as the story handler depends on them.
        public StoryHandler(IUIHandler uiHandler, IDataProvider dataProvider)
        {
            Player = new Player("");
            Data = dataProvider;
            UI = uiHandler;

            // The Injected dataprovider object supplies the story object
            this.Story = Data.GetStory();
        }

        /// <summary>
        /// Entry point for the story, this loads the scenes, gets the initial scene and draws the UI
        /// </summary>
        public void StartStory()
        {
            // Gets the inital scene object from Story
            Scene? initialScene = Story.GetInitialScene();

            // Guard check if the scene was found
            if (initialScene is null)
            {
                // This scenario is unrecoverable, so we throw an error
                throw new Exception("No inital scene was found!"); // <------- SPØRG
                //UI.DrawError("Could not find a valid start scene!");
                return;
            }
            
            this.TransitionToScene(initialScene);
        }

        public bool isCurrentSceneOftype<T>()
        {
            if(CurrentScene is T)
            {
                return true;
            }
            return false;
        }

        // Helpers

        /// <summary>
        /// Transitions to a given scene
        /// </summary>
        /// <param name="scene">The scene to transition to</param>
        public void TransitionToScene(Scene scene)
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
        public void PerformChoice(int sceneId)
        {
            // Guard check if currentScene is a context Scene, as only a contextScene contains chocies
            if (CurrentScene is not ContextScene)
            {
                UI.DrawError($"Dette valg kan du ikke tage på nuværende tidspunkt!");
                return;
            }

            // We explicitcast to context scene as we know it is one based on guard check
            ContextScene contextScene = (ContextScene)GetCurrentScene();

            // Try to find the contextscene in the list of contextscenes by index based on the users input.
            if (contextScene.Choices.ElementAtOrDefault(sceneId - 1) == null)
            {
                UI.DrawError($"{sceneId} er ikke et gyldigt valg!");
                return;
            }

            // Gets the scenechoice object
            SceneChoice sceneChoice = contextScene.Choices[sceneId - 1];

            // Check if the sceneChoice is locked
            if (sceneChoice.IsLocked())
            {
                // Try to unlock the sceneChoice
                if (!sceneChoice.Unlock(Player.Inventory))
                {
                    UI.DrawError($"Du kan ikke gå hertil, du mangler vidst {sceneChoice.KeyItem.Name}");
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

        /// <summary>
        /// Handles the context scene
        /// </summary>
        private void HandleContextScene(ContextScene contextScene)
        {
            UI.HighlightArea(contextScene.AreaId);
            UI.DrawScene(contextScene, this.Player);
        }

        /// <summary>
        /// Handles what to draw given a Scene. If more CutScenes are linked to eachother, it calls recursively.
        /// </summary>
        /// <param name="cutScene"></param>
        private void HandleCutScene(CutScene cutScene)
        {
            // Draws the cutcene
            UI.DrawScene(cutScene, this.Player);
            UI.WaitForKeypress();

            // Check if next scene has id.
            if (cutScene.NextSceneId.HasValue)
            {
                throw new Exception("The cutscene does not link to a new scene");
            }

            // Tries to find the next scene
            Scene? nextScene = Story.FindScene<Scene>(cutScene.NextSceneId.Value);

            // Guard check if the next scene is null
            if (nextScene == null)
            {
                throw new Exception("The next scene object could not be resolved");
            }

            // Handle next scene.
            TransitionToScene(nextScene);
        }

        /// <summary>
        /// Handles the end scene
        /// </summary>
        /// <param name="endScene"></param>
        private void HandleEndScene(EndScene endScene)
        {
            // Draws endscene
            UI.DrawScene(endScene, this.Player);
        }

        /// <summary>
        /// Creates new instance of story, and restarts game.
        /// </summary>
        public void RestartGame()
        {
            // Reset players score and inventory
            GetPlayer().ResetPlayerScore();
            GetPlayer().ResetParterAggression();
            GetPlayer().Inventory.RemoveAllItems();

            // Reset story
            Data.ReloadStory();
            this.Story = Data.GetStory();
            StartStory();
        }
    }
}
