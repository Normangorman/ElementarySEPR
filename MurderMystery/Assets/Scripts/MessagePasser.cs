using UnityEngine;
using System.Collections;

public class MessagePasser : MonoBehaviour {

    public static void OnNPCSpokenTo(NPC npc)
    {
        StoryManager.instance.GetStoryScript().OnNPCSpokenTo(npc);
    }

    public static void OnItemFound(Item item)
    {
        StoryManager.instance.GetStoryScript().OnItemFound(item);
    }

    public static void OnPlayerChangeRoom(Constants.Rooms room)
    {
        StoryManager.instance.GetStoryScript().OnPlayerChangeRoom(room);
    }

    public static void OnPlayerPressSpacebar() { }
}