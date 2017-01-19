using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using ProgressBar;

public class UIController : MonoBehaviour, IUserInterface
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

    public void Awake()
    {
        Instance = this;
        InventoryList = GameObject.FindGameObjectWithTag("InventoryList").transform;
        GuestList = GameObject.FindGameObjectWithTag("GuestList").transform;
    }

    public void GetGuestList()
    {
        throw new NotImplementedException();
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

    public void GetInventoryList()
    {
        throw new NotImplementedException();
    }

    public void SetNpcAbilities(Constants.People person)
    {
        Dictionary<Constants.People, List<int>> Dict = Constants.CharacterValues;
        NpcFriendlyBar.Value = Dict[person][0];
        NpcCharismaBar.Value = Dict[person][1];
        NpcSarcasmBar.Value = Dict[person][2];
    }

    public void GetNpcIcon()
    {
        throw new NotImplementedException();
    }

    public void SetPerson(Constants.People p, int i, int j, int k)
    {
        Sprite img = Resources.Load("Poirot") as Sprite;
        PlayerIcon.GetComponent<Image>().sprite = img;
        SetPlayerAbilities(i, j, k);
    }

    public void SetPlayerAbilities(int i, int j, int k)
    {
        NpcFriendlyBar.Value = i;
        NpcCharismaBar.Value = j;
        NpcSarcasmBar.Value = k;
    }

    public void GetPlayerIcon()
    {
        throw new NotImplementedException();
    }

    public void GetTime()
    {
        throw new NotImplementedException();
    }

    public void SetDialogueBoxTet(string str)
    {
        DialogueBox.text = str;
    }

    public void GetPlayerAbilities()
    {
        throw new NotImplementedException();
    }

    public void GetNpcAbilities()
    {
        throw new NotImplementedException();
    }

    public void AddToInventoryList(Clue item)
    {
        GameObject r = Resources.Load("Item") as GameObject;
        GameObject  g = Instantiate(r, InventoryList, false) as GameObject;
        g.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(item.GetSpriteName());
        g.transform.GetChild(1).GetComponent<Text>().text = item.GetDescription();
    }

}
