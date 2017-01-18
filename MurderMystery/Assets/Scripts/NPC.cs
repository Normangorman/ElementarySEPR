using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class NPC : Character
{
    public Constants.People person;
    [HideInInspector] public InteractionPair interaction;

    /*
    public NPC(Constants.People person)
    {
        this.person = person;
    }
    */

    void Start()
    {
        gameObject.layer = 9;
        //interaction = GameObject.FindGameObjectWithTag("DoozyUI").GetComponent<InteractionPair>().instance;
    }

    void OnMouseDown()
    {
        Debug.Log(transform.name);
        interaction.InitiliaseInteraction(this);
    }

    public string GetName()
    {
        return person.ToString();
    }
}
