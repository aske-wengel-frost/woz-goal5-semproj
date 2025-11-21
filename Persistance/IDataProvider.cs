namespace cs.Persistance
{
    using cs.Domain;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IDataProvider
    {
        public Story getStory();
        public void setStory(Story story);
        //public bool setStory();
    }
}
