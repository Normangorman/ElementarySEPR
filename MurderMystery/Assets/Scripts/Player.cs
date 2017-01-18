using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Xml.Serialization;

public class Player : Character
{
    private int aggressive;
    private int friendly;
    private int charisma;
    private int sarcasm;

    public int Aggressive
    {
        get {return aggressive;}
        set
        {
            aggressive = Aggressive;
            UIController.AggressiveBar.Value = Aggressive; 
        }
    }
    public int Friendly
    {
        get { return friendly; }
        set
        {
            friendly = Friendly;
            UIController.FriendlyBar.Value = Friendly;
        }
    }
    public int Charisma
    {
        get { return charisma; }
        set
        {
            charisma = Charisma;
            UIController.CharismaBar.Value = Charisma;
        }
    }
    public int Sarcasm
    {
        get { return sarcasm; }
        set
        {
            sarcasm = Sarcasm;
            UIController.SarcasmBar.Value = Sarcasm;
        }
    }

    public int layerMask;
    private UIController UIController;
    private InteractionPair interaction;

    private int interaction_points = 100;
    public List<Item> InventoryList;

    private int i = 5;
    public float speed = 0.05f;                          
    
    public int InteractionPoints
    {
        get
        {
            return interaction_points;
        }
        set
        {
            interaction_points = InteractionPoints;
            //UIController.SetInteractionPoint(InteractionPoints);
        }
    }

    public void Awake()
    {
        layerMask = gameObject.layer;
        UIController = GameObject.FindGameObjectWithTag("DoozyUI").GetComponent<UIController>().Instance;
        interaction = GameObject.FindGameObjectWithTag("DoozyUI").GetComponent<InteractionPair>();
    }

    public void ChoosePlayer(string person)
    {
       UIController.SetPerson((Constants.People)Enum.Parse(typeof(Constants.People), person));
    } 


    public void OnTriggerEnter2D(Collider2D col)
    {
        /*
         * This function adds the item that the player goes accross to their inventory 
         * with its description that has been got from the json file
         */
        if (col.gameObject.CompareTag("Item"))
        {
            AddToInventory(item.GetComponent<Item>());
            item.gameObject.SetActive(false);
            // Show a notification to show that it has been picked up
        }
    }

    public void AddToInventory(Item item)
    {
        InventoryList.Add(item); // Adds it to the player's inventory
        UIController.AddToInventoryList(item); // Add it to the inventory list in the UI
    }

    void FixedUpdate()
    {
        transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * speed ;
        /*
         * Update is called every 0.05 seconds so it is not based on frame rate. 
         
        // gets the horizontal input from the player's keyboard
        float moveHorizontal = Input.GetAxis("Horizontal"); 
        // gets the vertical input from the player's keyboard
        float moveVertical = Input.GetAxis("Vertical"); 
        // Moves the player by how much they have inputed but not in the z axis
        transform.position += new Vector3 (moveHorizontal, moveVertical, 0) * Time.deltaTime * speed;
        */
    }
    // Update is called once per frame
    /*
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "stairs")
            speed = speed / 1.5f;
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "stairs")
            speed = speed * 1.5f;
    }
    */
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NPC n = GetPlayerInRadius();
            if (n != null)
            {
                interaction.instance.InitiliaseInteraction(n);
            }
        }
    }

    NPC GetPlayerInRadius()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 5);
        foreach (Collider2D col in hitColliders)
        {
            if (col.GetComponent<NPC>() != null)
            {
                return col.gameObject.GetComponent<NPC>();
            }
        }
        return null;
    }
}

