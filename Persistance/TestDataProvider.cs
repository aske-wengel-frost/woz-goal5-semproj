namespace cs.Persistance
{
    using cs.Domain;
    using cs.Presentation.MapTerminal;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class TestDataProvider : IDataProvider
    {
        private Story Story;
        public TestDataProvider()
        {
            Story = new Story();
        }
        public Story getStory()
        {
            throw new NotImplementedException();
        }

        public void setStory(Story story)
        {
            throw new NotImplementedException();
        }

        // HELPERS
        public void LoadAreas()
        {
            Story.Areas = new Dictionary<int, Area>
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
            this.Story.MapElements = new List<MapElement>
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
            Story.Areas = new Dictionary<int, Area>
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

            Story.Scenes = new Dictionary<int, Scene>
            {
                { 0, new ContextScene(0, "Køkken 1", 5, Køkken1,
                 new List<SceneChoice>
                 {
                     new SceneChoice(4, "Du forholder dig stille og roligt for at undgå konflikter.", 1),
                     new SceneChoice(2, "Du spørger, om han vil have en kop kaffe."),
                     new SceneChoice(3, "Du spørger ham om han har lyst til at hjælpe med maden."),
                     new SceneChoice(4, "Action baby"),
                }, Story.Areas[4]) },

                {1,  new ContextScene(1, "Soveværelse 1", 5, Soveværelse1,
                new List<SceneChoice>
                {
                    new SceneChoice(3, "Du nævner tidligere episoder, hvor han har opført sig kontrollerende."),
                    new SceneChoice(0, "Du sætter en grænse og siger 'Jeg har brug for at være alene.'"),
                    new SceneChoice(2, "Du undskylder og lytter til hvad din kæreste siger."),
                }, Story.Areas[2])},

                {2,  new ContextScene(2, "Stue 1", 3,  Stue1,
                    new List<SceneChoice>
                    {
                        new SceneChoice(3, "Du slukker tv’et og går fra stuen."),
                        new SceneChoice(1, "Du rejser dig og går og på vejen ud siger du 'Jeg gider ikke det her lige nu'.", 5),
                    }, Story.Areas[3])
                },

                {3, new ContextScene(3, "Badeværelse 1", -3, Badeværelse1,
                    new List<SceneChoice>
                    {
                        new SceneChoice(0, "Du siger roligt og i afmagt ‘Jeg har brug for et øjeblik alene’."),
                        new SceneChoice(1, "Du bliver forstyrret og når ikke at tænke, før du udbryder ‘Vil du sige noget!?’."),
                        new SceneChoice(2, "Du undskylder og skynder dig at slukke vandet og forlade badeværelset."),
                    }, Story.Areas[1])
                },

                { 4, new CutScene(4, "Seje reje", "Yo stupid ass did not just do that", 1)}
            };

            //this.LinkScenes();
        }
    }
}
