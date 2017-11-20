
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWithTimer : MonoBehaviour {

    // variables
    public float destroyTimer;

    private float timer;

	void Start () {
        timer = Time.time + destroyTimer;
	}
	
	void Update () {
        if (Time.time >= timer) {
            Destroy(this.gameObject);
        }
	}
}
