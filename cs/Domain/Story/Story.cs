namespace cs.Domain.Story
{
    using cs;
    using cs.Presentation.MapTerminal;
    using cs.Domain.Player;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Story
    {
        public string Name { get; set; }
        public Dictionary<int, Scene> Scenes { get; set; }
        public Dictionary<int, Area> Areas { get; set; }
        public Dictionary<int, Item> Items { get; set; }

        // maby not?
        public List<MapElement> MapElements { get; set; }

        public Story()  
        {
            Name = "";
            Scenes = new Dictionary<int, Scene>();
            Areas = new Dictionary<int, Area>();
            Items = new Dictionary<int, Item>();
            MapElements = new List<MapElement>();
        }

        /// <summary>
        /// Returns intial scene with ID = 0. 
        /// </summary>
        /// <returns>Scene</returns>
        public ContextScene? getInitialScene()
        {
            if(Scenes[0] is not ContextScene)
            {
                return null;
            }
            return (ContextScene)Scenes[0];
        }

        /// <summary>
        /// Find a scene based on the name property of the scene.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>The found Scene object</returns>
        public Scene? FindScene(int ID)
        {
            if (Scenes.ContainsKey(ID))
            {
                return Scenes[ID];
            }

            return null;
        }

        public void AddScene(Scene scene)
        {
            Scenes.Add(scene.ID, scene);
        }

        public void AddArea(Area area)
        {
            Areas.Add(area.ID, area);
        }

        public void AddItem(Item item)
        {
            Items.Add(item.ID, item);
        }

        public Item FindItemByName(string name)
        {
            // Gets the first item that matches
            Item? item = Items.Values.Where(x => x.Name.ToLower() == name.ToLower()).First();
            if(item == null)
            {
                throw new Exception($"No item with name {name} found");
            }
            return item;
        }

        // RETHINK THIS PLEASE
        public void AddMapElement(MapElement mapel)
        {
            MapElements.Add(mapel);
        }
    }
}
