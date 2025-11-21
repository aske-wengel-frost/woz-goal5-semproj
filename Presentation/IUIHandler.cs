namespace cs.Presentation
{
    using cs.Domain;
    using cs.Presentation.MapTerminal;

    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IUIHandler
    {
        public void DrawScene(Scene scene, int score);
        public void ClearScreen();
        public void DrawError(string errorMsg);
        public void DrawInfo(string infoMsg);
        public void InitMap(List<MapElement> elements);
        public void DrawMap();
        public void HighlightArea(int id);
        public void WaitForKeypress();

    }
}
