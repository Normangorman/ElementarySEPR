using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.IO;
using System;

//! Story Graph class.
/*! Represents the graph of the story, with states that are unlocked by player actions.*/ 
public class StoryGraph {
	/* 
     * This class represents the graph of a story in the game.
	 * It has states which can be unlocked by the player by performing certain actions in the game.
	 */

    //! Story Graph state class.
    /*! Represents a particular state within the story. */ 
	private class StoryGraphState {
		/* Represents a particular state within the story.
		 */ 
		public readonly string title; //!< State title.
		public readonly string description; //!< State description.
		public readonly List<string> requirements; //!< List of state requirements.
		public bool completed = false; //!< State completed condition.
        public bool unlocked = false; //!< State unlocked condition.

        //! State constructor.
        /*! 
         * \param title State title.
         * \param description State description.
         * \param requirements List of state requirements.
         */
        public StoryGraphState(string title, string description, List<string> requirements)
		{
            Debug.LogFormat("Creating new StoryGraphState: {0}, {1}, {2}", title, description, requirements.Count);
			this.title = title;
			this.description = description;
			this.requirements = requirements;
            if (requirements.Count == 0)
                unlocked = true;
		}
	}

    // vv perhaps an an actual graph data structure would be better vv
    private List<StoryGraphState> states = new List<StoryGraphState>(); //!< List of states. 
	private StoryScript storyScript; //!< Current story script.

    //! StoryGraph constructor.
    /*! 
     * \param storyScript Current story script.
     * \param graphFilePath JavaScript story graph file path.
     */
    public StoryGraph(StoryScript storyScript, string graphFilePath)
	{
		/* 
         * Takes a path to a json file specifying the structure of the story.
         * See Assets/Scripts/Story/example_story.json for an example
		 */
		Debug.Log("Loading story graph from: " + graphFilePath);
		this.storyScript = storyScript;

        JObject graphJSON = JObject.Parse(File.ReadAllText(graphFilePath));
        JArray statesJSON = (JArray)graphJSON.GetValue("states");
        foreach (var state in statesJSON.Children<JObject>())
        {
            string title = (string)state["title"];
            string description = (string)state["description"];
            JArray requirements = (JArray)state["requirements"];

            List<string> requirementsList;
            // State might not have any requirements
            if (requirements == null)
                requirementsList = new List<string>();
            else
                requirementsList = (List<string>)requirements.ToObject<List<string>>();

            Debug.LogFormat("var state: {0}, title {1}, desc {2}, reqs {3}", state, title, description, requirementsList);

            states.Add(new StoryGraphState(title, description, requirementsList));
        }

        Debug.LogFormat("StoryGraph loaded: {0} states", CountStates().ToString());
	}

    //! Changes a state's complete condition to true if it meets the requirements.
    /*! 
     * \param stateTitle Title of state that is to be completed.
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
            }
		}
	}

    //! Tests if state is completed.
    /*! 
     * \param stateTitle State title.
     * \return State completed condition.
     */
    public bool IsStateCompleted(string stateTitle)
    {
        return GetStateWithTitle(stateTitle).completed;
    }

    //! Tests if state is unlocked.
    /*! 
     * \param stateTitle State title.
     * \return State unlocked condition.
     */
    public bool IsStateUnlocked(string stateTitle)
    {
        return GetStateWithTitle(stateTitle).unlocked;
    }

    //! Test method for resetting states complete conditions to false.
    public void ResetStory()
    {
        // Mainly used for testing - resets the story by marking all states as incomplete.
        foreach (var state in states)
            state.completed = false;
    }

    //! Searches list of states for a state with the title given.
    /*!
     * \param stateTitle Title of state being searched for.
     * \return State that is found.
     */ 
	private StoryGraphState GetStateWithTitle(string stateTitle)
	{
		/* 
         * Returns the state with the given title
		 */
		foreach (var state in states)
		{
            Debug.Log("GetStateWithTitle considering state: " + state.title);
			if (state.title == stateTitle)
			{
				return state;
			}
		}
        throw new StateNotFound(stateTitle);
	}

    //! Test method for finding the number of states.
    /*!
     * \return Number of states.
     */ 
    public int CountStates()
    {
        // Only used for testing
        return states.Count;
    }
}

//! StateNotFound Exception.
/*! When a state is attempted to be referenced, but does not exist, it will throw this exception. */
public class StateNotFound : Exception
{
    //! StateNotFound error message.
    /*!
     * \param stateTitle Title of state that was not found.
     */ 
    public StateNotFound(string stateTitle) :
        base("GetStateWithTitle called for '" + stateTitle + "' but it was not found. This is likely a typo")
    { }
}

//! StateRequirementsNotMet Exception.
/*! When a state is attempted to be switched to a completed position, but the requirements for completion are not met properly, it will throw this exception. */
public class StateRequirementsNotMet : Exception
{
    //! StateRequirementsNotMet error message.
    /*!
     * \param stateTitle Title of state that has incorrect parameters.
     */
    public StateRequirementsNotMet(string stateTitle) :
        base("CompleteState called for '" + stateTitle + "' but requirements not met.")
    { }
}
