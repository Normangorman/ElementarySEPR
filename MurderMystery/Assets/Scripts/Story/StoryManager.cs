using UnityEngine;
using System.Collections.Generic;
using System;
using System.Runtime.Remoting;

public class StoryManager : MonoBehaviour {
    /* Main class for managing the story.
	 * Game objects that care about the story should interface with this class.
     * Specific stories are scripted using a StoryScript, and this class keeps a reference to the current StoryScript.
	 * When particular events happen in the game this class will call a StoryScript hook.
	 * 
	 * e.g. When the player unlocks a new state in the story, the OnStateUnlocked hook will be called.
	 */

    // set this to be the script for the active story
    // for assessment 2 there will probably only be 1 available storyScript
    public static StoryManager instance = null;

	private StoryScript storyScript;

    void Awake()
    {
        if (instance == null)
            instance = this;

        // TODO: Randomly pick from one of the available story scripts. For now there is just one.
        this.storyScript = new Mystery1Script();
    }

    public StoryScript GetStoryScript()
    {
        return storyScript;
    }

    public Dictionary<string, string> GetCurrentDialogueForPerson(Constants.People npc)
    {
        return storyScript.GetStoryGraph().GetCurrentDialogueForPerson(npc);
    }
	
	void Update () {
	}

    internal string GetRoomDescription()
    {
        throw new NotImplementedException();
    }

    internal string GetClueDescription()
    {
        throw new NotImplementedException();
    }
}
