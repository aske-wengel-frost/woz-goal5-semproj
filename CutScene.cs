namespace cs
{

    public class CutScene : Scene
    {
        public string ConditionInfo { get; set; }
        public Scene NextScene { get; set; }
        public CutScene(string condtionInfo, Scene nextScene, int id, string name)
            : base(id, name)
        {
            this.ConditionInfo = condtionInfo;
            this.NextScene = nextScene;
        }

    }

}
