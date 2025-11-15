namespace cs
{
    public abstract class Scene
    {
        public int ID { get; set; }
        public string Name { get; set; }
      
        public Scene(int id, string name)
        {
           ID = id;
           Name = name;
        }
    }
}
