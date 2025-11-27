namespace cs.Domain.Story
{
    using System.Text.Json.Serialization;

    // Attributes for Json serialization & deserialization, makes sure polymorphism works in export
    [JsonDerivedType(typeof(ContextScene), typeDiscriminator: "contextScene")]
    [JsonDerivedType(typeof(CutScene), typeDiscriminator: "cutScene")]
    [JsonDerivedType(typeof(EndScene), typeDiscriminator: "endScene")]
    
    public abstract class Scene
    {
        // Makes sure each new created scene has uniqe ID
        private static int currentID = 0;

        private int _ID;
        public int ID 
        { 
            get
            {
                return _ID;
            }
            set
            {
                // Makes sure to set the currentID
                _ID = value;
                if(value > currentID)
                {
                    currentID = value;
                }
            } 
        }
        public string Name { get; set; }
      
        public Scene(string name)
        {
           ID = GetNextId();
           Name = name;
        }

        /// <summary>
        /// checks if a given scene is equal to this scene
        /// </summary>
        /// <param name="obj">scene object to compare</param>
        /// <returns>returns true if 2 scenes have same ID</returns>
        public override bool Equals(object? obj)
        {
            if (obj is Scene)
            {
                Scene tmpScene = (Scene)obj;
                if (this.ID == tmpScene.ID) { return true; }
            }
            return false;
        }

        // Helpers
        private static int GetNextId()
        {
            return currentID++; 
        }

        /// <summary>
        /// Resets the static ID counter. Used when restarting the game.
        /// </summary>
        public static void ResetIdCounter()
        {
            currentID = 0;
        }
    }
}
