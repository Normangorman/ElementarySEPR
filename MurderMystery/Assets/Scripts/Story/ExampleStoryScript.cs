using UnityEngine;

//! Example Story Script.
/*! An example story script for testing use. */
public class ExampleStoryScript : StoryScript {
    private const string graphFilePath = "Assets/Scripts/Story/example_story.json"; //!< JavaScript graph file path.

    //! ExampleStoryScript constructor.
    public ExampleStoryScript() : base(graphFilePath)
    {
        Debug.Log("Loading ExampleStoryScript");
    }

    //! Triggers when a state is unlocked.
    /*
     * /param stateTitle Title of state.
     */
    public override void OnStateUnlocked(string stateTitle)
    {
        base.OnStateUnlocked(stateTitle);

        if (stateTitle == "Watch CCTV")
        {
            Debug.Log("TODO: Make CCTV object appear here");
        }
    }

    //! Triggers when an NPC interaction starts.
    /*!
     * \param npcName Name of interacted NPC.
     */ 
    public override void OnNPCSpokenTo(string npcName)
    {
        base.OnNPCSpokenTo(npcName);

        if (npcName == "Receptionist" && !storyGraph.IsStateCompleted("Introduction"))
        {
            storyGraph.CompleteState("Introduction");
        }
    }
}
