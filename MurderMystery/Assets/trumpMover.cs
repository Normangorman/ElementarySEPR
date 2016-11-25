using UnityEngine;
using System.Collections;

public class trumpMover : MonoBehaviour {
    public float speed = 1f;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * speed;
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.tag == "stairs")
			speed = speed / 1.5f;
	}

	void OnTriggerExit2D(Collider2D coll){
		if (coll.tag == "stairs")
			speed = speed * 1.5f;
	}
}
