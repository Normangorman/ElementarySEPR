using UnityEngine;
using System.Collections;
using DoozyUI;

//! Story Script class.
/*! Represents the script for a particular story, called by the story manager. */
public abstract class StoryScript {
    /* This class represents the script for a particular story.
	 * In our game's architecture we create dynamism by having several independent storylines.
	 * So there will be one StoryScript for each of the different stories.
     *
	 * The StoryManager will call the hooks in StoryScript at the appropriate times, allowing all the
	 * story-specific code to be contained within these classes.
	 */
    // should be set by child class in constructor
    protected StoryGraph storyGraph; //!< Story Graph for the story script.

    //! Called when a particular state is completed.
    /*! 
     * \param stateTitle Name of the completed state.
     */ 
	public virtual void OnStateCompleted(string stateTitle)
	{
        /* Called by the story graph when a state in the story has been completed.
         * (That's why StoryGraph has a reference to it's StoryScript)
		 */
        Debug.Log("State Completed: " + stateTitle);
	}

    //! Called when a particular state is unlocked.
    /*!
     * \param stateTitle Name of the state.
     */
    public virtual void OnStateUnlocked(string stateTitle)
	{
		/* Called by the story graph when a new state in the story has been unlocked
		 * (i.e. all it's requirements have been completed)
		 */
        //Debug.Log("State Unlocked: " + stateTitle);
	}

	public virtual void OnItemFound(Constants.Clues item)
    //! Called after the player picks up a clue.
    /*!
     * \param item Constants.Clue type of clue picked up.
     */
	{
		/* Called after the player picks up an item.
		 */
        Debug.Log("Clue Found " + item.ToString());
	}

	public virtual void OnNPCSpokenTo(Constants.People person)
    //! Called when an NPC is spoken to.
    /*!
     * \param npc Constants.People npc constant spoken to.
     */
	{
		/* Called when the player begins speaking to an NPC.
		 */
        Debug.Log("NPC spoken to " + person.ToString());
	}

	public virtual void OnNPCSpokenTo(Constants.People person, string topic)
    //! Called when an NPC is spoken to about a particular topic.
    /*! 
     * \param person Constants.People
     * \param topic string
     */
	{
		/* Called when the player speaks to an NPC about a certain topic
		 */
        Debug.LogFormat("NPC spoken to {0} about topic {1}", person.ToString(), topic);
	}

    //! Called when the player enters a new room.
    /*!
     * \param room New room entered.
     */
    public virtual void OnPlayerChangeRoom(Constants.Rooms room)
    {
		/* Called when the player enters a new room.
		 */
        Debug.Log("Player entered room" + room);
    }

    //! Called when the player accuses an NPC and checks if successful.
    /*!
     * \param person person accused.
     * \return Boolean of whether accusation was successful.
     */
    public virtual bool CheckAccusation(Constants.People person)
    {
        /* Called when the player accuses someone to check whether the accusation succeeded.
         * Returns true if the accusation was successful and false if not
         */
        return false;
    }

    //! Test function for getting the story graph object.
    /*!
     * \return Story Graph.
     */
    public StoryGraph GetStoryGraph()
    {
        // Only really needed for testing purposes
        return storyGraph;
    }
}
