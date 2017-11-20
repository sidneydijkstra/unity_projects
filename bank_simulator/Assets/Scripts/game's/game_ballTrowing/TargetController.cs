using UnityEngine;
using System.Collections;

public class TargetController : MonoBehaviour {

	public GameObject targetGood;
	public GameObject targetBad;
	public Transform startPos;

	public float spawnTimerSec;

	private float time;

	void Start () {
		time = Time.time;
	}

	void Update () 
	{
		if (Time.time >= time){
			time = Time.time + spawnTimerSec;
			Spawn ();
			Debug.Log ("time");
		}
	}

	void Spawn(){

		GameObject obj;

		if (Random.Range (0, 1) <= 30) {
			obj = targetBad;
		} else {
			obj = targetGood;
		}


		Instantiate (obj, startPos.position, Quaternion.Euler(new Vector3(0,90,90)));
	}
}
