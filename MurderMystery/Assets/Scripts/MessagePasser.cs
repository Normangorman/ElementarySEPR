using UnityEngine;
using System.Collections;

public class MessagePasser : MonoBehaviour {

    public static void Broadcast(string msg)
    {
        if (msg == "OnPlayerTalkToNPC")
        {
            //storymanager.OnPlayerTalkToNPC();
            //userinterface.OnPlayerTalkToNPC();
        }
    }
}