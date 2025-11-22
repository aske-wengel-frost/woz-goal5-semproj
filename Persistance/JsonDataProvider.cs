namespace cs.Persistance
{
    using cs.Domain.Player;
    using cs.Domain.Story;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class JsonDataProvider : IDataProvider
    {
        private Story Story;
        private string dataFilePath;
        public JsonDataProvider(string filePath = "./StoryDat.json")
        {
            this.dataFilePath = filePath;
            Story = new Story();
        }

        /// <summary>
        /// Gets the story from the json file and links the objects 
        /// </summary>
        /// <returns></returns>
        public Story getStory()
        {
            LoadStoryFromFile();
            LinkScenes();
            return Story;
        }

        /// <summary>
        /// Saves the story object back into json file
        /// </summary>
        /// <param name="story"></param>
        public void setStory(Story story)
        {
            Story = story;
            ExportStoryToFile();
        }

        // HELPERS

        private void LoadStoryFromFile()
        {
            // First check if a file already exists in the directory
            if (!File.Exists(this.dataFilePath))
            {
                // If it does not exist we generate it with a empty dictionary of scenes.
                string dat = JsonSerializer.Serialize<Story>(Story);
                //string dat = JsonSerializer.Serialize<Dictionary<int, Scene>>(this.Scenes);

                // Creates the file and appends the json
                File.AppendAllText(this.dataFilePath, dat);
            }

            // Read the text of the file
            string tmpJsonStr = File.ReadAllText(this.dataFilePath);

            // We load the deserialized scenes into the scenes property
            // Maby handle a null value here.
            Story = JsonSerializer.Deserialize<Story>(tmpJsonStr);
        }

        /// <summary>
        /// Resolves all SceneChoice references by linking to their target Scene-objects.
        /// This is needed, as a scene object may not exist when we want to assign the scene to the scene object within the Choices of the scene.
        /// </summary>
        private void LinkScenes()
        {
            // Loop through all scenes
            foreach (Scene scene in Story.Scenes.Values)
            {
                if (scene is ContextScene contextScene)
                {

                    // only if area object is not instanciated
                    if (contextScene.Area == null)
                    {
                        if (Story.Areas.TryGetValue(contextScene.AreaId, out Area area))
                        {
                            contextScene.Area = area;
                        }
                    }

                    // Loop through all scenechoices in theese scenes
                    foreach (SceneChoice sceneChoice in contextScene.Choices)
                    {
                        // try to resolve the name of the scene with a scene object
                        if (Story.Scenes.TryGetValue(sceneChoice.SceneId, out Scene OutScene))
                        {
                            if (OutScene is ContextScene targetScene)
                            {
                                sceneChoice.SceneObj = targetScene;
                            }
                        }

                        if(Story.Items.TryGetValue(sceneChoice.KeyItemId, out Item keyItem))
                        {
                            sceneChoice.KeyItem = keyItem;
                        }
                    }
                }
            }

            // loop through all areas and link items
            foreach(Area area in Story.Areas.Values)
            {
                if(area.itemIds.Count > 0)
                {
                    foreach(int itemId in area.itemIds)
                    {
                        // Find the item
                        Item? item = Story.Items[itemId];
                        if(item != null)
                        {
                            area.Items.Add(item.ID, item);

                        }
                    }
                }
            }
        }
        private void ExportStoryToFile()
        {
            string jsonStr = JsonSerializer.Serialize<Story>(this.Story, new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping });

            File.WriteAllText(dataFilePath, jsonStr);
        }
    }
}
