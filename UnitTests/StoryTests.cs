namespace UnitTests
{
    using cs.Domain;
    using cs.Domain.Player;
    using cs.Domain.Story;
    using cs.Persistance;
    using cs.Presentation;
    using NUnit.Framework;
    using System.Security.Cryptography.X509Certificates;

    public class StoryTests
    {
        int input = 0;
        string sInput = "mobil";
        
        private Story story;
        private Item item;
        private Area area;
        private Scene scene;

        [SetUp]
        public void Setup()
        {
            story = new Story();
            item = new Item("mobil", "En smartphone");
            area = new Area("værelse");
            scene = new ContextScene(
                "Køkken 1", 
                "Køkken tekst",
                new List<SceneChoice> 
                {
                    new SceneChoice(1, 5, 5, "Choice 1")
                }, 
                story.Areas[0]
            );
        }

        [Test]
        public void FindSceneTest()
        {
            story.AddScene( scene );

            Scene? result = story.FindScene(input);

            Assert.IsNotNull(result, "The Method should return null if theres no Scene that matches the given ID");

            Assert.That(result, Is.EqualTo(scene));
        }


        [Test]
        public void FindItemByNameTest()
        {
            story.AddArea( area );
            story.AddItem( item );

            Item? result = story.FindItemByName(sInput);

            Assert.That(result, Is.EqualTo(item));
        }
    }
}