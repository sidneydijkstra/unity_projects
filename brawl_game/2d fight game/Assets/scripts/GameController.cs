using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject[] players;
	public Transform firstSpawn;
	public Transform secondSpawn;

	public GameObject leftBar;
	public GameObject rightBar;
	public GameObject[] playerLogos;

	void Start () {


		GameObject plOne_ = players [PlayerPrefs.GetInt("P1Char")];
		GameObject plTwo_ = players [PlayerPrefs.GetInt("P2Char")];
		GameObject plOne = Instantiate (plOne_, firstSpawn.position, Quaternion.identity)as GameObject;
		GameObject plTwo = Instantiate (plTwo_, secondSpawn.position, Quaternion.identity)as GameObject;


		plOne.GetComponent<PlayerMovement> ().player = 0;
		plOne.GetComponent<PlayerGunController> ().player = 0;
		plOne.GetComponent<PlayerAnimator> ().player = 0;
		plOne.GetComponent<PlayerHealth> ().player = 0;
		plOne.GetComponent<PlayerGunController> ().target = plTwo;

		plTwo.GetComponent<PlayerMovement> ().player = 1;
		plTwo.GetComponent<PlayerGunController> ().player = 1;
		plTwo.GetComponent<PlayerAnimator> ().player = 1;
		plTwo.GetComponent<PlayerHealth> ().player = 1;
		plTwo.GetComponent<PlayerGunController> ().target = plOne;

		leftBar.GetComponent<GuiPlayerHealth> ().SpawnHealth(playerLogos[PlayerPrefs.GetInt("P1CharLogo")]);
		rightBar.GetComponent<GuiPlayerHealth> ().SpawnHealth(playerLogos[PlayerPrefs.GetInt("P2CharLogo")]);

		leftBar.GetComponent<GuiPlayerHealth> ().target = plOne;
		rightBar.GetComponent<GuiPlayerHealth> ().target = plTwo;

		plOne.GetComponent<PlayerHealth> ().playerhpbar = leftBar;
		plTwo.GetComponent<PlayerHealth> ().playerhpbar = rightBar;

	}
}
