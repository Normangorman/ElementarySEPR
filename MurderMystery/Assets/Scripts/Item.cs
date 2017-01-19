using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;

//! Item class.
/*! Item class holding all instances of interactable items in the scene. */

    // Should not be used for Assessment 2, use Clue instead, this is for items to enhance the player's ability
public class Item : MonoBehaviour
{
    public Constants.Clues clue; //!< item enum from Constants class.
    public string itemName; //!< string of Item name.
    public string description; //!< string of description of the item.

    private const string itemJsonFile = "Assets/Scripts/Resources/item_descriptions.json"; //!< json file reference for use by story graph.

    //! Constructor Class for Item
    /*! 
     * \param name item name.
     * \param description description of item.
     */
    public Item(string name, string description)
    {
        this.itemName = name;
        this.description = description;
    }

    //! Executes getItem upon initialisation.
    public void Start() 
    {
        getItem(clue);
    }

    //! Retrieves item from javascript file for use as an Item object.
    /*! 
     * \param itemType Item to get.
     * \return Item object.
     */
    public static Item getItem(Constants.Clues itemType) 
    {
        Debug.Log("Loading story graph from: " + itemJsonFile);

        JObject itemsJSON = JObject.Parse(File.ReadAllText(itemJsonFile));
        JArray listItemJSON = (JArray)itemsJSON.GetValue("items");
        foreach (var item in listItemJSON.Children<JObject>())
        {
            string title = item.Value<string>("name");
            string description = item.Value<string>("description");

            if (title == itemType.ToString())
            {
                return new Item(title, description);
            }
        }
        return null;
    }
}
