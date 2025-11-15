namespace cs
{
    using System;
    using System.Text.Json.Serialization;

    public class ContextScene : Scene
    {
        public int AreaId { get; set; }
        public int? RequiredItemId { get; set; } // TIl scener med krav om genstand

        private Area area;
        [JsonIgnore]
        public Area Area { get { return area; } set { AreaId = value != null ? value.ID : 0; area = value; } }
        public string? DialogueText { get; set; }
        public List<SceneChoice> Choices { get; set; }

        public ContextScene(int id, string name, string dialogueText, List<SceneChoice> choices, Area area = null, int? requiredItemId = null)
            : base(id, name)
        {
            DialogueText = dialogueText;
            Area = area;
            AreaId = area != null ? area.ID : 0;
            Choices = choices;
            RequiredItemId = requiredItemId;

        }


        /// Now that the abtract class is implemented, do we really need to check if they have the same id? 
        /// 
        // /// <summary>
        // /// checks if a given scene is equal to this scene
        // /// </summary>
        // /// <param name="obj">scene object to compare</param>
        // /// <returns>returns true if 2 scenes have same ID</returns>
        // public override bool Equals(object? obj)
        // {
        //     if (obj is Scene)
        //     {
        //         Scene tmpScene = (Scene)obj;
        //         if (this.ID == tmpScene.ID) { return true; }
        //     }
        //     return false;
        // }
    }
}
