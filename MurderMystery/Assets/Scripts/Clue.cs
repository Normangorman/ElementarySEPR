using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;

//! Clue Class.
/*! Object which the player can pick up.*/
public class Clue : MonoBehaviour
{
    public Constants.Clues type; //!< Clue name.
    [HideInInspector] public Sprite img; //!< Clue sprite image.
    [HideInInspector] public string description; //!< Clue description.

    //! When the instance is being loaded, this gets the clue description and sprite from the story manager.
    public void Awake()
    {
        description = StoryManager.instance.GetClueDescription(type);
        img = GetComponent<SpriteRenderer>().sprite;
    }

    //! Gets the clue name.
    /*!
     * \return Clue name string.
     */ 
    public string GetName()
    {
        return type.ToString();
    }

    //! Gets the clue description.
    /*!
     * \return Clue description string.
     */
    public string GetDescription()
    {
        return description;
    }
}
