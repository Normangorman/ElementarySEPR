using System.Collections.Generic;

public class ExampleStoryGraph : StoryGraph
{
	/* DO NOT EDIT: This file was generated automatically by twine2storygraph.py on 01/23/17 17:33:48
     * (See "MurderMystery/Helpers")
     * Hint: in Visual Studio highlight all and press Ctrl+k+f to fix indentation
	 */
    public ExampleStoryGraph(StoryScript storyScript) : base(storyScript)
    {
        this.storyName = "ExampleStory";
        this.storySynopsis = "This is the story synopsis.";
        this.states = new List<StoryGraphState>();

        this.clueDescriptions = new Dictionary<Constants.Clues, string>();
        this.clueDescriptions[Constants.Clues.Knife] = "A bloody knife";


        {
        string title = "Intro";
	string[] requirements = {};
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnUnlocked = new Dictionary<Constants.People, Dictionary<string, string>>();
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnCompleted = new Dictionary<Constants.People, Dictionary<string, string>>();
	dialogueOnUnlocked[Constants.People.TheQueen] = new Dictionary<string, string>
{
    {"Love", "Do you love me?"},
{"NO_TOPIC", "Hello"}
};


dialogueOnCompleted[Constants.People.TheQueen] = new Dictionary<string, string>
{
    {"Love", "I love you."}
};


	AddState(new StoryGraphState(title, requirements, dialogueOnUnlocked, dialogueOnCompleted));
}
{
        string title = "Find knife";
	string[] requirements = {"Intro"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnUnlocked = new Dictionary<Constants.People, Dictionary<string, string>>();
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnCompleted = new Dictionary<Constants.People, Dictionary<string, string>>();
	dialogueOnUnlocked[Constants.People.TheQueen] = new Dictionary<string, string>
{
    {"Knife", "You found a knife! Looks sharp!"}
};



	AddState(new StoryGraphState(title, requirements, dialogueOnUnlocked, dialogueOnCompleted));
}

    }
}
