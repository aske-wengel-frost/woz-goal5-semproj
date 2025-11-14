namespace cs
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IUIHandler
    {
        
        public void DrawScene(Scene scene);
        public string GetUserInput(String input);
        public void ClearScreen();
        public void DrawError(string errorMsg);
        public void DrawInfo(string infoMsg);
        public void RefreshMap(Dictionary<int, Area> areas);

    }
}
