"""
This is a script to convert a Twine story (see http://twinery.org/), into a simplified JSON representation.

The BeautifulSoup4 library is needed.
> pip install beautifulsoup4

Usage:
    Set TWINE_STORY_FILE_PATH to the path of the input Twine story.
    Set OUTPUT_FILE_PATH to where you want the output JSON to go.
    Run the script
    > python twine2storygraph.py

The output JSON looks something like this:
    {
    "states": [
        {
            "description": "Talk to main hall receptionist", 
            "title": "Intro"
        }, 
        {
            "requirements": [
                "Intro"
            ], 
            "description": "Nobody in kitchen. Find everybody's meal orders. They all have Beef Wellington, apart from pablo who had Chicken Korma - what a wimp.", 
            "title": "Explore kitchen"
        }, 
        ...
    ]
    }

"""
from bs4 import BeautifulSoup
import json
import re

TWINE_STORY_FILE_PATH = "C:\Users\Ben\Projects\ElementarySEPR\git2\ElementarySEPR\MurderMystery\Helpers\mystery1.html"
OUTPUT_FILE_PATH = "C:\Users\Ben\Projects\ElementarySEPR\git2\ElementarySEPR\MurderMystery\Helpers\mystery1.json"

def read_story():
    with open(TWINE_STORY_FILE_PATH, 'r') as twine_f:
        return BeautifulSoup(twine_f.read(), "html.parser")

def get_output(soup):
    story_data = soup.find("tw-storydata")
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
    output = {}
    output["story_name"] = story_data.attrs["name"]
    output["states"] = []
    for state in story_states:
        title = state.attrs["name"]
        description = re.match(r"(.*)(\[\[)?", state.string).group(1)

        state_output = {}
        state_output["title"] = title
        state_output["description"] = description
        if title in requirements:
            state_output["requirements"] = requirements[title]

        output["states"].append(state_output)

    return output

def write_output(output):
    with open(OUTPUT_FILE_PATH, 'w') as f:
        f.write(json.dumps(output, sort_keys=False, indent=4))

print("Reading story from: " + TWINE_STORY_FILE_PATH)
soup = read_story()
print("Getting output...")
output = get_output(soup)
print("Writing output to: " + OUTPUT_FILE_PATH)
write_output(output)
print("Done!")
