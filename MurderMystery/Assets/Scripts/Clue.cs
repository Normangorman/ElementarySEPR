using UnityEngine;
using System.Collections;

public class Clue : Item
{
    private InteractionPair interaction;

    void OnMouseDown()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

}
