using System.Diagnostics;
using UnityEngine.UI;

//! NPC Class.
/*! NPC Class for characters other than player that are non-playable. */
public class NPC : Character
{
    public string description;
    
    public void Awake()
    {
        SetFriendliness(Constants.CharacterValues[person][0]);
        SetCharisma(Constants.CharacterValues[person][1]);
        SetSarcasm(Constants.CharacterValues[person][2]);
    }

    //!<Executes on initialisation, sets object layer, interaction and character name.
    void Start() 
    {
        gameObject.layer = 9;
    }

    public string GetName()
    {
        return person.ToString();
    }
}
