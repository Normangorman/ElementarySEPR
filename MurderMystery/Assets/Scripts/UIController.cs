using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using ProgressBar;

public class UIController : MonoBehaviour, IUserInterface
{
    #region Variables
    public UIController Instance;

    public ProgressBarBehaviour InteractionPointBar;
    public GameObject PlayerIcon;
    public GameObject NPCIcon;
    public ProgressBarBehaviour CharismaBar;
    public ProgressBarBehaviour FriendlyBar;
    public ProgressBarBehaviour AggressiveBar;
    public ProgressBarBehaviour SarcasmBar;
    public ProgressBarBehaviour NpcCharismaBar;
    public ProgressBarBehaviour NpcFriendlyBar;
    public ProgressBarBehaviour NpcAggressiveBar;
    public ProgressBarBehaviour NpcSarcasmBar;
    public Text DialogueBox;
    public GameObject GuestList;
    public GameObject InventoryList;
    #endregion

    public void Awake()
    {
        Instance = this;
        //SetPlayerAbilities();
        //SetNpcAbilities();
        //SetInteractionPoint();
    }

    public void GetGuestList()
    {
        throw new NotImplementedException();
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
        NpcAggressiveBar.Value = Dict[person][0];
        NpcFriendlyBar.Value = Dict[person][1];
        NpcCharismaBar.Value = Dict[person][2];
        NpcSarcasmBar.Value = Dict[person][3];
    }

    public void GetNpcIcon()
    {
        throw new NotImplementedException();
    }

    public void SetPerson(Constants.People p)
    {
        SetPlayerAbilities(p);
        SetPlayerIcon(p);
    }

    public void SetPlayerIcon(Constants.People p)
    {
        Sprite img = Resources.Load(p.ToString()) as Sprite;
        PlayerIcon.GetComponent<Image>().sprite = img;
    }

    public void SetPlayerAbilities(Constants.People person)
    {
        Dictionary<Constants.People, List<int>> Dict = Constants.CharacterValues;
        NpcAggressiveBar.Value = Dict[person][0];
        NpcFriendlyBar.Value = Dict[person][1];
        NpcCharismaBar.Value = Dict[person][2];
        NpcSarcasmBar.Value = Dict[person][3];
    }

    public void SetPlayerAbilities(int i, int j, int k, int l)
    {
        NpcAggressiveBar.Value = i;
        NpcFriendlyBar.Value = j;
        NpcCharismaBar.Value = k;
        NpcSarcasmBar.Value = l;
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

    public void AddToInventoryList(Item item)
    {
        /*
        Transform parent = GameObject.FindGameObjectWithTag("InventoryList").transform;
        GameObject r = Resources.Load("Item") as GameObject;
        GameObject  g = Instantiate(r, parent, false) as GameObject;
        g.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(item.GetSpriteName());
        g.transform.GetChild(1).GetComponent<Text>().text = item.GetDescription();
        */
    }

}
