using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs
{
    public class Scene
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public Area Area { get; set;  }
        public string? DialogueText { get; set; }
        public List<SceneChoice> Choices { get; set; }
        public Scene(int id, string name, string dialogueText, Area area, List<SceneChoice> choices)
        {
            ID = id;
            Name = name;
            DialogueText = dialogueText;
            Area = area;
            Choices = choices;
        }

        /// <summary>
        /// checks if a given scene is equal to this scene
        /// </summary>
        /// <param name="obj">scene object to compare</param>
        /// <returns>returns true if 2 scenes have same ID</returns>
        public override bool Equals(object? obj)
        {
            if (obj is Scene)
            {
                Scene tmpScene = (Scene)obj;
                if (this.ID == tmpScene.ID) { return true; }
            }
            return false;
        }
    }
}
