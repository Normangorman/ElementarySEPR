using UnityEngine;
using System.Collections;

public class MessagePasser : MonoBehaviour {

    public static void OnNPCSpokenTo(NPC npc)
    {
        StoryManager.instance.GetStoryScript().OnNPCSpokenTo(npc);
    }

    public static void OnItemFound(Clue item)
    {
        StoryManager.instance.GetStoryScript().OnItemFound(item);
        DoozyUI.UIManager.ShowNotification("Example_1_Notification_5", 2f, true, StoryManager.instance.GetClueDescription());
    }

    public static void OnPlayerChangeRoom(Constants.Rooms room)
    {
        StoryManager.instance.GetStoryScript().OnPlayerChangeRoom(room);
        DoozyUI.UIManager.ShowNotification("Example_1_Notification_5", 2f, true, StoryManager.instance.GetRoomDescription());
    }

    public static void OnAccuseCharacter(NPC n)
    {
        StoryManager.instance.OnAccuseCharacter(n);
    }

    public static void OnPlayerPressSpacebar() { }
}