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
            //Resets the ID counters before each test so we know the ID starts at 0
            Scene.ResetIdCounter();
            Area.ResetIdCounter();

            //Initializes the test variables that will be used in the tests
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
        }

        #region Tests
        #region Add Tests

        //TEST 1: Adding Scene
        //We want to verify that adding a scene to the story correctly adds it to the Scenes dictionary.
        [Test]
        public void AddSceneTest()
        {
            //ARRANGE & ACT: Adds the scene to the story
            story.AddScene(scene);

            //ASSERT: Verifies that the scene was added to the Scenes dictionary
            Assert.That(story.Scenes.ContainsValue(scene));
        }

        //TEST 2: Adding Area
        //We want to verify that adding an area to the story correctly adds it to the Areas dictionary.
        [Test]
        public void AddAreaTest()
        {
            //ARRANGE & ACT: Adds the area to the story
            story.AddArea(area);

            //ASSERT: Verifies that the area was added to the Areas dictionary
            Assert.That(story.Areas.ContainsValue(area));
        }

        //TEST 3: Adding Item
        //We want to verify that adding an item to the story correctly adds it to the Items dictionary.
        [Test]
        public void AddItemTest()
        {
            //ARRANGE & ACT: Adds the item to the story
            story.AddItem(item);

            //ASSERT: Verifies that the item was added to the Items dictionary
            Assert.That(story.Items.ContainsValue(item));
        }
        #endregion

        #region Find Scene Tests

        //TEST 4: Finding Scene by ID
        //We want to verify that finding a scene by its ID returns the correct scene.
        [Test]
        public void FindSceneByIDTest()
        {
            //ARRANGE: Adds the scene to the story
            story.AddScene(scene);

            //ACT: Finds the scene by its ID
            Scene result = story.FindScene<Scene>(Sceneinput);

            //ASSERT: Verifies that the found scene is the same as the one we added
            Assert.That(result, Is.EqualTo(scene));
        }

        //TEST 5: Finding Scene by Name
        //We want to verify that finding a scene by its Name returns the correct scene.
        [Test]
        public void FindSceneByNameTest()
        {
            //ARRANGE: Adds the scene to the story
            story.AddScene(scene);

            //ACT: Finds the scene by its name
            Scene result = story.FindScene<Scene>(SceneNameInput);

            //ASSERT: Verifies that the found scene is the same as the one we added
            Assert.That(result, Is.EqualTo(scene));
        }
        #endregion

        #region Find Item Tests

        //TEST 6: Finding Item by ID
        //We want to verify that finding an item by its ID returns the correct item.
        [Test]
        public void FindItemByIDTest()
        {
            //ARRANGE: Adds the item to the story
            story.AddItem(item);

            //ACT: Finds the item by its ID
            Item result = story.FindItem(ItemInput);

            //ASSERT: Verifies that the found item is the same as the one we added
            Assert.That(result, Is.EqualTo(item));
        }

        //TEST 7: Finding Item by Name
        //We want to verify that finding an item by its Name returns the correct item.
        [Test]
        public void FindItemByNameTest()
        {
            //ARRANGE: Adds the item to the story
            story.AddItem(item);

            //ACT: Finds the item by its Name
            Item result = story.FindItem(ItemNameInput);

            //ASSERT: Verifies that the found item is the same as the one we added
            Assert.That(result, Is.EqualTo(item));
        }
        #endregion

        #region Find Area Tests

        //TEST 8: Finding Area by ID
        //We want to verify that finding an area by its ID returns the correct area.
        [Test]
        public void FindAreaByIDTest()
        {
            //ARRANGE: Adds the area to the story
            story.AddArea(area);

            //ACT: Finds the area by its ID
            Area result = story.FindArea(AreaInput);

            //ASSERT: Verifies that the found area is the same as the one we added
            Assert.That(result, Is.EqualTo(area));
        }

        //TEST 9: Finding Area by Name
        //We want to verify that finding an area by its Name returns the correct area.
        [Test]
        public void FindAreaByNameTest()
        {
            //ARRANGE: Adds the area to the story
            story.AddArea(area);

            //ACT: Finds the area by its Name
            Area result = story.FindArea(AreaNameInput);

            //ASSERT: Verifies that the found area is the same as the one we added
            Assert.That(result, Is.EqualTo(area));
        }
        #endregion
        #endregion
    }
}