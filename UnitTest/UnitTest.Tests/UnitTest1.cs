using NUnit.Framework;
using cs; 
using cs.Presentation;

namespace cs.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        Console.WriteLine("Testing......");
    }

    [Test]
    public void Test1()
    {
        Displayer = new textDisplay();
        Displayer.Display("RIHIHI");
        Assert.Pass();
    }
}
