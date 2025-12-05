using System;
using System.IO;
using System.Collections;
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
        
        //Initialize variables to hold to the count of the amounts of objects
        int countScenes = 0;
        int countContext = 0;
        int countCut = 0;
        int countEnd = 0;
        int countAreas = 0;
        int countItems = 0;


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
            //Iterate through the objects scenes in the dictionary in story
            for (int i = 0; i <= story.Scenes.Count(); i++)
            {
                if (story.Scenes.ContainsKey(i))
                {
                    //+1 of the scene count
                    countScenes++;
                    //Check if the scene is a ContextScene and 
                    if (story.Scenes[i] is ContextScene) { countContext++; continue; }
                    if (story.Scenes[i] is CutScene) { countCut++; continue;}
                    countEnd++;
                }
            }
            for (int i = 0; i <= story.Areas.Count(); i++)
            {
                if (story.Areas.ContainsKey(i))
                {
                    countAreas++;
                }
            }
            for (int i = 0; i <= story.Items.Count(); i++)
            {
                if (story.Items.ContainsKey(i))
                {
                    countItems++;
                }
            }

            Assert.Multiple(() =>
            {
                Assert.AreEqual(countScenes, 20, "Not every Scenes are loaded");
                Assert.AreEqual(countContext, 7, "Not every ContextScens are loaded");
                Assert.AreEqual(countCut, 12, "Not every CutScenes are loaded");
                Assert.AreEqual(countEnd, 1, "Not every EndScenes are loaded");
                Assert.AreEqual(countAreas, 6, "Not every Áreas are loaded");
                Assert.AreEqual(countItems, 3, "Not every Items are loaded");
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

                if (contextScene.Area is Area && contextScene.Area == story.Areas[contextScene.AreaId])
                {
                    count++;
                }
            }
            Assert.AreEqual(count, countContext, "Not every scenes have been assigned an Area");
        }

        public void TestLinkedAreaItem()
        {
            int count = 0;
            Hashtable ht = new Hashtable();
            for (int i = 0; i < story.Areas.Count(); i++)
            {
                if (story.Areas[i].Items.Count() > 0)
                {
                    for (int j = 0 ; j < story.Areas[i].Items.Count() ; j++)
                    {
                        ht.Add(count.ToString(), story.Areas[i].Items[j].Id);
                        count++;
                    }
                }
            }
            Assert.AreEqual(countItems, ht.Cast<DictionaryEntry>().Count(), "Not every Items are loaded in an Area");
        }

        public void TestLinkedSceneChoices()
        {
            bool Linked = true;
            for (int i = 0; i < story.Scenes.Count(); i++)
            {
                if (story.Scenes[i] is not ContextScene)
                {
                    continue;
                }

                ContextScene contextScene = story.Scenes[i] as ContextScene;

                for (int j = 0; j < contextScene.Choices.Count(); j++)   
                {
                    if (contextScene.Choices[i].SceneObj is not Scene && story.Scenes[contextScene.Choices[i].SceneId] != contextScene.Choices[i].SceneObj)
                    {
                        Linked = false;
                    }
                }
            }
            Assert.IsTrue(Linked, "Not every ContextScenes have a SceneChoice object");
        }
    }
}
