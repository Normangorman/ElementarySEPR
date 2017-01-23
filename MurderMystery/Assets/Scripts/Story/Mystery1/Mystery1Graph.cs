using System.Collections.Generic;

public class Mystery1Graph : StoryGraph
{
	/* DO NOT EDIT: This file was generated automatically by twine2storygraph.py on 01/23/17 15:47:10
     * (See "MurderMystery/Helpers")
     * Hint: in Visual Studio highlight all and press Ctrl+k+f to fix indentation
	 */
    public Mystery1Graph(StoryScript storyScript) : base(storyScript)
    {
        this.storyName = "Mystery1";
        this.storySynopsis = "In this mystery, PabloEscobar has been murdered. Dumbledore is the murderer. It turns out that Pablo has an awful gluten allergy and Dumbledore has 'poisoned' his food with breadcrumbs! The motive clue is a wanted poster featuring Pablo.";
        this.states = new List<StoryGraphState>();

        this.clueDescriptions = new Dictionary<Constants.Clues, string>();
        this.clueDescriptions[Constants.Clues.WantedPoster] = "A wanted poster with a picture of Pablo Escobar on it. A reward of $1,000,000 is offered. It is torn and looks like someone has tried to dispose of it recently.";
this.clueDescriptions[Constants.Clues.BrokenTape] = "A broken video tape. It looks like someone has smashed it up on purpose. Maybe it's a CCTV tape...";
this.clueDescriptions[Constants.Clues.Money] = "A $20 note. It seems to have been discarded by someone.";
this.clueDescriptions[Constants.Clues.DumbledoresCookbook] = "A small, rune bound cookbook that seems to belong to Dumbledore. When you open the book, it seems to automatically fall on a falafel recipe.";
this.clueDescriptions[Constants.Clues.DoctorsNote] = "A doctor's note with a fairly recent date. It seems to belong to Pablo. The words EXTREME GLUTEN ALLERGY stand out.";
this.clueDescriptions[Constants.Clues.FreddysClothing] = "A ripped piece of Freddie Mercury's clothing.";
this.clueDescriptions[Constants.Clues.MealOrders] = "A list of everybody's meal orders. They all had burger, apart from Pablo who had falafel.";
this.clueDescriptions[Constants.Clues.Breadcrumbs] = "A handful of breadcrumbs. Perhaps someone was a messy eater?";
this.clueDescriptions[Constants.Clues.Epipen] = "An epipen with the words USE IN CASE OF GLUTEN INGESTION inscribed on the side.";
this.clueDescriptions[Constants.Clues.Knife] = "A large kitchen knife that seems to be bloodstained. An obvious murder weapon - although maybe someone was just chopping meat?";
this.clueDescriptions[Constants.Clues.Pistol] = "A small handgun. The safety is on and there are bullets loaded.";


        {
        string title = "Intro";
	string[] requirements = {};
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnUnlocked = new Dictionary<Constants.People, Dictionary<string, string>>();
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnCompleted = new Dictionary<Constants.People, Dictionary<string, string>>();
	dialogueOnUnlocked[Constants.People.JamesBond] = new Dictionary<string, string>
{
    {"NO_TOPIC", "Hi Poirot... what a shocking murder! You should probably go see the Receptionist."}
};

dialogueOnUnlocked[Constants.People.Dumbledore] = new Dictionary<string, string>
{
    {"NO_TOPIC", "Hi Poirot... what a shocking murder! You should probably go see the Receptionist."}
};

dialogueOnUnlocked[Constants.People.TheQueen] = new Dictionary<string, string>
{
    {"NO_TOPIC", "Hi Poirot... what a shocking murder! You should probably go see the Receptionist."}
};

dialogueOnUnlocked[Constants.People.FreddieMercury] = new Dictionary<string, string>
{
    {"NO_TOPIC", "Hi Poirot... what a shocking murder! You should probably go see the Receptionist."}
};

dialogueOnUnlocked[Constants.People.Receptionist] = new Dictionary<string, string>
{
    {"CCTV", "Ah yes - I thought you might want to see that. We only have footage from inside the lecture theatre, and it's just a video of James Bond pacing around. I hope that's useful."},
{"NO_TOPIC", "Hi Poirot, we're glad to see you. There's been a murder in the Ron Cooke Hub, Pablo Escobar's dead. Sorry we didn't check to see how he died, thought it would be more fun for you to find out..."}
};

dialogueOnUnlocked[Constants.People.MarilynMonroe] = new Dictionary<string, string>
{
    {"NO_TOPIC", "Hi Poirot... what a shocking murder! You should probably go see the Receptionist."}
};

dialogueOnUnlocked[Constants.People.DonaldTrump] = new Dictionary<string, string>
{
    {"NO_TOPIC", "Hi Poirot... what a shocking murder! You should probably go see the Receptionist."}
};


dialogueOnCompleted[Constants.People.JamesBond] = new Dictionary<string, string>
{
    {"NO_TOPIC", "This party was so great. It's a shame how much death kills the vibe right?"}
};

dialogueOnCompleted[Constants.People.Dumbledore] = new Dictionary<string, string>
{
    {"NO_TOPIC", "What a tragedy... Pablo was so young and so talented..."}
};

dialogueOnCompleted[Constants.People.TheQueen] = new Dictionary<string, string>
{
    {"NO_TOPIC", "One is most disturbed by this horrible event."}
};

dialogueOnCompleted[Constants.People.FreddieMercury] = new Dictionary<string, string>
{
    {"NO_TOPIC", "WAZZUP dude? Is this just fantasy or what? MURDER on such a beautiful evening? I can hardly believe it."}
};

dialogueOnCompleted[Constants.People.MarilynMonroe] = new Dictionary<string, string>
{
    {"NO_TOPIC", "Hey Poirot. How's the investigation going?"},
{"James Bond", "I've been trying to talk to James Bond this evening but he is just so stony."}
};

dialogueOnCompleted[Constants.People.DonaldTrump] = new Dictionary<string, string>
{
    {"NO_TOPIC", "Hey buddy, how's the investigation going? If I was you, I would have my eyes on James Bond, he seems like a shady character."}
};


	AddState(new StoryGraphState(title, requirements, dialogueOnUnlocked, dialogueOnCompleted));
}
{
        string title = "Find meal orders";
	string[] requirements = {"Intro"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnUnlocked = new Dictionary<Constants.People, Dictionary<string, string>>();
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnCompleted = new Dictionary<Constants.People, Dictionary<string, string>>();
	

	AddState(new StoryGraphState(title, requirements, dialogueOnUnlocked, dialogueOnCompleted));
}
{
        string title = "Question Receptionist about CCTV";
	string[] requirements = {"Intro"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnUnlocked = new Dictionary<Constants.People, Dictionary<string, string>>();
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnCompleted = new Dictionary<Constants.People, Dictionary<string, string>>();
	

	AddState(new StoryGraphState(title, requirements, dialogueOnUnlocked, dialogueOnCompleted));
}
{
        string title = "Question Bond about CCTV";
	string[] requirements = {"Question Receptionist about CCTV"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnUnlocked = new Dictionary<Constants.People, Dictionary<string, string>>();
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnCompleted = new Dictionary<Constants.People, Dictionary<string, string>>();
	dialogueOnUnlocked[Constants.People.JamesBond] = new Dictionary<string, string>
{
    {"NO_TOPIC", "Hello Poirot, can I help you with anything?"},
{"CCTV: $TheQueen was meant to meet me in there at around 20", "00 for a discussion about Brexit, but is clearly not feeling very punctual today. I had to wait around for a while."}
};



	AddState(new StoryGraphState(title, requirements, dialogueOnUnlocked, dialogueOnCompleted));
}
{
        string title = "Find wanted poster";
	string[] requirements = {"Intro"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnUnlocked = new Dictionary<Constants.People, Dictionary<string, string>>();
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnCompleted = new Dictionary<Constants.People, Dictionary<string, string>>();
	
dialogueOnCompleted[Constants.People.TheQueen] = new Dictionary<string, string>
{
    {"Wanted poster", "A $1,000,000 reward!? Buckingham Palace is in need of a new roof you know..."}
};

dialogueOnCompleted[Constants.People.DonaldTrump] = new Dictionary<string, string>
{
    {"Wanted poster", "Huh, I didn't realize Pablo was a wanted man. With such a large reward - I can see how topping him off might be... temping"}
};


	AddState(new StoryGraphState(title, requirements, dialogueOnUnlocked, dialogueOnCompleted));
}
{
        string title = "Find Mercury's clothing";
	string[] requirements = {"Intro"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnUnlocked = new Dictionary<Constants.People, Dictionary<string, string>>();
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnCompleted = new Dictionary<Constants.People, Dictionary<string, string>>();
	
dialogueOnCompleted[Constants.People.FreddieMercury] = new Dictionary<string, string>
{
    {"Ripped clothing", "Oh yeah I think that's a bit of my jeans. I just love to dance. It must have torn off earlier."}
};

dialogueOnCompleted[Constants.People.TheQueen] = new Dictionary<string, string>
{
    {"Ripped clothing", "Well before the murder I'm fairly sure all Freddie Mercury's clothes were intact. How curious..."}
};


	AddState(new StoryGraphState(title, requirements, dialogueOnUnlocked, dialogueOnCompleted));
}
{
        string title = "Find epipen";
	string[] requirements = {"Intro"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnUnlocked = new Dictionary<Constants.People, Dictionary<string, string>>();
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnCompleted = new Dictionary<Constants.People, Dictionary<string, string>>();
	
dialogueOnCompleted[Constants.People.Receptionist] = new Dictionary<string, string>
{
    {"Epipen", "Hmm, that might belong to Pablo Escobar actually. They were the only one who ordered a gluten-free meal."}
};


	AddState(new StoryGraphState(title, requirements, dialogueOnUnlocked, dialogueOnCompleted));
}
{
        string title = "Find a bloody kitchen knife";
	string[] requirements = {"Intro"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnUnlocked = new Dictionary<Constants.People, Dictionary<string, string>>();
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnCompleted = new Dictionary<Constants.People, Dictionary<string, string>>();
	
dialogueOnCompleted[Constants.People.Dumbledore] = new Dictionary<string, string>
{
    {"Knife", "A knife is such an inefficient weapon."}
};

dialogueOnCompleted[Constants.People.JamesBond] = new Dictionary<string, string>
{
    {"Knife", "You found a knife? Looks like an ideal murder weapon... although the body doesn't seem to have any wounds..."}
};

dialogueOnCompleted[Constants.People.MarilynMonroe] = new Dictionary<string, string>
{
    {"Knife>", "Ewww blood. Gross."}
};


	AddState(new StoryGraphState(title, requirements, dialogueOnUnlocked, dialogueOnCompleted));
}
{
        string title = "Question Queen about meeting with Bond";
	string[] requirements = {"Question Bond about CCTV"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnUnlocked = new Dictionary<Constants.People, Dictionary<string, string>>();
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnCompleted = new Dictionary<Constants.People, Dictionary<string, string>>();
	dialogueOnUnlocked[Constants.People.TheQueen] = new Dictionary<string, string>
{
    {"Meeting with Bond", "Oh yes - me and James had a hearty chat about Brexit."}
};



	AddState(new StoryGraphState(title, requirements, dialogueOnUnlocked, dialogueOnCompleted));
}
{
        string title = "Find Dumbledore's Cookbook";
	string[] requirements = {"Intro"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnUnlocked = new Dictionary<Constants.People, Dictionary<string, string>>();
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnCompleted = new Dictionary<Constants.People, Dictionary<string, string>>();
	
dialogueOnCompleted[Constants.People.Dumbledore] = new Dictionary<string, string>
{
    {"Cookbook", "You think I'm the killer? Because of that? HAHAHAHAHA don't quit your day job my friend, I just appreciate good food."},
{"NO_TOPIC", "Hello there Poirot, any luck with the mystery?"}
};


	AddState(new StoryGraphState(title, requirements, dialogueOnUnlocked, dialogueOnCompleted));
}
{
        string title = "Find a broken tape";
	string[] requirements = {"Intro"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnUnlocked = new Dictionary<Constants.People, Dictionary<string, string>>();
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnCompleted = new Dictionary<Constants.People, Dictionary<string, string>>();
	
dialogueOnCompleted[Constants.People.Receptionist] = new Dictionary<string, string>
{
    {"Broken tape", "You found this in the bins? Hmm... the room code on the tape is 056 - that means it's the tape for the Kitchen. It must have been stolen!"}
};


	AddState(new StoryGraphState(title, requirements, dialogueOnUnlocked, dialogueOnCompleted));
}
{
        string title = "Find $20 note";
	string[] requirements = {"Intro"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnUnlocked = new Dictionary<Constants.People, Dictionary<string, string>>();
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnCompleted = new Dictionary<Constants.People, Dictionary<string, string>>();
	

	AddState(new StoryGraphState(title, requirements, dialogueOnUnlocked, dialogueOnCompleted));
}
{
        string title = "Find breadcrumbs";
	string[] requirements = {"Intro"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnUnlocked = new Dictionary<Constants.People, Dictionary<string, string>>();
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnCompleted = new Dictionary<Constants.People, Dictionary<string, string>>();
	

	AddState(new StoryGraphState(title, requirements, dialogueOnUnlocked, dialogueOnCompleted));
}
{
        string title = "Find doctor's note";
	string[] requirements = {"Intro"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnUnlocked = new Dictionary<Constants.People, Dictionary<string, string>>();
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnCompleted = new Dictionary<Constants.People, Dictionary<string, string>>();
	

	AddState(new StoryGraphState(title, requirements, dialogueOnUnlocked, dialogueOnCompleted));
}
{
        string title = "Find gun";
	string[] requirements = {"Intro"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnUnlocked = new Dictionary<Constants.People, Dictionary<string, string>>();
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnCompleted = new Dictionary<Constants.People, Dictionary<string, string>>();
	
dialogueOnCompleted[Constants.People.JamesBond] = new Dictionary<string, string>
{
    {"Gun", "Oh you found my pistol. I take it with me everywhere but I must have dropped it."}
};

dialogueOnCompleted[Constants.People.MarilynMonroe] = new Dictionary<string, string>
{
    {"Gun", "You found a gun!! What a perfect murder weapon!"}
};


	AddState(new StoryGraphState(title, requirements, dialogueOnUnlocked, dialogueOnCompleted));
}
{
        string title = "Question Trump about money";
	string[] requirements = {"Find $20 note"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnUnlocked = new Dictionary<Constants.People, Dictionary<string, string>>();
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnCompleted = new Dictionary<Constants.People, Dictionary<string, string>>();
	dialogueOnUnlocked[Constants.People.DonaldTrump] = new Dictionary<string, string>
{
    {"Money", "You found a $20 note? I must have dropped that earlier. You know, I was talking to Dumbledore earlier and he mentioned that Hogwarts is in real financial trouble right now. Maybe you should give it to him HAHAHA!"}
};



	AddState(new StoryGraphState(title, requirements, dialogueOnUnlocked, dialogueOnCompleted));
}
{
        string title = "Accuse Dumbledore";
	string[] requirements = {"Find wanted poster","Question Trump about money"};
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnUnlocked = new Dictionary<Constants.People, Dictionary<string, string>>();
	Dictionary<Constants.People, Dictionary<string, string>> dialogueOnCompleted = new Dictionary<Constants.People, Dictionary<string, string>>();
	

	AddState(new StoryGraphState(title, requirements, dialogueOnUnlocked, dialogueOnCompleted));
}

    }
}
