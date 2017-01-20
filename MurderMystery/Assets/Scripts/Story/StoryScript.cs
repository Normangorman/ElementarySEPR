using UnityEngine;
using System.Collections;

//! Story Script class.
/*! Represents the script for a particular story. */ 
public abstract class StoryScript : MonoBehaviour {
    /* This class represents the script for a particular story.
	 * In our game's architecture we create dynamism by having several independent storylines.
	 * So there will be one StoryScript for each of the different stories.
     *
	 * The StoryManager will call the hooks in StoryScript at the appropriate times, allowing all the
	 * story-specific code to be contained within these classes.
	 */
	protected StoryGraph storyGraph; //!< The story graph for the story script.

    //! Story script constructor.
    /*!
     * \param storyGraphFilePath The file path for the JavaScript story graph.
     */
    public StoryScript(string storyGraphFilePath)
    {
        this.storyGraph = new StoryGraph(this, storyGraphFilePath);
    }

    //! Called when a state of the story is complete.
    /*!
     * \param stateTitle The name of the completed state.
     */
    public virtual void OnStateCompleted(string stateTitle)
	{
        /* Called by the story graph when a state in the story has been completed.
         * (That's why StoryGraph has a reference to it's StoryScript)
		 */
	}

    //! Called when a new story state has been unlocked.
    /*!
     * \param stateTitle The name of the unlocked state.
     */ 
	public virtual void OnStateUnlocked(string stateTitle)
	{
		/* Called by the story graph when a new state in the story has been unlocked
		 * (i.e. all it's requirements have been completed)
		 */
	}

    //! Called after the player picks up an item.
    /*!
     * \param npcName The name of the item that has been picked up.
     */
    public virtual void OnItemFound(string itemName)
	{
		/* Called after the player picks up an item.
		 */
	}

    //! Called after the player finishes an NPC interaction.
    /*!
     * \param npcName Name of the npc being spoken to.
     */ 
	public virtual void OnNPCSpokenTo(string npcName)
	{
		/* Called after the player finishes speaking to an NPC.
		 */
	}

    //! Called when the player enters a new room.
    /*!
     * \param roomName The name of the room that the player enters.
     */
    public virtual void OnPlayerEnterRoom(string roomName)
	{
		/* Called when the player enters a new room.
		 */
	}

    //! Called when the player leaves a room.
    /*!
     * \param npcName The name of the room that the player was in.
     */
    public virtual void OnPlayerLeaveRoom(string roomName)
	{
		/* Called when the player leaves a room
		 */
	}

    //! A test method to return the current story graph.
    /*!
     * \return The current story graph.
     */
    public StoryGraph GetStoryGraph()
    {
        // Only really needed for testing purposes
        return storyGraph;
    }
}
