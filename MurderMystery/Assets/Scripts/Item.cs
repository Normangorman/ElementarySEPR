using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;

public class Item : MonoBehaviour
{
    public Constants.Items item;
    public string name;
    public string description;

    private const string itemJsonFile = "Assets/Scripts/Resources/item_descriptions.json";

    public Item(string name, string description)
    {
        this.name = name;
        this.description = description;
    }

    public void Start()
    {
        getItem(item);
    }


    public static Item getItem(Constants.Items itemType)
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
