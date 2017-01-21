using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

//! NPC Class.
/*! NPC Class for characters other than player that are non-playable. */
public class NPC : Character
{

    // Properties of NPC
    public int Friendly { get; set; }
    public int Charisma //!< Charisma value getter and setter.
    { get; set; }
    public int Sarcasm //!< Sarcasm value getter and setter.
    { get; set; }
    public Constants.People person;
    public Sprite Icon;
    public string description;
    
    public NPC(Constants.People person)
    {
        this.person = person;
        Icon = Resources.Load(person + ".png") as Sprite;
        Friendly = Constants.CharacterValues[person][0];
        Charisma = Constants.CharacterValues[person][1];
        Sarcasm = Constants.CharacterValues[person][2];
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
