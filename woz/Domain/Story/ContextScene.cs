namespace woz.Domain.Story
{
    using System;
    using System.Text.Json.Serialization;

    public class ContextScene : Scene
    {
        public int AreaId { get; set; }
        //public int? RequiredItemId { get; set; } // TIl scener med krav om genstand
        public int ScenePoints { get; init; } // Point givet udfra Player's valg i sidste scene

        private Area area;
        [JsonIgnore]
        public Area Area { get { return area; } set { AreaId = value != null ? value.Id : 0; area = value; } }
        public string? DialogueText { get; init; }
        public List<SceneChoice> Choices { get; init; }

        public ContextScene(string name, int scenePoints, string dialogueText, Area area = null)
            : base(name)
        {
            DialogueText = dialogueText;
            Area = area;
            AreaId = area != null ? area.Id : 0;
            ScenePoints = scenePoints;
            Choices = new List<SceneChoice>();

        }

        public void AddSceneChoice(SceneChoice sceneChoice)
        {
            Choices.Add(sceneChoice);
        }
        
        
    }
}
