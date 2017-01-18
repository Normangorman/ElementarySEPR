using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class NPC : Character
{
    public Constants.People name;
    public InteractionPair interaction;

    void Start()
    {
        gameObject.layer = 9;
        //interaction = GameObject.FindGameObjectWithTag("DoozyUI").GetComponent<InteractionPair>().instance;
        name = Constants.People.DonaldTrump;
    }

    void OnMouseDown()
    {
        Debug.Log(transform.name);
        interaction.InitiliaseInteraction(this);
    }

}
