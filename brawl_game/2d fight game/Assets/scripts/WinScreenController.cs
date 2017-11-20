using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScreenController : MonoBehaviour {

	public GameObject text;

	void Start () {
		text.GetComponent<TextMesh>().text = "Player " + (PlayerPrefs.GetInt("WINNER")+1) + " won";
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.LoadLevel (0);
		}
	}
}
