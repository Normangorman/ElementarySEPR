using UnityEngine;

public class ExampleStoryScript : StoryScript {
    /* Used for testing purposes exclusively */

    public ExampleStoryScript()
    {
        Debug.Log("Loading ExampleStoryScript");
        this.storyGraph = new ExampleStoryGraph(this);
    }

    public override void OnNPCSpokenTo(Constants.People person)
    {
        base.OnNPCSpokenTo(person);

        if (person == Constants.People.TheQueen && storyGraph.IsStateActive("Intro"))
        {
            storyGraph.CompleteState("Intro");
        }
    }

    public override void OnItemFound(Constants.Clues clue)
    {
        base.OnItemFound(clue);

        if (clue == Constants.Clues.Knife && storyGraph.IsStateUnlocked("Find knife"))
        {
            storyGraph.CompleteState("Find knife");
        }
    }
}