using System;
using System.IO;
using woz;
using woz.Domain;
using woz.Domain.Story;
using woz.Domain.Commands;
using woz.Presentation;
using woz.Persistance;
using woz.Domain.Player;
namespace UnitTests 
{
    public class UnitTests
    {   
        public JsonDataProvider data;
        public Story story;

        [SetUp]
        public void Setup()
        {
            //Initialize the JsonDataProvider object
            data = new JsonDataProvider();

            //Initialize the Story object
            story = data.GetStory();
        }

        [Test]
        public void TestInitialization()
        {
            int count1 = 0;
            int count2 = 0;
            int count3 = 0;

            for (int i = 0; i <= story.Scenes.Count(); i++)
            {
                if (story.Scenes.ContainsKey(i))
                {
                    count1++;
                }
            }
            for (int i = 0; i <= story.Areas.Count(); i++)
            {
                if (story.Areas.ContainsKey(i))
                {
                    count2++;
                }
            }
            for (int i = 0; i <= story.Items.Count(); i++)
            {
                if (story.Items.ContainsKey(i))
                {
                    count3++;
                }
            }

            Assert.Multiple(() =>
            {
                Assert.AreEqual(count1, 20, "Every Scenes are not loaded");
                Assert.AreEqual(count2, 6, "Every Áreas are not loaded");
                Assert.AreEqual(count3, 3, "Every Items are not loaded");
            });
        }

        [Test]
        public void TestLinkedSceneAreas()
        {
            int count = 0;
            for(int i = 0; i < story.Scenes.Count(); i++)
            {
                if (story.Scenes[i] is not ContextScene)
                {
                    continue;
                }

                ContextScene contextScene = story.Scenes[i] as ContextScene;

                if (contextScene.Area is Area && contextScene.Area != null)
                {
                    count++;
                }
            }
            Assert.AreEqual(count, 7, "Not every scenes have been assigned an Area");
        }

        public void TestLinkedAreaItem()
        {
            int count = 0;
            for (int i = 0; i < story.Areas.Count(); i++)
            {
                if (story.Areas[i] is Item && story.Areas[i] != null)
                {
                    count++;
                }
            }
            Assert.AreEqual(count, story.Areas.Count(), "Not every areas have been assigned an Item");
        }
    }
}
