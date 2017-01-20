using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

//! NPC Class.
/*! NPC Class for characters other than player that are non-playable. */
public class NPC : Character
{
    public Constants.People person;
    [HideInInspector] public InteractionPair interaction; //!< Interaction that occurs

    /*
    public NPC(Constants.People person)
    {
        this.person = person;
    }
    */
    public Constants.People characterName; //!< Name of NPC

    //!<Executes on initialisation, sets object layer, interaction and character name.
    void Start() 
    {
        gameObject.layer = 9;
        //interaction = GameObject.FindGameObjectWithTag("DoozyUI").GetComponent<InteractionPair>().instance;
    }

    //! Starts an interaction event when clicked on.
    void OnMouseDown() 
    {
        Debug.Log(transform.name);
        interaction.InitialiseInteraction(this);
    }

    public string GetName()
    {
        return person.ToString();
    }
}
