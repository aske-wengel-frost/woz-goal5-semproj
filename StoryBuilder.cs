using System.Reflection.Metadata.Ecma335;
using cs;

class StoryBuilder
{
    Dictionary<int, Scene> story = new Dictionary<int, Scene>();
    /// <summary>
    /// Takes given params, and adds to given story by id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="area"></param>
    /// <param name="name"></param>
    /// <param name="dialogueText"></param>
    /// <param name="choices"></param>
    /// <returns>Scene</returns>
    public Scene AddScene(int id, Area area, string name, string dialogueText, List<SceneChoice> choices)
    {
        Scene curScene = new Scene(id, name, dialogueText, area, choices);
        story[id] = curScene;
        return curScene;
    }


    /// <summary>
    /// Resolves all SceneChoice references by linking to their target Scene-objects.
    /// </summary>
    public void LinkScenes()
    {
        foreach (Scene scene in story.Values)
        {
            foreach (SceneChoice choice in scene.Choices)
            {
                if (story.TryGetValue(choice.SceneId, out Scene? proxyScene))
                {
                    choice.SceneObj = proxyScene;
                }
            }
        }
    }

    /// <summary>
    /// Returns intial scene with ID = 0. 
    /// </summary>
    /// <returns>Scene</returns>
    public Scene getIntiialScene()
    {
        return story[0];
    }

    /// <summary>
    /// Returns scene with specified id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Scene</returns>
    public Scene findSceneById(int id)
    {
        return story[id];
    }

    internal void AddScene(int v1, Area area, string v2, List<SceneChoice> sceneChoices)
    {
        throw new NotImplementedException();
    }
}