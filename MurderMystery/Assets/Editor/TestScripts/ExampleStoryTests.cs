using UnityEngine;
using System.Collections;
using NUnit.Framework;

[TestFixture]
public class ExampleStoryTests
{
    ExampleStoryScript script;
    StoryGraph graph;

    [SetUp]
    public void SetUpTests()
    {
        script = new ExampleStoryScript();
        graph = script.GetStoryGraph();
    }

    [Test]
    public void TestStory()
    {
        Assert.True(graph.IsStateUnlocked("Intro"));
        Assert.False(graph.IsStateComplete("Intro"));
        Assert.False(graph.IsStateUnlocked("Find knife"));

        script.OnNPCSpokenTo(Constants.People.TheQueen);
        Assert.True(graph.IsStateComplete("Intro"));
        Assert.True(graph.IsStateUnlocked("Find knife"));

        script.OnItemFound(Constants.Clues.Knife);
        Assert.True(graph.IsStateComplete("Find knife"));
    }
}