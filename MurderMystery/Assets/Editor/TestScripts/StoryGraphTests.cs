using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

[TestFixture]
public class StoryGraphTests
{
    ExampleStoryGraph graph;

    private void ResetGraph()
    {
        // We're only testing the graph here but the best way to get an instance of one is to make a story script
        ExampleStoryScript script = new ExampleStoryScript();
        graph = (ExampleStoryGraph)script.GetStoryGraph();
    }

    [SetUp]
    public void SetUpTests()
    {
        ResetGraph();
    }

    [Test]
    public void TestStoryName()
    {
        Assert.AreEqual(graph.GetStoryName(), "ExampleStory");
    }

    [Test]
    public void TestStorySynopsis()
    {
        Assert.AreEqual(graph.GetSynopsis(), "This is the story synopsis.");
    }

    [Test]
    public void TestStates()
    {
        Assert.AreEqual(graph.CountStates(), 2);
        Assert.True(graph.IsStateUnlocked("Intro"));
        Assert.False(graph.IsStateComplete("Intro"));
        Assert.False(graph.IsStateUnlocked("Find knife"));
        Assert.False(graph.IsStateComplete("Find knife"));
    }

    [Test]
    public void TestCompleteState()
    {
        Assert.False(graph.IsStateComplete("Intro"));
        graph.CompleteState("Intro");
        Assert.True(graph.IsStateComplete("Intro"));

        // This state should have been unlocked by completing intro
        Assert.True(graph.IsStateUnlocked("Find knife"));
        Assert.False(graph.IsStateComplete("Find knife"));
    }

    [Test]
    public void TestGetClueDescription()
    {
        Assert.AreEqual(graph.GetClueDescription(Constants.Clues.Knife), "A bloody knife");
    }

    [Test]
    public void TestGetDialogue()
    {
        ResetGraph();
        Dictionary<string, string> dialogue = graph.GetCurrentDialogueForPerson(Constants.People.TheQueen);
        Assert.True(dialogue.ContainsKey("NO_TOPIC"));
        Assert.True(dialogue.ContainsKey("Love"));
        Assert.False(dialogue.ContainsKey("Knife"));
        Assert.AreEqual(dialogue["NO_TOPIC"], "Hello");
        Assert.AreEqual(dialogue["Love"], "Do you love me?");

        graph.CompleteState("Intro");
        dialogue = graph.GetCurrentDialogueForPerson(Constants.People.TheQueen);
        Assert.True(dialogue.ContainsKey("NO_TOPIC"));
        Assert.True(dialogue.ContainsKey("Love"));
        Assert.True(dialogue.ContainsKey("Knife"));
        Assert.AreEqual(dialogue["NO_TOPIC"], "Hello");
        Assert.AreEqual(dialogue["Love"], "I love you.");
        Assert.AreEqual(dialogue["Knife"], "You found a knife! Looks sharp!");
    }

    [Test]
    public void TestCompleteStateThrowsExceptionWhenStateNotFound()
    {
        // Tests that if we try and complete a state with an unknown title, an Exception is thrown.
        Assert.Throws(typeof(StateNotFound), delegate { graph.CompleteState("Party on the moon"); });
    }

    [Test]
    public void TestCompleteStateThrowsExceptionWhenRequirementsNotMet()
    {
        // Tests that if we try and complete a state when its requirements are not met, an Exception is thrown.
        ResetGraph();
        Assert.Throws(typeof(StateRequirementsNotMet), delegate { graph.CompleteState("Find knife"); });
    }
}