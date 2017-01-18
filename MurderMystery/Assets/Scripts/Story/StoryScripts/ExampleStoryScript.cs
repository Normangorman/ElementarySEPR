using UnityEngine;

public class ExampleStoryScript : StoryScript {
    private const string graphFilePath = "Assets/Scripts/Story/example_story.json";

    public ExampleStoryScript() : base(graphFilePath)
    {
        Debug.Log("Loading ExampleStoryScript");
    }

    public override void OnStateUnlocked(string stateTitle)
    {
        base.OnStateUnlocked(stateTitle);

        if (stateTitle == "Watch CCTV")
        {
            Debug.Log("TODO: Make CCTV object appear here");
        }
    }

    public override void OnNPCSpokenTo(NPC npc)
    {
        base.OnNPCSpokenTo(npc);

        if (npc.person == Constants.People.Receptionist &&
            !storyGraph.IsStateComplete("Introduction"))
        {
            storyGraph.CompleteState("Introduction");
        }
    }
}
