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
    public Constants.People person;
    public Sprite Icon;
    private int friendly;
    private int charisma;
    private int sarcasm;
    public List<Clue> InventoryList;
    private int interaction_points = 100;
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
    public int Friendly
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
    private int i = 5; //!<.
    public float speed = 0.05f; //!< Player movement speed modifier .                      

   
    private StoryManager StoryManager;
    public int layerMask; //!<.
    private UIController UIController; //!< UIController object.
   
    //! sets UIController object and interaction once all objects initialised.
    public void Awake() 
    {
        layerMask = gameObject.layer;
        InventoryList = new List<Clue>();
        UIController = GameObject.FindGameObjectWithTag("DoozyUI").GetComponent<UIController>().Instance;
        StoryManager = GameObject.FindGameObjectWithTag("StoryManager").GetComponent<StoryManager>();
    }

    public void ChoosePlayer(int person)
    {
        if (person == 0)
        {
            this.person = Constants.People.Poirot;
            Icon = GetSprite("_NPC/Images/Poirot");
            friendly = Constants.CharacterValues[Constants.People.Poirot][0];
            charisma = Constants.CharacterValues[Constants.People.Poirot][1];
            sarcasm = Constants.CharacterValues[Constants.People.Poirot][2];
            UIController.SetPerson(this);
        }
        else if (person == 1)
        {
            this.person = Constants.People.Poirot2;
            Icon = GetSprite("_NPC/Images/Poirot");
            friendly = Constants.CharacterValues[Constants.People.Poirot2][0];
            charisma = Constants.CharacterValues[Constants.People.Poirot2][1];
            sarcasm = Constants.CharacterValues[Constants.People.Poirot2][2];
            UIController.SetPerson(this);
        }
        Time.timeScale = 1;
        UIController.SetCameraViewPort();
    }

    public Sprite GetSprite(string path)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>(path);
        return sprites[1];
    }

    public void OnTriggerEnter2D(Collider2D col) 
    {
        /*
         * This function adds the item that the player goes accross to their inventory 
         * with its description that has been got from the json file
         */
        if (col.tag == "stairs")
            speed = speed / 1.5f;

        if (col.tag == "NPC")
            DoozyUI.UIManager.ShowNotification(Constants.NotificationPath, 3f, true, "Found NPC", "Press SPACE BAR to interact");

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
                DoozyUI.UIManager.ShowNotification("Example_1_Notification_5", 3f, true, "You have collected an Item", "You have collected an Item");
            }
        }
    } // Deals with picking up clues

    public void AddToInventory(Clue item)
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

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "stairs")
            speed = speed * 1.5f;
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
                Dictionary<string, string> dialogue = StoryManager.instance.GetCurrentDialogueForPerson(n.person);
                Debug.Log("Dialogue for: " + n.person.ToString());
                foreach (string topic in dialogue.Keys)
                {
                    Debug.LogFormat("{0}: {1}", topic, dialogue[topic]);

                }

                UIController.SetButtonText(dialogue);
                InitialiseInteraction();
            }
            else
            {
                Debug.Log("No NPC found");
            }
            MessagePasser.OnPlayerPressSpacebar();
        }
    }

    NPC GetNearbyNPC()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 5);
        foreach (Collider2D col in hitColliders)
        {
            //Debug.Log("Overlapping col: " + col.ToString());
            if (col.GetComponent<NPC>() != null)
            {
                return col.gameObject.GetComponent<NPC>();
            }
        }
        return null;
    }

    public void InitialiseInteraction()
    {
        /*
         * This function is for initialising the interaction between an NPC and the player
         * It should create a view point for the camera
         * It should set the physics game time to 0
         * It should show the text boxes and everything that needs to be shown on UI
         */
        DoozyUI.UIManager.ShowUiElement("NPCUI");
        DoozyUI.UIManager.ShowUiElement("ResponseButtons");
        DoozyUI.UIManager.ShowUiElement("DialogueBox");

        Time.timeScale = 0;
    }

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

        Time.timeScale = 1;
    }
}

