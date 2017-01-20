using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using ProgressBar;

//! UI Class.
/*! UI Controller, manipulates all UI objects. */
public class UIController : MonoBehaviour
{
    #region Variables
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
    #endregion

    //! Initialises UI object before game loads.
    public void Awake()
    {
        Instance = this;
        InventoryList = GameObject.FindGameObjectWithTag("InventoryList").transform;
        GuestList = GameObject.FindGameObjectWithTag("GuestList").transform;
    }

    
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

    public void SetInteractionPoint(int i = 0)
    {
        InteractionPointBar.Value = i;
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


    public void SetPerson(Constants.People p)
    {
        PlayerIcon.GetComponent<Image>().sprite = Resources.Load("Poirot") as Sprite;
        Dictionary<Constants.People, List<int>> Dict = Constants.CharacterValues;
        NpcFriendlyBar.Value = Dict[p][0];
        NpcCharismaBar.Value = Dict[p][1];
        NpcSarcasmBar.Value = Dict[p][2];
    }

    public void SetPlayerIcon(Constants.People person)
    {
        Sprite img = Resources.Load(person.ToString()) as Sprite;
        PlayerIcon.GetComponent<Image>().sprite = img;
    }


    public void SetPlayerAbilities(int i, int j, int k)
    {
        NpcFriendlyBar.Value = i;
        NpcCharismaBar.Value = j;
        NpcSarcasmBar.Value = k;
    }


    public void SetDialogueBoxText(string str)
    {
        DialogueBox.text = str;
    }
    
    public void AddToInventoryList(Clue clue)
    {
        GameObject r = Resources.Load("Item") as GameObject;
        GameObject  g = Instantiate(r, InventoryList, false) as GameObject;
        g.transform.GetChild(0).GetComponent<Image>().sprite = clue.img;
        g.transform.GetChild(1).GetComponent<Text>().text = clue.GetDescription();
    }

}
