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
        private UITerminal uiTerminal;
        private StoryHandler storyHandler;
        private Registry registry;
        private CommandMove command;
        private string[] commandNames;

        [SetUp]
        public void Setup()
        {
            // Sets up UITerminal
            uiTerminal = new UITerminal();

            // Sets up the StoryHandler
            storyHandler = new StoryHandler(uiTerminal, new JsonDataProvider());

            // Sets the player object to the storyHandler player
            storyHandler.player = new cs.Domain.Player.Player("Name");

            // Sets up the registry object class
		    registry = new Registry(storyHandler, new CommandUnknown()); 
                        
            // Make a new CommandMove Object
            command = new CommandMove();   

            // Make a list of the command names 
            commandNames = new string[] {"bevæg", "go", "gå"};
        }

        public string GetTerminalOutput(string input)
        {
            // Sets up a StringWriter class that writes to a StringBuilder, where the Console.SetOut give the StringWriter the char it should put into the strinbuilder.
            var output1 = new StringWriter();
            Console.SetOut(output1);
            
            // Run the registry dispatch method
            registry.Dispatch(input);

            // Convert the stringwriter into a string
            return output1.ToString().Trim();
        }

        [Test]
        public void TestInitialization()
        {
            // Get the output from the terminal
            string consoleOutput1 = GetTerminalOutput("bevæg");
            
            // Test if the output to the terminal is correct
            Assert.AreEqual("Woopsie, forstår ikke 'bevæg' 😕", consoleOutput1, "Registry initilazation failed");
        }

        [Test]
        public void TestCommandInsertion()
        { 
            // Insert the command into the registry dictionary
            registry.Register(commandNames, new CommandMove());
            
            // Use the GetCommandNames and a new array to make sure it's actually the names from the registry
            string[] newCommandNames = registry.GetCommandNames();
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual("bevæg", newCommandNames[0], "Command name 'bevæg' not working");
                Assert.AreEqual("go", newCommandNames[1], "Command name 'go' not working"); 
                Assert.AreEqual("gå", newCommandNames[2], "Command name 'gå' not working");                
            });
        }
        //TMP = Too Many Parameters or none
        [Test]
        public void TestCommandTMPExecution()
        {
            
            registry.Register(commandNames, new CommandMove());
            
            string consoleOutput1 = GetTerminalOutput("bevæg"); 
            string consoleOutput2 = GetTerminalOutput("bevæg 2 3");

            // Test to see if the command matches the string below
            Assert.Multiple(() =>
            {
                Assert.AreEqual("For mange argumenter!", consoleOutput1, "The command feedback for non parameters failed"); 
                Assert.AreEqual("For mange argumenter!", consoleOutput2, "The command for too many parameters failed");
            });
        }
        
        [Test]
        public void TestCommandInvalidChoice()
        {
            // Insert the command into the registry dictionary
            registry.Register(commandNames, new CommandMove());
            
            // Convert the stringwriter into a string
            string consoleOutput1 = GetTerminalOutput("Bevæg 5");
            
            // Test if the output to the terminal is correct
            Assert.AreEqual("5 er ikke et gyldigt valg!", consoleOutput1, "The invalid message failed"); 
        }

        [Test]
        public void TestCommandAction()
        {
            // Start the story  
            storyHandler.StartStory();

            // Get the current scene
            Scene scene1 = storyHandler.GetCurrentScene();
            
            // Insert the CommandMove into registry
            registry.Register(commandNames, new CommandMove());

            // Execute the CommandMove through the dispatch method
            registry.Dispatch("bevæg 2");

            // Get the new current scene
            Scene scene2 = storyHandler.GetCurrentScene();

            // Check if the two current scenes are different
            Assert.AreNotEqual(scene1.Name, scene2.Name, "The CommandMove execution failed");
        }
    }
}
