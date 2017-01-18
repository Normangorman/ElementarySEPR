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
    #endregion

    //! Initialises UI object before game loads.
    public void Awake()
    {
        Instance = this;
        //SetPlayerAbilities();
        //SetNpcAbilities();
        //SetInteractionPoint();
    }

    //! Retrieves GuestList.
    public void GetGuestList()
    {
        throw new NotImplementedException();
    }

     //! Sets InteractionPointBar value.
     /*!
      * \param i New interactionPointBar value
      */
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
        NpcAggressiveBar.Value = Dict[person][0];
        NpcFriendlyBar.Value = Dict[person][1];
        NpcCharismaBar.Value = Dict[person][2];
        NpcSarcasmBar.Value = Dict[person][3];
    }

    //! Retrieves NPC icon.
    public void GetNpcIcon() 
    {
        throw new NotImplementedException();
    }

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
        PlayerIcon.GetComponent<Image>().sprite = img;
    }

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
    {
        NpcAggressiveBar.Value = i;
        NpcFriendlyBar.Value = j;
        NpcCharismaBar.Value = k;
        NpcSarcasmBar.Value = l;
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

    //! Adds an Item to the Inventory List in the UI.
    /*!
     *  \param item Item that is to be added to the inventory.
     */
    public void AddToInventoryList(Item item)
    {
        Transform parent = GameObject.FindGameObjectWithTag("InventoryList").transform;
        GameObject r = Resources.Load("Item") as GameObject;
        GameObject  g = Instantiate(r, parent, false) as GameObject;
        g.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(item.itemName);
        g.transform.GetChild(1).GetComponent<Text>().text = item.description;
    }

}
