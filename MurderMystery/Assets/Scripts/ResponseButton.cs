using UnityEngine;
using System.Collections;

//! Response button interface class.
/*! Called when the NPC has a specific topic to talk about. */
public class ResponseButton : MonoBehaviour
{
    public string ButtonText; //!< Text displayed on the button.
    public string Response; //!< Response to the button press.
}
