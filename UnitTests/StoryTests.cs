namespace UnitTests
{
    using cs.Domain;
    using cs.Domain.Player;
    using cs.Domain.Story;
    using cs.Persistance;
    using cs.Presentation;
    using NUnit.Framework;
    using System.Security.Cryptography.X509Certificates;

    public class Tests
    {
        int input = 10;
        string sInput = "mobil";
        

        [SetUp]
        public void Setup()
        {
            Story story = new Story();
            Item item = new Item("mobil", "En smartphone");
            Area area = new Area("værelse");
            Scene scene = new ContextScene(
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
        public void Test1()
        {
            object? result = story.FindScene(input);
            if (result is null)
            {
                Assert.Fail();
            }
            else { Assert.Pass(); }
        }

        [Test]
        public void Test2()
        {
            object? result = story.FindItemByName(sInput);
            if (result is Item)
            {
                Assert.Pass();
            }
        }
    }
}