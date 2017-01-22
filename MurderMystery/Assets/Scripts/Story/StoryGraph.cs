using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class StoryGraph {
	/* 
     * This class represents the graph of a story in the game.
	 * It has states which can be unlocked by the player by performing certain actions in the game.
	 */

	public class StoryGraphState {
		/* Represents a particular state within the story.
		 */ 
		public readonly string title;
		public readonly string[] requirements;
		public bool completed = false;
        public bool unlocked = false;
        public Dictionary<Constants.People, Dictionary<string, string>> dialogue; // (character -> (topic -> text)) dictionary

		public StoryGraphState(string title, string[] requirements, Dictionary<Constants.People, Dictionary<string, string>> dialogue)
		{
            //Debug.LogFormat("Creating new StoryGraphState: {0}, {1}, {2}", title, requirements.Count);
			this.title = title;
			this.requirements = requirements;
            if (requirements.Length == 0)
                unlocked = true;
            this.dialogue = dialogue;
		}
	}

    protected string storyName; // set in child class when it is constructed
    protected List<StoryGraphState> states; // set in child class
    protected string storySynopsis; // to be shown in the credits when you complete the game
    private StoryScript storyScript;
    // currentDialogue is a (person -> (topic -> text)) dictionary.
    // As new states are unlocked it is populated so that the most up to date text for each topic is available.
    protected Dictionary<Constants.Clues, string> clueDescriptions;
    private Dictionary<Constants.People, Dictionary<string, string>> currentDialogue;

	public StoryGraph(StoryScript storyScript)
	{
        this.storyScript = storyScript;
        this.currentDialogue = new Dictionary<Constants.People, Dictionary<string, string>>();

        // Child class should set storyName and states in it's constructor.
	}

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

		// Check if completing this state unlocks any new states
		foreach (var otherState in states)
		{
            if (otherState.unlocked) continue;

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
                Debug.Log("Unlocking state: " + otherState.title);
                storyScript.OnStateUnlocked(otherState.title);
                otherState.unlocked = true;

                UpdateCurrentDialogue(otherState);
            }
		}
	}

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

    public string GetClueDescription(Constants.Clues clue)
    {
        Debug.Log("GetClueDescription called for: " + clue.ToString());
        if (!clueDescriptions.ContainsKey(clue))
        {
            throw new Exception("No description found for clue: " + clue);
        }

        return clueDescriptions[clue];
    }

    public void CompleteStateIfNeeded(string stateTitle)
    {
        // Utility function: a common pattern is "if (!state completed) complete state;"
        // This reduces that to: "complete state if not already"
        if (!IsStateComplete(stateTitle))
        {
            CompleteState(stateTitle);
        }
    }

    public bool IsStateComplete(string stateTitle)
    {
        return GetStateWithTitle(stateTitle).completed;
    }

    public bool IsStateUnlocked(string stateTitle)
    {
        return GetStateWithTitle(stateTitle).unlocked;
    }

    public bool IsStateActive(string stateTitle)
    {
        // Active state = unlocked but not yet complete
        return IsStateUnlocked(stateTitle) && !IsStateComplete(stateTitle);
    }

    public void ResetStory()
    {
        // Mainly used for testing - resets the story by marking all states as incomplete.
        foreach (var state in states)
            state.completed = false;
    }

    public int CountStates()
    {
        // Only used for testing
        return states.Count;
    }

    protected void AddState(StoryGraphState state)
    {
        // Adds a new state to the list of states
        states.Add(state);

        if (state.unlocked)
        {
            // Some states (such as the intro state) have no requirements.
            // Thus they are never unlocked by CompleteState so dialogoue is not updated for them
            // so do that here instead.
            UpdateCurrentDialogue(state);
        }
    }

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

    private void UpdateCurrentDialogue(StoryGraphState newlyUnlockedState)
    {
        // Update the currentDialogue with this state's dialogue
        foreach (Constants.People person in newlyUnlockedState.dialogue.Keys)
        {
            Dictionary<string, string> personDialogue = newlyUnlockedState.dialogue[person];
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

public class StateNotFound : Exception
{
    public StateNotFound(string stateTitle) :
        base("GetStateWithTitle called for '" + stateTitle + "' but it was not found. This is likely a typo")
    { }
}

public class StateRequirementsNotMet : Exception
{
    public StateRequirementsNotMet(string stateTitle) :
        base("CompleteState called for '" + stateTitle + "' but requirements not met.")
    { }
}


