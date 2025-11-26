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
        private CommandMove command;
        private string[] commandNames;

        [SetUp]
        public void Setup()
        {
            // Sets up the registry object class
		    registry = new Registry(new StoryHandler(new UITerminal(), new JsonDataProvider()), new CommandUnknown()); 
                        
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
            Assert.AreEqual("Woopsie, forstår ikke 'bevæg' 😕", consoleOutput1); 
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
                Assert.AreEqual("bevæg", newCommandNames[0]);
                Assert.AreEqual("go", newCommandNames[1]); 
                Assert.AreEqual("gå", newCommandNames[2]);                
            });
        }
        //TMP = Too Many Parameters or none
        [Test]
        public void TestCommandTMPExecution()
        {
            
            registry.Register(commandNames, new CommandMove());
            
            string consoleOutput1 = GetTerminalOutput("bevæg");     

            // Test to see if the command matches the string below
            Assert.AreEqual("For mange argumenter!", consoleOutput1); 
        }
        
        [Test]
        public void TestCommandInvalidChoice()
        {
            // Insert the command into the registry dictionary
            registry.Register(commandNames, new CommandMove());
            
            // Convert the stringwriter into a string
            string consoleOutput1 = GetTerminalOutput("Bevæg 5");
            
            // Test if the output to the terminal is correct
            Assert.AreEqual("5 er ikke et gyldigt valg!", consoleOutput1); 
        }
    }
}
