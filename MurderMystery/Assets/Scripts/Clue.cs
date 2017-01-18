using UnityEngine;
using System.Collections;

public class Clue
{
    private InteractionPair interaction;

    void OnMouseDown()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

}
