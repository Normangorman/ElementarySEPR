using UnityEngine;
using System.Collections;

public class Char_Anim : MonoBehaviour {
	public Sprite left;
	public Sprite right;
	public Sprite back;
	public Sprite forward;

    public const float NPC_TURN_CHANCE = 0.015f;

	private SpriteRenderer rend;

	void Start(){
		rend = this.GetComponent<SpriteRenderer> ();
		rend.sprite = forward;
	}

	// Update is called once per frame
	void Update () {
        if (gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.RightArrow)) {
                rend.sprite = right;
            } else if (Input.GetKey(KeyCode.LeftArrow)) {
                rend.sprite = left;
            } else if (Input.GetKey(KeyCode.UpArrow)) {
                rend.sprite = back;
            } else if(Input.GetKey(KeyCode.DownArrow)){
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
