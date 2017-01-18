using UnityEngine;
using System.Collections;

public class Char_Anim : MonoBehaviour {
	public Sprite left;
	public Sprite right;
	public Sprite back;
	public Sprite forward;

	private SpriteRenderer rend;

	void Start(){
		rend = this.GetComponent<SpriteRenderer> ();
		rend.sprite = forward;
	}
	// Update is called once per frame
	void Update () {
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
}
