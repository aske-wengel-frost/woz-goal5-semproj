namespace cs.Domain.Story
{
    using cs.Domain.Player;

    using System.Text.Json.Serialization;

    // SceneChoice class to represent the choices available in a scene
    public class SceneChoice
    {
        public string? Description { get; set; }

        public int SceneId { get; set; }
        [JsonIgnore]
        public Scene? SceneObj { get; set; }

        public int KeyItemId { get; set; }
        [JsonIgnore]
        public Item? KeyItem { get; set; }

        public SceneChoice(Scene sceneObj, string description, Item? keyItem = null)
        {
            Description = description;

            if (sceneObj != null)
            {
                SceneId = sceneObj.ID;
            }
            SceneObj = sceneObj;

            // bad?
            KeyItemId = -1;
            if(keyItem != null)
            {
                KeyItemId = keyItem.ID;
            }
            KeyItem = keyItem;
        }

        public bool isLocked()
        {
            if(KeyItem == null)
            {
                return false;
            }
            return true;
        }

        public bool Unlock(Inventory inv)
        {
            // try to find required item in inventory
            if(inv.ItemExists(KeyItemId))
            {
                // inv.RemoveItem();
                return true;
            }

            return false;
        }
    }
}
