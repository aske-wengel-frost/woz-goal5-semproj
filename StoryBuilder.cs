namespace cs
{
    using System;
    using System.Text.Json;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// This class controls everything to do with building and loading a story (Collection of scenes)
    /// </summary>
    class StoryBuilder
    {
        public Dictionary<int, Scene> Scenes { get; set; }
        public Dictionary<int, Area> Areas { get; set; }

        private string scenesFilePath;

        // default path for json is set as default value of parameter in constructor
        public StoryBuilder(string scenesFilePath = "./dat.json")
        {
            this.scenesFilePath = scenesFilePath;

            // initialize Scenes and Areas
            Scenes = new Dictionary<int, Scene>();
            Areas = new Dictionary<int, Area>();
        }

        public bool AddScene(Scene scene)
        {
            // Adds the scene object and sets the key in the dictionary to the name of the Scene
            Scenes.Add(scene.ID, scene);

            return true;
        }

        /// <summary>
        /// Resolves all SceneChoice references by linking to their target Scene-objects.
        /// This is needed, as a scene object may not exist when we want to assign the scene to the scene object within the Choices of the scene.
        /// </summary>
        public void LinkScenes()
        {
            // Loop through all scenes
            foreach (Scene scene in Scenes.Values)
            {
                // Loop through all scenechoices in theese scenes
                foreach (SceneChoice sceneChoice in scene.Choices)
                {
                    // try to resolve the name of the scene with a scene object
                    if (Scenes.TryGetValue(sceneChoice.SceneId, out Scene OutScene))
                    {
                        // set the scene object on the scenechoice object to the found instance
                        sceneChoice.SceneObj = OutScene;
                    }
                }
            }
        }

        /// <summary>
        /// Returns intial scene with ID = 0. 
        /// </summary>
        /// <returns>Scene</returns>
        public Scene getIntiialScene()
        {
            return Scenes[0];
        }

        /// <summary>
        /// Find a scene based on the name property of the scene.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>The found Scene object</returns>
        public Scene FindScene(int ID)
        {
            Scene? tmpScene = Scenes[ID];

            return tmpScene;
        }

        /// <summary>
        /// Creates scenes and adds them to the Scenes dictionary
        /// </summary>
        public void LoadScenes()
        {
            Areas = new Dictionary<int, Area>
            {
              {0, new Area(0, "Dinmors indgang")},
              {1, new Area(1, "Dinmors køkken")},
              {2, new Area(2, "Dinmors badeværelse")},
              {3, new Area(3, "Dinmors kælder🤓")},
            };

            string dummyDialougeText = "Dette er noget tekst som er den del af denne sygt nice historie som vi har skrvete, WOW det er en god historie var? og shit hvor skal denne tekst være lang sådan man virkelig kan se at der er gjordt noget ud af denne blockbuster storyline vi har lavet her. tak for idag husk og like og subscribe";


            this.AddScene(new Scene(0, "Start 1", dummyDialougeText, Areas[0],
                new List<SceneChoice> {
            new SceneChoice(2, "Smid kniven, og kast dig ind i badeværelset."),
            new SceneChoice(3, "Arbejde på dine daglige steps, og gå ned ad trapperne til kælderen.")
                }));

            this.AddScene(new Scene(1, "Start 2", dummyDialougeText, Areas[1],
                new List<SceneChoice> {
            new SceneChoice(2, "Smid kniven, og kast dig ind i badeværelset."),
            new SceneChoice(3, "Arbejde på dine daglige steps, og gå ned ad trapperne til kælderen.")
                }));

            // Add the target scenes
            this.AddScene(new Scene(2, "Badeværelset", "Du er nu i badeværelset.", Areas[2], new List<SceneChoice>()));
            this.AddScene(new Scene(3, "Kælderen", "Du går ned i kælderen.", Areas[3], new List<SceneChoice>()));

            this.LinkScenes();

        }


        /// <summary>
        /// Imports dictionary of scenes from given json-file.
        /// </summary>
        /// <param name="filePath">The filepath to the json file containing the scenes</param>
        /// <returns>returns>
        public void LoadScenesFromFile()
        {
            // First check if a file already exists in the directory
            if (!File.Exists(this.scenesFilePath))
            {
                // If it does not exist we generate it with a empty dictionary of scenes.
                string dat = JsonSerializer.Serialize<Dictionary<int, Scene>>(new Dictionary<int, Scene>());
                //string dat = JsonSerializer.Serialize<Dictionary<int, Scene>>(this.Scenes);

                // Creates the file and appends the json
                File.AppendAllText(this.scenesFilePath, dat);
            }
            
            // Read the text of the file
            string tmpJsonStr = File.ReadAllText(this.scenesFilePath);

            // We load the deserialized scenes into the scenes property
            // Maby handle a null value here.
            this.Scenes = JsonSerializer.Deserialize<Dictionary<int, Scene>>(tmpJsonStr);
        }


        /// <summary>
        /// Serializes a Dictoinary of scenes to json, and saves in a file
        /// </summary>
        /// <param name="scenes">The dictionary of scenes for export</param>
        /// <param name="filePath">The filepath including filename where teh file will be saved</param>
        /// <returns></returns>
        public void ExportScenesToFile(Dictionary<int, Scene> scenes, string filePath)
        {
            string jsonStr = JsonSerializer.Serialize(scenes);

            File.WriteAllText(filePath, jsonStr);
        }

    }
}
