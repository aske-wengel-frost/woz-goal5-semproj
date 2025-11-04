namespace cs
{
    using System;

    // SceneChoice class to represent the choices available in a scene
    public class SceneChoice
    {
        public string? Description { get; set; }
        public int SceneId { get; set; }
        public Scene? SceneObj { get; set; }
        public SceneChoice(int sceneId, string description)
        {
            SceneId = sceneId;
            Description = description;
        }
    }
}
