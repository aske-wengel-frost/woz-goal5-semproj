namespace cs
{
    using System.Text.Json.Serialization;

    [JsonDerivedType(typeof(ContextScene), typeDiscriminator: "contextScene")]
    [JsonDerivedType(typeof(CutScene), typeDiscriminator: "cutScene")]
    public abstract class Scene
    {
        public int ID { get; set; }
        public string Name { get; set; }
      
        public Scene(int id, string name)
        {
           ID = id;
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
    }
}
