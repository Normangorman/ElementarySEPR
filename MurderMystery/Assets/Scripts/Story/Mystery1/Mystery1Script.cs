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

	public override void OnItemFound(Item item)
	{
        base.OnItemFound(item);
        if (!IsIntroComplete()) return;

        if (item.type == Constants.Items.WantedPoster) {
            storyGraph.CompleteStateIfNeeded("Find wanted poster in bins");
        }
        else if (item.type == Constants.Items.CateringNotes)
        {
            storyGraph.CompleteStateIfNeeded("Find catering notes");
        }
        else if (item.type == Constants.Items.CookBook)
        {
            storyGraph.CompleteStateIfNeeded("Find Dumbledore's Cookbook");
        }
        else if (item.type == Constants.Items.MercurysClothing)
        {
            storyGraph.CompleteStateIfNeeded("Find Mercury's clothing");
        }
        else if (item.type == Constants.Items.Dagger)
        {
            storyGraph.CompleteStateIfNeeded("Find a bloody dagger");
        }
	}

	public override void OnNPCSpokenTo(NPC npc)
	{
        base.OnNPCSpokenTo(npc);

        if (npc.person == Constants.People.Receptionist) {
            if (storyGraph.IsStateActive("Intro"))
            {
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

	public override void OnPlayerEnterRoom(Constants.Rooms room)
	{
        base.OnPlayerEnterRoom(room);

        if (room == Constants.Rooms.IslandInteraction && storyGraph.IsStateActive("Inspect Island"))
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
        else if (room == Constants.Rooms.BinStore && storyGraph.IsStateActive("Inspect bin store"))
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
        else if (room == Constants.Rooms.Stairwell && storyGraph.IsStateActive("Inspect stairwell"))
        {
            storyGraph.CompleteState("Inspect stairwell");
        }
	}

	public override void OnPlayerLeaveRoom(Constants.Rooms room)
	{
        base.OnPlayerLeaveRoom(room);
	}

    private bool IsIntroComplete()
    {
        // Since this needs to be repeated a lot this function reduces verbosity
        return storyGraph.IsStateComplete("Intro");
    }
}
