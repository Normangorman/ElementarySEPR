using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.IO;

public class StoryGraph {
	/* This class represents the graph of a story in the game.
	 * It has states which can be unlocked by the player by performing certain actions in the game.
	 */

	private class StoryGraphState {
		/* Represents a particular state within the story.
		 */ 
		public readonly string title;
		public readonly string description;
		public readonly List<string> requirements;
		public bool completed = false;
		public StoryGraphState(string title, string description, List<string> requirements)
		{
			this.title = title;
			this.description = description;
			this.requirements = requirements;
		}
	}

	private List<StoryGraphState> states = new List<StoryGraphState>(); // perhaps an an actual graph data structure would be better
	private StoryScript storyScript;

	public StoryGraph(StoryScript storyScript, string graphFilePath)
	{
		/* Takes a path to a json file specifying the structure of the story.
         * See Assets/Scripts/Story/example_story.json for an example
		 */
		Debug.Log("Loading story graph from: " + graphFilePath);
		this.storyScript = storyScript;

        JObject graphJSON = JObject.Parse(File.ReadAllText(graphFilePath));
        JArray statesJSON = (JArray)graphJSON.GetValue("states");
        foreach (var state in statesJSON.Children<JObject>())
        {
            List<string> stateRequirements = state.Value<List<string>>("requirements");
            states.Add(new StoryGraphState(
                state.Value<string>("title"),
                state.Value<string>("description"),
                stateRequirements));
        }
	}

	public void CompleteState(string stateTitle)
	{
        /* Marks the state with the given title as being completed.
         * Also calls the StoryScript's OnStateCompleted hook for the given state.
         * If any new states are unlocked by this completion, the OnStateUnlocked hook is called for each one.
         */
		Debug.Log("Setting state as completed: " + stateTitle);
        storyScript.OnStateCompleted(stateTitle);

		StoryGraphState state = GetStateWithTitle(stateTitle);
		state.completed = true;

		// Check if completing this state unlocked any new states
		List<StoryGraphState> childStates = new List<StoryGraphState>();
		foreach (var otherState in states)
		{
			if (otherState.requirements.Count > 0)
			{
				if (otherState.requirements.Find(x => x == stateTitle) != "")
					childStates.Add(otherState);
			}
		}

		if (childStates.Count > 0)
		{
			Debug.LogFormat("Found {} other states with this state as a parent.", childStates.Count);
			foreach (var childState in childStates) {
				bool allRequirementsComplete = true;
				foreach (string requirementTitle in childState.requirements)
				{
					if (!GetStateWithTitle(requirementTitle).completed)
					{
						allRequirementsComplete = false;
						Debug.LogFormat("Missing requirement for state {0}: {1}", childState.title, requirementTitle); 
						break;
					}
				}

				if (allRequirementsComplete)
				{
					storyScript.OnStateUnlocked(childState.title);
				}
			}
		}
	}

    public bool IsStateCompleted(string stateTitle)
    {
        return GetStateWithTitle(stateTitle).completed;
    }

	private StoryGraphState GetStateWithTitle(string stateTitle)
	{
		/* Returns the state with the given title
		 */
		foreach (var state in states)
		{
			if (state.title == stateTitle)
			{
				return state;
			}
		}
        return null;
	}
}
