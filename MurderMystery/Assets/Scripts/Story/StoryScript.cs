using UnityEngine;
using System.Collections;

public abstract class StoryScript : MonoBehaviour {
    /* This class represents the script for a particular story.
	 * In our game's architecture we create dynamism by having several independent storylines.
	 * So there will be one StoryScript for each of the different stories.
     *
	 * The StoryManager will call the hooks in StoryScript at the appropriate times, allowing all the
	 * story-specific code to be contained within these classes.
	 */
	protected StoryGraph storyGraph;

    public StoryScript(string storyGraphFilePath)
    {
        this.storyGraph = new StoryGraph(this, storyGraphFilePath);
    }

	public virtual void OnStateCompleted(string stateTitle)
	{
        /* Called when a state in the story has been completed.
		 */
        storyGraph.CompleteState(stateTitle);
	}

	public virtual void OnStateUnlocked(string stateTitle)
	{
		/* Called when a new state in the story has been unlocked
		 * (i.e. all it's requirements have been completed)
		 */
	}

	public virtual void OnItemFound(string itemName)
	{
		/* Called after the player picks up an item.
		 */
	}

	public virtual void OnNPCSpokenTo(string npcName)
	{
		/* Called after the player finishes speaking to an NPC.
		 */
	}

	public virtual void OnPlayerEnterRoom(string roomName)
	{
		/* Called when the player enters a new room.
		 */
	}

	public virtual void OnPlayerLeaveRoom(string roomName)
	{
		/* Called when the player leaves a room
		 */
	}
}
