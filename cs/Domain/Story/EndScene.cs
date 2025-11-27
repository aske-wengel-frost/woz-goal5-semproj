namespace cs.Domain.Story
{

    public class EndScene : Scene
    {
        public string EndSceneContent { get; set; }

        public EndScene(string name, string endSceneContent)
            : base(name)
        {
            this.EndSceneContent = endSceneContent;
        }
    }
}
