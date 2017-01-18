using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : Character
{

    public int layerMask;
    private UIController UIController;
    private InteractionPair interaction;

    private int interaction_points = 100;
    public List<Constants.Items> InventoryList;

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
            UIController.SetInteractionPoint(InteractionPoints);
        }
    }

    public void Awake()
    {
        layerMask = gameObject.layer;
        //UIController = GameObject.FindGameObjectWithTag("DoozyUI").GetComponent<UIController>().Instance;
        //interaction = GameObject.FindGameObjectWithTag("DoozyUI").GetComponent<InteractionPair>();
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

            MessagePasser.BroadcastMessage("OnPlayerPressSpacebar");
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

