namespace cs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class StoryBuilder
    {
        public Dictionary<string, Scene> Scenes { get; set; }

        public StoryBuilder()
        {
            
        }

        public bool AddScene(Scene scene)
        {
            // Adds the scene object and sets the key in the dictionary to the name of the Scene
            Scenes.Add(scene.Name, scene);

            return true;
        }

        /// <summary>
        /// Responsible for linking scene objects in scene objects in choices in Scenes dictionary with the actual scene object
        /// This is needed, as a scene object may not exist when we want to assign the scene to the scene object within the Choices of the scene.
        /// </summary>
        public void LinkScenes()
        {
            // Loop through all scenes
            foreach(Scene scene in Scenes.Values)
            {
                // Loop through all scenechoices in theese scenes
                foreach(SceneChoice sceneChoice in scene.Choices)
                {
                    // try to resolve the name of the scene with a scene object
                    if (Scenes.TryGetValue(sceneChoice.SceneName, out Scene OutScene))
                    {
                        // set the scene object on the scenechoice object to the found instance
                        sceneChoice.SceneObj = OutScene;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the first scene of the story (The entry scene)
        /// </summary>
        /// <returns>The first scene of the story</returns>
        public Scene GetEntryScene()
        {
            // Some criteria, for now it will be the scene with name "Entrance"
            return Scenes["Entrance"];
        }

        /// <summary>
        /// Find a scene based on the name property of the scene.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>The found Scene object</returns>
        public Scene FindScene(string name)
        {
            Scene? tmpScene = Scenes[name];

            return tmpScene;
        }

    }
}
