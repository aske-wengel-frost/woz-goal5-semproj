namespace cs
{

    public class CutScene : Scene
    {
        public string ConditionInfo { get; set; }
        public int? NextSceneId { get; set; } // Might end the story.
        public CutScene(int id, string name, string conditionInfo, int? nextSceneId = null)
            : base(id, name)
        {
            this.ConditionInfo = conditionInfo;
            this.NextSceneId = nextSceneId;
        }

    }

}
