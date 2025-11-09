namespace cs
{
    using System;
    using System.Text.Json.Serialization;

    // SceneChoice class to represent the choices available in a scene
    public class SceneChoice
    {
        public string? Description { get; set; }
        public int SceneId { get; set; }
        
        [JsonIgnore]
        public Scene? SceneObj { get; set; }
        public SceneChoice(int sceneId, string description)
        {
            SceneId = sceneId;
            Description = description;
        }
    }
}
