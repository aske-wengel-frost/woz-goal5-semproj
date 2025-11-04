namespace cs.UI
{
    public interface UII
    {
        public void DrawScene(Scene scene, StoryHandler storyHandler);
        public string GetUserInput(string prompt = ">> ");
        public void ClearUI();
        public void DrawError(string err);
    }

}