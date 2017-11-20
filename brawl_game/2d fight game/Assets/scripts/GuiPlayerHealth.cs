using UnityEngine;
using System.Collections;

public class GuiPlayerHealth : MonoBehaviour {

	public GameObject target;

	public int lrDisplay;

	public GameObject bar;
	public GameObject[] healthbars;

	public int lenght;

	public int minhp = 1;
	PlayerHealth ph;

	// Use this for initialization

	void Start () {
	}

	public void SpawnHealth(GameObject img){
		for (int i = 1; i < lenght; i += lrDisplay) {
			Vector3 pos = new Vector3 (transform.position.x + (i * 0.3F), transform.position.y, transform.position.z);
			GameObject img_ = Instantiate (img, pos, Quaternion.identity) as GameObject;
			healthbars [i - 1] = img_;
		}
	}

	public void Dmg(){
		print (healthbars.Length - minhp);
		Destroy (healthbars [healthbars.Length - minhp]);
		minhp++;
	}

}
