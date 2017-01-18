using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

//! NPC Class.
/*! NPC Class for characters other than player that are non-playable. */
public class NPC : Character
{
    public Constants.People characterName; //!< Name of NPC
    public InteractionPair interaction; //!< Interaction that occurs

    //!<Executes on initialisation, sets object layer, interaction and character name.
    void Start() 
    {
        gameObject.layer = 9;
        //interaction = GameObject.FindGameObjectWithTag("DoozyUI").GetComponent<InteractionPair>().instance;
        characterName = Constants.People.DonaldTrump;
    }

    //! Starts an interaction event when clicked on.
    void OnMouseDown() 
    {
        Debug.Log(transform.name);
        interaction.InitialiseInteraction(this);
    }

}
