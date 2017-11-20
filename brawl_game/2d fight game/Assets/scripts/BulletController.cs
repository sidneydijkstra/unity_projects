using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	GameObject[] player;
	public GameObject target;

	float time;

	void Start(){
		time = Time.time + 5;
	}

	void Update(){

		if (Time.time >= time) {
			Destroy(gameObject);
		}

		player = GameObject.FindGameObjectsWithTag ("Player");
		if (RayGet(player[0]) <= 0.1 && player[0] == target) {
			print ("enter");
			player [0].GetComponent<PlayerHealth> ().PlayerDamage (1);
			Destroy (gameObject);
		}else if (RayGet(player[1]) <= 0.1 && player[1] == target) {
			print ("enter");
			player [1].GetComponent<PlayerHealth> ().PlayerDamage (1);
			Destroy (gameObject);
		}

	}

	public float RayGet (GameObject obj){
		RaycastHit hit;
		Physics.Linecast (transform.position, obj.transform.position, out hit);
		return hit.distance;
	}

}
