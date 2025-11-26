using System;
using System.IO;
using cs.Domain;
using cs.Domain.Story;
using cs.Domain.Commands;
using cs.Presentation;
using cs.Persistance;
namespace UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Registry registry = new Registry(new StoryHandler(new UITerminal(), new JsonDataProvider()), new CommandUnknown());
            // Arrange the Console.Out, so capture the text displayed onto the terminal into output1
            var output1 = new StringWriter();
            var output2 = new StringWriter();
            Console.SetOut(output1);
            
            // Run the registry dispatch method
            registry.Dispatch("bevæg");

            // Convert the stringwriter into a string
            string consoleOutput1 = output1.ToString().Trim();

            // We now arrange the Console.Out with output2
            Console.SetOut(output2);
            registry.Register(new [] {"bevæg", "go", "gå"}, new CommandMove());
            
            // We run the dispath method again
            registry.Dispatch("bevæg");

            string consoleOutput2 = output2.ToString().Trim();
            
            // Test if the output to the terminal is correct
            Assert.Multiple(() =>
            {
                Assert.AreEqual("Woopsie, forstår ikk 'bevæg' 😕", consoleOutput1); 
                Assert.AreEqual("For mange argumenter!", consoleOutput2);
            });
        }
    }
}
