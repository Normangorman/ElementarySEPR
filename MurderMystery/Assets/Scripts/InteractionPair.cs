using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class InteractionPair : MonoBehaviour
{
    private UIController UIController;

    private enum InteractionType { character, item}
    private enum Turn { player, character}

    private InteractionType interactionType;
    public InteractionPair instance;
    public Player player;
    public NPC npc;
    public Clue item;

    public void Start()
    {
        instance = this;
        UIController = GameObject.FindGameObjectWithTag("DoozyUI").GetComponent<UIController>().Instance;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void InitiliaseInteraction(NPC a_NPC)
    {
        npc = a_NPC;
        interactionType = InteractionType.character;
        Debug.Log(npc.person);
        GetNextStory(Turn.character);
    }

    public void InitiliaseInteraction(Clue a_item)
    {
        item = a_item;
        interactionType = InteractionType.item;
        Debug.Log(item.GetName());
    }

    private string GetNextStory(Turn turn)
    {
        // From the story manager get the next thing that the player and the npc can say
        string text;
        if (turn == Turn.player)
        {
            text = "Player: Hello, I am the player. Who are you?";
        }
        else
        {
            text = npc.person + ": Hello this is the president of the US speaking, who are you?";
        }
        UIController.SetDialogueBoxTet(text);
        return text;
    }

    public void GetFriendlyReply()
    {
        UIController.SetDialogueBoxTet("I'm Player, how are you finding the party?");
        GameObject button = GameObject.FindGameObjectWithTag("ResponseButton");
        button.SetActive(false);
    }

}
