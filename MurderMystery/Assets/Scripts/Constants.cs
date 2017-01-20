using UnityEngine;
using System.Collections.Generic;
using System;

//! Constants Class.
/*! Class of enums containing names for objects and other info. */
public class Constants
{
    // Enum of all the characters in the game
	public enum People { DonaldTrump, Dumbledore, MarilynMonroe, FreddieMercury, JamesBond, TheQueen, Receptionist,
                         PabloEscobar, Poirot, Poirot2, Detective} //!< Enum of all characters

    // Enum of all the rooms in the game
    // TODO: this should only be the rooms we are actually using
    // More in at the minute because certain rooms are mentioned in storyline 1 which don't actually exist in game
    public enum Rooms { Kitchen, Lift, Staircase1, Staircase2, BinBay, BusinessRoom1, BusinessRoom2, ExhibitionRoom, InteractionIsland, LectureTheatre, Reception, Terrace, GrandHall,
                        Balcony, CommonRoom, Room360 } //!< Enum of all rooms

    // Enum of all the clues in the game
    public enum Clues { Dagger, Letter, Hat, BlondeHair, WantedPoster, CateringNotes, CookBook, MercurysClothing} //!< Enum of all clues

    // Enum of all the items in the game These can be used to enhance the player's abilities
    //public enum Items { Hat, WalkingStick, MagnifyingGlass, Dagger }

    public static Dictionary<Clues, string> ClueDescriptions = new Dictionary<Clues, string>
    {
        {Clues.Dagger, "A dagger with a smear or vivid red blood along the sharp side of the blade"},
        {Clues.Letter, "A letter addressed to ..... confessing their...."},
        {Clues.BlondeHair, "A long strand of blonde hair attached to ...."},
        {Clues.WantedPoster, "A wanted poster depiciting Pablo Escobar. A large reward is offered."},
        {Clues.CateringNotes, "Catering notes ..."},
        {Clues.CookBook, "An old cookbook that has ..."},
        {Clues.MercurysClothing, "Clothing that...."}
    };

    // Dictionary of the person and their intial values for their character traits
    public static Dictionary<People, List<int>> CharacterValues = new Dictionary<People, List<int>> //!< Dictionary associating characters and initial character traits.
    {
        // Person : Aggresive, friendly, charisma, sarcasm. ALWAYS IN THIS ORDER
        {People.DonaldTrump, new List<int> {0, 15, 15} },
        {People.Dumbledore, new List<int> {80, 5, 5} },
        {People.MarilynMonroe, new List<int> {30, 40, 20} },
        {People.FreddieMercury, new List<int> { 50, 20, 20} },
        {People.JamesBond, new List<int> {10, 20, 20} },
        {People.TheQueen, new List<int> {75, 15, 0} },
        {People.Poirot, new List<int> {10, 70, 20} },
        {People.Poirot2, new List<int> {60, 10, 30} }
    };

    public static People GetPersonByName(string name)
    {
        bool matched = false;
        Constants.People matchedPerson = Constants.People.Detective; // because the compiler whines if it has no value, give it some random value

        if (name == "Poirot") // Special case for detective
        {
            matched = true;
            matchedPerson = Constants.People.Detective;
        }
        else
        {
            // Find the NPC with this name
            foreach (Constants.People person in Enum.GetValues(typeof(Constants.People)))
            {
                if (name == person.ToString())
                {
                    matched = true;
                    matchedPerson = person;
                    break;
                }
            } 
        }

        if (matched)
        {
            Debug.Log("Matched " + name);
        }
        else
        {
            throw new PersonNotFound(name);
        }

        return matchedPerson;
    }
}

public class PersonNotFound : Exception
{
    public PersonNotFound(string personName) :
        base("Couldn't find the associated Constants.People value with: " + personName)
    { }
}
