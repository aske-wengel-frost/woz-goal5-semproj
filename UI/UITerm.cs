namespace cs.UI
{
    public class UITerm : UII
    {
        public UITerm()
        { }

        public void ClearUI()
        {
            Console.Clear();
        }

        public void DrawError(string err)
        {
            Console.WriteLine($"Error: {err}");
        }

        public string GetUserInput(string prompt = ">> ")
        {
            Console.Write(prompt);
            string? usrInp = Console.ReadLine();
            return usrInp != null ? usrInp : "";

        }

        public void DrawScene(Scene scene, StoryHandler storyHandler)
        {
            Console.WriteLine($"Scene: {scene.Name} - You are in Area: {scene.Area.Name}");
            Console.WriteLine();
            Console.WriteLine("=================================================================");
            Console.WriteLine(scene.DialogueText);

            // Draw all current choices.
            Console.WriteLine("Your current opts are: ");
            foreach (SceneChoice sceneChoice in scene.Choices)
            { Console.WriteLine($" -> {sceneChoice.Description} : [{sceneChoice.SceneId}]"); }
        }
    }

}