using UnityEngine;
using System.Collections;
//using UnityEngine.Audio;

public class SightController : MonoBehaviour {

	AudioSource audio;

	public Transform tv;
	public Transform lamp;


	Transform cam;
	bool onoff = true;

	void Start () {
		tv.gameObject.SetActive (false);

		cam = Camera.main.transform;
		audio = GameObject.FindGameObjectWithTag("RADIO").GetComponent<AudioSource> ();
	}

	void Update () {
		if (RayDetect("TVON")) {
			if (Input.GetMouseButtonDown (0)) {
				tv.gameObject.SetActive (true);
			}
		}
		else if (RayDetect("TVOFF")) {
			if (Input.GetMouseButtonDown (0)) {
				tv.gameObject.SetActive (false);
			}
		}
		else if (RayDetect("RADIOON")) {
			if (Input.GetMouseButtonDown (0)) {
				audio.Pause ();
				audio.Play();
			}
		}
		else if (RayDetect("RADIOOFF")) {
			if (Input.GetMouseButtonDown (0)) {
				audio.Pause ();
			}
		}
		else if (RayDetect("LAMPONOFF")) {
			if (Input.GetMouseButtonDown (0)) {

				if (onoff == true) {
					lamp.gameObject.SetActive(false);
					onoff = false;
				}else if (onoff == false) {
					lamp.gameObject.SetActive(true);
					onoff = true;
				}

			}
		}
	}

	public bool RayDetect(string tag){
	
		RaycastHit hit;
		Physics.Raycast (transform.position, cam.forward, out hit);
		if (hit.collider.tag == tag) {
			Debug.DrawLine(transform.position,hit.transform.position,Color.red);
			return true;
		} else {
			Debug.DrawLine(transform.position,hit.transform.position,Color.red);
			return false;
		}
	}

	public Transform RayGet (string tag){
		RaycastHit hit;
		Physics.Raycast (transform.position, cam.forward, out hit);
		if (hit.collider.tag == tag) {
			Debug.DrawLine(transform.position,hit.transform.position,Color.red);
			return hit.transform;
		} else {
			Debug.DrawLine(transform.position,hit.transform.position,Color.red);
			return null;
		}
	}

}