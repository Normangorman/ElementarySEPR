from bs4 import BeautifulSoup
import json
import re

TWINE_STORY_FILE_PATH = "C:/Users/Ben/Projects/ElementarySEPR/SEPR Mystery 1 v2.html"
OUTPUT_FILE_PATH = "C:/Users/Ben/Projects/ElementarySEPR/git/MurderMystery/Assets/Scripts/Story/Mystery1.json"

with open(TWINE_STORY_FILE_PATH, 'r') as twine_f:
    soup = BeautifulSoup(twine_f.read(), "html.parser")

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

with open(OUTPUT_FILE_PATH, 'w') as f:
    f.write(json.dumps(output, sort_keys=False, indent=4))

print("Done!")