using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public float time;

    // This dictionary is the person and the description of that person
    Dictionary<Constants.People, string> GuestList = new Dictionary<Constants.People, string>
    {
        {Constants.People.DonaldTrump, "Twat"},
        {Constants.People.Dumbledore, "Cool Beard"},
        {Constants.People.FreddieMercury, "Singer"},
        {Constants.People.JamesBond, "007"},
        {Constants.People.MarilynMonroe, "Actress"},
        {Constants.People.TheQueen, "Old"}
    };

    public void Awake()
    {
        Time.timeScale = 0;
    }
}
