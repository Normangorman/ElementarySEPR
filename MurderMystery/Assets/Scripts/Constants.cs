using System.Collections.Generic;

//! Constants Class.
/*! Class of enums containing names for objects and other info. */
public class Constants
{
    // Enum of all the characters in the game
	public enum People { DonaldTrump, Dumbledore, MarilynMonroe, FreddieMercury, JamesBond, TheQueen, Receptionist,
                         PabloEscobar, Poirot, Poirot2} //!< Enum of all characters

    // Enum of all the rooms in the game
    public enum Rooms { IslandInteraction, Cafe, Office1, Office2, Office3, Balcony, LectureTheatre, BinStore, 
                        Kitchen, CommonRoom, Room360, Stairwell } //!< Enum of all rooms

    // Enum of all the clues in the game
    public enum Clues { Dagger, Letter, Hat, BlondeHair, WantedPoster, CateringNotes, CookBook, MercurysClothing} //!< Enum of all clues

    // Enum of all the items in the game These can be used to enhance the player's abilities
    //public enum Items { Hat, WalkingStick, MagnifyingGlass, Dagger }

    public static Dictionary<Clues, string> ItemDescriptions = new Dictionary<Clues, string>
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
        {People.Poirot2, new List<int> {60, 10, 30} },
    };

}
