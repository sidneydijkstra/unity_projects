using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public GameObject menu;
	public GameObject charMenu;
	public RawImage background;
	public Texture normalBackground;
	public Texture creditBackground;

	public Text[] menuTekst;
	public int tab;
	public Text[] playerOneChar;
	public Text[] playerTwoChar;

	float time;
	float time_;
	float time__;
	bool inMenu = false;

	bool playerone;
	bool playertwo;

	int tapone;
	int taptwo;

	void Start () {
		time = Time.time + 0.2f;
		time_ = Time.time + 0.2f;
		time__ = Time.time + 0.2f;
	}

	void Update () {
		if (inMenu == false) {
			if (Time.time >= time) {

				if (Input.GetAxis ("UpAxes") == 1) {
					tab--;
					time = Time.time + 0.2f;
				} else if (Input.GetAxis ("UpAxes") == -1) {
					tab++;
					time = Time.time + 0.2f;
				}
				if (tab <= -1) {
					tab = 2;
				} else if (tab >= 3) {
					tab = 0;
				}
			}

			if (tab == 0) {
				menuTekst [0].color = Color.red;
				menuTekst [1].color = Color.gray;
				menuTekst [2].color = Color.gray;
			} else if (tab == 1) {
				menuTekst [0].color = Color.gray;
				menuTekst [1].color = Color.red;
				menuTekst [2].color = Color.gray;
			} else if (tab == 2) {
				menuTekst [0].color = Color.gray;
				menuTekst [1].color = Color.gray;
				menuTekst [2].color = Color.red;
			}

			if (Input.GetKeyDown (KeyCode.Joystick1Button0)) {
				if (tab == 0) {
					menu.SetActive (false);
					charMenu.SetActive (true);
					inMenu = true;
				} else if (tab == 1) {
					menu.SetActive (false);
					background.texture = creditBackground;
				} else if (tab == 2) {
					Application.Quit ();
				}
			}
			if (Input.GetKeyDown(KeyCode.Joystick1Button1)) {
				menu.SetActive (true);
				charMenu.SetActive (false);
				background.texture = normalBackground;
			}
		} else {
			if (Time.time >= time_) {

				if (Input.GetAxis ("UpAxes") == 1) {
					tapone--;
					time_ = Time.time + 0.2f;
				} else if (Input.GetAxis ("UpAxes") == -1) {
					tapone++;
					time_ = Time.time + 0.2f;
				}
				if (tapone <= -1) {
					tapone = 2;
				} else if (tapone >= 3) {
					tapone = 0;
				}
			}
			if (Time.time >= time__) {

				if (Input.GetAxis ("UpAxes_") == 1 || Input.GetKeyDown(KeyCode.K)) {
					taptwo--;
					time__ = Time.time + 0.2f;
				} else if (Input.GetAxis ("UpAxes_") == -1 || Input.GetKeyDown(KeyCode.L)) {
					taptwo++;
					time__ = Time.time + 0.2f;
				}
				if (taptwo <= -1) {
					taptwo = 2;
				} else if (taptwo >= 3) {
					taptwo = 0;
				}
			}
			if (tapone == 0) {
				playerOneChar [0].color = Color.red;
				playerOneChar [1].color = Color.gray;
				playerOneChar [2].color = Color.gray;
			} else if (tapone == 1) {
				playerOneChar [0].color = Color.gray;
				playerOneChar [1].color = Color.red;
				playerOneChar [2].color = Color.gray;
			} else if (tapone == 2) {
				playerOneChar [0].color = Color.gray;
				playerOneChar [1].color = Color.gray;
				playerOneChar [2].color = Color.red;
			}
			if (taptwo == 0) {
				playerTwoChar [0].color = Color.red;
				playerTwoChar [1].color = Color.gray;
				playerTwoChar [2].color = Color.gray;
			} else if (taptwo == 1) {
				playerTwoChar [0].color = Color.gray;
				playerTwoChar [1].color = Color.red;
				playerTwoChar [2].color = Color.gray;
			} else if (taptwo == 2) {
				playerTwoChar [0].color = Color.gray;
				playerTwoChar [1].color = Color.gray;
				playerTwoChar [2].color = Color.red;
			}

			if (Input.GetKeyDown (KeyCode.Joystick1Button0)) {
				playerone = true;
				print ("playerone");
			}
			if (Input.GetKeyDown (KeyCode.KeypadEnter)) {
				playertwo = true;
				print ("playertwo");
			}

			if (playerone == true && playertwo == true && tapone != taptwo) {
				print ("true");
				PlayerPrefs.SetInt ("P1Char", tapone);
				PlayerPrefs.SetInt ("P1CharLogo", tapone);
				PlayerPrefs.SetInt ("P2Char", taptwo);
				PlayerPrefs.SetInt ("P2CharLogo", taptwo);
				Application.LoadLevel (1);
			} else if(playerone && playertwo && tapone == taptwo){
				print ("error");
			}
			print (tapone + "|" + taptwo);

		}


	}
}
