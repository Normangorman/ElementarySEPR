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
    private InteractionPair interaction; //!< interaction pair.
    
    public Clue(Constants.Clues type)
    {
        this.type = type;
    }

    public string GetName()
    {
        return type.ToString();
    }

    public string GetSpriteName()
    {
        // The name of the sprite associated with this item
        return GetName();
    }

    public string GetDescription()
    {
        return Constants.ItemDescriptions[type];
    }
}
