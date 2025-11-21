/* StoryHandler class to hold all StoryHandler relevant to a session.
 */
using System.Runtime.CompilerServices;

namespace cs
{

    public class StoryHandler
    {
        bool done = false;

        public Story story { get; set; }
        public EndScene EndScene { get; set; } // Added EndScene property
        private Scene? currentScene { get; set; }
        public DataLoader dataLoader { get; set; }

        public IUIHandler _UIHandler { get; set; }

        public Player player { get; set; } //Make a player property.
        
        // New constructor with respect to our design. 
        // With respect to dependency of our UIHandler.
        // Godt eksempel p� dependeny injection
        public StoryHandler(IUIHandler uiHandler)
        {
            _UIHandler = uiHandler;
            dataLoader = new DataLoader();
            
            dataLoader.Load();
            dataLoader.ExportStoryToFile();

            this.story = dataLoader.story;

            EndScene = new EndScene(this); // Initialize EndScene with the current StoryHandler instance

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
            currentScene = contextScene;

            if (contextScene is null)
            {
                _UIHandler.DrawError("Could not find start valid start scene!");
                return;
            }

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
            string ErrorMsg = "Ikke et validt valg. Prøv igen.";
            Int32.TryParse(usrInp, out int usrInpValue);
            if (UITerminal.SceneChoiceAsc.ContainsKey(usrInpValue))
            {
                Scene? sceneProxy = story.FindScene(UITerminal.SceneChoiceAsc[usrInpValue]);

                // Check wheter scene is a ContextScene.
                if (sceneProxy is ContextScene contextScene)
                {
                    if (currentScene is ContextScene curCtx && curCtx.Choices.Exists(_ => _.SceneObj == contextScene))
                    {
                        // Checks if the choice requires an itemd
                        if (contextScene.RequiredItemId != null)
                        {
                            // Check whether choice needs item.
                            if (!GetPlayer().Inventory.ItemExists(contextScene.RequiredItemId))
                            {
                                _UIHandler.DrawError("Du mangler en genstand for at vælge dette!");
                                return;
                            }
                            _UIHandler.DrawError($"Måske hvis du bruger en genstand ville du kunne komme videre.");
                            return;
                        }

                        currentScene = contextScene;
                        _UIHandler.HighlightArea(contextScene.AreaId);
                        player.Score += contextScene.ScenePoints; //Adds the points of the currentScene to the playerScore
                        _UIHandler.DrawScene(currentScene, player.Score);

                    }

                    else { _UIHandler.DrawError(ErrorMsg); }

                }

                // Check whether scene is a cutscene, and handle respectively.
                else if (sceneProxy is CutScene cutScene)
                {
                    if (cutScene != null)
                    {
                        currentScene = cutScene;
                        this.HandleCutScene(cutScene);
                    }
                    else { _UIHandler.DrawError(ErrorMsg); }

                }

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
                if (choice.SceneObj.RequiredItemId.HasValue && choice.SceneObj.RequiredItemId.Value == item.ID)
                {
                    // If TRUE, proceed to the next scene
                    _UIHandler.DrawInfo($"Du brugte: {item.Name}.");

                    // Find the next scene based on the choice - almost like the PerformChoice method
                    Scene? nextScene = story.FindScene(choice.SceneId);

                    // Goes to the next scene if found
                    if (nextScene != null)
                    {
                        currentScene = nextScene;
                        _UIHandler.HighlightArea(contextScene.AreaId);
                        player.Score += contextScene.ScenePoints; //Adds the points of the currentScene to the playerScore
                        _UIHandler.DrawScene(currentScene, player.Score);
                        return true;
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
                Scene? nextScene = story.FindScene(cutScene.NextSceneId.Value);
                if (nextScene != null)
                {
                    // Handle next scene.
                    currentScene = nextScene;
                    if (currentScene is CutScene nextCutScene)
                    {
                        HandleCutScene(nextCutScene);
                    }
                    else { _UIHandler.DrawScene(nextScene, player.Score); }
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

