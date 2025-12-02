namespace woz.Domain.Story
{
    public class CutScene : Scene
    {
        public string ConditionInfo { get; init; }
        public int? NextSceneId { get; init; }
        public CutScene(string name, string conditionInfo, int? nextSceneId = -1)
            : base(name)
        {
            this.ConditionInfo = conditionInfo;
            this.NextSceneId = nextSceneId;
        }
    }

}
