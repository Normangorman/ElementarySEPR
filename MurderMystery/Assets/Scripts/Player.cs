using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Xml.Serialization;

//! Player Class.
/*! Class which holds the Character that the player controls. */
public class Player : Character
{
    //Player Properties

    public List<Clue> InventoryList; //!< Player's inventory list.
    private int interaction_points = 100; //!< Number of interaction points, set to a default of 100.
    public int InteractionPoints //!< Interaction Points getter and setter.
    {
        get
        {
            return interaction_points;
        }
        set
        {
            if (value >= 100)
            {
                interaction_points = 100;
            }
            else
            {
                interaction_points = value;
            }
            UIController.SetInteractionPoint(value);
        }
    }

    //! Sets player Friendliness value.
    /*
     * \param i Integer value.
     */ 
    public new void SetFriendliness(int i)
    {
        base.SetFriendliness(i);
        UIController.FriendlyBar.Value = GetFriendliness();
    }

    //! Sets player Charisma value.
    /*
     * \param i Integer value.
     */
    public new void SetCharisma(int i)
    {
        base.SetCharisma(i);
        UIController.CharismaBar.Value = GetCharisma();
    }

    //! Sets player Sarcasm value.
    /*
     * \param i Integer value.
     */
    public new void SetSarcasm(int i)
    {
        base.SetSarcasm(i);
        UIController.SarcasmBar.Value = GetSarcasm();
    }

    private int i = 5; //!<.
    public float speed = 0.1f; //!< Player movement speed modifier .                      

    public Constants.InteractionType CurrentInteractionType; //!< Type of the current player interaction.
    private Dictionary<string, string> CurrentDialogue; //!< Current dictionary of dialogue.

    public NPC currentNpc; //!< Current NPC that is being interacted with.

    private StoryManager StoryManager; //!< Story manager object.
    public int layerMask; //!< Layer of the player in the game.
    private UIController UIController; //!< UIController object.
   
    //! sets UIController object and interaction once all objects initialised.
    public void Awake() 
    {
        layerMask = gameObject.layer;
        InventoryList = new List<Clue>();
        UIController = GameObject.FindGameObjectWithTag("DoozyUI").GetComponent<UIController>().Instance;
        StoryManager = GameObject.FindGameObjectWithTag("StoryManager").GetComponent<StoryManager>();
    }

    //! Called when the player is selecting a character to play as.
    /*
     * \param person Integer referring to character selected.
     */ 
    public void ChoosePlayer(int person)
    {
        if (person == 0)
        {
            this.person = Constants.People.Poirot;
            Icon = GetSprite("_NPC/Images/Poirot");
            Friendly = Constants.CharacterValues[Constants.People.Poirot][0];
            Charisma = Constants.CharacterValues[Constants.People.Poirot][1];
            Sarcasm = Constants.CharacterValues[Constants.People.Poirot][2];
            UIController.SetPerson(this);
        }
        else if (person == 1)
        {
            this.person = Constants.People.Poirot2;
            Icon = GetSprite("_NPC/Images/Poirot");
            Friendly = Constants.CharacterValues[Constants.People.Poirot2][0];
            Charisma = Constants.CharacterValues[Constants.People.Poirot2][1];
            Sarcasm = Constants.CharacterValues[Constants.People.Poirot2][2];
            UIController.SetPerson(this);
        }
        Time.timeScale = 1;
        UIController.SetCameraViewPort(true);
    }

    //!< Gets a sprite image given a file path.
    /*
     * \param path The path of the sprite.
     */ 
    public Sprite GetSprite(string path)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>(path);
        return sprites[1];
    }

    //! When the player enters a trigger with the specific tags, apply the appropriate modifier to a player or UI attribute.
    /*
     * \param col Collider that is entered.
     */ 
    public void OnTriggerEnter2D(Collider2D col) 
    {
        /*
         * This function adds the item that the player goes accross to their inventory 
         * with its description that has been got from the json file
         */
        Debug.Log("Player#OnTriggerEnter2D");

        if (col.tag == "stairs")
            speed = speed / 1.5f;

        if (col.tag == "NPC")
            DoozyUI.UIManager.ShowNotification(Constants.NotificationPath, 1f, true, "Found NPC\nPress SPACE BAR to interact");

        if (col.gameObject.CompareTag("Clue"))
        {
            Debug.Log("Found item: " + col.gameObject.name);
            Clue itemComponent = col.gameObject.GetComponent<Clue>();

            if (itemComponent == null)
            {
                Debug.LogError("Object has Clue tag but no Clue component!");
            }
            else
            {
                AddToInventory(itemComponent);
                col.gameObject.SetActive(false);
                MessagePasser.OnItemFound(itemComponent);
                InteractionPoints += 5;
            }
        }
    } 

    //! Adds a clue item to the player's inventory in the list and the UI.
    /*!
     * \param item Clue that is to be added.
     */ 
    public void AddToInventory(Clue item)
    {
        InventoryList.Add(item); // Adds it to the player's inventory
        UIController.AddToInventoryList(item); // Add it to the inventory list in the UI
    }

    //! Every 0.05 seconds gets movement information and moves player accordingly.
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

    //! When the player leaves a collider, change the appropriate values.
    /*
     * \param col Collider that is left.
     */
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "stairs")
            speed = speed * 1.5f;

        if (col.tag == "NPC")
            CancelInteraction();
    }

    //! Every frame, checks for space press and passes a message if an NPC is in range.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NPC n = GetNearbyNPC();
            if (n != null)
            {
                StoryManager.GetCurrentDialogueForPerson(n.person);
                MessagePasser.OnNPCSpokenTo(n);

                // Show the dialogue UI
                CurrentDialogue = StoryManager.instance.GetCurrentDialogueForPerson(n.person);
                UIController.SetDialogueBoxText(CurrentDialogue["NO_TOPIC"]);
                Debug.Log("Dialogue for: " + n.person);
                foreach (string topic in CurrentDialogue.Keys)
                {
                    Debug.LogFormat("{0}: {1}", topic, CurrentDialogue[topic]);

                }
                InitialiseInteraction();
                UIController.SetNPC(n);
            }
            else
            {
                Debug.Log("No NPC found");
            }
            MessagePasser.OnPlayerPressSpacebar();
        }
    }

    //! Gets a nearby NPC
    /*!
     * \return NPC object, if nearby.
     */ 
    public NPC GetNearbyNPC()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 5);
        foreach (Collider2D col in hitColliders)
        {
            if (col.GetComponent<NPC>() != null)
            {
                currentNpc = col.gameObject.GetComponent<NPC>();
                return col.gameObject.GetComponent<NPC>();
            }
        }
        return null;
    }

    //! Initialises interaction between player and an NPC.
    public void InitialiseInteraction()
    {
        /*
         * This function is for initialising the interaction between an NPC and the player
         * It should create a view point for the camera
         * It should set the physics game time to 0
         * It should show the text boxes and everything that needs to be shown on UI
         */
        DoozyUI.UIManager.ShowUiElement("NPCUI");
        DoozyUI.UIManager.ShowUiElement("InteractionTypeButtons");
        DoozyUI.UIManager.ShowUiElement("DialogueBox");
        
    }

    //! Resets objects and view when interaction is cancelled.
    public void CancelInteraction()
    {
        /*
         * This function is for initialising the interaction between an NPC and the player
         * It return camera to where it normally is situated
         * It should set the physics game time to 1
         * Hides all the UI elements
         */
        DoozyUI.UIManager.HideUiElement("NPCUI");
        DoozyUI.UIManager.HideUiElement("ResponseButtons");
        DoozyUI.UIManager.HideUiElement("DialogueBox");
        DoozyUI.UIManager.HideUiElement("InteractionTypeButtons");
        DoozyUI.UIManager.HideUiElement("InteractionStyleButtons");
    }

    //! Tests if the NPC should accept the response, depending on player points.
    public void TestForAcceptResponse()
    {
        switch (CurrentInteractionType)
        {
            case Constants.InteractionType.Friendly:
                int i = Math.Abs(GetFriendliness() - currentNpc.GetFriendliness());
                if (i <= 20)
                {
                    UIController.SetButtonText(CurrentDialogue);
                    InteractionPoints -= i;
                }
                else
                {
                    UIController.SetDialogueBoxText(Constants.GuestDefaultSayings[currentNpc.person]);
                    InteractionPoints -= 30;
                }
                break;
            case Constants.InteractionType.Charismatic:
                int j = Math.Abs(GetCharisma() - currentNpc.GetCharisma());
                if (j <= 20)
                {
                    UIController.SetButtonText(CurrentDialogue);
                    InteractionPoints -= j;
                }
                else
                {
                    UIController.SetDialogueBoxText(Constants.GuestDefaultSayings[currentNpc.person]);
                    InteractionPoints -= 30;
                }
                break;
            case Constants.InteractionType.Sarcastic:
                int k = Math.Abs(GetSarcasm() - currentNpc.GetSarcasm());
                if (k <= 20)
                {
                    UIController.SetButtonText(CurrentDialogue);
                    InteractionPoints -= k;
                }
                else
                {
                    UIController.SetDialogueBoxText(Constants.GuestDefaultSayings[currentNpc.person]);
                    InteractionPoints -= 30;
                }
                break;
        }
        DoozyUI.UIManager.HideUiElement("ResponseButtons");
        DoozyUI.UIManager.HideUiElement("InteractionTypeButtons");
        DoozyUI.UIManager.HideUiElement("InteractionStyleButtons");
    }

    //! Called when an NPC is accused.
    public void AccuseCharacter()
    {
        MessagePasser.OnAccuseCharacter(currentNpc);
    }

    //! Sets the current interaction type and calls a response test.
    /*
     * \param interaction Type of interaction.
     */ 
    public void SetInteractionType(string interaction)
    {
        CurrentInteractionType = (Constants.InteractionType)Enum.Parse(typeof(Constants.InteractionType), interaction);
        TestForAcceptResponse();
    }

    //! Stops the player from moving.
    public void SetSpeedNull()
    {
        speed = 0;
        UIController.SetCameraViewPort(false);
    }

    //! Resumes the player movement.
    public void SetSpeedGo()
    {
        speed = 0.1f;
        UIController.SetCameraViewPort(true);
    }

}

