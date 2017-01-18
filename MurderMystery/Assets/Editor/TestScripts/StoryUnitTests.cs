using UnityEngine;
using System.Collections;
using NUnit.Framework;

[TestFixture]
public class StoryUnitTests {
    ExampleStoryScript script;
    StoryGraph graph;

    [SetUp]
    public void SetUpTests()
    {
        // We can't just make a StoryScript with the new keyword because it's a MonoBehaviour
        // So we must create a new GameObject and add the script as a component
        GameObject go = new GameObject();
        script = go.AddComponent<ExampleStoryScript>();
        graph = script.GetStoryGraph();
    }

    [Test]
    public void ExampleTest()
    {
        Assert.True(true);
    }

    [Test]
    public void TestLoadExampleStoryScriptWorks()
    {
        // Test that the script loads it's StoryGraph from the JSON file correctly
        Assert.NotNull(graph);
        Assert.Greater(graph.CountStates(), 0);
    }

    [Test]
    public void TestCompleteStateWorks()
    {
        // Test that if a state is marked as completed using CompleteState, then IsStateComplete returns true
        Assert.False(graph.IsStateCompleted("Introduction"));
        graph.CompleteState("Introduction");
        Assert.True(graph.IsStateCompleted("Introduction"));
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
        Assert.Throws(typeof(StateRequirementsNotMet), delegate { graph.CompleteState("Watch CCTV"); });
    }

    [Test]
    public void TestStateUnlocking()
    {
        // A state should be unlocked if and only if all it's requirements are complete
        graph.ResetStory();
        graph.CompleteState("Introduction");
        Assert.True(graph.IsStateUnlocked("Watch CCTV"));
        Assert.True(graph.IsStateUnlocked("Search for clues"));
        Assert.False(graph.IsStateUnlocked("Interrogate Dumbledore"));
    }
}
