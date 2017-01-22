using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;

public class Clue : MonoBehaviour
{
    //! Clue Class.
    /*! Object which the player can pick up.*/
    public Constants.Clues type;
    [HideInInspector] public Sprite img;
    [HideInInspector] public string description;
    
    public void Awake()
    {
        description = StoryManager.instance.GetClueDescription(type);
        img = GetComponent<SpriteRenderer>().sprite;
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
