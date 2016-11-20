using UnityEngine;
using System;
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
        GetPlayerAbilities();
        GetNpcAbilities();
    }

    public void GetGuestList()
    {
        throw new NotImplementedException();
    }

    public void SetInteractionPoint(int i)
    {
        InteractionPointBar.Value = i;
    }

    public void GetInventoryList()
    {
        throw new NotImplementedException();
    }

    public void GetNpcAbilities()
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

    public void GetPlayerAbilities()
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
}
