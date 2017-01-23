using UnityEngine;
using System.Collections.Generic;
using System;
using System.Runtime.Remoting;

//! Story Manager Class.
/*! Class for managing the story, all objects that depend on the story interface with this class */
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
    public static StoryManager instance = null; //!< The instance of the active story.

	private StoryScript storyScript; //!< The story script object.

    //! On load, set the instance and pick a story script.
    void Awake()
    {
        if (instance == null)
            instance = this;

        // TODO: Randomly pick from one of the available story scripts. For now there is just one.
        this.storyScript = new Mystery1Script();
    }

    //! Get the active story script.
    /*!
     * \return Active story script.
     */ 
    public StoryScript GetStoryScript()
    {
        return storyScript;
    }

    //! Gets the dialogue for the current person from the story graph.
    /*!
     * \param npc Current NPC being interacted with.
     * \return Dictionary of dialogue for person.
     */ 
    public Dictionary<string, string> GetCurrentDialogueForPerson(Constants.People npc)
    {
        return storyScript.GetStoryGraph().GetCurrentDialogueForPerson(npc);
    }

    //! Gets the description of the room.
    public string GetRoomDescription()
    {
        throw new NotImplementedException();
    }

    //! Gets clue description of a requested clue.
    /*!
     * \param clue Current clue being interacted with.
     * \return Clue description.
     */
    public string GetClueDescription(Constants.Clues clue)
    {
        return storyScript.GetStoryGraph().GetClueDescription(clue);
    }

    //! Gets the description of an NPC.
    public string GetNpcDescription()
    {
        throw new NotImplementedException();
    }

    //! Called when an NPC is accused, checks if accusation is valid and responds appropriately.
    /*!
     * \param n NPC Accused
     */ 
    public void OnAccuseCharacter(NPC n)
    {
        // Interacts with the GameManager to see if the player has won or lost
        if (storyScript.CheckAccusation(n.person))
        {
            GameManager.instance.WinGame(storyScript.GetStoryGraph().GetSynopsis());
        }
        else
        {
            MessagePasser.OnFailedAccusation(n);
        }
    }
}
