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
    public UIController Instance;
    public Transform GuestList;
    public Transform InventoryList;

    public ProgressBarBehaviour InteractionPointBar;
    public Image PlayerIcon;
    public Image NPCIcon;
    public ProgressBarBehaviour CharismaBar;
    public ProgressBarBehaviour FriendlyBar;
    public ProgressBarBehaviour SarcasmBar;
    public ProgressBarBehaviour NpcCharismaBar;
    public ProgressBarBehaviour NpcFriendlyBar;
    public ProgressBarBehaviour NpcSarcasmBar;
    public GameObject Button0;
    public GameObject Button1;
    public GameObject Button2;
    public Text DialogueBox;

    public Camera MiniMapCamera;
    #endregion

    //! Initialises UI object before game loads.
    public void Awake()
    {
        Instance = this;
        InventoryList = GameObject.FindGameObjectWithTag("InventoryList").transform;
        GuestList = GameObject.FindGameObjectWithTag("GuestList").transform;
    }


    public void SetInteractionPoint(int i)
    {
        InteractionPointBar.Value = i;
    }

    public void SetCameraViewPort()
    {
        MiniMapCamera.rect = new Rect(0.7f, 0.7f, 0.3f, 0.3f);
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


    public void SetPerson(Player player)
    {
        PlayerIcon.sprite = player.Icon;
        FriendlyBar.Value = player.Friendly;
        CharismaBar.Value = player.Charisma;
        SarcasmBar.Value = player.Sarcasm;
        SetInteractionPoint(100);
    }

    public void SetPlayerIcon(Constants.People person)
    {
        Sprite img = Resources.Load(person.ToString()) as Sprite;
        PlayerIcon.GetComponent<Image>().sprite = img;
    }


    public void SetPlayerAbilities(int i, int j, int k)
    {
        FriendlyBar.Value = i;
        CharismaBar.Value = j;
        SarcasmBar.Value = k;
    }


    public void SetDialogueBoxText(string str)
    {
        DialogueBox.text = str;
    }

    public void SetButtonText(Dictionary<string, string> dialogue)
    {
        Transform ResponseButtonTrans = GameObject.FindGameObjectWithTag("ResponseButtons").transform;
        GameObject button = Resources.Load("ResponseButton") as GameObject;
        int i = 0;
        dialogue.Remove(dialogue.Keys.First());
        foreach (string key in dialogue.Keys)
        {
            GameObject b = Instantiate(button,ResponseButtonTrans,false) as GameObject;
            b.transform.GetChild(0).GetComponent<Text>().text = key;
            b.GetComponent<ResponseButton>().ButtonText = key;
            b.GetComponent<ResponseButton>().Response = dialogue[key];
            b.name = "ResponseButton" + i;
            b.GetComponent<Button>().onClick.AddListener(() => MakeDialogueText("ResponseButton" + i));
        }
    }

    public void MakeDialogueText(string name)
    {
        GameObject button = GameObject.Find(name);
        SetDialogueBoxText(button.GetComponent<ResponseButton>().Response);
    }

    public void AddToInventoryList(Clue clue)
    {
        GameObject r = Resources.Load("Item") as GameObject;
        GameObject  g = Instantiate(r, InventoryList, false) as GameObject;
        g.transform.GetChild(0).GetComponent<Image>().sprite = clue.img;
        g.transform.GetChild(1).GetComponent<Text>().text = clue.GetDescription();
    }

}
