namespace cs.Domain
{
    using System.Text.Json.Serialization;

    // SceneChoice class to represent the choices available in a scene
    public class SceneChoice
    {
        public string? Description { get; set; }
        public int SceneId { get; set; }

        [JsonIgnore]
        public ContextScene? SceneObj { get; set; }
        public int? KeyItemId { get; set; }
        public Item? KeyItem { get; set; }
        public SceneChoice(int sceneId, string description, int? keyItemId = null)
        {
            SceneId = sceneId;
            Description = description;
            KeyItemId = keyItemId;
        }

        public bool isLocked()
        {
            if(KeyItemId == null)
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
