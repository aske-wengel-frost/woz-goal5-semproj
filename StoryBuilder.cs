namespace cs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

class StoryBuilder
{
        public Dictionary<int, Scene> Scenes { get; set; }

        public StoryBuilder()
    {
            
    }

        public bool AddScene(Scene scene)
        {
            // Adds the scene object and sets the key in the dictionary to the name of the Scene
            Scenes.Add(scene.ID, scene);

            return true;
        }

    /// <summary>
    /// Resolves all SceneChoice references by linking to their target Scene-objects.
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
                    if (Scenes.TryGetValue(sceneChoice.SceneId, out Scene OutScene))
                {
                        // set the scene object on the scenechoice object to the found instance
                        sceneChoice.SceneObj = OutScene;
                }
            }
        }
    }

    /// <summary>
    /// Returns intial scene with ID = 0. 
    /// </summary>
    /// <returns>Scene</returns>
    public Scene getIntiialScene()
    {
        return story[0];
    }

    /// <summary>
        /// Find a scene based on the name property of the scene.
    /// </summary>
        /// <param name="name"></param>
        /// <returns>The found Scene object</returns>
        public Scene FindScene(int ID)
    {
            Scene? tmpScene = Scenes[ID];

            return tmpScene;
        }

    }
}