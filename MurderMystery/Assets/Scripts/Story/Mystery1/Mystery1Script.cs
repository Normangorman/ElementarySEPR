using UnityEngine;

public class Mystery1Script : StoryScript {
    public Mystery1Script()
    {
        Debug.Log("Loading Mystery1Script");
        this.storyGraph = new Mystery1Graph(this);
    }

	public override void OnStateCompleted(string stateTitle)
	{
        base.OnStateCompleted(stateTitle);
	}

	public override void OnStateUnlocked(string stateTitle)
	{
        base.OnStateUnlocked(stateTitle);
	}

	public override void OnItemFound(Constants.Clues type)
	{
        base.OnItemFound(type);

        CheckClueState(type, Constants.Clues.FreddysClothing, "Find Mercury's Clothing");
        CheckClueState(type, Constants.Clues.Money, "Find $20 note");
        CheckClueState(type, Constants.Clues.WantedPoster, "Find wanted poster");
        CheckClueState(type, Constants.Clues.DumbledoresCookbook, "Find Dumbledore's Cookbook");
        CheckClueState(type, Constants.Clues.Breadcrumbs, "Find breadcrumbs");
        CheckClueState(type, Constants.Clues.Knife, "Find a bloody kitchen knife");
        CheckClueState(type, Constants.Clues.MealOrders, "Find meal orders");
        CheckClueState(type, Constants.Clues.DoctorsNote, "Find doctor's note");
        CheckClueState(type, Constants.Clues.Pistol, "Find gun");
        CheckClueState(type, Constants.Clues.Epipen, "Find epipen");
        CheckClueState(type, Constants.Clues.BrokenTape, "Find a broken tape");

	}

	public override void OnNPCSpokenTo(Constants.People person)
	{
        base.OnNPCSpokenTo(person);

        if (person == Constants.People.Receptionist)
        {
            if (storyGraph.IsStateActive("Intro"))
            {
                storyGraph.CompleteState("Intro");
            }
        }
	}

    public override void OnNPCSpokenTo(Constants.People person, string topic)
    {
        base.OnNPCSpokenTo(person, topic);

        if (person == Constants.People.Receptionist)
        {
            if (topic == "CCTV" && storyGraph.IsStateActive("Question Receptionist about CCTV"))
            {
                storyGraph.CompleteState("Question Receptionist about CCTV");
            }
        }
        else if (person == Constants.People.JamesBond)
        {
            if (topic == "CCTV" && storyGraph.IsStateActive("Question Bond about CCTV"))
            {
                storyGraph.CompleteState("Question Bond about CCTV");
            }
        }
        else if (person == Constants.People.TheQueen)
        {
            if (topic == "Meeting with Bond" && storyGraph.IsStateActive("Question Queen about meeting with Bond"))
            {
                storyGraph.CompleteState("Question Queen about meeting with Bond");
            }
        }
        else if (person == Constants.People.DonaldTrump)
        {
            if (topic == "Money" && storyGraph.IsStateActive("Question Trump about money"))
            {
                storyGraph.CompleteState("Question Trump about money");
            }
        }
    }

	public override void OnPlayerChangeRoom(Constants.Rooms room)
	{
        base.OnPlayerChangeRoom(room);
	}

    public override bool CheckAccusation(Constants.People person)
    {
        if (storyGraph.IsStateUnlocked("Accuse Dumbledore") && person == Constants.People.Dumbledore)
            return true;
        else
            return false;
    }

    private void CheckClueState(Constants.Clues foundClue, Constants.Clues stateClue, string stateName)
    {
        if (foundClue == stateClue && storyGraph.IsStateActive(stateName))
        {
            storyGraph.CompleteState(stateName);
        }
    }
}
