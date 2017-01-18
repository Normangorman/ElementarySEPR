using UnityEngine;
using System.Collections;

//! Clue Class.
/*! Object which the player can pick up.*/
public class Clue
{
    private InteractionPair interaction; //!< interaction pair.

    //! When Clue is clicked on, set player.
    void OnMouseDown() 
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

}
