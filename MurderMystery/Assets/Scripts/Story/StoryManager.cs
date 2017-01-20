using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Remoting;

//! Story Manager Class.
/*! Controls all story aspects and scripts, and objects that interface with story. */
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
	private StoryScript storyScript; //!< The active story script for the current game.

    //! On Start, sets story script to one of the available scripts.
	void Start () {
        // TODO: Randomly pick from one of the available story scripts. For now there is just one.
        this.storyScript = new ExampleStoryScript();
	}
	
	void Update () {
	}

    //! Called after player picks up an item.
	public void NotifyItemFound(string itemName)
	{
		/* Called after the player picks up an item.
		 */
		Debug.Log("Item found: " + itemName);
		storyScript.OnItemFound(itemName);
	}

    //! Called after the player finishes an interaction with an NPC.
    /*!
     * \param npcName The name of the NPC that was being interacted with.
     */ 
	public void NotifyNPCSpokenTo(string npcName)
	{
		/* Called after the player finishes speaking to an NPC.
		 */
		Debug.Log("NPC Spoken to: " + npcName);
		storyScript.OnNPCSpokenTo(npcName);
	}

    //! Called when the player enters a new room.
    /*!
     * \param roomName The name of the room that the player enters.
     */
    public void NotifyPlayerEnterRoom(string roomName)
	{
		/* Called when the player enters a new room.
		 */
		Debug.Log("Player entered room: " + roomName);
		storyScript.OnPlayerEnterRoom(roomName);
	}

    //! Called when the player leaves a room.
    /*!
     * \param npcName The name of the room that the player was in.
     */
    public void NotifyPlayerLeaveRoom(string roomName)
	{
		/* Called when the player leaves a room
		 */
		Debug.Log("Player left room: " + roomName);
		storyScript.OnPlayerLeaveRoom(roomName);
	}
}
