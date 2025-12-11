namespace UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using woz.Domain.Commands;
    using woz.Domain.Player;
    using woz.Domain.Story;
    using woz.Presentation;

    public class DummyUIHandler : IUIHandler
    {
        public void ClearScreen()
        {
            
        }

        public void DrawError(string errorMsg)
        {
            
        }

        public void DrawHelp(Dictionary<string, ICommand> commands)
        {
            
        }

        public void DrawInfo(string infoMsg)
        {
            
        }

        public void DrawInventory(Inventory inventory)
        {
            
        }

        public void DrawMap()
        {
            
        }

        public void DrawScene(Scene scene, Player player)
        {
            
        }

        public void HighlightArea(int id)
        {
            
        }

        public void InitMap(Dictionary<int, Area> areas)
        {
            
        }

        public void WaitForKeypress()
        {
            
        }
    }
}
