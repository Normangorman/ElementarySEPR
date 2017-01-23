using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using ProgressBar;

//! UI Class.
/*! UI Controller, manipulates all UI objects. */
public class UIController : MonoBehaviour
{
    #region Variables
    [HideInInspector]
    public UIController Instance; //!< Instance of the UI Controller.
    public Transform GuestList; //!< Guest list object.
    public Transform InventoryList; //!< Inventory list object.

    public ProgressBarBehaviour InteractionPointBar; //!< Interaction Points bar graphic.
    public Image PlayerIcon; //!< Player Icon sprite.
    public Image NPCIcon; //!< NPC Icon sprite.
    public ProgressBarBehaviour CharismaBar; //!< Charisma Bar graphic.
    public ProgressBarBehaviour FriendlyBar; //!< Friendly Bar graphic.
    public ProgressBarBehaviour SarcasmBar; //!< Sarcasm Bar graphic.
    public ProgressBarBehaviour NpcCharismaBar; //!< NPC Charisma Bar graphic.
    public ProgressBarBehaviour NpcFriendlyBar; //!< NPC Friendly Bar graphic.
    public ProgressBarBehaviour NpcSarcasmBar; //!< NPC Sarcasm Bar graphic.
    public Text SynopsisWinText; //!< Text that displays when the win condition is met.
    public Text SynopsisLoseText; //!< Text that displays when the lose condition is met.
    public Text DialogueBox; //!< Text that displays a dialogue event occurs.

    public Camera MiniMapCamera; //!< Camera Object for the minimap.
    #endregion

    //! Initialises UI object before game loads.
    public void Awake()
    {
        Instance = this;
        InventoryList = GameObject.FindGameObjectWithTag("InventoryList").transform;
    }

    //! Sets interaction point bar values.
    /*!
     * \param i Integer value.
     */ 
    public void SetInteractionPoint(int i)
    {
        InteractionPointBar.Value = i;
    }

    //! Displays or hides minimap camera view port depending on a boolean value.
    /*!
     * \param b On or off boolean value.
     */
    public void SetCameraViewPort(bool b)
    {
        if (b)
            MiniMapCamera.rect = new Rect(0.7f, 0.7f, 0.3f, 0.3f);
        else
        {
            MiniMapCamera.rect = new Rect(0.7f, 0.7f, 0f, 0f);
        }
    }


    //! Sets NPC trait bars according to character dictionary.
    /*!
     * \param person Character for which the bars are to be set.
     */ 
    public void SetNPC(NPC npc)
    {
        NPCIcon.sprite = npc.Icon;
        NpcFriendlyBar.Value = npc.Friendly;
        NpcCharismaBar.Value = npc.Charisma;
        NpcSarcasmBar.Value = npc.Sarcasm;
    }

    //! Sets player details to display on UI.
    /*!
     * \param player Player object.
     */
    public void SetPerson(Player player)
    {
        PlayerIcon.sprite = player.Icon;
        FriendlyBar.Value = player.Friendly;
        CharismaBar.Value = player.Charisma;
        SarcasmBar.Value = player.Sarcasm;
        SetInteractionPoint(100);
    }

    //! Sets the player icon for the person selected.
    /*!
     * \param person Person object.
     */
    public void SetPlayerIcon(Constants.People person)
    {
        Sprite img = Resources.Load(person.ToString()) as Sprite;
        PlayerIcon.GetComponent<Image>().sprite = img;
    }


    //! Sets the player abilities to given values.
    /*!
     * \param i Friendly value.
     * \param j Charisma value.
     * \param k Sarcasm value.
     */
    public void SetPlayerAbilities(int i, int j, int k)
    {
        FriendlyBar.Value = i;
        CharismaBar.Value = j;
        SarcasmBar.Value = k;
    }

    //! Sets the dialogue box text to a given string.
    /*!
     * \param str String to set text to.
     */
    public void SetDialogueBoxText(string str)
    {
        DialogueBox.text = str;
    }

    //! Sets the button text for the appropriate dialogue response.
    /*!
     * \param dialogue Dictionary of the dialogue.
     */
    public void SetButtonText(Dictionary<string, string> dialogue)
    {
        Transform ResponseButtonTrans = GameObject.FindGameObjectWithTag("ResponseButtons").transform;
        foreach(Transform child in ResponseButtonTrans)
        {
            Destroy(child.gameObject);
        }

        GameObject button = Resources.Load("ResponseButton") as GameObject;
        int i = 0;
        dialogue.Remove("NO_TOPIC");
        foreach (string key in dialogue.Keys)
        {
            GameObject b = Instantiate(button,ResponseButtonTrans,false) as GameObject;
            b.transform.GetChild(0).GetComponent<Text>().text = key;
            b.GetComponent<ResponseButton>().ButtonText = key;
            b.GetComponent<ResponseButton>().Response = dialogue[key];
            b.name = "ResponseButton" + i;
            string s = key; // make a copy of key to avoid a bug with the listener function - where every listener uses the same key
            int j = i;
            b.GetComponent<Button>().onClick.AddListener(() => MakeDialogueText("ResponseButton" + j, s));
            i++;
        }
    }

    //! Makes dialogue text for the next response.
    /*!
     * \param name 
     * \param topic
     */
    public void MakeDialogueText(string name, string topic)
    {
        Debug.LogFormat("MakeDialogueText: {0}, {1}", name, topic);
        GameObject button = GameObject.Find(name);
        SetDialogueBoxText(button.GetComponent<ResponseButton>().Response);
        MessagePasser.OnNPCSpokenTo(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().currentNpc, topic);

    }

    //! Adds a clue to the inventory list.
    /*!
     * \param clue Clue that has been added.
     */
    public void AddToInventoryList(Clue clue)
    {
        GameObject r = Resources.Load("Item") as GameObject;
        GameObject  g = Instantiate(r, InventoryList, false) as GameObject;
        g.transform.GetChild(0).GetComponent<Image>().sprite = clue.img;
        g.transform.GetChild(1).GetComponent<Text>().text = clue.GetDescription();
    }

    //! Sets the text that displays on the win condition.
    /*!
     * \param str Text that is to be set.
     */
    public void SetSynopsisWinText(string str)
    {
        SynopsisWinText.text = str;
        SetCameraViewPort(false);
    }

    //! Sets the text that displays on the lose condition.
    /*!
     * \param str Text that is to be set.
     */
    public void SetSynopsisLoseText(string str)
    {
        SynopsisLoseText.text = str;
        SetCameraViewPort(false);
    }
}
