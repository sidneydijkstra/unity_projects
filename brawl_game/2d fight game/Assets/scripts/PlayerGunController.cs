using UnityEngine;
using System.Collections;

public class PlayerGunController : MonoBehaviour {

	public int player;

	public float bulletSpeed;

	public GameObject bullet;

	public Transform outLeft;
	public Transform outRight;

	public Transform outExtraA;
	public Transform outExtraB;

	public GameObject target;

	public float normalTimer;
	public float SpecialTimer;

	float atime;
	float btime;


	void Start () {
		atime = Time.time + normalTimer;
		btime = Time.time + SpecialTimer;
	}

	void Update () {
		if (player == 0) {
			if (Input.GetKeyDown (KeyCode.Joystick1Button2) && Time.time >= atime) {
				var b = (GameObject)Instantiate (bullet, outLeft.position, Quaternion.identity);
				b.GetComponent<BulletController> ().target = target;
				b.GetComponent<Rigidbody2D> ().velocity = -outLeft.right * bulletSpeed;
				atime = Time.time + normalTimer;
			} else if (Input.GetKeyDown (KeyCode.Joystick1Button1) && Time.time >= atime) {
				var b = (GameObject)Instantiate (bullet, outRight.position, Quaternion.identity);
				b.GetComponent<BulletController> ().target = target;
				b.GetComponent<Rigidbody2D> ().velocity = outRight.right * bulletSpeed;
				atime = Time.time + normalTimer;
			} else if (Input.GetKeyDown (KeyCode.Joystick1Button3) && Time.time >= btime) {
				var b = (GameObject)Instantiate (bullet, outExtraA.position, Quaternion.identity);
				b.GetComponent<Rigidbody2D> ().velocity = outExtraA.up * bulletSpeed;
				var b_ = (GameObject)Instantiate (bullet, outExtraB.position, Quaternion.identity);
				b_.GetComponent<BulletController> ().target = target;
				b_.GetComponent<Rigidbody2D> ().velocity = outExtraB.up * bulletSpeed;
				btime = Time.time + SpecialTimer;
			}
		} else if (player == 1) {
			if (Input.GetKeyDown (KeyCode.Keypad1) && Time.time >= atime) {
				var b = (GameObject)Instantiate (bullet, outLeft.position, Quaternion.identity);
				b.GetComponent<BulletController> ().target = target;
				b.GetComponent<Rigidbody2D> ().velocity = -outLeft.right * bulletSpeed;
				atime = Time.time + normalTimer;
			} else if (Input.GetKeyDown (KeyCode.Keypad2) && Time.time >= atime) {
				var b = (GameObject)Instantiate (bullet, outRight.position, Quaternion.identity);
				b.GetComponent<BulletController> ().target = target;
				b.GetComponent<Rigidbody2D> ().velocity = outRight.right * bulletSpeed;
				atime = Time.time + normalTimer;
			} else if (Input.GetKeyDown (KeyCode.DownArrow) && Time.time >= btime) {
				var b = (GameObject)Instantiate (bullet, outExtraA.position, Quaternion.identity);
				b.GetComponent<BulletController> ().target = target;
				b.GetComponent<Rigidbody2D> ().velocity = outExtraA.up * bulletSpeed;
				var b_ = (GameObject)Instantiate (bullet, outExtraB.position, Quaternion.identity);
				b_.GetComponent<BulletController> ().target = target;
				b_.GetComponent<Rigidbody2D> ().velocity = outExtraB.up * bulletSpeed;
				btime = Time.time + SpecialTimer;
			}
		}
	}


	//A:0 D:1 J:2 Z:3 X:4 S:5
}
