using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.IO;
using System;

public class StoryGraph {
	/* 
     * This class represents the graph of a story in the game.
	 * It has states which can be unlocked by the player by performing certain actions in the game.
	 */

	private class StoryGraphState {
		/* Represents a particular state within the story.
		 */ 
		public readonly string title;
		public readonly string description;
		public readonly List<string> requirements;
		public bool completed = false;
        public bool unlocked = false;

		public StoryGraphState(string title, string description, List<string> requirements)
		{
            //Debug.LogFormat("Creating new StoryGraphState: {0}, {1}, {2}", title, description, requirements.Count);
			this.title = title;
			this.description = description;
			this.requirements = requirements;
            if (requirements.Count == 0)
                unlocked = true;
		}
	}

	private List<StoryGraphState> states = new List<StoryGraphState>(); // perhaps an an actual graph data structure would be better
	private StoryScript storyScript;

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

            //Debug.LogFormat("var state: {0}, title {1}, desc {2}, reqs {3}", state, title, description, requirementsList);

            states.Add(new StoryGraphState(title, description, requirementsList));
        }

        Debug.LogFormat("StoryGraph loaded: {0} states", CountStates().ToString());
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
            }
		}
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

    public int CountStates()
    {
        // Only used for testing
        return states.Count;
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
