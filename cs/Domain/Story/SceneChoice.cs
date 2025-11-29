namespace cs.Domain.Story
{
    using cs.Domain.Player;

    using System.Text.Json.Serialization;

    // SceneChoice class to represent the choices available in a scene
    public class SceneChoice
    {
        public string? Description { get; init; }
        public int ScorePoints { get; init; }
        public int PartnerAggression { get; init; }

        public int SceneId { get; init; }
        [JsonIgnore]
        public Scene? SceneObj { get; set; }

        public int KeyItemId { get; init; }
        [JsonIgnore]
        public Item? KeyItem { get; set; }
        public SceneChoice(Scene sceneObj, int scorePoints, int partnerAggression, string description, Item? keyItem = null)
        {
            Description = description;
            ScorePoints = scorePoints;
            PartnerAggression = partnerAggression;

            if (sceneObj != null)
            {
                SceneId = sceneObj.Id;
            }
            SceneObj = sceneObj;

            // -1 represents no key item!
            KeyItemId = -1;
            if(keyItem != null)
            {
                KeyItemId = keyItem.Id;
            }
            KeyItem = keyItem;
        }

        public bool IsLocked()
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
                // Should we remove the item?
                //inv.RemoveItem();
                return true;
            }

            return false;
        }
    }
}
