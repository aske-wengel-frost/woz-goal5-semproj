namespace cs.Domain.Story
{

    public class EndScene : Scene
    {
        public string EndSceneContent { get; init; }

        public EndScene(string name, string endSceneContent)
            : base(name)
        {
            this.EndSceneContent = endSceneContent;
        }
    }
}
