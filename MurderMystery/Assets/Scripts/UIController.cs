using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using ProgressBar;

//! UI Class.
/*! UI Controller, manipulates all UI objects. */
public class UIController : MonoBehaviour, IUserInterface
{
    #region Variables
<<<<<<< HEAD
    [HideInInspector]
    public UIController Instance;
    public Transform GuestList;
    public Transform InventoryList;

    public ProgressBarBehaviour InteractionPointBar;
    public GameObject PlayerIcon;
    public GameObject NPCIcon;
    public ProgressBarBehaviour CharismaBar;
    public ProgressBarBehaviour FriendlyBar;
    public ProgressBarBehaviour SarcasmBar;
    public ProgressBarBehaviour NpcCharismaBar;
    public ProgressBarBehaviour NpcFriendlyBar;
    public ProgressBarBehaviour NpcSarcasmBar;
    public Text Button1Text;
    public Text Button2Text;
    public Text Button3Text;
    public Text DialogueBox;
=======
    public UIController Instance; //!< Instance of UIController.

    public ProgressBarBehaviour InteractionPointBar; //!< Bar for Interaction Points.
    public GameObject PlayerIcon; //!< Player Icon on Minimap.
    public GameObject NPCIcon; //!< NPC Icon on Minimap.
    public ProgressBarBehaviour CharismaBar; //!< Bar for Charisma Points.
    public ProgressBarBehaviour FriendlyBar; //!< Bar for Friendly Points.
    public ProgressBarBehaviour AggressiveBar; //!< Bar for Aggressive Points.
    public ProgressBarBehaviour SarcasmBar; //!< Bar for Sarcasm Points.
    public ProgressBarBehaviour NpcCharismaBar; //!< Bar for NPC Charisma Points.
    public ProgressBarBehaviour NpcFriendlyBar; //!< Bar for NPC Friendly Points.
    public ProgressBarBehaviour NpcAggressiveBar; //!< Bar for NPC Aggressive Points.
    public ProgressBarBehaviour NpcSarcasmBar; //!< Bar for NPC Sarcasm Points.
    public Text DialogueBox; //!< Text Box for dialogue.
    public GameObject GuestList; //!< GuestList Game Object.
    public GameObject InventoryList; //!< InventoryList Game Object.
>>>>>>> origin/Joe/Documentation
    #endregion

    //! Initialises UI object before game loads.
    public void Awake()
    {
        Instance = this;
        InventoryList = GameObject.FindGameObjectWithTag("InventoryList").transform;
        GuestList = GameObject.FindGameObjectWithTag("GuestList").transform;
    }

    //! Retrieves GuestList.
    public void GetGuestList()
    {
        throw new NotImplementedException();
    }

<<<<<<< HEAD
    public void SetResponseTexts(string Text1, string Text2, string Text3)
    {
        /*
         * Sets the texts for the different response buttons. Call this function with the names that
         * you want to appear on the buttons when you are responding to an NPC
         */
        Button1Text.text = Text1;
        Button2Text.text = Text2;
        Button3Text.text = Text3;
    }

=======
     //! Sets InteractionPointBar value.
     /*!
      * \param i New interactionPointBar value
      */
>>>>>>> origin/Joe/Documentation
    public void SetInteractionPoint(int i = 0)
    {
        InteractionPointBar.Value = i;
    }

    //! Retrieves InventoryList.
    public void GetInventoryList()
    {
        throw new NotImplementedException();
    }

    //! Sets NPC trait bars according to character dictionary.
    /*!
     * \param person Character for which the bars are to be set.
     */ 
    public void SetNpcAbilities(Constants.People person)
    {
        Dictionary<Constants.People, List<int>> Dict = Constants.CharacterValues;
        NpcFriendlyBar.Value = Dict[person][0];
        NpcCharismaBar.Value = Dict[person][1];
        NpcSarcasmBar.Value = Dict[person][2];
    }

    //! Retrieves NPC icon.
    public void GetNpcIcon() 
    {
        throw new NotImplementedException();
    }

<<<<<<< HEAD
    public void SetPerson(Constants.People p, int i, int j, int k)
    {
        Sprite img = Resources.Load("Poirot") as Sprite;
=======
    //! Sets player.
    /*!
     * \param p Person for which the bars and icon are to be set.
     */
    public void SetPerson(Constants.People p)
    {
        SetPlayerAbilities(p);
        SetPlayerIcon(p);
    }

    //! Sets player icon.
    /*!
     * \param person Person for which the icon is to be set.
     */
    public void SetPlayerIcon(Constants.People person)
    {
        Sprite img = Resources.Load(person.ToString()) as Sprite;
>>>>>>> origin/Joe/Documentation
        PlayerIcon.GetComponent<Image>().sprite = img;
        SetPlayerAbilities(i, j, k);
    }

<<<<<<< HEAD
    public void SetPlayerAbilities(int i, int j, int k)
=======
    //! Sets player traits bars using character dictionary.
    /*!
     * \param person Person for which the trait bars is to be set.
     */
    public void SetPlayerAbilities(Constants.People person)
    {
        Dictionary<Constants.People, List<int>> Dict = Constants.CharacterValues;
        NpcAggressiveBar.Value = Dict[person][0];
        NpcFriendlyBar.Value = Dict[person][1];
        NpcCharismaBar.Value = Dict[person][2];
        NpcSarcasmBar.Value = Dict[person][3];
    }

    //! Sets Player trait bars to new values.
    /*!
     * \param i Aggressive bar value.
     * \param j Friendly bar value.
     * \param k Charisma bar value.
     * \param l Sarcasm bar value.
     */
    public void SetPlayerAbilities(int i, int j, int k, int l)
>>>>>>> origin/Joe/Documentation
    {
        NpcFriendlyBar.Value = i;
        NpcCharismaBar.Value = j;
        NpcSarcasmBar.Value = k;
    }

    //! Gets player sprite.
    public void GetPlayerIcon()
    {
        throw new NotImplementedException();
    }

    //! Gets the time remaining in the game.
    public void GetTime()
    {
        throw new NotImplementedException();
    }

    //! Sets dialogue text box content.
    /*!
     *  \param str Text that dialogue is to be set to.
     */ 
    public void SetDialogueBoxText(string str)
    {
        DialogueBox.text = str;
    }

    //! Gets Player traits.
    public void GetPlayerAbilities()
    {
        throw new NotImplementedException();
    }

    //! Gets NPC traits.
    public void GetNpcAbilities()
    {
        throw new NotImplementedException();
    }

<<<<<<< HEAD
    public void AddToInventoryList(Clue item)
=======
    //! Adds an Item to the Inventory List in the UI.
    /*!
     *  \param item Item that is to be added to the inventory.
     */
    public void AddToInventoryList(Item item)
>>>>>>> origin/Joe/Documentation
    {
        GameObject r = Resources.Load("Item") as GameObject;
        GameObject  g = Instantiate(r, InventoryList, false) as GameObject;
        g.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(item.GetSpriteName());
        g.transform.GetChild(1).GetComponent<Text>().text = item.GetDescription();
    }

}
