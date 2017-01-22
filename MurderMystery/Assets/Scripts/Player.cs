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
            interaction_points = value;
            UIController.SetInteractionPoint(value);
        }
    }

    public new void SetFriendliness(int i)
    {
        base.SetFriendliness(i);
        UIController.FriendlyBar.Value = GetFriendliness();
    }
    public new void SetCharisma(int i)
    {
        base.SetCharisma(i);
        UIController.CharismaBar.Value = GetCharisma();
    }
    public new void SetSarcasm(int i)
    {
        base.SetSarcasm(i);
        UIController.SarcasmBar.Value = GetSarcasm();
    }

    private int i = 5; //!<.
    public float speed = 0.05f; //!< Player movement speed modifier .                      

    public Constants.InteractionType CurrentInteractionType;
    private Dictionary<string, string> CurrentDialogue;


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
            DoozyUI.UIManager.ShowNotification(Constants.NotificationPath, 3f, true, "Found NPC\nPress SPACE BAR to interact");

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

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "stairs")
            speed = speed * 1.5f;

        if (col.tag == "NPC")
            DoozyUI.UIManager.ShowNotification(Constants.NotificationPath, 2f, true, "Leaving NPC\nYou have left the interaction zone");
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
    public NPC GetNearbyNPC()
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
        DoozyUI.UIManager.ShowUiElement("InteractionTypeButtons");
        DoozyUI.UIManager.ShowUiElement("DialogueBox");
        
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
        DoozyUI.UIManager.HideUiElement("InteractionTypeButtons");
        DoozyUI.UIManager.HideUiElement("InteractionStyleButtons");
    }

    public void TestForAcceptResponse()
    {
        switch (CurrentInteractionType)
        {
            case Constants.InteractionType.Friendly:
                int i = Math.Abs(GetFriendliness() - GetNearbyNPC().GetFriendliness());
                if (i <= 20)
                {
                    UIController.SetButtonText(CurrentDialogue);
                    InteractionPoints = i;
                }
                else
                {
                    UIController.SetDialogueBoxText("NEEDS TO BE SET TO DEFAULT SAYING FOR EACH CHARACTER");
                    InteractionPoints -= 30;
                }
                break;
            case Constants.InteractionType.Charismatic:
                i = Math.Abs(GetCharisma() - GetNearbyNPC().GetCharisma());
                if (i <= 20)
                {
                    UIController.SetButtonText(CurrentDialogue);
                    InteractionPoints -= i;
                }
                else
                {
                    UIController.SetDialogueBoxText("NEEDS TO BE SET TO DEFAULT SAYING FOR EACH CHARACTER");
                    InteractionPoints -= 30;
                }
                break;
            case Constants.InteractionType.Sarcastic:
                i = Math.Abs(GetSarcasm() - GetNearbyNPC().GetSarcasm());
                if (i <= 20)
                {
                    UIController.SetButtonText(CurrentDialogue);
                    InteractionPoints -= i;
                }
                else
                {
                    UIController.SetDialogueBoxText("NEEDS TO BE SET TO DEFAULT SAYING FOR EACH CHARACTER");
                    InteractionPoints -= 30;
                }
                break;
        }
        DoozyUI.UIManager.HideUiElement("ResponseButtons");
        DoozyUI.UIManager.HideUiElement("InteractionTypeButtons");
        DoozyUI.UIManager.HideUiElement("InteractionStyleButtons");
    }

    public void AccuseCharacter()
    {
        MessagePasser.OnAccuseCharacter(GetNearbyNPC());
    }

    public void SetInteractionType(string interaction)
    {
        CurrentInteractionType = (Constants.InteractionType)Enum.Parse(typeof(Constants.InteractionType), interaction);
        TestForAcceptResponse();
    }

}

