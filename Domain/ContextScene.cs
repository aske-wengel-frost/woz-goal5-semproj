namespace cs.Domain
{
    using System;
    using System.Text.Json.Serialization;

    public class ContextScene : Scene
    {
        public int AreaId { get; set; }
        //public int? RequiredItemId { get; set; } // TIl scener med krav om genstand
        public int ScenePoints { get; set; } // Point givet udfra Player's valg i sidste scene

        private Area area;
        [JsonIgnore]
        public Area Area { get { return area; } set { AreaId = value != null ? value.ID : 0; area = value; } }
        public string? DialogueText { get; set; }
        public List<SceneChoice> Choices { get; set; }

        public ContextScene(int id, string name, int scenePoints, string dialogueText, List<SceneChoice> choices, Area area = null)
            : base(id, name)
        {
            DialogueText = dialogueText;
            Area = area;
            AreaId = area != null ? area.ID : 0;
            Choices = choices;
            //RequiredItemId = requiredItemId;
            ScenePoints = scenePoints;

        }
        
    }
}
