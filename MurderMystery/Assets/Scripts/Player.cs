using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Xml.Serialization;

//! Player Class.
/*! Class which holds the Character that the player controls. */
public class Player : Character
{
    private int aggressive; //!< Player's aggressive value.
    private int friendly; //!< Player's friendly value.
    private int charisma; //!< Player's charisma value.
    private int sarcasm; //!< Player's sarcasm value.

    public int Aggressive //!< Aggressive value getter and setter.
    {
        get {return aggressive;}
        set
        {
            aggressive = Aggressive;
            UIController.AggressiveBar.Value = Aggressive; 
        }
    }
    public int Friendly //!< Friendly value getter and setter.
    {
        get { return friendly; }
        set
        {
            friendly = Friendly;
            UIController.FriendlyBar.Value = Friendly;
        }
    }
    public int Charisma //!< Charisma value getter and setter.
    {
        get { return charisma; }
        set
        {
            charisma = Charisma;
            UIController.CharismaBar.Value = Charisma;
        }
    }
    public int Sarcasm //!< Sarcasm value getter and setter.
    {
        get { return sarcasm; }
        set
        {
            sarcasm = Sarcasm;
            UIController.SarcasmBar.Value = Sarcasm;
        }
    }

    public int layerMask; //!<.
    private UIController UIController; //!< UIController object.
    private InteractionPair interaction; //!< Interaction object.

    private int interaction_points = 100; //!< Number of interactions remaining.
    public List<Item> InventoryList; //!< Inventory of Items represented as a List.

    private int i = 5; //!<.
    public float speed = 0.05f; //!< Player movement speed modifier .                      

    public int InteractionPoints //!< Interaction Points getter and setter.
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

    //! sets UIController object and interaction once all objects initialised.
    public void Awake() 
    {
        layerMask = gameObject.layer;
        //UIController = GameObject.FindGameObjectWithTag("DoozyUI").GetComponent<UIController>().Instance;
        //interaction = GameObject.FindGameObjectWithTag("DoozyUI").GetComponent<InteractionPair>();
    }

    //! Sets character to player.
    /*!
     * \param person Chosen character.
     */ 
    public void ChoosePlayer(string person) 
    {
       UIController.SetPerson((Constants.People)Enum.Parse(typeof(Constants.People), person));
    }

    //! Adds item that player triggers to inventory list.
    /*!
     * \param col Collider entered.
     */ 
    public void OnTriggerEnter2D(Collider2D col) 
    {
        /*
         * This function adds the item that the player goes accross to their inventory 
         * with its description that has been got from the json file
         */
        if (col.gameObject.CompareTag("Item"))
        {
            AddToInventory(col.gameObject.GetComponent<Item>());
            col.gameObject.SetActive(false);
            // Show a notification to show that it has been picked up
        }
    }

    //! Adds an item to the inventory list.
    /*!
     * \param item Item to be added.
     */ 
    public void AddToInventory(Item item)
    {
        InventoryList.Add(item); // Adds it to the player's inventory
        UIController.AddToInventoryList(item); // Add it to the inventory list in the UI
    }

    //! Every 0.05 seconds gets movement infor and moves player accordingly.
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

    //! Every frame, checks for space press and passes a message if an NPC is in range.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NPC n = GetPlayerInRadius();
            if (n != null)
            {
                interaction.instance.InitialiseInteraction(n);
            }

            MessagePasser.Broadcast("OnPlayerPressSpacebar");
        }
    }

    //! Checks for NPCs in range of the player.
    /*!
     * \return Character in radius.
     */
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

