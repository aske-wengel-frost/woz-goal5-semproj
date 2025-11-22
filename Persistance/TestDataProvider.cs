namespace cs.Persistance
{
    using cs.Domain;
    using cs.Domain.Player;
    using cs.Domain.Story;
    using cs.Presentation.MapTerminal;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    class TestDataProvider : IDataProvider
    {
        private Story Story;
        public TestDataProvider()
        {
            Story = new Story();
            BuildTestStory();
        }
        public Story getStory()
        {
            return Story;
        }

        public void setStory(Story story)
        {
            throw new NotImplementedException();
        }

        public void exportTestStory()
        {
            string jsonStr = JsonSerializer.Serialize<Story>(Story);

            File.WriteAllText("EXPORTED.json", jsonStr);
        }

        // HELPERS

        private void BuildTestStory()
        {
            // Add items to story
            Story.AddItem(new Item("Mobil", "En smartphone"));
            Story.AddItem(new Item("Toilet nøgle", "En nøgle til toilettet"));
            Story.AddItem(new Item("Pandekage", "MMMM mums en pandekage"));

            // Add Areas to story
            Story.AddArea(new Area("Entré"));
            Story.AddArea(new Area("Badeværelse"));
            Story.AddArea(new Area("Soveværelse"));
            Story.AddArea(new Area("Stue"));
            Story.AddArea(new Area("Køkken").AddItem(Story.FindItemByName("mobil")).AddItem(Story.FindItemByName("Pandekage")));


            string Køkken1 = "Du står i køkkenet og laver morgenmad. Du hører din kæreste vågne, og lidt efter kommer han ind i køkkenet.";
            string Soveværelse1 = "Du har lagt dig på sengen, din kæreste står i døren og siger 'Det hele er din skyld'. Du føler dig fortvivlet og fanget.";
            string Stue1 = "Du vil gerne se nyhederne, men din kæreste syntes det er spild af tid.";
            string Badeværelse1 = "Du træder ind i badet. Du vasker uroen og hans kritiske kommentarer væk med det varme vand. Kort efter hører du din kæreste træde ind.";

            // Add Scenes to test story
            Story.AddScene(new ContextScene(
                "Køkken 1", 
                5,
                Køkken1,
                new List<SceneChoice> 
                {
                    new SceneChoice(4, "Du forholder dig stille og roligt for at undgå konflikter.", Story.FindItemByName("mobil")),
                    new SceneChoice(2, "Du spørger, om han vil have en kop kaffe."),
                    new SceneChoice(3, "Du spørger ham om han har lyst til at hjælpe med maden."),
                    new SceneChoice(4, "Action baby"),
                }, 
                Story.Areas[4]
            ));

            Story.AddScene(new ContextScene(
                "Soveværelse 1",
                5,
                Soveværelse1,
                new List<SceneChoice>
                {
                    new SceneChoice(3, "Du nævner tidligere episoder, hvor han har opført sig kontrollerende."),
                    new SceneChoice(0, "Du sætter en grænse og siger 'Jeg har brug for at være alene.'"),
                    new SceneChoice(2, "Du undskylder og lytter til hvad din kæreste siger."),
                },
                Story.Areas[2]
            ));

            Story.AddScene(new ContextScene(
                "Stue 1",
                3,
                Stue1,
                new List<SceneChoice>
                {
                    new SceneChoice(3, "Du slukker tv’et og går fra stuen."),
                    new SceneChoice(1, "Du rejser dig og går og på vejen ud siger du 'Jeg gider ikke det her lige nu'.", Story.FindItemByName("pandekage")),
                },
                Story.Areas[3]
            ));

            Story.AddScene(new ContextScene(
                "Badeværelse 1",
                -3,
                Badeværelse1,
                new List<SceneChoice>
                {
                    new SceneChoice(0, "Du siger roligt og i afmagt ‘Jeg har brug for et øjeblik alene’."),
                    new SceneChoice(1, "Du bliver forstyrret og når ikke at tænke, før du udbryder ‘Vil du sige noget!?’."),
                    new SceneChoice(2, "Du undskylder og skynder dig at slukke vandet og forlade badeværelset."),
                },
                Story.Areas[1]
            ));

             Story.AddScene(new CutScene("Seje reje", "Yo stupid ass did not just do that", 1));


            // Add Map Elements to story
            Story.AddMapElement(new MapRoomElement(0, 2, 2, 12, 10, "Entré"));
            Story.AddMapElement(new MapRoomElement(1, 49, 2, 8, 12, "Badeværelse"));
            Story.AddMapElement(new MapRoomElement(2, 11, 2, 8, 20, "Soveværelse"));
            Story.AddMapElement(new MapRoomElement(3, 30, 2, 8, 20, "Stue"));
            Story.AddMapElement(new MapRoomElement(4, 2, 13, 9, 40, "Køkken"));
            Story.AddMapElement(new MapRoomElement(5, 11, 9, 5, 50, "Gang"));
        }
    }
}
