using System.Collections.Generic;

public class Mystery1Graph : StoryGraph
{
	/* DO NOT EDIT: This file was generated automatically by twine2storygraph.py on 01/21/17 15:31:36
     * (See "MurderMystery/Helpers")
     * Hint: in Visual Studio highlight all and press Ctrl+k+f to fix indentation
	 */
    public Mystery1Graph(StoryScript storyScript) : base(storyScript)
    {
        this.storyName = "Mystery1";
        this.states = new List<StoryGraphState>();

		{
	string title = "Intro";
	string description = "Talk to main hall receptionist";
	string[] requirements = {};
	Dictionary<Constants.People, Dictionary<string, string>> dialogue = new Dictionary<Constants.People, Dictionary<string, string>>();
	dialogue[Constants.People.Receptionist] = new Dictionary<string, string>
{
    {"NO_TOPIC", "Hi Poirot, we're glad to see you. There's been a murder in the Ron Cooke Hub, PabloEscobar's dead. Sorry we didn't check to see how he died, thought it would be more fun for you to find out..."}
};


	AddState(new StoryGraphState(title, description, requirements, dialogue));
}
{
	string title = "Explore kitchen";
	string description = "Nobody in kitchen. Find everybody's meal orders. They all have burger, apart from pablo who had falafel.";
	string[] requirements = {"Intro"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogue = new Dictionary<Constants.People, Dictionary<string, string>>();
	
	AddState(new StoryGraphState(title, description, requirements, dialogue));
}
{
	string title = "Inspect Reception";
	string description = "Talk to Recpetionist about the CCTV footage.";
	string[] requirements = {"Intro"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogue = new Dictionary<Constants.People, Dictionary<string, string>>();
	dialogue[Constants.People.Receptionist] = new Dictionary<string, string>
{
    {"CCTV", "We only have footagee from inside the lecture theatre, and it's just a video of JamesBond pacing around. I hope that's useful."}
};


	AddState(new StoryGraphState(title, description, requirements, dialogue));
}
{
	string title = "Question Bond";
	string description = "Bond explains that he was meeting the Queen. They had a conversation about Brexit. Apparently Buckingham Palace is getting renovated and they were discussing interior decorating.";
	string[] requirements = {"Inspect Reception"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogue = new Dictionary<Constants.People, Dictionary<string, string>>();
	dialogue[Constants.People.JamesBond] = new Dictionary<string, string>
{
    {"NO_TOPIC", "Hello Poirot, can I help you with anything?"}
};


	AddState(new StoryGraphState(title, description, requirements, dialogue));
}
{
	string title = "Find catering notes";
	string description = "You find some catering notes lying around, on it is a note saying 'Dumbledore needs to speak to the chef'.";
	string[] requirements = {"Intro"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogue = new Dictionary<Constants.People, Dictionary<string, string>>();
	
	AddState(new StoryGraphState(title, description, requirements, dialogue));
}
{
	string title = "Inspect lecture theatre";
	string description = "There are two sets of footprints where James Bond was in the CCTV. That's suspicious.";
	string[] requirements = {"Inspect Reception"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogue = new Dictionary<Constants.People, Dictionary<string, string>>();
	
	AddState(new StoryGraphState(title, description, requirements, dialogue));
}
{
	string title = "Inspect bin store";
	string description = "You meet Trump who says he heard non-famous voices coming from the balcony - is very worried he may have to talk to somebody with no influence.";
	string[] requirements = {"Intro"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogue = new Dictionary<Constants.People, Dictionary<string, string>>();
	dialogue[Constants.People.DonaldTrump] = new Dictionary<string, string>
{
    {"NO_TOPIC", "How's it going Poirot?"},
{"general", "Actually I did notice something fishy going on, I heard some people talking up on the balcony but get this - they weren't even famous! I know right, don't know what that's all about."}
};


	AddState(new StoryGraphState(title, description, requirements, dialogue));
}
{
	string title = "Find wanted poster in bins";
	string description = "You find a wanted poster with a picture of Pablo on.";
	string[] requirements = {"Intro"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogue = new Dictionary<Constants.People, Dictionary<string, string>>();
	
	AddState(new StoryGraphState(title, description, requirements, dialogue));
}
{
	string title = "Find Mercury's clothing";
	string description = "You find a ripped piece of Freddy Mercury's clothing.";
	string[] requirements = {"Intro"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogue = new Dictionary<Constants.People, Dictionary<string, string>>();
	
	AddState(new StoryGraphState(title, description, requirements, dialogue));
}
{
	string title = "Inspect stairwell";
	string description = "Nobody is around. You find an epipen with 'Deliver in case of gluten ingestion' written on the side.";
	string[] requirements = {"Explore kitchen"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogue = new Dictionary<Constants.People, Dictionary<string, string>>();
	
	AddState(new StoryGraphState(title, description, requirements, dialogue));
}
{
	string title = "Inspect common room";
	string description = "Find Marilyn Monroe. She mentions that the queen has been the least talkative and quite emotionless and boring.";
	string[] requirements = {"Intro"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogue = new Dictionary<Constants.People, Dictionary<string, string>>();
	dialogue[Constants.People.MarilynMonroe] = new Dictionary<string, string>
{
    {"NO_TOPIC", "Hello Poirot, can I assist you with anything?"},
{"general", "Nothing particularly exciting has happened, TheQueen has been a bit stony this evening, hasn't spoken much and is just a bit boring."}
};


	AddState(new StoryGraphState(title, description, requirements, dialogue));
}
{
	string title = "Talk to Mercury about clothing";
	string description = "Mercury says his ripped clothes were because he was dancing too hard.";
	string[] requirements = {"Find Mercury's clothing"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogue = new Dictionary<Constants.People, Dictionary<string, string>>();
	dialogue[Constants.People.FreddieMercury] = new Dictionary<string, string>
{
    {"NO_TOPIC", "Hey Poirot, what can I do for you?"},
{"clothes", "Oh that's mine yeah, I think that's a bit of my jeans. Must have done it dancing with PabloEscobar earlier on."}
};


	AddState(new StoryGraphState(title, description, requirements, dialogue));
}
{
	string title = "Find a bloody dagger";
	string description = "You find a bloody dagger lying under a chair.";
	string[] requirements = {"Intro"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogue = new Dictionary<Constants.People, Dictionary<string, string>>();
	
	AddState(new StoryGraphState(title, description, requirements, dialogue));
}
{
	string title = "Talk to Dumbledore about suspicions";
	string description = "Go to the balcony. Dumbledore says that he is an aspiring chef, he mentioned this to the receptionist who made a note to say that the chef should talk to him.";
	string[] requirements = {"Find catering notes","Find Dumbledore's Cookbook"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogue = new Dictionary<Constants.People, Dictionary<string, string>>();
	dialogue[Constants.People.Dumbledore] = new Dictionary<string, string>
{
    {"cooking", "You think I'm the killer? Because of that? HAHAHAHAHA don't quit your day job my friend, I just appreciate good food."},
{"NO_TOPIC", "Hello there Poirot, any luck?"}
};


	AddState(new StoryGraphState(title, description, requirements, dialogue));
}
{
	string title = "Inspect balcony";
	string description = "Find a walkie talkie on the balcony inside a flower pot (or something inconspicuous).";
	string[] requirements = {"Intro"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogue = new Dictionary<Constants.People, Dictionary<string, string>>();
	
	AddState(new StoryGraphState(title, description, requirements, dialogue));
}
{
	string title = "Inspect island";
	string description = "Talk to Queen and ask her where she was at time of death. She says she was with Bond talking about things.";
	string[] requirements = {"Question Bond"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogue = new Dictionary<Constants.People, Dictionary<string, string>>();
	dialogue[Constants.People.TheQueen] = new Dictionary<string, string>
{
    {"CCTV", "Well I was in the middle of a conversation with JamesBond actually, I'll be honest they do like to chatter..."},
{"NO_TOPIC", "Oh hi Poirot, you startled me!"}
};


	AddState(new StoryGraphState(title, description, requirements, dialogue));
}
{
	string title = "Find Dumbledore's Cookbook";
	string description = "You find a small rune-bound book on the floor. Inside are some very exotic sounding recipes and Dumbledore's name is incribed on the inside cover.";
	string[] requirements = {"Intro"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogue = new Dictionary<Constants.People, Dictionary<string, string>>();
	
	AddState(new StoryGraphState(title, description, requirements, dialogue));
}
{
	string title = "Inspect 360 room";
	string description = "Nobody is there but there's a montage of Freddy Mercury dancing his tits off.";
	string[] requirements = {"Talk to Mercury about clothing"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogue = new Dictionary<Constants.People, Dictionary<string, string>>();
	
	AddState(new StoryGraphState(title, description, requirements, dialogue));
}
{
	string title = "Talk to Queen about Mercury's clothes";
	string description = "Ask the Queen what she thinks about Mercury's ripped clothes, she says she saw his clothes all in order before the murder...";
	string[] requirements = {"Find Mercury's clothing"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogue = new Dictionary<Constants.People, Dictionary<string, string>>();
	dialogue[Constants.People.TheQueen] = new Dictionary<string, string>
{
    {"NO_TOPIC", "Hi how is the detective work coming along?"},
{"clothes", "Well before the murder I'm fairly sure all FreddieMercury's clothes were in tact. How curious..."}
};


	AddState(new StoryGraphState(title, description, requirements, dialogue));
}
{
	string title = "Talk to Queen";
	string description = "Says that it's a shame about Pablo, says she had a long, deep conversation with him about all the wonderful things he did for the people of Columbia and for cocaine as an industry.";
	string[] requirements = {"Intro"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogue = new Dictionary<Constants.People, Dictionary<string, string>>();
	dialogue[Constants.People.TheQueen] = new Dictionary<string, string>
{
    {"NO_TOPIC", "Hello detective, do you need a hand with anything?"}
};


	AddState(new StoryGraphState(title, description, requirements, dialogue));
}
{
	string title = "Talk to Receptionist about Dinner";
	string description = "Ask them about the epipens. They explain he wanted a gluten-free meal, thought he was just a hipster.";
	string[] requirements = {"Inspect stairwell"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogue = new Dictionary<Constants.People, Dictionary<string, string>>();
	dialogue[Constants.People.Receptionist] = new Dictionary<string, string>
{
    {"allergy", "Oh that's PabloEscobar's, maybe they dropped it. They asked for a gluten-free meal but we just thought they were a bit of a hipster."},
{"NO_TOPIC", "What's new detective?"}
};


	AddState(new StoryGraphState(title, description, requirements, dialogue));
}

	}
}
