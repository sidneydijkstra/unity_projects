using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour {

	SightController sc;

	public Text globalText;
	public Text timerText;

	float timeRest;
	float timer = 0;

	void Start () {
		sc = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SightController> ();

		globalText.text = "";
		timerText.text = "";

		timeRest = Time.time + 1f;
	}
	

	void Update () {
		
		globalText.text = "";

		if (sc.RayDetect ("TVON")) {
			globalText.text = "Set TV ON.";
		}else if (sc.RayDetect("TVOFF")) {
			globalText.text = "Set TV OFF.";
		}else if (sc.RayDetect("RADIOON")) {
			globalText.text = "Set RADIO ON.";
		}else if (sc.RayDetect("RADIOOFF")) {
			globalText.text = "Set RADIO OFF.";
		}else if (sc.RayDetect("LAMPONOFF")) {
			globalText.text = "Turn LAMP ON/LAMP OFF";
		}

		if (Time.time >= timeRest){
			timer++;
			timeRest = Time.time + 1f;
		}

		timerText.text = timer + " SEC";

	}



}
