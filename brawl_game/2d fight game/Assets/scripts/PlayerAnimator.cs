using UnityEngine;
using System.Collections;

public class PlayerAnimator : MonoBehaviour {
	public int player;
	[SerializeField]
	private Sprite[] anIdel;
	private bool jInput;
	private bool wLInput;
	private bool wRInput;
	private bool sLInput;
	private bool sRInput;
	private bool sBInput;

	SpriteRenderer sRender;
	int curent;

	void Start () {
		sRender = GetComponent<SpriteRenderer> ();
	}

	void Update () {
		InputManager ();
		spriteHandler ();
	}

	private void InputManager(){
		if (Input.GetAxis("Horizontal") <= -0.2F && player == 0 || Input.GetKey(KeyCode.LeftArrow) && player == 1) {
			wLInput = true;
			wRInput = false;
			jInput = false;
		}else
			if (Input.GetAxis("Horizontal") >= 0.2F && player == 0 || Input.GetKey(KeyCode.RightArrow) && player == 1) {
			wLInput = false;
			wRInput = true;
			jInput = false;
		} else {
			wLInput = false;
			wRInput = false;
			jInput = false;
		}
		if (!IsGround()) {
			wLInput = false;
			wRInput = false;
			jInput = true;
		}
		if (!IsGround() && Input.GetKey(KeyCode.S)) {
			wLInput = false;
			wRInput = false;
			jInput = false;
		}
	}

	private void spriteHandler(){
		if (wLInput)
			sRender.sprite = anIdel [1];
		else if (wRInput)
			sRender.sprite = anIdel [2];
		else if (jInput)
			sRender.sprite = anIdel [3];
		else if(!wLInput && !wRInput && !jInput){
			sRender.sprite = anIdel [0];
		}
	}

	bool IsGround(){
		if (Physics.Raycast (transform.position, -Vector3.up, 0.4f)) {
			return true;
		}else{
			return false;
		}
	}
}
