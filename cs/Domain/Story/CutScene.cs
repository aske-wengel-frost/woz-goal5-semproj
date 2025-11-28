namespace cs.Domain.Story
{
    public class CutScene : Scene
    {
        public string ConditionInfo { get; set; }
        public int? NextSceneId { get; set; } // Might end the story.
        public CutScene(string name, string conditionInfo, int? nextSceneId = null)
            : base(name)
        {
            this.ConditionInfo = conditionInfo;
            this.NextSceneId = nextSceneId;
        }
    }

}
