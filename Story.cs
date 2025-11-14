namespace cs
{
    using cs.MapTerminal;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Story
    {
        public string Name { get; set; }
        public Dictionary<int, Scene> Scenes { get; set; }
        public Dictionary<int, Area> Areas { get; set; }

        // maby not?
        public List<MapElement> MapElements { get; set; }

        public Story()  
        {
            Name = "";
            Scenes = new Dictionary<int, Scene>();
            Areas = new Dictionary<int, Area>();
            MapElements = new List<MapElement>();
        }

        /// <summary>
        /// Returns intial scene with ID = 0. 
        /// </summary>
        /// <returns>Scene</returns>
        public Scene getIntiialScene()
        {
            return Scenes[0];
        }

        /// <summary>
        /// Find a scene based on the name property of the scene.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>The found Scene object</returns>
        public Scene? FindScene(int ID)
        {
            if (Scenes.ContainsKey(ID))
            {
                return Scenes[ID];
            }

            return null;
        }
    }
}
