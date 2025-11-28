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
    using System.Text.Encodings.Web;
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
        public Story GetStory()
        {
            return Story;
        }

        public void SetStory(Story story)
        {
            throw new NotImplementedException();
        }

        public void ExportTestStory()
        {
            string jsonStr = JsonSerializer.Serialize<Story>(Story, new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping });

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
            Story.AddArea(new Area("Entré", frame: new Frame(2, 2, 12, 10)));
            Story.AddArea(new Area("Badeværelse", frame: new Frame(49, 2, 8, 12)));
            Story.AddArea(new Area("Soveværelse", frame: new Frame(11, 2, 8, 20)));
            Story.AddArea(new Area("Stue", frame: new Frame(30, 2, 8, 20)));
            Story.AddArea(new Area("Køkken", frame: new Frame(2, 13, 9, 40))
                .AddItem(Story.FindItem("mobil"))
                .AddItem(Story.FindItem("Pandekage"))
                .AddItem(Story.FindItem("Toilet nøgle")));
            Story.AddArea(new Area("Gang", frame: new Frame(11, 9, 5, 50)));


            // Add Scenes to test story
            string køkken1_desc = "Du står i køkkenet og laver morgenmad. Du hører din kæreste vågne, og lidt efter kommer han ind i køkkenet.";
            string soveværelse1_desc = "Du har lagt dig på sengen, din kæreste står i døren og siger 'Det hele er din skyld'. Du føler dig fortvivlet og fanget.";
            string stue1_desc = "Du vil gerne se nyhederne, men din kæreste syntes det er spild af tid.";
            string badeværelse1_desc = "Du træder ind i badet. Du vasker uroen og hans kritiske kommentarer væk med det varme vand. Kort efter hører du din kæreste træde ind.";

            ContextScene køkken1 = new ContextScene("Køkken 1", 5, køkken1_desc, Story.Areas[4]);
            Story.AddScene(køkken1);

            ContextScene soveværelse1 = new ContextScene("Soveværelse 1", 5, soveværelse1_desc, Story.Areas[2]);
            Story.AddScene(soveværelse1);

            ContextScene stue1 = new ContextScene("Stue 1", 3, stue1_desc, Story.Areas[3]);
            Story.AddScene(stue1);

            ContextScene badeværelse1 = new ContextScene("Badeværelse 1", -3, badeværelse1_desc, Story.Areas[1]);
            Story.AddScene(badeværelse1);

            CutScene sejereje = new CutScene("Seje reje", "Yo stupid ass did not just do that", 1);
            Story.AddScene(sejereje);


            // Now that all scenes have been added to story, we can add scene choices to scenes (This is done as a scene mostly linkes "forward" to a scene not yet created)
            // ===== KØKKEN 1 ===== //
            køkken1.AddSceneChoice(new SceneChoice(Story.FindScene<CutScene>("Seje reje"), 1, 2, "Du forholder dig stille og roligt for at undgå konflikter.", Story.FindItem("mobil")));
            køkken1.AddSceneChoice(new SceneChoice(Story.FindScene<ContextScene>("Stue 1"), 2, 10, "Du spørger, om han vil have en kop kaffe."));
            køkken1.AddSceneChoice(new SceneChoice(Story.FindScene<ContextScene>("Badeværelse 1"),5, -2, "Du spørger ham om han har lyst til at hjælpe med maden."));
            køkken1.AddSceneChoice(new SceneChoice(Story.FindScene<CutScene>("Seje reje"), 5, -2, "Action baby"));

            // ===== Soveværelse 1 ===== //
            soveværelse1.AddSceneChoice(new SceneChoice(Story.FindScene<ContextScene>("Badeværelse 1"), 5, -2, "Du nævner tidligere episoder, hvor han har opført sig kontrollerende."));
            soveværelse1.AddSceneChoice(new SceneChoice(Story.FindScene<ContextScene>("Køkken 1"), 5, -2, "Du sætter en grænse og siger 'Jeg har brug for at være alene.'"));
            soveværelse1.AddSceneChoice(new SceneChoice(Story.FindScene<ContextScene>("Stue 1"), 5, -2, "Du undskylder og lytter til hvad din kæreste siger."));

            // ===== Stue 1 ===== //
            stue1.AddSceneChoice(new SceneChoice(Story.FindScene<ContextScene>("Badeværelse 1"), 5, 4, "Du slukker tv’et og går fra stuen."));
            stue1.AddSceneChoice(new SceneChoice(Story.FindScene<ContextScene>("Soveværelse 1"), 5, 2, "Du rejser dig og går og på vejen ud siger du 'Jeg gider ikke det her lige nu'.", Story.FindItem("pandekage")));
            // Create EndScene test
            string endSceneContentInfo =
            "---------=======================================================================================---------\n" +
            "Tak fordi du spillede!\n\n" +
            "Dette spil er skabt for at skabe opmærksomhed omkring psykisk vold mod kvinder.\n" +
            "Vi håber, at du gennem disse valg og scenarier har fået indblik i,\n" +
            "hvordan psykisk vold kan påvirke et menneske - både synligt og usynligt.\n\n" +
            "Husk: Du er ikke alene. Der findes altid hjælp.\n" +
            "Hvis du kender nogle eller har selv oplevet lignende situationer,\n" +
            "så tøv ikke med at række ud for støtte og hjælp.\n" +
            "Lev Uden Volds Hotline: 1888\n" +
            "---------=======================================================================================---------\n\n" +
            "Vil du gerne prøve igen? (ja/nej)\n> ";

            EndScene endScene = new EndScene("Endscene", endSceneContentInfo);
            Story.AddScene(endScene);

            // ===== Badeværelse 1 ===== //
            badeværelse1.AddSceneChoice(new SceneChoice(Story.FindScene<ContextScene>("Køkken 1"), 5, 2, "Du siger roligt og i afmagt ‘Jeg har brug for et øjeblik alene’."));
            badeværelse1.AddSceneChoice(new SceneChoice(Story.FindScene<ContextScene>("Soveværelse 1"), 5, -2, "Du bliver forstyrret og når ikke at tænke, før du udbryder ‘Vil du sige noget!?’."));
            badeværelse1.AddSceneChoice(new SceneChoice(Story.FindScene<ContextScene>("Stue 1"), 5, 8, "Du undskylder og skynder dig at slukke vandet og forlade badeværelset."));
        }

        public void ReloadStory()
        {
            Area.ResetIdCounter();
            Item.ResetIdCounter();
            Scene.ResetIdCounter();

            BuildTestStory();
        }
    }
}
