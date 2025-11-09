namespace cs
{
    using System;
    using System.Text.Json.Serialization;

    public class Scene
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int AreaId { get; set; }

        private Area area;
        [JsonIgnore]
        public Area Area { get {return area; } set { AreaId = value != null ? value.ID : 0; area = value; } }
        public string? DialogueText { get; set; }
        public List<SceneChoice> Choices { get; set; }
        public Scene(int id, string name, string dialogueText, List<SceneChoice> choices, Area area = null)
        {
            ID = id;
            Name = name;
            DialogueText = dialogueText;
            Area = area;
            AreaId = area != null ? area.ID : 0;
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
