using System.Diagnostics;
using UnityEngine.UI;

//! NPC Class.
/*! NPC Class for characters other than player that are non-playable. */
public class NPC : Character
{
    public string description; //!< Textual description of the NPC.
    
    //! When the instance loads, initialise the character values to the defaults for that character.
    public void Awake()
    {
        SetFriendliness(Constants.CharacterValues[person][0]);
        SetCharisma(Constants.CharacterValues[person][1]);
        SetSarcasm(Constants.CharacterValues[person][2]);
    }

    //! Executes on initialisation, sets object layer for the NPC.
    void Start() 
    {
        gameObject.layer = 9;
    }

    //! Gets the name of the NPC.
    /*!
     * \return NPC name.
     */ 
    public string GetName()
    {
        return person.ToString();
    }
}
