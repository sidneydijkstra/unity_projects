using UnityEngine;
using System.Collections;

public class TargetConfig : MonoBehaviour {

	public Transform targetEnd;
	public float speed;

	private bool hit = false;

	void Start () {
	
	}

	void Update () {
		if (hit == false) {
			transform.position = Vector3.MoveTowards(transform.position, targetEnd.position, speed *Time.deltaTime);
			//transform.Rotate (Vector3.right * 9999,Space.World);
			//Debug.Log ("move");
		}
		if (transform.position == targetEnd.position) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter(){
		Destroy (gameObject);
	}

}
