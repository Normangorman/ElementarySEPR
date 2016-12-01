using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Xml.Serialization;

public class Player : Character
{
    private int aggressive;
    private int friendly;
    private int charisma;
    private int sarcasm;

    public int Aggressive
    {
        get {return aggressive;}
        set
        {
            aggressive = Aggressive;
            UIController.AggressiveBar.Value = Aggressive; 
        }
    }
    public int Friendly
    {
        get { return friendly; }
        set
        {
            friendly = Friendly;
            UIController.FriendlyBar.Value = Friendly;
        }
    }
    public int Charisma
    {
        get { return charisma; }
        set
        {
            charisma = Charisma;
            UIController.CharismaBar.Value = Charisma;
        }
    }
    public int Sarcasm
    {
        get { return sarcasm; }
        set
        {
            sarcasm = Sarcasm;
            UIController.SarcasmBar.Value = Sarcasm;
        }
    }

    public int layerMask;
    private UIController UIController;
    private InteractionPair interaction;

    private int interaction_points = 100;
    public List<Item> InventoryList;

    private int i = 5;
    public float speed = 2f;                          
    
    public int InteractionPoints
    {
        get
        {
            return interaction_points;
        }
        set
        {
            interaction_points = InteractionPoints;
            //UIController.SetInteractionPoint(InteractionPoints);
        }
    }

    public void Awake()
    {
        layerMask = gameObject.layer;
        UIController = GameObject.FindGameObjectWithTag("DoozyUI").GetComponent<UIController>().Instance;
        interaction = GameObject.FindGameObjectWithTag("DoozyUI").GetComponent<InteractionPair>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ItemPickup"))
        {
            AddToInventory(other.GetComponent<Item>());
            other.gameObject.SetActive(false);
        }
    }

    public void AddToInventory(Item item)
    {
        InventoryList.Add(item);
        UIController.AddToInventoryList(item);
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        transform.position += new Vector3 (moveHorizontal, moveVertical, 0) * Time.deltaTime * speed;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NPC n = GetPlayerInRadius();
            if (n != null)
            {
                interaction.instance.InitiliaseInteraction(n);
            }
        }
    }

    NPC GetPlayerInRadius()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 5);
        foreach (Collider2D col in hitColliders)
        {
            if (col.GetComponent<NPC>() != null)
            {
                return col.gameObject.GetComponent<NPC>();
            }
        }
        return null;
    }
}

