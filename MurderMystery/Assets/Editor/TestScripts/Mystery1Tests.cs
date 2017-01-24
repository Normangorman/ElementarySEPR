using UnityEngine;
using System.Collections;
using NUnit.Framework;

[TestFixture]
public class Mystery1Tests
{
    Mystery1Script script;
    StoryGraph graph;

    [SetUp]
    public void SetUpTests()
    {
        script = new Mystery1Script();
        graph = script.GetStoryGraph();
    }

    [Test]
    public void TestStory()
    {
        // Run through the entire story and check that all the transitions work
        Assert.True(graph.IsStateUnlocked("Intro"));
        Assert.False(graph.IsStateComplete("Intro"));

        // Every find clue state should be unlocked but not yet complete
        Assert.True(graph.IsStateUnlocked("Find a broken tape"));
        Assert.True(graph.IsStateUnlocked("Find epipen"));
        Assert.False(graph.IsStateComplete("Find a broken tape"));
        Assert.False(graph.IsStateComplete("Find epipen"));

        // Complete intro
        script.OnNPCSpokenTo(Constants.People.Receptionist);
        Assert.True(graph.IsStateComplete("Intro"));
        Assert.True(graph.IsStateUnlocked("Find a broken tape"));
        Assert.True(graph.IsStateUnlocked("Find epipen"));

        // Complete a couple of the finding clue states
        script.OnItemFound(Constants.Clues.BrokenTape);
        Assert.True(graph.IsStateComplete("Find a broken tape"));
        script.OnItemFound(Constants.Clues.Epipen);
        Assert.True(graph.IsStateUnlocked("Find epipen"));

        // Complete a dialogue topic state
        Assert.False(graph.IsStateComplete("Question Receptionist about CCTV"));
        script.OnNPCSpokenTo(Constants.People.Receptionist, "CCTV");
        Assert.True(graph.IsStateComplete("Question Receptionist about CCTV"));

        Assert.True(graph.IsStateUnlocked("Question Bond about CCTV"));
        script.OnNPCSpokenTo(Constants.People.JamesBond, "CCTV");
        Assert.True(graph.IsStateComplete("Question Bond about CCTV"));

        Assert.True(graph.IsStateUnlocked("Question Queen about meeting with Bond"));
        script.OnNPCSpokenTo(Constants.People.TheQueen, "Meeting with Bond");
        Assert.True(graph.IsStateComplete("Question Queen about meeting with Bond"));

        // Test the critical path to the accusation
        Assert.False(graph.IsStateComplete("Find $20 note"));
        script.OnItemFound(Constants.Clues.Money);
        Assert.True(graph.IsStateComplete("Find $20 note"));

        Assert.False(graph.IsStateComplete("Find wanted poster"));
        script.OnItemFound(Constants.Clues.WantedPoster);
        Assert.True(graph.IsStateComplete("Find wanted poster"));

        Assert.True(graph.IsStateUnlocked("Question Trump about money"));
        script.OnNPCSpokenTo(Constants.People.DonaldTrump, "Money");
        Assert.True(graph.IsStateComplete("Question Trump about money"));

        Assert.True(graph.IsStateUnlocked("Accuse Dumbledore"));
    }
}