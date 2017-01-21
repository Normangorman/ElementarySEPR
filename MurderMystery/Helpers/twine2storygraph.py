"""
This is a script to convert a Twine story (see http://twinery.org/), into a C# file to be used directly in our game.

The BeautifulSoup4 library is needed.
> pip install beautifulsoup4

Usage:
    Set TWINE_STORY_FILE_PATH to the path of the input Twine story.
    Set OUTPUT_FILE_PATH to where you want the output C# file to go.
    Run the script
    > python twine2storygraph.py
"""
from bs4 import BeautifulSoup
import re
import time
import json

TWINE_STORY_FILE_PATH = "C:\Users\Ben\Projects\ElementarySEPR\SEPR Mystery 1 FINAL.html"
OUTPUT_FILE_PATH = "C:\Users\Ben\Projects\ElementarySEPR\git2\ElementarySEPR\MurderMystery\Assets\Scripts\Story\Mystery1\Mystery1Graph.cs"

OUTPUT_TEMPLATE = """using System.Collections.Generic;

public class $CLASSNAME : StoryGraph
{
	/* DO NOT EDIT: This file was generated automatically by $SCRIPTNAME on $DATE
     * (See "MurderMystery/Helpers")
     * Hint: in Visual Studio highlight all and press Ctrl+k+f to fix indentation
	 */
    public $CLASSNAME(StoryScript storyScript) : base(storyScript)
    {
        this.storyName = "$STORYNAME";
        this.states = new List<StoryGraphState>();

		$STATES
	}
}
"""

STATE_BLOCK_TEMPLATE = """{
	string title = "$TITLE";
	string description = "$DESCRIPTION";
	string[] requirements = {$REQUIREMENTS};
	Dictionary<Constants.People, Dictionary<string, string>> dialogue = new Dictionary<Constants.People, Dictionary<string, string>>();
	$DIALOGUE
	AddState(new StoryGraphState(title, description, requirements, dialogue));
}
"""

DIALOGUE_TEMPLATE = """dialogue[$PERSONCONSTANT] = new Dictionary<string, string>
{
    $TOPICS
};
"""

class TwineStoryState:
	def __init__(self, title, description, requirements, dialogue):
		self.title = title
		self.description = description
		self.requirements = requirements
		self.dialogue = dialogue

class TwineStory:
	def __init__(self, twine_file_path):
		print("Reading story from: " + twine_file_path)
		with open(twine_file_path, 'r') as twine_f:
			self.soup = BeautifulSoup(twine_f.read(), "html.parser")
		self.story_name = ""
		self.aliases = {}
		self.states = []

		self._loadStoryName()
		self._loadAliases()
		self._loadStates()

	def compile(self, output_path):
        # Compiles into a C# file using OUTPUT_TEMPLATE defined above
		output = OUTPUT_TEMPLATE

		output_storyname = "".join([part.capitalize() for part in self.story_name.split(" ")])
		output_classname = output_storyname + "Graph"
                output_scriptname = __file__
                output_date = time.strftime("%x %X")
                output_states = self._getStatesOutput()

                output = output.replace("$CLASSNAME", output_classname)
                output = output.replace("$STORYNAME", output_storyname)
                output = output.replace("$SCRIPTNAME", output_scriptname)
                output = output.replace("$DATE", output_date)
                output = output.replace("$STATES", output_states)

                print("Writing output to: " + output_path)
                with open(output_path, 'w') as f:
                    f.write(output)

        def _getStatesOutput(self):
                output = ""
                for state in self.states:
                    state_output = STATE_BLOCK_TEMPLATE
                    state_output = state_output.replace("$TITLE", state.title)
                    state_output = state_output.replace("$DESCRIPTION", state.description)

                    # Build requirements output
                    output_reqs = ','.join(['"' + req + '"' for req in state.requirements])
                    state_output = state_output.replace("$REQUIREMENTS", output_reqs)

                    # Build dialogue output
                    output_dialogue = ""
                    for (alias, person_dialogue) in state.dialogue.items():
                        person_dialogue_output = DIALOGUE_TEMPLATE
                        person_dialogue_output = person_dialogue_output.replace("$PERSONCONSTANT", "Constants.People." + self.aliases[alias])
                        # The { and } are doubled up because python requires this for single brackets in str.format
                        output_topics = ",\n".join(['{{"{0}", "{1}"}}'.format(topic, text) for (topic, text) in person_dialogue.items()])
                        person_dialogue_output = person_dialogue_output.replace("$TOPICS", output_topics)

                        output_dialogue += person_dialogue_output + "\n"

                    state_output = state_output.replace("$DIALOGUE", output_dialogue)
                    output += state_output

                return output

	def _loadStoryName(self):
		story_data = self.soup.find("tw-storydata")
		self.story_name = story_data.attrs["name"]

	def _loadAliases(self):
		story_data = self.soup.find("tw-storydata")
		story_states = story_data.find_all("tw-passagedata")
		for state in story_states:
			state_title = state.attrs["name"]

			# Alias data is contained in the Intro state by convention
			if state_title == "Intro":
				print("Aliases:")
				for (alias, name) in re.findall(r'(\w+)\s+=\s+(.+)', state.string):
					print(alias + " -> " + name)
					self.aliases[alias] = name

	def _loadStates(self):
		story_data = self.soup.find("tw-storydata")
		story_states = story_data.find_all("tw-passagedata")

		# Iterate once through all states to get data on requirements
		# In twine the requirements are specified in the parent for its children, but we want the opposite:
		# for the child to specify which parents are requirements
		requirements = {}
		for state in story_states:
			state_title = state.attrs["name"]

			for link in re.findall(r"\[\[(.*)\]\]", state.string):
				child_title = link.split("|")[1]
				if child_title in requirements:
					requirements[child_title].append(state_title) 
				else:
					requirements[child_title] = [state_title]

		# Now iterate again to build up output
		for state_data in story_states:
			title = state_data.attrs["name"]
			description = state_data.string.split("\n")[0].replace("\"", "'")
			reqs = requirements[title] if title in requirements else []

			# dialogue
			# this regex is ugly but its purpose is just to extract the dialogue tags which are
			# written in the twine story. An example tag is:
			# \<char1 topic=weather>: "There's a storm brewing!"
			dialogueTags = re.findall(r'\\<(\w+)\s*(topic=(\w+))?>:\s*"(.+)"', state_data.string)
			dialogue = {}
			for (name, _, topic, text) in dialogueTags:
                                text = text.replace('"', '') # remove any quotes
                                
                                # Replace all aliases with their actual names. Maybe this should be done in C# instead??
                                # Aliases in text look like <char1>
                                text = re.sub("<(\w+)>", lambda m: self.aliases[m.group(1)], text)

                                if name not in dialogue:
                                        dialogue[name] = {}

                                if topic == "":
                                        dialogue[name]["NO_TOPIC"] = text
                                else:
                                        dialogue[name][topic] = text

			self.states.append(TwineStoryState(title, description, reqs, dialogue))

story = TwineStory(TWINE_STORY_FILE_PATH)
story.compile(OUTPUT_FILE_PATH)
print("Done!")
