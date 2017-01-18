using System.Collections.Generic;

//! Constants Class.
/*! Class of enums containing names for objects and other info. */
public class Constants
{
    // Enum of all the characters in the game
	public enum People { DonaldTrump, Dumbledore, MarilynMonroe, FreddieMercury, JamesBond, TheQueen} //!< Enum of all characters.

    // Enum of all the rooms in the game
    public enum Rooms { IslandInteraction, Cafe, Office1, Office2, Office3, Balcony } //!< Enum of all rooms.

    // Enum of all the clues in the game
    public enum Clues { Dagger, Letter, BlondeHair } //!< Enum of all clues.

    // Enum of all the items in the game
    public enum Items { Hat, WalkingStick, MagnifyingGlass } //!< Enum of all items.

    // Dictionary of the person and their intial values for their character traits
    public static Dictionary<People, List<int>> CharacterValues = new Dictionary<People, List<int>> //!< Dictionary associating characters and initial character traits.
    {
        // Person : Aggresive, friendly, charisma, sarcasm. ALWAYS IN THIS ORDER
        {People.DonaldTrump, new List<int> {70, 0, 15, 15} },
        {People.Dumbledore, new List<int> {10, 80, 5, 5} },
        {People.MarilynMonroe, new List<int> {10, 30, 40, 20} },
        {People.FreddieMercury, new List<int> {10, 50, 20, 20} },
        {People.JamesBond, new List<int> {50, 10, 20, 20} },
        {People.TheQueen, new List<int> {0, 75, 15, 0} }
    };


}
