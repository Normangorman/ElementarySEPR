using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveDown = KeyCode.S;

    public float speed = 10.0f;
    private Rigidbody2D RigidBody;

	// Use this for initialization
	void Start ()
    {
        RigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(moveUp))
        {
            RigidBody.velocity = Vector2.up * speed;
        }
        else if (Input.GetKey(moveDown))
        {
            RigidBody.velocity = Vector2.up * speed * -1;
        }
        else if (!Input.anyKey)
        {
            RigidBody.velocity = Vector2.zero;
        }
	}
}
