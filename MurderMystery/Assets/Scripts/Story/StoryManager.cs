using UnityEngine;
using System.Collections;
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
	private StoryScript storyScript;

	void Start () {
        // TODO: Randomly pick from one of the available story scripts. For now there is just one.
        this.storyScript = new ExampleStoryScript();
	}
	
	void Update () {
	}

	public void NotifyItemFound(string itemName)
	{
		/* Called after the player picks up an item.
		 */
		Debug.Log("Item found: " + itemName);
		storyScript.OnItemFound(itemName);
	}

	public void NotifyNPCSpokenTo(string npcName)
	{
		/* Called after the player finishes speaking to an NPC.
		 */
		Debug.Log("NPC Spoken to: " + npcName);
		storyScript.OnNPCSpokenTo(npcName);
	}

	public void NotifyPlayerEnterRoom(string roomName)
	{
		/* Called when the player enters a new room.
		 */
		Debug.Log("Player entered room: " + roomName);
		storyScript.OnPlayerEnterRoom(roomName);
	}

	public void NotifyPlayerLeaveRoom(string roomName)
	{
		/* Called when the player leaves a room
		 */
		Debug.Log("Player left room: " + roomName);
		storyScript.OnPlayerLeaveRoom(roomName);
	}
}
