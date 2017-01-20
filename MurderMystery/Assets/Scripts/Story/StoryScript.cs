using UnityEngine;
using System.Collections;

public abstract class StoryScript {
    /* This class represents the script for a particular story.
	 * In our game's architecture we create dynamism by having several independent storylines.
	 * So there will be one StoryScript for each of the different stories.
     *
	 * The StoryManager will call the hooks in StoryScript at the appropriate times, allowing all the
	 * story-specific code to be contained within these classes.
	 */
	protected StoryGraph storyGraph; // should be set by child class in constructor

	public virtual void OnStateCompleted(string stateTitle)
	{
        /* Called by the story graph when a state in the story has been completed.
         * (That's why StoryGraph has a reference to it's StoryScript)
		 */
        Debug.Log("State Completed: " + stateTitle);
	}

	public virtual void OnStateUnlocked(string stateTitle)
	{
		/* Called by the story graph when a new state in the story has been unlocked
		 * (i.e. all it's requirements have been completed)
		 */
        Debug.Log("State Unlocked: " + stateTitle);
	}

	public virtual void OnItemFound(Clue item)
	{
		/* Called after the player picks up an item.
		 */
        Debug.Log("Clue Found " + item.GetName());
	}

	public virtual void OnNPCSpokenTo(NPC npc)
	{
		/* Called after the player finishes speaking to an NPC.
		 */
        Debug.Log("NPC spoken to " + npc.GetName());
	}

    public virtual void OnPlayerChangeRoom(Constants.Rooms room)
    {
		/* Called when the player enters a new room.
		 */
        Debug.Log("Player entered room" + room.ToString());
    }

    public StoryGraph GetStoryGraph()
    {
        // Only really needed for testing purposes
        return storyGraph;
    }
}
