namespace cs.Domain.Story
{
    public class CutScene : Scene
    {
        public string ConditionInfo { get; init; }
        public int? NextSceneId { get; init; } // Might end the story.
        public CutScene(string name, string conditionInfo, int? nextSceneId = null)
            : base(name)
        {
            this.ConditionInfo = conditionInfo;
            this.NextSceneId = nextSceneId;
        }
    }

}
