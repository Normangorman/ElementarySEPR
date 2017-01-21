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

	public override void OnItemFound(Clue item)
	{
        base.OnItemFound(item);
        if (!IsIntroComplete()) return;

        if (item.type == Constants.Clues.WantedPoster) {
            storyGraph.CompleteStateIfNeeded("Find wanted poster in bins");
        }
        else if (item.type == Constants.Clues.CateringNotes)
        {
            storyGraph.CompleteStateIfNeeded("Find catering notes");
        }
        else if (item.type == Constants.Clues.CookBook)
        {
            storyGraph.CompleteStateIfNeeded("Find Dumbledore's Cookbook");
        }
        else if (item.type == Constants.Clues.MercurysClothing)
        {
            storyGraph.CompleteStateIfNeeded("Find Mercury's clothing");
        }
        else if (item.type == Constants.Clues.Dagger)
        {
            storyGraph.CompleteStateIfNeeded("Find a bloody dagger");
        }
	}

	public override void OnNPCSpokenTo(NPC npc)
	{
        base.OnNPCSpokenTo(npc);

        Debug.Log("Mystery1Script#OnNPCSpokenTo: " + npc.ToString());

        if (npc.person == Constants.People.Receptionist) {
            Debug.Log("Spoken to Receptionist");
            if (storyGraph.IsStateActive("Intro"))
            {
                Debug.Log("Intro is active");
                storyGraph.CompleteState("Intro");
                storyGraph.CompleteState("Inspect Reception"); // TODO: unlock when you ask specifically about CCTV
            }
            else if (storyGraph.IsStateActive("Talk to Receptionist about Dinner"))
                storyGraph.CompleteState("Talk to Receptionist about Dinner");
        }
        else if (npc.person == Constants.People.JamesBond && storyGraph.IsStateActive("Question Bond"))
        {
            storyGraph.CompleteState("Question Bond");
        }
        else if (npc.person == Constants.People.Dumbledore && storyGraph.IsStateActive("Talk to Dumbledore about suspicions"))
        {
            storyGraph.CompleteState("Talk to Dumbledore about suspicions");
        }
        else if (npc.person == Constants.People.TheQueen) {
            if (storyGraph.IsStateActive("Talk to Queen"))
                storyGraph.CompleteState("Talk to Queen");
            else if (storyGraph.IsStateActive("Talk to Queen about Mercury's clothes"))
                storyGraph.CompleteState("Talk to Queen about Mercury's clothes");
        }
        else if (npc.person == Constants.People.FreddieMercury && storyGraph.IsStateActive("Talk to Mercury about clothing"))
        {
            storyGraph.CompleteState("Talk to Mercury about clothing");
        }
	}

	public override void OnPlayerChangeRoom(Constants.Rooms room)
	{
        base.OnPlayerChangeRoom(room);

        if (room == Constants.Rooms.InteractionIsland && storyGraph.IsStateActive("Inspect Island"))
        {
            storyGraph.CompleteState("Inspect Island");
        }
        else if (room == Constants.Rooms.LectureTheatre && storyGraph.IsStateActive("Inspect lecture theatre"))
        {
            storyGraph.CompleteState("Inspect lecture theatre");
        }
        else if (room == Constants.Rooms.Balcony && storyGraph.IsStateActive("Inspect balcony"))
        {
            storyGraph.CompleteState("Inspect balcony");
        }
        else if (room == Constants.Rooms.BinBay && storyGraph.IsStateActive("Inspect bin store"))
        {
            storyGraph.CompleteState("Inspect bin store");
        }
        else if (room == Constants.Rooms.Kitchen && storyGraph.IsStateActive("Explore kitchen"))
        {
            storyGraph.CompleteState("Explore kitchen");
        }
        else if (room == Constants.Rooms.CommonRoom && storyGraph.IsStateActive("Inspect common room"))
        {
            storyGraph.CompleteState("Inspect common room");
        }
        else if (room == Constants.Rooms.Room360 && storyGraph.IsStateActive("Inspect 360 room"))
        {
            storyGraph.CompleteState("Inspect 360 room");
        }
        else if (room == Constants.Rooms.Staircase1 && storyGraph.IsStateActive("Inspect stairwell"))
        {
            storyGraph.CompleteState("Inspect stairwell");
        }
	}

    private bool IsIntroComplete()
    {
        // Since this needs to be repeated a lot this function reduces verbosity
        return storyGraph.IsStateComplete("Intro");
    }
}
