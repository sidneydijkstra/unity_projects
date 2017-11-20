using UnityEngine;
using System.Collections;

public class EventTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider col){

		GameObject obj = col.transform.gameObject;

		if(obj.name == "GAME_DOGE"){
			Destroy (obj);
		}
	}

}
