namespace woz.Domain.Story
{

    /// <summary>
    /// The EndScene class represent the final scene in a story.
    /// Where the story is concluded and the ending is presented to the player
    /// with a score and summary of the cause about violent towards women.
    /// </summary>
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
