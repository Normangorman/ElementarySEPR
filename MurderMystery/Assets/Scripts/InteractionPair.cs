using System.Runtime.Remoting.Messaging;
using UnityEngine;

//! Interaction Pair class.
/*! Manages the interactions between player and an object*/
public class InteractionPair : MonoBehaviour
{
    private UIController UIController; //! UIController allows manipulation of UI
    private enum Turn { player, character} //!< enum of possible character interaction turns
    private InteractionType interactionType; //!< the interaction type variable
    public InteractionPair instance; //!< instance of the InteractionPair class
    public Player player; //!< Player object
    public NPC npc; //!< NPC object
    public Clue item; //!< Clue object

    enum InteractionType { item, npc }
    
    public void Start() 
    {
        instance = this;
        UIController = GameObject.FindGameObjectWithTag("DoozyUI").GetComponent<UIController>().Instance;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    

    public void InitialiseInteraction(NPC a_NPC) 
    {
        npc = a_NPC;
        GetNextStory(Turn.character);
    }

    //! Sets the Interaction to an Item interaction
    /*! 
     * \param a_item the Item object that is being interacted with.
     */
    public void InitialiseInteraction(Clue a_item) 
    {
        item = a_item;
        interactionType = InteractionType.item;
        Debug.Log(item.GetName());
    }

    //! Gets the next string that will be spoken by Player/NPC.
    /*! 
     * \param a_item the Item object that is being interacted with.
     * \return string of speech
     */
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
            text = " Hello this is the president of the US speaking, who are you?";
        }
        UIController.SetDialogueBoxText(text);
        return text;
    }

    //! Produces a friendly reply from the player and deactivates response button.
    public void GetFriendlyReply()
    {
        UIController.SetDialogueBoxText("I'm Player, how are you finding the party?");
        GameObject button = GameObject.FindGameObjectWithTag("ResponseButton");
        button.SetActive(false);
    }
}
