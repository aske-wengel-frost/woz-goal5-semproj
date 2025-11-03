using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs
{
    // SceneChoice class to represent the choices available in a scene
    public class SceneChoice
    {
        public string? Description { get; set; }
        public string SceneName { get; set; }
        public Scene? SceneObj { get; set; }
        public SceneChoice(string sceneName, string description)
        {
            SceneName = sceneName;
            Description = description;
        }
    }
}
