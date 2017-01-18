using UnityEngine;
using System.Collections.Generic;

//! GameManager class.
/*! Manages time and people dictionary in the game environment.*/
public class GameManager : MonoBehaviour
{

    public float Time; //!< Time constant which changes throughout gameplay.

    // This dictionary is the person and the description of that person
    Dictionary<Constants.People, string> GuestList = new Dictionary<Constants.People, string> //!< Dictionary associating characters and textual descriptions.
    {
        {Constants.People.DonaldTrump, "Twat"},
        {Constants.People.Dumbledore, "Cool Beard"},
        {Constants.People.FreddieMercury, "Singer"},
        {Constants.People.JamesBond, "007"},
        {Constants.People.MarilynMonroe, "Actress"},
        {Constants.People.TheQueen, "Old"}
    };


}
