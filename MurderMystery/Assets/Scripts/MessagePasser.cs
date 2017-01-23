using UnityEngine;
using System.Collections;

//! Debug Message Passer class.
/*! Prints a message to the debug log, and reduces the dependencies between classes. */
public class MessagePasser : MonoBehaviour {

    //! Called when an NPC is spoken to.
    /*!
     * \param npc The NPC that is being spoken to.
     */ 
    public static void OnNPCSpokenTo(NPC npc)
    {
        StoryManager.instance.GetStoryScript().OnNPCSpokenTo(npc);
    }

    //! Called when an NPC is spoken to and a specific topic is given.
    /*!
     * \param npc The NPC that is being spoken to.
     * \param topic The topic of the conversation.
     */
    public static void OnNPCSpokenTo(NPC npc, string topic)
    {
        Debug.LogFormat("MessagePasser#OnNPCSpokenTo: {0}, {1}", npc.GetName(), topic);
        StoryManager.instance.GetStoryScript().OnNPCSpokenTo(npc, topic);
    }

    //! Called when an item is found.
    /*!
     * \param item The item that has been found.
     */
    public static void OnItemFound(Clue item)
    {
        StoryManager.instance.GetStoryScript().OnItemFound(item);
        DoozyUI.UIManager.ShowNotification(Constants.NotificationPath, 1.5f, true, StoryManager.instance.GetClueDescription(item.type));
    }

    //! Called when the player changes rooms.
    /*!
     * \param room.
     */
    public static void OnPlayerChangeRoom(Constants.Rooms room)
    {
        StoryManager.instance.GetStoryScript().OnPlayerChangeRoom(room);
    }

    //! Called when the player accuses an NPC.
    /*!
     * \param n NPC accused.
     */
    public static void OnAccuseCharacter(NPC n)
    {
        Debug.LogFormat("Accusing character: {0}", n.person.ToString());
        StoryManager.instance.OnAccuseCharacter(n);
    }

    //! Called when a character accusation fails.
    /*!
     * \param n NPC accused.
     */
    public static void OnFailedAccusation(NPC n)
    {
        // TODO: Add an alert that you failed
        GameManager.instance.OnFailedAccusation(n);
    }

    //! Called when the player presses the spacebar.
    public static void OnPlayerPressSpacebar() { }
}