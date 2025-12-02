namespace woz.Persistance
{
    using woz.Domain.Story;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IDataProvider
    {
        public Story GetStory();
        public void SetStory(Story story);
        public void ReloadStory();
    }
}
