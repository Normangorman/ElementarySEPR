using UnityEngine;
using System.Collections;

//! Character Animation class.
/*! Handles the animation in relation to the movement of the player and NPCs.
 * Sets the correct sprite for the direction being travelled. */
public class Char_Anim : MonoBehaviour {
	public Sprite left; //!< Character sprite when facing left.
	public Sprite right; //!< Character sprite when facing right.
    public Sprite back; //!< Character sprite when facing back.
    public Sprite forward; //!< Character sprite when facing forward.

    public const float NPC_TURN_CHANCE = 0.015f; //!< Probability that an NPC's sprite will change the direction it is facing.

    private SpriteRenderer rend; //!< The sprite renderer object for rendering the sprites.

    //! On initialisation, the sprite is set to a default position of forwards.
    void Start(){
		rend = this.GetComponent<SpriteRenderer> ();
		rend.sprite = forward;
	}

	// Update is called once per frame
    //! Every frame, tests which key the player is pressing, and renders the correct sprite.
	void Update () {
        if (gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
                rend.sprite = right;
            } else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
                rend.sprite = left;
            } else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
                rend.sprite = back;
            } else if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                rend.sprite = forward;
            }
        }
        else
        {
            // Randomly turn NPCs
            if (Random.Range(0.0f, 1.0f) < NPC_TURN_CHANCE)
            {
                float r = Random.Range(0.0f, 1.0f);
                if (r < 0.25)
                    rend.sprite = right;
                else if (r < 0.5)
                    rend.sprite = left;
                else if (r < 0.75)
                    rend.sprite = back;
                else
                    rend.sprite = forward;
            }
        }
	}
}
