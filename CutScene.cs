namespace cs
{

    public class CutScene : Scene
    {
        public string ConditionInfo { get; set; }
        public int? NextSceneID { get; set; } // Might end the story. 
        public CutScene(int id, string name, string condtionInfo, int? nextSceneID = null)
            : base(id, name)
        {
            this.ConditionInfo = condtionInfo;
            this.NextSceneID = nextSceneID;
        }

    }

}
