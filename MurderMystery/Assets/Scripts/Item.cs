using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;

public class Item : MonoBehaviour
{
    public Constants.Items type;

    /*
    public Item(Constants.Items type)
    {
        this.type = type;
    }
    */

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
