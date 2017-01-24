# -*- coding: utf-8 -*-
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
import sys

TWINE_STORY_FILE_PATH = "C:\Users\Ben\Projects\ElementarySEPR\git3\ElementarySEPR\MurderMystery\Assets\TwineStories\Mystery 1 FINAL.html"
OUTPUT_FILE_PATH = "C:\Users\Ben\Projects\ElementarySEPR\git3\ElementarySEPR\MurderMystery\Assets\Scripts\Story\Mystery1\Mystery1Graph.cs"

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
        this.storySynopsis = "$SYNOPSIS";
        this.states = new List<StoryGraphState>();

        this.clueDescriptions = new Dictionary<Constants.Clues, string>();
        $CLUEDESCRIPTIONS

        $STATES
    }
}
"""

STATE_BLOCK_TEMPLATE = """{
        string title = "$TITLE";
	string[] requirements = {$REQUIREMENTS};
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnUnlocked = new Dictionary<Constants.People, Dictionary<string, string>>();
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnCompleted = new Dictionary<Constants.People, Dictionary<string, string>>();
	$DIALOGUE
	AddState(new StoryGraphState(title, requirements, dialogueOnUnlocked, dialogueOnCompleted));
}
"""

# $WHEN is either dialogueOnUnlocked or dialogueOnCompleted
DIALOGUE_TEMPLATE = """$WHEN[$PERSONCONSTANT] = new Dictionary<string, string>
{
    $TOPICS
};
"""

CLUE_TEMPLATE = """this.clueDescriptions[Constants.Clues.$CLUE] = "$DESCRIPTION";"""

class TwineStoryState:
        def __init__(self, title, requirements, dialogueOnUnlocked, dialogueOnCompleted):
		self.title = title
		self.requirements = requirements
		self.dialogueOnUnlocked = dialogueOnUnlocked
		self.dialogueOnCompleted = dialogueOnCompleted

class TwineStory:
	def __init__(self, twine_file_path):
		print("Reading story from: " + twine_file_path)
		with open(twine_file_path, 'r') as twine_f:
                        twine_text = twine_f.read().replace("&lt;", "<").replace("&gt;", ">")
			self.soup = BeautifulSoup(twine_text, "html.parser")
		self.story_name = ""
                self.story_synopsis = ""
                self.clue_descriptions = {}
		self.aliases = {}
		self.states = []

		self._loadStoryName()
                self._loadStorySynopsis()
                self._loadClues()
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
                output_synopsis = self.story_synopsis
                output_clues = self._getOutputClueDescriptions()

                output = output.replace("$CLASSNAME", output_classname)
                output = output.replace("$STORYNAME", output_storyname)
                output = output.replace("$SCRIPTNAME", output_scriptname)
                output = output.replace("$DATE", output_date)
                output = output.replace("$SYNOPSIS", output_synopsis)
                output = output.replace("$CLUEDESCRIPTIONS", output_clues)
                output = output.replace("$STATES", output_states)

                print("Writing output to: " + output_path)
                with open(output_path, 'w') as f:
                    f.write(output.encode('utf-8'))

        def _getStatesOutput(self):
                output = ""
                for state in self.states:
                    state_output = STATE_BLOCK_TEMPLATE
                    state_output = state_output.replace("$TITLE", state.title)

                    # Build requirements output
                    output_reqs = ','.join(['"' + req + '"' for req in state.requirements])
                    state_output = state_output.replace("$REQUIREMENTS", output_reqs)

                    # Build dialogue output
                    output_dialogue = self._getOutputDialogue(state, "OnUnlocked") + "\n" + self._getOutputDialogue(state, "OnCompleted")
                   
                    state_output = state_output.replace("$DIALOGUE", output_dialogue)
                    output += state_output

                return output

        def _getOutputClueDescriptions(self):
            output = ""
            for (clue, desc) in self.clue_descriptions.items():
                output += CLUE_TEMPLATE.replace("$CLUE", clue.replace('"', '')).replace("$DESCRIPTION", desc) + "\n"
            return output

        def _getOutputDialogue(self, state, when="OnUnlocked"):
                output = ""
                if when == "OnUnlocked":
                        dialogue = state.dialogueOnUnlocked
                else:
                        dialogue = state.dialogueOnCompleted
                        
                for (alias, person_dialogue) in dialogue.items():
                        person_output = DIALOGUE_TEMPLATE
                        person_output = person_output.replace("$WHEN", "dialogueOnUnlocked" if when == "OnUnlocked" else "dialogueOnCompleted")
                        person_output = person_output.replace("$PERSONCONSTANT", "Constants.People." + self.aliases[alias])
                        # The { and } are doubled up because python requires this for single brackets in str.format
                        output_topics = ",\n".join(['{{"{0}", "{1}"}}'.format(topic, text) for (topic, text) in person_dialogue.items()])
                        person_output = person_output.replace("$TOPICS", output_topics)

                        output += person_output + "\n"

                return output
                        

	def _loadStoryName(self):
		story_data = self.soup.find("tw-storydata")
		self.story_name = story_data.attrs["name"]

        def _loadStorySynopsis(self):
                # Synopsis data is contained in the Intro state by convention
                intro_state = self._getIntroState()
                try:
                        synopsis_tag = intro_state.findAll('synopsis')[0]
                except IndexError:
                        print("ERROR: Could not find synopsis tag on intro state")
                        #print(intro_state)
                        #print(intro_state.string)
                        sys.exit(1)
                print("Synopsis: " + synopsis_tag.string)
                self.story_synopsis = synopsis_tag.string

	def _loadAliases(self):
                # Alias data is contained in the Intro state by convention
                intro_state = self._getIntroState()
                print("Aliases:")
                aliases_tag = intro_state.findAll('aliases')[0]
                for (alias, name) in re.findall(r'(\w+)\s+=\s+(.+)', aliases_tag.string):
                        print(alias + " -> " + name)
                        self.aliases[alias] = name

        def _loadClues(self):
                # Clues data is contained in the Intro state by convention
                intro_state = self._getIntroState() 
                print("Clues:")
                clue_tags = intro_state.findAll('clue')
                for clue_tag in clue_tags:
                        name = clue_tag['name']
                        description = clue_tag.string
                        #print("{0}: {1}".format(name, description))
                        self.clue_descriptions[name] = description

        def _loadStates(self):
		story_data = self.soup.find("tw-storydata")
		story_states = story_data.find_all("tw-passagedata")
		print("{0} tw-passagedata found".format(len(story_states)))

		# Iterate once through all states to get data on requirements
		# In twine the requirements are specified in the parent for its children, but we want the opposite:
		# for the child to specify which parents are requirements
		requirements = {}
		for state in story_states:
			state_title = state.attrs["name"]
                       
			for link in re.findall(r"\[\[(.*)\]\]", str(state)):
				child_title = link.split("|")[1]
				if child_title in requirements:
					requirements[child_title].append(state_title) 
				else:
					requirements[child_title] = [state_title]

		# Now iterate again to build states list along with their requirements
		for state_data in story_states:
			title = state_data.attrs["name"]
			reqs = requirements[title] if title in requirements else []
                        dialogueOnUnlocked = self._extractDialogue(state_data, 'dialogueunlocked')
                        dialogueOnCompleted = self._extractDialogue(state_data, 'dialoguecompleted')
                        self.states.append(TwineStoryState(title, reqs, dialogueOnUnlocked, dialogueOnCompleted))

                print("{0} in self.states".format(len(self.states)))

        def _getIntroState(self):
                # Returns the intro state as a BeautifulSoup soup
		story_data = self.soup.find("tw-storydata")
		story_states = story_data.find_all("tw-passagedata")
		for state in story_states:
			state_title = state.attrs["name"]

			if state_title == "Intro":
                                return state

        def _lookupAlias(self, alias):
                # Looks up the alias in the aliases dict
                if alias in self.aliases:
                        # Convert an alias like "JamesBond" to "James Bond"
                        name = self.aliases[alias]
                        return name[0] + camelCaseToSpaces(name[1:])
                else:
                        print("Couldn't resolve alias: " + alias)
                        return alias

        def _extractDialogue(self, state_data, tagname):
                # Extracts the dialogue from given state_data, returning a dictionary mapping topic -> text
                dialogue_tags = state_data.findAll(tagname)
                if len(dialogue_tags) != 1: # states have either no dialogue or 1 dialogue tag
                        return {}
                else:
                        all_dialogue = dialogue_tags[0].string
                        #print(dialogue_tags[0])

                dialogue = {}
                # This regex is ugly but it's purpose is just to extract the name, topic and text from a dialogue line that looks something like
                # Receptionist: Hey
                # JamesBond|CCTV: sup dude
                for (name, _, topic, text) in re.findall(r'(\w+)(\|([^:]+))?:\s*(.+)\s*', all_dialogue):
                        print("name={0}, topic={1}, text={2}".format(name, topic, text))
                        text = text.replace('"', '') # remove any double quotes
                        
                        # Replace all aliases with their actual names. Maybe this should be done in C# instead??
                        # Aliases in text look like <char1>
                        text = re.sub("\$([a-zA-Z]+)", lambda m: self._lookupAlias(m.group(1)), text)

                        if name not in dialogue:
                                dialogue[name] = {}

                        if not topic:
                                dialogue[name]["NO_TOPIC"] = text
                        else:
                                dialogue[name][topic] = text

                return dialogue

def camelCaseToSpaces(ident):
        # Converts camel case string like "testString" to "test String"
        new = ""
        for x in ident:
                if x.islower(): new += x
                else: new += " " + x
        return new

story = TwineStory(TWINE_STORY_FILE_PATH)
story.compile(OUTPUT_FILE_PATH)
"""
with open(TWINE_STORY_FILE_PATH, 'r') as twine_f:
        twine_text = twine_f.read().replace("&lt;", "<").replace("&gt;", ">")
        soup = BeautifulSoup(twine_text, "html.parser")
        story_data = soup.find("tw-storydata")
        print(story_data)
"""
print("Done!")
