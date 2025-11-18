namespace cs
{
    using cs.MapTerminal;

    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Text.Encodings.Web;
    using System.Text.Json;
    using System.Text.Unicode;

    /// <summary>
    /// This class controls everything to do with building and loading a story (Collection of scenes)
    /// </summary>
    public class DataLoader
    {
        public Story story { get; set; }
        public string dataFilePath { get; set; }

        public DataLoader(string dataFilePath = "./StoryDat.json")
        {
            this.dataFilePath = dataFilePath;
            story = new Story();
        }

        public void Load()
        {
            this.LoadStoryFromFile();

            //this.LoadAreas();
            //this.LoadScenes();
            //this.LoadMapElements();
            //this.story.Name = "Test Story";


            this.LinkScenes();
            // this.LinkMapLayout();
        }

        private void LoadStoryFromFile()
        {
            // First check if a file already exists in the directory
            if (!File.Exists(this.dataFilePath))
            {
                // If it does not exist we generate it with a empty dictionary of scenes.
                string dat = JsonSerializer.Serialize<Story>(new Story());
                //string dat = JsonSerializer.Serialize<Dictionary<int, Scene>>(this.Scenes);

                // Creates the file and appends the json
                File.AppendAllText(this.dataFilePath, dat);
            }

            // Read the text of the file
            string tmpJsonStr = File.ReadAllText(this.dataFilePath);

            // We load the deserialized scenes into the scenes property
            // Maby handle a null value here.
            this.story = JsonSerializer.Deserialize<Story>(tmpJsonStr);
        }

        /// <summary>
        /// Resolves all SceneChoice references by linking to their target Scene-objects.
        /// This is needed, as a scene object may not exist when we want to assign the scene to the scene object within the Choices of the scene.
        /// </summary>
        private void LinkScenes()
        {
            // Loop through all scenes
            foreach (Scene scene in story.Scenes.Values)
            {
                if (scene is ContextScene contextScene)
                {

                    // only if area object is not instanciated
                    if (contextScene.Area == null)
                    {
                        if (story.Areas.TryGetValue(contextScene.AreaId, out Area area))
                        {
                            contextScene.Area = area;
                        }
                    }

                    // Loop through all scenechoices in theese scenes
                    foreach (SceneChoice sceneChoice in contextScene.Choices)
                    {
                        // try to resolve the name of the scene with a scene object
                        if (story.Scenes.TryGetValue(sceneChoice.SceneId, out Scene OutScene))
                        {
                            if (OutScene is ContextScene targetScene)
                            {
                                sceneChoice.SceneObj = targetScene;
                            }
                        }
                    }
                }
            }
        }

        public void LoadAreas()
        {
            story.Areas = new Dictionary<int, Area>
            {
                {0, new Area(0, "Entré")},
                {1, new Area(1, "Badeværelse")},
                {2, new Area(2,"Soveværelse")},
                {3, new Area(3,"Stue")},
                {4, new Area(4,"Køkken", new List<Item> {new Item(1, "Mobil", "En mobiltelefon")})},
            };
        }

        public void LoadMapElements()
        {
            this.story.MapElements = new List<MapElement>
            {
                new MapRoomElement(0, 2, 2, 12, 10, "Entré"),
                new MapRoomElement(1, 49, 2, 8, 12, "Badeværelse"),
                new MapRoomElement(2, 11, 2, 8, 20, "Soveværelse"),
                new MapRoomElement(3, 30, 2, 8, 20, "Stue"),
                new MapRoomElement(4, 2, 13, 9, 40, "Køkken"),
                new MapRoomElement(5, 11, 9, 5, 50, "Gang"),
                //new MapTextElement(7, 40, 10, "Cock and balls :3") {Color = ConsoleColor.Cyan}
            };
        }

        public void LoadScenes()
        {
            story.Areas = new Dictionary<int, Area>
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

            story.Scenes = new Dictionary<int, Scene>
            {
                { 0, new ContextScene(0, "Køkken 1", 5, Køkken1,
                 new List<SceneChoice>
                 {
                     new SceneChoice(123, "Du forholder dig stille og roligt for at undgå konflikter."),
                     new SceneChoice(2, "Du spørger, om han vil have en kop kaffe."),
                     new SceneChoice(3, "Du spørger ham om han har lyst til at hjælpe med maden."),
                     new SceneChoice(4, "Action baby"),
                }, story.Areas[4]) },

                {1,  new ContextScene(1, "Soveværelse 1", 5, Soveværelse1,
                new List<SceneChoice>
                {
                    new SceneChoice(3, "Du nævner tidligere episoder, hvor han har opført sig kontrollerende."),
                    new SceneChoice(0, "Du sætter en grænse og siger 'Jeg har brug for at være alene.'"),
                    new SceneChoice(2, "Du undskylder og lytter til hvad din kæreste siger."),
                }, story.Areas[2], 1)},

                {2,  new ContextScene(2, "Stue 1", 3,  Stue1,
                    new List<SceneChoice>
                    {
                        new SceneChoice(3, "Du slukker tv’et og går fra stuen."),
                        new SceneChoice(1, "Du rejser dig og går og på vejen ud siger du 'Jeg gider ikke det her lige nu'."),
                    }, story.Areas[3])
                },

                {3, new ContextScene(3, "Badeværelse 1", -3, Badeværelse1,
                    new List<SceneChoice>
                    {
                        new SceneChoice(0, "Du siger roligt og i afmagt ‘Jeg har brug for et øjeblik alene’."),
                        new SceneChoice(1, "Du bliver forstyrret og når ikke at tænke, før du udbryder ‘Vil du sige noget!?’."),
                        new SceneChoice(2, "Du undskylder og skynder dig at slukke vandet og forlade badeværelset."),
                    }, story.Areas[1])
                },
            
                { 4, new CutScene(4, "Seje reje", "Yo stupid ass did not just do that", 1)}
            };

            this.LinkScenes();
        }


        /// <summary>
        /// Serializes a Dictoinary of scenes to json, and saves in a file
        /// </summary>
        /// <param name="filePath">The filepath including filename where teh file will be saved</param>
        /// <returns></returns>
        public void ExportStoryToFile(string filePath = "./EXPORTED_STORY.json")
        {
            string jsonStr = JsonSerializer.Serialize<Story>(this.story, new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping});

            File.WriteAllText(filePath, jsonStr);
        }

    }
}
