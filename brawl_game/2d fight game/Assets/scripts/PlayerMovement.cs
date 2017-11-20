using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public int player;

	public float speed;
	public float jump;
	public float gravity;
	public int jumpTimes;
	int jumps;

	Vector3 moveDirection;
	CharacterController con;

	void Start () {
		con = GetComponent<CharacterController> (); //get playercontroller
	}

	void Update () {//update START
		if(player == 0){
			if (con.isGrounded) {
				moveDirection = new Vector3(0, 0, 0);
				moveDirection = transform.TransformDirection(moveDirection);
				moveDirection *= speed;

				jumps = 0;
			}
			if (Input.GetKeyDown (KeyCode.JoystickButton0) && con.isGrounded) {
				moveDirection.y = jump;
				jumps++;
			}else if (Input.GetKeyDown (KeyCode.JoystickButton0) && jumps < jumpTimes) {
				moveDirection.y = jump;
				jumps++;
			}
			moveDirection.x = Input.GetAxis ("Horizontal") * speed;
			moveDirection.y -= gravity * Time.deltaTime;
			con.Move(moveDirection * Time.deltaTime);
		}else if (player == 1){
			if (con.isGrounded) {
				moveDirection = new Vector3(0, 0, 0);
				moveDirection = transform.TransformDirection(moveDirection);
				moveDirection *= speed;

				jumps = 0;
			}
			if (Input.GetKeyDown (KeyCode.UpArrow) && con.isGrounded) {
				moveDirection.y = jump;
				jumps++;
			}else if (Input.GetKeyDown (KeyCode.UpArrow) && jumps < jumpTimes) {
				moveDirection.y = jump;
				jumps++;
			}
			moveDirection.x = Input.GetAxis ("Horizontal_") * speed;
			moveDirection.y -= gravity * Time.deltaTime;
			con.Move(moveDirection * Time.deltaTime);
		}
	}
}
