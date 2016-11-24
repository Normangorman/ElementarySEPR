using UnityEngine;
using System.Collections;

public class trumpMover : MonoBehaviour {
    public float speed = 1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed, 0);
	}
}
