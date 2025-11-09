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
        public Scene? FindScene(int ID)
        {
            if(Scenes.ContainsKey(ID))
            {
                return Scenes[ID];
            }

            return null;
        }
        
        public void LoadScenesNew()
        {
            Areas = new Dictionary<int, Area>
            {
                {0, new Area(0, "Entré")},
                {1, new Area(1, "Badeværelse")},
                {2, new Area(2,"Soveværelse")},
                {3, new Area(3,"Stue")},
                {4, new Area(4,"Køkken", new List<Item> {new Item(1, "Mobil", "En mobiltelefon")})},
            };
            string Køkken1 = "Du står i køkkenet og laver morgenmad. Du hører din kæreste vågne, og lidt efter kommer han ind i køkkenet.";
            string Soveværelse1 = "Du har lagt dig på sengen, din kæreste står i døren og siger 'Det hele er din skyld'. Du føler dig fortvivlet og fanget.";
            string Stue1 = "Du vil gerne se nyhederne, men din kæreste syntes det er spild af tid.";
            string Badeværelse1 = "Du træder ind i badet. Du vasker uroen og hans kritiske kommentarer væk med det varme vand. Kort efter hører du din kæreste træde ind.";
            
            this.AddScene(new Scene(0, "Køkken 1", Køkken1, Areas[4],
                new List<SceneChoice>
                {
                    new SceneChoice(1, "Du forholder dig stille og roligt for at undgå konflikter."),
                    new SceneChoice(2, "Du spørger om han vil have en kop kaffe."),
                    new SceneChoice(3, "Du spørger ham om han har lyst til at hjælpe med maden."),
                }));
                
            this.AddScene(new Scene(1, "Soveværelse 1", Soveværelse1, Areas[2],
                new List<SceneChoice>
                {
                    new SceneChoice(3, "Du nævner tidligere episoder, hvor han har opført sig kontrollerende."),
                    new SceneChoice(0, "Du sætter en grænse og siger 'Jeg har brug for at være alene.'"),
                    new SceneChoice(2, "Du undskylder og lytter til hvad din kæreste siger."),
                }));
            
            this.AddScene(new Scene(2, "Stue 1", Stue1, Areas[3],
                new List<SceneChoice>
                {
                    new SceneChoice(3, "Du slukker tv’et og går fra stuen."),
                    new SceneChoice(1, "Du rejser dig og går og på vejen ud siger du 'Jeg gider ikke det her lige nu'."),
                }));
            
            this.AddScene(new Scene(3, "Badeværelse 1", Badeværelse1, Areas[1],
                new List<SceneChoice>
                {
                    new SceneChoice(0, "Du siger roligt og i afmagt ‘Jeg har brug for et øjeblik alene’."),
                    new SceneChoice(1, "Du bliver forstyrret og når ikke at tænke før du udbryder ‘Vil du sige noget!?’."),
                    new SceneChoice(2, "Du undskylder og skynder dig at slukke vandet og forlade badeværelset."),
                }));
            this.LinkScenes();
        }

        /// <summary>
        /// Creates scenes and adds them to the Scenes dictionary
        /// </summary>
       public void LoadScenes()
        {
            Areas = new Dictionary<int, Area>
            {
              {0, new Area(0, "Dinmors indgang", new List<Item> {new Item (0, "mobile", "...")})},
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


        public void LoadScenes_Oli()
        {
            Areas = new Dictionary<int, Area>
            {
              {0, new Area(0, "Indkørsel")},
              {1, new Area(1, "Entré")},
              {2, new Area(2, "Køkken alrum")},
              {3, new Area(3, "Stue")},
              {4, new Area(4, "Soveværelse")},
              {5, new Area(5, "Tarresse")},
              {6, new Area(6, "Badeværelset")},
              {7, new Area(7, "Haven")},
            };

            string dummyDialougeText = "Dette er noget tekst som er den del af denne sygt nice historie som vi har skrvete, WOW det er en god historie var? og shit hvor skal denne tekst være lang sådan man virkelig kan se at der er gjordt noget ud af denne blockbuster storyline vi har lavet her. tak for idag husk og like og subscribe";

            // Scene: Start 1 Location: Indkørsel
            this.AddScene(new Scene(0, "Starten 1", dummyDialougeText, Areas[0],
                new List<SceneChoice> {
                    new SceneChoice(1, "Gå ind af hovedøren"),
                    new SceneChoice(2, "Smid dine ting foran døren og gå om på terassen")
                }));

            // Scene 2: Starten 2; Location: Entré
            this.AddScene(new Scene(1, "Starten 2", dummyDialougeText, Areas[1],
                new List<SceneChoice> {
                    new SceneChoice(4, "Gå ind i stuen og hils på din mand"),
                    new SceneChoice(3, "Gå direkte til dit værelse")
                }));

            // Scene 3: Starten 3; Location: Terrassen
            this.AddScene(new Scene(2, "Starten 3", dummyDialougeText, Areas[5],
                new List<SceneChoice> {
                    new SceneChoice(5, "Gå ud i haven"),
                    new SceneChoice(6, "Gå ind i køkken alrummet")
                }));




            // Scene 4: Starten 4; Location: Soveværelse
            this.AddScene(new Scene(3, "Starten 4", dummyDialougeText, Areas[4],
                new List<SceneChoice> {
                    new SceneChoice(2, "Åben døren og gå ud i stuen"),
                }));

            // Scene 5: Starten 5; Location: Stue
            this.AddScene(new Scene(4, "Starten 5", dummyDialougeText, Areas[3],
                new List<SceneChoice> {
                    new SceneChoice(2, "Gå på toilettet"),
                }));


            // Scene 6: Starten 6; Location: Haven
            this.AddScene(new Scene(5, "Starten 6", dummyDialougeText, Areas[7],
                new List<SceneChoice> {
                    new SceneChoice(7, "END"),
                }));

            // Scene 7: Starten 7; Location: Køkken alrum
            this.AddScene(new Scene(6, "Starten 7", dummyDialougeText, Areas[2],
                new List<SceneChoice> {
                    new SceneChoice(7, "END"),
                }));


            //DUMMY END SCENE
            this.AddScene(new Scene(7, "END", "!!!END!!!", Areas[0],new List<SceneChoice> {}));

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
        /// <param name="filePath">The filepath including filename where teh file will be saved</param>
        /// <returns></returns>
        public void ExportScenesToFile(string filePath = "./EXPORTED.json")
        {
            string jsonStr = JsonSerializer.Serialize(Scenes);

            File.WriteAllText(filePath, jsonStr);
        }

    }
}
