namespace cs.Domain.Story
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

        public ContextScene(string name, int scenePoints, string dialogueText, Area area = null)
            : base(name)
        {
            DialogueText = dialogueText;
            Area = area;
            AreaId = area != null ? area.ID : 0;
            ScenePoints = scenePoints;
            Choices = new List<SceneChoice>();

        }

        public void AddSceneChoice(SceneChoice sceneChoice)
        {
            Choices.Add(sceneChoice);
        }
        
        
    }
}
