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
        private Registry registry;

        [SetUp]
        public void Setup()
        {
		    registry = new Registry(new StoryHandler(new UITerminal(), new JsonDataProvider()), new CommandUnknown());            
        }

        [Test]
        public void TestInitialization()
        {
            // Arrange the Console.Out, so capture the text displayed onto the terminal into output1
            var output1 = new StringWriter();
            Console.SetOut(output1);
            
            // Run the registry dispatch method
            registry.Dispatch("bevæg");

            // Convert the stringwriter into a string
            string consoleOutput1 = output1.ToString().Trim();
            
            // Test if the output to the terminal is correct
            Assert.AreEqual("Woopsie, forstår ikke 'bevæg' 😕", consoleOutput1); 
        }

        [Test]
        public void TestCommandInsertion()
        { 
            // Make a new CommandMove Object
            CommandMove command = new CommandMove();   

            // Make a list of the command names 
            string[] commandNames = {"bevæg", "go", "gå"};

            // Insert the command into the registry dictionary
            registry.Register(commandNames, new CommandMove());
            
            // Use the GetCommandNames and a new array to make sure it's actually the names from the registry
            string[] newCommandNames = registry.GetCommandNames();
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual("bevæg", newCommandNames[0]);
                Assert.AreEqual("go", newCommandNames[1]); 
                Assert.AreEqual("gå", newCommandNames[2]);                
            });
        }

        [Test]
        public void TestCommandErrorExecution()
        {
            var output1 = new StringWriter();
            Console.SetOut(output1);
            
            // Run the registry dispatch method
            registry.Dispatch("bevæg");

            // Convert the stringwriter into a string
            string consoleOutput1 = output1.ToString().Trim();
            
            // Test if the output to the terminal is correct
            Assert.AreEqual("For mange argumenter!", consoleOutput1); 
        }
    }
}
