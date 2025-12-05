using System;
using System.IO;
using woz;
using woz.Domain;
using woz.Domain.Story;
using woz.Domain.Commands;
using woz.Presentation;
using woz.Persistance;
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
            //storyHandler.player = new cs.Domain.Player.Player("Name");

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

        public void Start()
        {
            storyHandler.StartStory();
            string simulatedInput = Environment.NewLine;
            StringReader reader = new StringReader(simulatedInput);
            Console.SetIn(reader);
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
                    
        [Test]
        public void TestCommandAction()
        {
            // Start the story and get the current scene
            Start();
            Scene scene1 = storyHandler.GetCurrentScene();
            ContextScene contextScene1 = scene1 as ContextScene;       
            
            // Setup a new story object
            JsonDataProvider data = new JsonDataProvider();
            Story story = data.GetStory();

            // Find the second scene choice 
            CutScene cutScene2 = contextScene1.Choices[3 - 1].SceneObj as CutScene;
            Scene scene2 = story.FindScene<Scene>(cutScene2.NextSceneId.Value);

            // Insert the CommandMove into registry and execute
            registry.Register(commandNames, new CommandMove());
            registry.Dispatch("bevæg 3");

            // Get the new current scene
            Scene scene3 = storyHandler.GetCurrentScene();

            // Check if the two current scenes are different
            Assert.Multiple(() =>
            {
                Assert.AreNotEqual(scene1.Name, scene3.Name, "The CommandMove execution failed");
                Assert.AreEqual(scene2.Name, scene3.Name, "Failed to switch to the proper scene");
            });
        } 
    }
}
