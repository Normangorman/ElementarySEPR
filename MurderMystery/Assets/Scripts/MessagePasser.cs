using UnityEngine;
using System.Collections;

public class MessagePasser : MonoBehaviour {

    public static void OnNPCSpokenTo(NPC npc)
    {
        StoryManager.instance.GetStoryScript().OnNPCSpokenTo(npc);
    }

    public static void OnNPCSpokenTo(NPC npc, string topic)
    {
        Debug.LogFormat("MessagePasser#OnNPCSpokenTo: {0}, {1}", npc.GetName(), topic);
        StoryManager.instance.GetStoryScript().OnNPCSpokenTo(npc, topic);
    }

    public static void OnItemFound(Clue item)
    {
        StoryManager.instance.GetStoryScript().OnItemFound(item);
        DoozyUI.UIManager.ShowNotification(Constants.NotificationPath, 2f, true, StoryManager.instance.GetClueDescription(item.type));
    }

    public static void OnPlayerChangeRoom(Constants.Rooms room)
    {
        StoryManager.instance.GetStoryScript().OnPlayerChangeRoom(room);
    }

    public static void OnAccuseCharacter(NPC n)
    {
        Debug.LogFormat("Accusing character: {0}", n.person.ToString());
        StoryManager.instance.OnAccuseCharacter(n);
    }

    public static void OnFailedAccusation(NPC n)
    {
        // TODO: Add an alert that you failed
        GameManager.instance.OnFailedAccusation(n);
    }

    public static void OnPlayerPressSpacebar() { }
}