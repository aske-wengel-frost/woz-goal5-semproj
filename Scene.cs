using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs
{
    public class Scene
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        //Area
        public string? DialogueText { get; set; }
        //SceneChoice list

        public Scene(int id, string name, string dialogueText)
        {
            Id = id;
            Name = name;
            DialogueText = dialogueText;
        }

        /// <summary>
        /// checks if a given scene is equal to this scene
        /// </summary>
        /// <param name="obj">scene object to compare</param>
        /// <returns>returns true if 2 scenes have same ID</returns>
        public override bool Equals(object obj)
        {
            if (obj is Scene) 
            {
                Scene tmpScene = (Scene)obj;
                if (this.Id == tmpScene.Id)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
