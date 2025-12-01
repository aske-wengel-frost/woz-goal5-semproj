namespace cs.Domain.Story
{
    using cs;
    using cs.Domain.Player;
    using cs.Presentation.MapTerminal;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    public class Story
    {
        public string Name { get; init; }
        public Dictionary<int, Scene> Scenes { get; set; }
        public Dictionary<int, Area> Areas { get; set; }
        public Dictionary<int, Item> Items { get; set; }


        public Story(string name = "Story")  
        {
            Name = name;
            Scenes = new Dictionary<int, Scene>();
            Areas = new Dictionary<int, Area>();
            Items = new Dictionary<int, Item>();
        }

        /// <summary>
        /// Returns intial scene with ID = 0. 
        /// </summary>
        /// <returns>Scene</returns>
        public Scene? GetInitialScene()
        {
            return Scenes[0];
        }

        /// <summary>
        /// Find a scene based on the name property of the scene.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>The found Scene object</returns>
        public T FindScene<T>(int ID)
        {
            if (!Scenes.ContainsKey(ID))
            {
                throw new Exception($"No scene with ID {ID} found!");
            }

            Scene scene = Scenes[ID];

            if(scene is T typedScene)
            {
                return typedScene;
            }
            else
            {
                throw new Exception($"Scene with id {ID} does exist, but it is of type {scene.GetType().ToString()} and not {typeof(T).Name}");
                // Return default value of T (propably null in this case)
                //return default(T);
            }
        }

        public T FindScene<T>(string name)
        {
            Scene? scene = Scenes.Values.Where(x => x.Name.ToLower() == name.ToLower()).First();

            if (scene is null)
            {
                throw new Exception($"No scene with name {name} found!");
            }

            if (scene is T typedScene)
            {
                return typedScene;
            }
            else
            {
                throw new Exception($"Scene with name {name} does exist, but it is of type {scene.GetType().ToString()} and not {typeof(T).Name}");
            }
        }

        public Item FindItem(int id)
        {
            if(!Scenes.ContainsKey(id))
            {
                throw new Exception($"No item with ID {id} found!");
            }

            return Items[id];
        }

        public Item FindItem(string name)
        {
            Item? item = Items.Values.Where(x => x.Name.ToLower() == name.ToLower()).First();

            if (item is null)
            {
                throw new Exception($"No item with name {name} found!");
            }

            return item;
        }


        public Area FindArea(int id)
        {
            if (!Areas.ContainsKey(id))
            {
                throw new Exception($"No area with ID {id} found!");
            }

            return Areas[id];
        }

        public Area FindArea(string name)
        {
            Area? area = Areas.Values.Where(x => x.Name.ToLower() == name.ToLower()).First();

            if (area is null)
            {
                throw new Exception($"No area with name {name} found!");
            }

            return area;
        }


        public void AddScene(Scene scene)
        {
            Scenes.Add(scene.Id, scene);
        }

        public void AddArea(Area area)
        {
            Areas.Add(area.Id, area);
        }

        public void AddItem(Item item)
        {
            Items.Add(item.Id, item);
        }
    }
}
