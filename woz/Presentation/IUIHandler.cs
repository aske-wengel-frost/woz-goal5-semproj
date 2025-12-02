namespace woz.Presentation
{
    using woz.Domain.Player;
    using woz.Domain.Story;
    using woz.Presentation.MapTerminal;

    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IUIHandler
    {
        public void DrawScene(Scene scene, Player player);
        public void ClearScreen();
        public void DrawError(string errorMsg);
        public void DrawInfo(string infoMsg);
        public void InitMap(Dictionary<int, Area> areas);
        public void DrawMap();
        public void HighlightArea(int id);
        public void WaitForKeypress();

    }
}
