using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        transform.localScale = new Vector3(1, 1, 1);
        transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), 0, Mathf.RoundToInt(transform.position.z));
        transform.name = transform.position.ToString();
    }
	
}
