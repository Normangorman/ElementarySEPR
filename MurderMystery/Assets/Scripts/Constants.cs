using UnityEngine;
using System.Collections.Generic;
using System;

//! Constants Class.
/*! Class of enums containing names for objects and other info. */
public class Constants
{
    // Enum of all the characters in the game
	public enum People { DonaldTrump, Dumbledore, MarilynMonroe, FreddieMercury, JamesBond, TheQueen, Receptionist,
                         PabloEscobar, Poirot, Poirot2, CompSciNerd} //!< Enum of all characters

    public enum InteractionType { Friendly, Charismatic, Sarcastic} //!< Enum of all interaction types.

    // Enum of all the rooms in the game
    public enum Rooms { Kitchen, Lift, Staircase1, Staircase2, BinBay, BusinessRoom1, BusinessRoom2, ExhibitionRoom, InteractionIsland, LectureTheatre, Reception, Terrace, GrandHall,
                        Balcony, CommonRoom, Room360 } //!< Enum of all rooms

    // Enum of all the clues in the game
    public enum Clues { WantedPoster, BrokenTape, Money, DumbledoresCookbook, DoctorsNote, FreddysClothing, MealOrders, Breadcrumbs,
                        Knife, Pistol, Epipen } //!< Enum of all clues

    public static string NotificationPath = "1_Notification_1"; //!< File path for notification prefab.

    // Enum of all the items in the game These can be used to enhance the player's abilities
    //public enum Items { Hat, WalkingStick, MagnifyingGlass, Dagger }

    // Dictionary of the person and their intial values for their character traits
    public static Dictionary<People, List<int>> CharacterValues = new Dictionary<People, List<int>> //!< Dictionary associating characters and initial character traits.
    {
        // Person : friendly, charisma, sarcasm. ALWAYS IN THIS ORDER
        {People.DonaldTrump, new List<int> {0, 15, 15} },
        {People.Dumbledore, new List<int> {80, 5, 5} },
        {People.MarilynMonroe, new List<int> {30, 40, 20} },
        {People.FreddieMercury, new List<int> { 50, 20, 20} },
        {People.JamesBond, new List<int> {10, 20, 20} },
        {People.TheQueen, new List<int> {75, 15, 0} },
        {People.Poirot, new List<int> {10, 70, 20} },
        {People.Poirot2, new List<int> {60, 10, 30} },
        { People.PabloEscobar, new List<int> { 0, 30, 70} },
        {People.Receptionist,new List<int> {90, 10, 0 } }
    };

    public static Dictionary<Constants.People, string> GuestDefaultSayings = new Dictionary<Constants.People, string> //!< Dictionary associating characters and textual descriptions.
    {
        {People.DonaldTrump, "I have nothing to say! Get away from me.... NOW!"},
        {People.Dumbledore, "Unfortunately I have nothing else to give you, I am terribly sorry. I hope you find your man" },
        {People.MarilynMonroe, "I'm sorry darling, I can't help you" },
        {People.FreddieMercury, "Get off my back!" },
        {People.JamesBond, "Don't have anything for you, I need to get back to business" },
        {People.TheQueen, "Dear Sir, I regret to inform you that I have no other information" },
        {People.Poirot, "" },
        {People.Poirot2, ""},
        {People.PabloEscobar, "Go away or you will lose your tongue!!!" },
        {People.Receptionist, "Sorry! Don't know anything about that!" }
    };

    //! Given a name of a person, returns a person from the enum of people.
    /*!
     * \param name Given name.
     * \return A person from list of game characters.
     */ 
    public static People GetPersonByName(string name)
    {
        bool matched = false;
        Constants.People matchedPerson = Constants.People.Poirot; // because the compiler whines if it has no value, give it some random value

        if (name == "Poirot") // Special case for detective
        {
            matched = true;
            matchedPerson = Constants.People.Poirot;
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

//! Person Not Found error exception.
/*! If a reference is made to a person that does not exist, this error is given. */
public class PersonNotFound : Exception
{
    public PersonNotFound(string personName) :
        base("Couldn't find the associated Constants.People value with: " + personName)
    { }
}
