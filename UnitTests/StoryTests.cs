namespace UnitTests
{
    using cs.Domain;
    using cs.Domain.Player;
    using cs.Domain.Story;
    using NUnit.Framework;
    using NUnit.Framework.Interfaces;

    public class StoryTests
    {
        //Inputs for tests
        int Sceneinput = 0;
        string SceneNameInput = "Køkken 1";
        int ItemInput = 0;
        string ItemNameInput = "mobil";
        int AreaInput = 0;
        string AreaNameInput = "værelse";

        //Variables that will be used for tests
        private Story story;
        private Item item;
        private Area area;
        private Scene scene;
        private Dictionary<int, Item> items;

        //Setup method to initialize test variables and adding them to Story
        [SetUp]
        public void Setup()
        {
            story = new Story();
            item = new Item("mobil", "En smartphone");
            items = new Dictionary<int, Item>();
            area = new Area("værelse", items);
            scene = new ContextScene(
                "Køkken 1",
                5,
                "Køkken tekst",
                area
            );

            story.AddArea(area);
            story.AddScene(scene);
            story.AddItem(item);
        }


        //Test 1: Adding Scene
        [Test]
        public void AddSceneTest()
        {

            Assert.That(story.Scenes.ContainsValue(scene));
        }

        //Test 2: Adding Area
        [Test]
        public void AddAreaTest()
        {
            Assert.That(story.Areas.ContainsValue(area));
        }

        //Test 3: Adding Item
        [Test]
        public void AddItemTest()
        {
            Assert.That(story.Items.ContainsValue(item));
        }


        //Test 4: Finding Scene by ID
        [Test]
        public void FindSceneByIDTest()
        {
            Scene? result = story.FindScene<Scene>(Sceneinput);

            Assert.That(result, Is.EqualTo(scene));
        }

        //Test 5: Finding Scene by Name
        [Test]
        public void FindSceneByNameTest()
        {
            Scene result = story.FindScene<Scene>(SceneNameInput);

            Assert.That(result, Is.EqualTo(scene));
        }


        //Test 6: Finding Item by ID
        [Test]
        public void FindItemByIDTest()
        {
            Item result = story.FindItem(ItemInput);

            Assert.That(result, Is.EqualTo(item));
        }

        //Test 7: Finding Item by Name
        [Test]
        public void FindItemByNameTest()
        {
            Item result = story.FindItem(ItemNameInput);

            Assert.That(result, Is.EqualTo(item));
        }


        //Test 8: Finding Area by ID
        [Test]
        public void FindAreaByIDTest()
        {
            Area result = story.FindArea(AreaInput);

            Assert.That(result, Is.EqualTo(area));
        }

        //Test 9: Finding Area by Name
        [Test]
        public void FindAreaByNameTest()
        {
            Area result = story.FindArea(AreaNameInput);

            Assert.That(result, Is.EqualTo(area));
        }
    }
}