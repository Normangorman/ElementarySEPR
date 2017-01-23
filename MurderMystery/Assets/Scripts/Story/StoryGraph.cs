using UnityEngine;
using System.Collections.Generic;
using System;

//! Story Graph object class.
/*! Represents the graph of a story in the game, with states that are unlocked by player actions. */
public abstract class StoryGraph {
	/* 
     * This class represents the graph of a story in the game.
	 * It has states which can be unlocked by the player by performing certain actions in the game.
	 */

    //! StoryGraph State object class.
    /*! Represents a state within the story graph. */
	public class StoryGraphState {
		/* Represents a particular state within the story.
		 */ 
		public readonly string title; //!< State title.
		public readonly string[] requirements; //!< State unlock requirements.
		public bool completed = false; //!< State completed boolean.
        public bool unlocked = false; //!< State unlocked boolean.
        // dialogue dictionaries map (character -> (topic -> text)) 
        // unlock this dialogue when the state is unlocked
        public Dictionary<Constants.People, Dictionary<string, string>> dialogueOnUnlocked; //!< Dictionary of dialogue when unlocked.
        // unlock this dialogue when the state is completed 
        public Dictionary<Constants.People, Dictionary<string, string>> dialogueOnCompleted; //!< Dictionary of dialogue when completed.

        //! StoryGraphState Constructor.
        /*!
         * \param title State title.
         * \param requirements State unlock requirements.
         * \param dialogueOnUnlocked State unlocked boolean.
         * \param dialogueOnCompleted State completed boolean.
         */
        public StoryGraphState(string title, string[] requirements,
            Dictionary<Constants.People, Dictionary<string, string>> dialogueOnUnlocked,
            Dictionary<Constants.People, Dictionary<string, string>> dialogueOnCompleted)
		{
            //Debug.LogFormat("Creating new StoryGraphState: {0}, {1}, {2}", title, requirements.Count);
			this.title = title;
			this.requirements = requirements;
            this.dialogueOnUnlocked = dialogueOnUnlocked;
            this.dialogueOnCompleted = dialogueOnCompleted;
		}
	}
    // set in child class when it is constructed
    protected string storyName; //!< Story name
    // set in child class
    protected List<StoryGraphState> states; //!< List of graph states.
    // to be shown in the credits when you complete the game
    protected string storySynopsis; //!< Text of story synopsis.
    // currentDialogue is a (person -> (topic -> text)) dictionary.
    private StoryScript storyScript; //!< Current story graph script.

    // As new states are unlocked it is populated so that the most up to date text for each topic is available.
    protected Dictionary<Constants.Clues, string> clueDescriptions; //!< List of descriptions of clues.
    private Dictionary<Constants.People, Dictionary<string, string>> currentDialogue; //!< List of current dialogue.

    //! StoryGraph constructor.
    /*!
     * \param storyScript Current story graph script.
     */ 
    public StoryGraph(StoryScript storyScript)
	{
        this.storyScript = storyScript;
        this.currentDialogue = new Dictionary<Constants.People, Dictionary<string, string>>();

        // Child class should set storyName and states in it's constructor.
	}

    //! Marks state as completed and triggers OnStateCompleted, raises error if requirements not met or invalid state name.
    /*
     * \param stateTitle State requested for completion.
     */ 
	public void CompleteState(string stateTitle)
	{
        /* Marks the state with the given title as being completed.
         * Also calls the StoryScript's OnStateCompleted hook for the given state.
         * If any new states are unlocked by this completion, the OnStateUnlocked hook is called for each one.
         *   Raises an Exception if the state with the given title was not found (probably due to typo).
         *   Raises an Exception if the state's requirements were not met (StoryScripts should not try and do this). 
         */
		Debug.Log("CompleteState called for " + stateTitle);
		StoryGraphState state = GetStateWithTitle(stateTitle);

        // Check if this state's requirements are actually met
        foreach (var req in state.requirements)
        {
            if (!GetStateWithTitle(req).completed)
            {
                throw new StateRequirementsNotMet(stateTitle);
            }
        }
        Debug.Log("All requirements are met");

        // If they are met, mark the state completed
        Debug.Log("Marking state as completed and calling storyScript's OnStateCompleted");
        storyScript.OnStateCompleted(stateTitle);
		state.completed = true;
        UpdateCurrentDialogue(state);

        // Check if completing this state unlocks any new states
        Debug.Log("Checking for newly unlocked states");
		foreach (var otherState in states)
		{
            //Debug.Log("Iterating: " + otherState.title);
            if (otherState.title == stateTitle || otherState.unlocked)
            {
                //Debug.Log("Skipping " + otherState.title);
                continue;
            }

            bool allReqsComplete = true;
            foreach (var req in otherState.requirements)
            {
                if (!GetStateWithTitle(req).completed)
                {
                    allReqsComplete = false;
                    break;
                }
            }

            if (allReqsComplete)
            {
                //Debug.Log("All reqs completed");
                UnlockState(otherState);
            }
            else
            {
                //Debug.Log("All reqs not completed");
            }
		}
	}

    //! Marks state as unlocked and triggers OnStateUnlocked.
    /*
     * \param state State to be unlocked.
     */
    private void UnlockState(StoryGraphState state)
    {
        Debug.Log("Unlocking state: " + state.title);
        storyScript.OnStateUnlocked(state.title);
        state.unlocked = true;
        UpdateCurrentDialogue(state);
    }

    //! Gets the dialogue for the current person being interacted with.
    /*!
     * \param person NPC being interacted with.
     * \return Dialogue for the current person
     */ 
    public Dictionary<string, string> GetCurrentDialogueForPerson(Constants.People person)
    {
        // Returns a (topic -> text) dictionary
        if (!currentDialogue.ContainsKey(person))
        {
            throw new Exception("No dialogue found for person: " + person);
        }

        // Make a clone of the dialogue dictionary so that it can be edited safely by the recipient
        Dictionary<string, string> dialogueClone = new Dictionary<string, string>();
        foreach (var key in currentDialogue[person].Keys)
        {
            dialogueClone[key] = currentDialogue[person][key];
        }

        return dialogueClone;
    }

    //! Gets the clue description for the requested clue.
    /*!
     * \param clue Requested clue.
     * \return Clue description
     */ 
    public string GetClueDescription(Constants.Clues clue)
    {
        Debug.Log("GetClueDescription called for: " + clue.ToString());
        if (!clueDescriptions.ContainsKey(clue))
        {
            throw new Exception("No description found for clue: " + clue);
        }

        return clueDescriptions[clue];
    }

    //! Gets the current story synopsis.
    public string GetSynopsis()
    {
        return storySynopsis;
    }

    //! Utility function to complete state if not already complete.
    /*!
     * \param stateTitle Name of state.
     */ 
    public void CompleteStateIfNeeded(string stateTitle)
    {
        // Utility function: a common pattern is "if (!state completed) complete state;"
        // This reduces that to: "complete state if not already"
        if (!IsStateComplete(stateTitle))
        {
            CompleteState(stateTitle);
        }
    }

    //! Gets the completed boolean from a state.
    /*!
     * \param stateTitle
     * \return Boolean of completed or incomplete.
     */
    public bool IsStateComplete(string stateTitle)
    {
        return GetStateWithTitle(stateTitle).completed;
    }

    //! Gets the unlocked boolean from a state.
    /*!
     * \param stateTitle Name of state.
     * \return Boolean of unlocked or locked.
     */
    public bool IsStateUnlocked(string stateTitle)
    {
        return GetStateWithTitle(stateTitle).unlocked;
    }

    //! Finds if a state is unlocked but not complete
    /*!
     * \param stateTitle Title of state.
     * \return Boolean of activeness.
     */
    public bool IsStateActive(string stateTitle)
    {
        // Active state = unlocked but not yet complete
        return IsStateUnlocked(stateTitle) && !IsStateComplete(stateTitle);
    }

    //! Test function to reset all states to incomplete.
    public void ResetStory()
    {
        // Mainly used for testing - resets the story by marking all states as incomplete.
        foreach (var state in states)
            state.completed = false;
    }

    //! Test function for finding the number of states.
    /*!
     * \return Number of states
     */
    public int CountStates()
    {
        // Only used for testing
        return states.Count;
    }

    //! Adds a new state to the list of states.
    /*!
     * \param state State object to be added.
     */
    protected void AddState(StoryGraphState state)
    {
        // Adds a new state to the list of states
        states.Add(state);

        if (state.requirements.Length == 0)
        {
            // Some states (such as the intro state) have no requirements.
            UnlockState(state);
        }
    }

    //! Gets a state with a given title, throws error if state is not found.
    /*!
     * \param stateTitle Title of state.
     * \return State with given title.
     */
    private StoryGraphState GetStateWithTitle(string stateTitle)
	{
		/* 
         * Returns the state with the given title
		 */
		foreach (var state in states)
		{
            //Debug.Log("GetStateWithTitle considering state: " + state.title);
			if (state.title == stateTitle)
			{
				return state;
			}
		}
        throw new StateNotFound(stateTitle);
	}

    //! Updates the dialogue for a state.
    /*! 
     * \param state State that should be updated.
     */
    private void UpdateCurrentDialogue(StoryGraphState state)
    {
        // Updates the dialogue for a state, either when it is freshly unlocked or freshly completed
        if (state.completed)
        {
            UpdateCurrentDialogueFromDict(state.dialogueOnCompleted);
        }
        else if (state.unlocked)
        {
            UpdateCurrentDialogueFromDict(state.dialogueOnUnlocked);
        }
    }

    //! Updates the currentDialogue with the dialogue in a give dictionary.
    /*!
     * \param dict Dictionary of new dialogue.
     */
    private void UpdateCurrentDialogueFromDict(Dictionary<Constants.People, Dictionary<string, string>> dict)
    {
        // Update the currentDialogue with the dialogue in this dictionary 
        foreach (Constants.People person in dict.Keys)
        {
            Dictionary<string, string> personDialogue = dict[person];
            foreach (string topic in personDialogue.Keys)
            {
                string text = personDialogue[topic];

                if (!currentDialogue.ContainsKey(person))
                {
                    currentDialogue[person] = new Dictionary<string, string>();
                }

                currentDialogue[person][topic] = text;
            }
        }
    }
}

//! StateNotFound error class.
/*!
 * Thrown when a state is searched for with an invalid name.
 */
public class StateNotFound : Exception
{
    //! StateNotFound error message.
    /*!
     * \param stateTitle State name that was incorrectly indexed.
     */ 
    public StateNotFound(string stateTitle) :
        base("GetStateWithTitle called for '" + stateTitle + "' but it was not found. This is likely a typo")
    { }
}

//! StateRequirementsNotMet error class.
/*!
 * Thrown when a state is attempted to be completed or unlocked but the requirements are not met.
 */
public class StateRequirementsNotMet : Exception
{
    //! StateRequirementsNotMet error message.
    /*!
     * \param stateTitle State name that requirements are not met.
     */
    public StateRequirementsNotMet(string stateTitle) :
        base("CompleteState called for '" + stateTitle + "' but requirements not met.")
    { }
}


