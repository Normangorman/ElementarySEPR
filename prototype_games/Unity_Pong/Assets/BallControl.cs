using UnityEngine;
using System.Collections;

public class BallControl : MonoBehaviour {

    private Rigidbody2D RigidBody;

    void GoBall(){
        float rand = Random.Range(0.0f, 2.0f);
        if (rand < 1.0f)
        {
            RigidBody.AddForce(new Vector2(20.0f, -15.0f));
        }
        else
        {
            RigidBody.AddForce(new Vector2(-20.0f, -15.0f));
        }
    }

	// Use this for initialization
	void Start () {
        RigidBody = GetComponent<Rigidbody2D>();
        Invoke("GoBall", 2.0f);
	}

    void ResetBall(){
        RigidBody.velocity = Vector2.zero;
        gameObject.transform.position = new Vector2(0, 0);
    }

    void RestartGame(){
        ResetBall();
        Invoke("GoBall", 1.0f);
    }

    void OnCollisionEnter2D (Collision2D coll) {
        if(coll.collider.CompareTag("Player"))
        {
            RigidBody.velocity = 
                new Vector2(
                    RigidBody.velocity.x , 
                    RigidBody.velocity.y / 2.0f + coll.collider.attachedRigidbody.velocity.y / 3.0f
                    );
        }
    }
}