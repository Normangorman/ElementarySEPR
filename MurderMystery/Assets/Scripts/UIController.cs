using UnityEngine;
using System;
using System.Security.Policy;
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

    public void SetNpcAbilities()
    {
        NpcAggressiveBar.Value = 50;
        NpcCharismaBar.Value = 30;
        NpcFriendlyBar.Value = 90;
        NpcSarcasmBar.Value = 10;
    }

    public void GetNpcIcon()
    {
        throw new NotImplementedException();
    }

    public void SetPlayerAbilities()
    {
        AggressiveBar.Value = 50;
        CharismaBar.Value = 30;
        FriendlyBar.Value = 90;
        SarcasmBar.Value = 10;
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
        var r = Resources.Load("Item") as GameObject;
        GameObject  g = Instantiate(r, transform, false) as GameObject;
        g.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(item.name);
        g.transform.GetChild(1).GetComponent<Text>().text = item.description;
    }

}
