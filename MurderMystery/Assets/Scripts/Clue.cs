using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;

public class Clue : MonoBehaviour
{
    //! Clue Class.
    /*! Object which the player can pick up.*/
    public Constants.Clues type;
    public Sprite img;
    public string description;
    
    public void Awake()
    {
        description = Constants.ClueDescriptions[type];
        img = Resources.Load(type.ToString()) as Sprite;
    }

    public string GetName()
    {
        return type.ToString();
    }

    public string GetDescription()
    {
        return description;
    }
}
