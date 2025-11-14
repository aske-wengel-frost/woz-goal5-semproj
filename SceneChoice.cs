namespace cs
{
    using System;
    using System.Text.Json.Serialization;

    // SceneChoice class to represent the choices available in a scene
    public class SceneChoice
    {
        public string? Description { get; set; }
        public int SceneId { get; set; }
        public int? RequiredItemId { get; set; } // TIl scener med krav om genstand

        [JsonIgnore]
        public Scene? SceneObj { get; set; }
        public SceneChoice(int sceneId, string description, int? requiredItemId = null)
        {
            SceneId = sceneId;
            Description = description;
            RequiredItemId = requiredItemId;
        }
    }
}
